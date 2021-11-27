using Snippet.Micro.Consul;
using Snippet.Micro.WebGateway;
using Snippet.Micro.Yarp;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddConsulConfig(builder.Configuration.GetSection("Consul"));
builder.Services.AddConsulDiscoveryService();

builder.Services.AddReverseProxy()
    .LoadFromMemory(new List<RouteConfig>(), new List<ClusterConfig>());
builder.Services.AddHostedService<ReloadProxyConfigService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapReverseProxy();
});

app.Run();
