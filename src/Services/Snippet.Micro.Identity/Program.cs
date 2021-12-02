using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Snippet.Micro.Consul;
using Snippet.Micro.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddConsulConfig(builder.Configuration.GetSection("Consul"));
builder.Services.AddConsulRegisterService();

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

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
}).AddAspNetIdentity<AppUser>()
.AddConfigurationStore(options =>
{
    options.ConfigureDbContext = b => b.UseMySql(builder.Configuration.GetConnectionString("ConfigDb"), new MySqlServerVersion("8.0.21"));
}).AddOperationalStore(options =>
{
    options.ConfigureDbContext = b => b.UseMySql(builder.Configuration.GetConnectionString("OperationDb"), new MySqlServerVersion("8.0.21"));
}).AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

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

    if (appDbContext.Database.EnsureCreated())
    {
        userManager.CreateAsync(new AppUser
        {
            UserName = "admin"
        }, "Abc123...").Wait();
    }

    if (configurationDbContext.Database.EnsureCreated())
    {
        // apiresource
        var apiResource = new ApiResource("testapi", "TestService API Resource")
        {
            Scopes = new List<string> { "snippet.micro.test" }
        };
        configurationDbContext.ApiResources.Add(apiResource.ToEntity());

        // apiscope
        var apiScope = new ApiScope("snippet.micro.test", "TestService API Scope");
        configurationDbContext.ApiScopes.Add(apiScope.ToEntity());

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
            ClientName = "react client of wiki application",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            RequireClientSecret = false,
            AccessTokenLifetime = 3600 * 12,

            AllowedScopes = new List<string>
                    {
                        "snippet.micro.test",
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
