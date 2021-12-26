using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Winton.Extensions.Configuration.Consul;

namespace Snippet.Micro.Consul
{
    public static class ConsulConfigurationExtension
    {
        /// <summary>
        /// 将consul作为配置中心，替换appsetting
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder AddConsulConfiguraion(this WebApplicationBuilder builder)
        {
            builder.Host.ConfigureAppConfiguration((context, configuration) =>
            {
                // 从appsetting获取consul地址
                var localconfig = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
                var consul_server = localconfig["consul_server"];
                var consul_path = localconfig["consul_path"];

                // 使用consul配置
                configuration.AddConsul(consul_path, op =>
                {
                    op.ConsulConfigurationOptions = cco =>
                    {
                        cco.Address = new Uri(consul_server);
                    };
                    op.ReloadOnChange = true;
                });
            });

            return builder;
        }
    }
}
