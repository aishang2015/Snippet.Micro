using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Snippet.Micro.Redis
{
    public static class StackExchangeExtension
    {
        public static IServiceCollection AddRedis(this IServiceCollection services,
            IConfigurationSection configurationSection)
        {
            services.Configure<RedisOption>(configurationSection);
            var redisOption = configurationSection.Get<RedisOption>();
            services.AddSingleton(new RedisClient(redisOption));
            return services;
        }
    }
}
