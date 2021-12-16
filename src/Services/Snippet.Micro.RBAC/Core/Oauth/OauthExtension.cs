using Refit;
using Snippet.Micro.RBAC.Core.Oauth.Apis;

namespace Snippet.Micro.RBAC.Core.Oauth
{
    public static class OauthExtension
    {
        public static IServiceCollection AddOauth(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OauthOption>(configuration.GetSection("OauthOption"));
            services.AddScoped<OauthHelper>();

            services.AddRefitClient<IGithubAuthApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://github.com");
                });

            services.AddRefitClient<IGithubApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://api.github.com");
                });

            services.AddRefitClient<IBaiduApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://openapi.baidu.com");
                });

            return services;
        }
    }
}