using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Snippet.Micro.Consul
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services,
            IConfigurationSection configurationSection)
        {
            services.Configure<ConsulOption>(configurationSection);
            var consulOption = configurationSection.Get<ConsulOption>();

            services.AddSingleton<IConsulClient>(new ConsulClient(x => x.Address =
                new Uri($"http://{consulOption.ConsulIp}:{consulOption.ConsulPort}")));
            return services;
        }

        public static IServiceCollection AddConsulRegisterService(this IServiceCollection services)
        {
            services.AddHostedService<ConsulRegisterService>();
            return services;
        }

        public static IServiceCollection AddConsulDiscoveryService(this IServiceCollection services)
        {
            services.AddSingleton<IServiceDiscoveryService, ServiceDiscoveryService>();
            return services;
        }
    }
}
