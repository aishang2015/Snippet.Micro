using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Snippet.Micro.Consul;
using Snippet.Micro.Identity.Data;
using Snippet.Micro.Identity.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 使用consul配置
builder.AddConsulConfiguraion();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddConsulRegisterService(builder.Configuration.GetSection("Consul"));

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("AppDb"), new MySqlServerVersion("8.0.21"));
}).AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;

}).AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

var security = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("yE52PzuI267UoIwIYqdy2kdlKgcK"));
var credential = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
}).AddAspNetIdentity<AppUser>()
//.AddCustomTokenRequestValidator<CustomTokenRequestValidator>()
.AddConfigurationStore(options =>
{
    options.ConfigureDbContext = b => b.UseMySql(builder.Configuration.GetConnectionString("ConfigDb"), new MySqlServerVersion("8.0.21"));
}).AddOperationalStore(options =>
{
    options.ConfigureDbContext = b => b.UseMySql(builder.Configuration.GetConnectionString("OperationDb"), new MySqlServerVersion("8.0.21"));
})
//.AddSigningCredential(security, SecurityAlgorithms.HmacSha256)
.AddDeveloperSigningCredential(true, "tempkey.jwk")
.AddProfileService<CustomProfileService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .SetIsOriginAllowed(o => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

// todo 下边这段代码会引起IDX10501错误
//app.Use(async (context, next) =>
//{
//    //设置stream存放ResponseBody
//    var responseOriginalBody = context.Response.Body;
//    using var memStream = new MemoryStream();
//    context.Response.Body = memStream;

//    await next.Invoke();

//    var result = context.Response.Body;

//    //处理执行其他中间件后的ResponseBody
//    memStream.Position = 0;
//    var responseReader = new StreamReader(memStream);
//    var responseBody = await responseReader.ReadToEndAsync();

//    var newResult = new
//    {
//        IsSuccess = true,
//        Message = string.Empty,
//        Data = JsonConvert.DeserializeObject(responseBody)
//    };
//    var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(newResult));

//    var ms2 = new MemoryStream();
//    ms2.Write(buffer, 0, buffer.Length);
//    ms2.Seek(0, SeekOrigin.Begin);

//    await ms2.CopyToAsync(responseOriginalBody);
//    context.Response.Body = responseOriginalBody;
//});

app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
    var persistedGrantDbContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    if (configurationDbContext.Database.EnsureCreated())
    {
        // apiresource
        new List<ApiResource>
        {
            new ApiResource("testapi", "TestService API Resource")
            {
                Scopes = new List<string> { "snippet.micro.test" }
            },
            new ApiResource("rbacapi", "RBACService API Resource")
            {
                Scopes = new List<string> { "snippet.micro.rbac" }
            }
        }.ForEach(apiResource => configurationDbContext.ApiResources.Add(apiResource.ToEntity()));

        // apiscope
        new List<ApiScope>
        {
            new ApiScope("snippet.micro.test", "TestService API Scope"),
            new ApiScope("snippet.micro.rbac", "RBACService API Scope"),
        }.ForEach(apiScope => configurationDbContext.ApiScopes.Add(apiScope.ToEntity()));

        // identity resources
        var resources = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        foreach (var resource in resources)
        {
            configurationDbContext.IdentityResources.Add(resource.ToEntity());
        }

        // client
        var client = new Client
        {
            ClientId = "snippet.micro.web",
            ClientName = "web client for snippet micro",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            AllowOfflineAccess = true,
            RequireClientSecret = false,
            AccessTokenLifetime = 3600 * 12,

            AllowedCorsOrigins = { "http://localhost:3000" },

            AllowedScopes = new List<string>
                    {
                        "snippet.micro.test",
                        "snippet.micro.rbac",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
        };
        configurationDbContext.Clients.Add(client.ToEntity());

        configurationDbContext.SaveChanges();
    }

    if (persistedGrantDbContext.Database.EnsureCreated())
    {

    }

}

app.Run();
