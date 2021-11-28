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
            var serviceList = new List<string>
            {
                "Snippet.Micro.TestService"
            };

            var clusters = new List<ClusterConfig>();
            var routes = new List<RouteConfig>();

            foreach (var serviceName in serviceList)
            {

                var destinations = new Dictionary<string, DestinationConfig>();
                var services = await _discoveryService.GetServicesAsync(serviceName);
                for (var i = 0; i < services.Count; i++)
                {
                    var service = services[i];
                    destinations[$"{serviceName}/destination{i}"] =
                        new DestinationConfig
                        {
                            Address = $"http://{service.Address}:{service.Port}/",
                        };

                }
                clusters.Add(new ClusterConfig()
                {
                    ClusterId = serviceName,
                    Destinations = destinations,
                    LoadBalancingPolicy = "PowerOfTwoChoices"
                });

                routes.Add(new RouteConfig()
                {
                    RouteId = serviceName,
                    ClusterId = serviceName,
                    Match = new RouteMatch
                    {
                        Path = "/user-api/{**catch-all}",
                    },
                    Transforms = new List<Dictionary<string, string>>
                    {
                        new Dictionary<string, string>()
                        {
                            { "PathPattern","/api/{**catch-all}" }
                        }
                    }
                });
            }

            (_proxyConfigProvider as InMemoryConfigProvider).Update(routes, clusters);

        }
    }
}
