using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Snippet.Micro.Rbac.Api
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddRbacApi(this IServiceCollection services)
        {
            // 通过内部网关进行服务调用
            services
                .AddRefitClient<IApiInfo>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:20001/rbac"));
            return services;
        }

    }
}
