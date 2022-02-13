using Snippet.Micro.Consul;
using Snippet.Micro.InternalGateway;
using Snippet.Micro.Yarp;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddConsulDiscoveryService(builder.Configuration.GetSection("Consul"));

builder.Services.AddReverseProxy()
    .LoadFromMemory(new List<RouteConfig>(), new List<ClusterConfig>());
builder.Services.AddHostedService<ReloadProxyConfigService>();

builder.Services.AddCors(options =>
{
    var origins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>();
    var methods = builder.Configuration.GetSection("Cors:Mehtods").Get<string[]>();
    var headers = builder.Configuration.GetSection("Cors:Headers").Get<string[]>();

    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins(origins).SetIsOriginAllowedToAllowWildcardSubdomains()
            .WithMethods(methods)
            .WithHeaders(headers)
            .DisallowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("CorsPolicy");

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapReverseProxy();
});

app.Run();
