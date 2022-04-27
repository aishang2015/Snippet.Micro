using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Snippet.Micro.Common.Midlewares;
using Snippet.Micro.Consul;
using Snippet.Micro.WebGateway;
using Snippet.Micro.Yarp;
using System.Security.Claims;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddConsulDiscoveryService(builder.Configuration.GetSection("Consul"));

builder.Services.AddReverseProxy()
    .LoadFromMemory(new List<RouteConfig>(), new List<ClusterConfig>());
builder.Services.AddHostedService<ReloadProxyConfigService>();

builder.Services.AddCors(options =>
{
    var origins = builder.Configuration.GetSection("Cors:Origins")?.Get<string[]>();
    var methods = builder.Configuration.GetSection("Cors:Mehtods")?.Get<string[]>();
    var headers = builder.Configuration.GetSection("Cors:Headers")?.Get<string[]>();

    origins = origins == null || origins.Length == 0 ? new string[] { "*" } : origins;
    methods = methods == null || methods.Length == 0 ? new string[] { "GET", "POST", "OPTIONS", "PUT", "PATCH", "DELETE" } : methods;
    headers = headers == null || headers.Length == 0 ? new string[] { "*" } : headers;

    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins(origins).SetIsOriginAllowedToAllowWildcardSubdomains()
            .WithMethods(methods)
            .WithHeaders(headers)
            .DisallowCredentials());
});

builder.Services.AddAuthorization(options =>
    options.AddPolicy("customPolicy", policy =>
        policy.RequireAuthenticatedUser()));

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.RequireHttpsMetadata = false;
        options.Authority = builder.Configuration["Authority"];
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false,
            ValidAudience = "rbacapi",
            ValidateIssuer = false,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("CorsPolicy");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthToHeader();

app.UseEndpoints(endpoints =>
{
    endpoints.MapReverseProxy();
});

app.Run();
