using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Snippet.Micro.MassTransit
{
    public static class MassTransitExtension
    {
        public static IServiceCollection AddRabbitMassTransit(this IServiceCollection services,
            IConfigurationSection configurationSection, List<Type> consumers)
        {
            services.Configure<MassTransitOption>(configurationSection);
            var masstransitOption = configurationSection.Get<MassTransitOption>();

            services.AddMassTransit(config =>
            {
                foreach (var type in consumers)
                {
                    config.AddConsumer(type);
                }

                config.UsingRabbitMq((context, configure) =>
                {
                    configure.Host(masstransitOption.Host, masstransitOption.Port, "/", rabbitHostConfig =>
                    {
                        rabbitHostConfig.Username(masstransitOption.UserName);
                        rabbitHostConfig.Password(masstransitOption.Password);
                    });

                    foreach (var type in consumers)
                    {
                        configure.ReceiveEndpoint(type.FullName, e =>
                        {
                            e.ConfigureConsumer(context, type);
                        });
                    }
                });
            });
            services.AddMassTransitHostedService();

            return services;
        }
    }
}
