using Microsoft.OpenApi.Models;

namespace Snippet.Micro.Rbac.App.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Snippet.Micro.Rbac", Version = "v1" });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Snippet.Micro.Rbac.App.xml");
                c.IncludeXmlComments(xmlPath, true);
            });
            return services;
        }
    }
}