using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Snippet.Micro.Common.Extensions
{
    public static class AuthenticationExtension
    {
        public static WebApplicationBuilder AddAuthCenterJwtAutnentication(this WebApplicationBuilder builder,
            TokenValidationParameters validationParameters)
        {
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = builder.Configuration["Authority"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = validationParameters;
                });
            return builder;
        }

        public static WebApplicationBuilder AddLocalJwtAutnentication(this WebApplicationBuilder builder,
            TokenValidationParameters validationParameters)
        {
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = builder.Configuration["Authority"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = validationParameters;
                });
            return builder;
        }
    }
}
