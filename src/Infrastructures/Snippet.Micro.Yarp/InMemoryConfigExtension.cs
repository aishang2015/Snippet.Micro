using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Configuration;

namespace Snippet.Micro.Yarp
{
    public static class InMemoryConfigExtension
    {
        public static IReverseProxyBuilder LoadFromMemory(this IReverseProxyBuilder builder,
            IReadOnlyList<RouteConfig> routes, 
            IReadOnlyList<ClusterConfig> clusters)
        {
            builder.Services.AddSingleton<IProxyConfigProvider>(new InMemoryConfigProvider(routes, clusters));
            return builder;
        }
    }
}
