using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Snippet.Micro.Common.Extensions
{
    public static class CorsExtension
    {

        public static WebApplicationBuilder AddCustomCors(this WebApplicationBuilder builder)
        {
            var origins = builder.Configuration.GetSection("Cors:Origins")?.Get<string[]>();
            var methods = builder.Configuration.GetSection("Cors:Mehtods")?.Get<string[]>();
            var headers = builder.Configuration.GetSection("Cors:Headers")?.Get<string[]>();

            origins = origins == null || origins.Length == 0 ? new string[] { "*" } : origins;
            methods = methods == null || methods.Length == 0 ? new string[] { "GET", "POST", "OPTIONS", "PUT", "PATCH", "DELETE" } : methods;
            headers = headers == null || headers.Length == 0 ? new string[] { "*" } : headers;

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CustomCorsPolicy",
                    builder => builder
                        .WithOrigins(origins).SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithMethods(methods)
                        .WithHeaders(headers)
                        .AllowCredentials());
            });

            return builder;
        }

        public static WebApplication UseCustomCors(this WebApplication app)
        {
            app.UseCors("CustomCorsPolicy");
            return app;
        }
    }
}
