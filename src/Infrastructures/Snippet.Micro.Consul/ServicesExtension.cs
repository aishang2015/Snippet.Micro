using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Snippet.Micro.Consul
{
    public static class ServicesExtension
    {

        #region 注册服务

        /// <summary>
        /// 将服务注册到consul
        /// </summary>
        public static WebApplicationBuilder AddConsulRegisterService(this WebApplicationBuilder builder,
            ConsulOption consulOption)
        {
            builder.Services.AddConsulConfig(consulOption);
            builder.Services.AddHostedService<ConsulRegisterService>();
            return builder;
        }

        /// <summary>
        /// 将服务注册到consul
        /// </summary>
        public static WebApplicationBuilder AddConsulRegisterService(this WebApplicationBuilder builder)
        {
            var configurationSection = builder.Configuration.GetSection("Consul");
            builder.Services.AddConsulConfig(configurationSection);
            builder.Services.AddHostedService<ConsulRegisterService>();
            return builder;
        }

        #endregion

        #region 发现服务

        /// <summary>
        /// 注册发现服务，可以使用发现服务读取consul的所有注册服务
        /// </summary>
        public static IServiceCollection AddConsulDiscoveryService(this IServiceCollection services,
            ConsulOption consulOption)
        {
            services.AddConsulConfig(consulOption);
            services.AddSingleton<IServiceDiscoveryService, ServiceDiscoveryService>();
            return services;
        }

        /// <summary>
        /// 注册发现服务，可以使用发现服务读取consul的所有注册服务
        /// </summary>
        public static IServiceCollection AddConsulDiscoveryService(this IServiceCollection services,
            IConfigurationSection configurationSection)
        {
            services.AddConsulConfig(configurationSection);
            services.AddSingleton<IServiceDiscoveryService, ServiceDiscoveryService>();
            return services;
        }

        #endregion

        #region 注册配置

        /// <summary>
        /// 添加consul配置，注册服务和发现服务都依赖此配置
        /// </summary>
        private static IServiceCollection AddConsulConfig(this IServiceCollection services,
            ConsulOption consulOption)
        {
            services.Configure<ConsulOption>(o => o = consulOption);

            services.AddSingleton<IConsulClient>(new ConsulClient(x => x.Address =
                new Uri($"http://{consulOption.ConsulIp}:{consulOption.ConsulPort}")));
            return services;
        }

        /// <summary>
        /// 添加consul配置，注册服务和发现服务都依赖此配置
        /// </summary>
        private static IServiceCollection AddConsulConfig(this IServiceCollection services,
            IConfigurationSection configurationSection)
        {
            services.Configure<ConsulOption>(configurationSection);
            var consulOption = configurationSection.Get<ConsulOption>();

            services.AddSingleton<IConsulClient>(new ConsulClient(x => x.Address =
                new Uri($"http://{consulOption.ConsulIp}:{consulOption.ConsulPort}")));
            return services;
        }

        #endregion
    }
}
