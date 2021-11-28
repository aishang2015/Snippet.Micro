using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snippet.Micro.Identity.Data;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    options.UserInteraction.LoginUrl = "/Account/Login";
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
        var clients = new Client
        {
            ClientId = "snippet.micro.web",
            ClientName = "react client of wiki application",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            RequireClientSecret = false,
            AccessTokenLifetime = 3600 * 12,

            AllowedScopes = new List<string>
                    {
                        "snippet.micro.web",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
        };
        configurationDbContext.Clients.Add(clients.ToEntity());

        var resources = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        foreach (var resource in resources)
        {
            configurationDbContext.IdentityResources.Add(resource.ToEntity());
        }

        configurationDbContext.SaveChanges();
    }

    if (persistedGrantDbContext.Database.EnsureCreated())
    {

    }

}

app.Run();
