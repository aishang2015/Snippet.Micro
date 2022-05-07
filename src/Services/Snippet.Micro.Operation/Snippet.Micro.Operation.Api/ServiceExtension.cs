using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Snippet.Micro.Operation.Api
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddOperationApi(this IServiceCollection services)
        {
            // 通过内部网关进行服务调用
            services
                .AddRefitClient<IApiInfo>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:20001/operation"));
            return services;
        }

    }
}
