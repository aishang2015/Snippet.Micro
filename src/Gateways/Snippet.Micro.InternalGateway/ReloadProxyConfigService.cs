using Snippet.Micro.Consul;
using Snippet.Micro.Yarp;
using Yarp.ReverseProxy.Configuration;

namespace Snippet.Micro.InternalGateway
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
                await Task.Delay(TimeSpan.FromSeconds(8));

                // service name, cluster id,route id ,route path
                var serviceList = new List<(string, string, string, string)>
                {
                    ("Snippet.Micro.TestService","test-service-cluster","test-service-route","test"),
                    ("Snippet.Micro.IdentityService","identity-service-cluster","identity-service-route","identity")
                };

                var clusters = new List<ClusterConfig>();
                var routes = new List<RouteConfig>();

                foreach (var serviceInfo in serviceList)
                {
                    var destinations = new Dictionary<string, DestinationConfig>();
                    var services = await _discoveryService.GetServicesAsync(serviceInfo.Item1);
                    for (var i = 0; i < services.Count; i++)
                    {
                        var service = services[i];
                        destinations[$"{service.Service}/destination{i}"] =
                            new DestinationConfig
                            {
                                Address = $"http://{service.Address}:{service.Port}/",
                            };
                    }
                    clusters.Add(new ClusterConfig()
                    {
                        ClusterId = serviceInfo.Item2,
                        Destinations = destinations,
                        LoadBalancingPolicy = "PowerOfTwoChoices"
                    });

                    routes.Add(new RouteConfig()
                    {
                        RouteId = serviceInfo.Item3,
                        ClusterId = serviceInfo.Item2,
                        Match = new RouteMatch
                        {
                            Path = $"/{serviceInfo.Item4}/{{**catch-all}}",
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

            (_proxyConfigProvider as InMemoryConfigProvider).Update(routes, clusters);

            }
        }
    }
}
