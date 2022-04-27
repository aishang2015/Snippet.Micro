using Snippet.Micro.Consul;
using Snippet.Micro.Yarp;
using Yarp.ReverseProxy.Configuration;

namespace Snippet.Micro.WebGateway
{
    public class ReloadProxyConfigService : BackgroundService
    {
        private readonly IServiceDiscoveryService _discoveryService;

        private readonly IProxyConfigProvider _proxyConfigProvider;

        public ReloadProxyConfigService(IServiceDiscoveryService discoveryService, IProxyConfigProvider proxyConfigProvider)
        {
            _discoveryService = discoveryService;
            _proxyConfigProvider = proxyConfigProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(1);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(20));

                var clusters = new List<ClusterConfig>();
                var routes = new List<RouteConfig>();

                var services = await _discoveryService.GetServicesAsync();
                foreach (var group in services.GroupBy(s => s.Service))
                {
                    var serviceName = group.Key;
                    var serviceKey = group.Key.Split('.').Last().ToLower();
                    var clusterName = $"{serviceKey}-cluster";
                    var routeName = $"{serviceKey}-route";
                    var routePath = serviceKey.Replace("service", string.Empty);
                    var destinations = new Dictionary<string, DestinationConfig>();

                    var groupServices = group.ToList();
                    for (var i = 0; i < group.Count(); i++)
                    {
                        var detail = groupServices[i];
                        destinations[$"{serviceName}/destination{i}"] =
                            new DestinationConfig
                            {
                                Address = $"http://{detail.Address}:{detail.Port}/",
                            };
                    }
                    clusters.Add(new ClusterConfig()
                    {
                        ClusterId = clusterName,
                        Destinations = destinations,
                        LoadBalancingPolicy = "PowerOfTwoChoices"
                    });
                    routes.Add(new RouteConfig()
                    {
                        RouteId = routeName,
                        ClusterId = clusterName,
                        AuthorizationPolicy = routePath != "identity" ? "customPolicy" : string.Empty,
                        Match = new RouteMatch
                        {
                            Path = $"/{routePath}/{{**catch-all}}",
                        },
                        Transforms = new List<Dictionary<string, string>>
                            {
                                new Dictionary<string, string>()
                                {
                                    { "PathPattern","/{**catch-all}" }
                                }
                            }
                    });
                }

                ((InMemoryConfigProvider)_proxyConfigProvider).Update(routes, clusters);
            }
        }
    }
}
