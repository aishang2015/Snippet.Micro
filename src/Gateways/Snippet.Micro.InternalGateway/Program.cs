using Snippet.Micro.Consul;
using Snippet.Micro.InternalGateway;
using Snippet.Micro.Yarp;
using Yarp.ReverseProxy.Configuration;

// �ڲ����� ����Ȩ����֤
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConsulDiscoveryService(builder.Configuration.GetSection("Consul"));

builder.Services.AddReverseProxy().LoadFromMemory(new List<RouteConfig>(), new List<ClusterConfig>());
builder.Services.AddHostedService<ReloadProxyConfigService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapReverseProxy();
});

app.Run();
