using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Snippet.Micro.Common.Constants;
using Snippet.Micro.Common.Models;
using System.Text.Json;

namespace Snippet.Micro.RBAC.Core.Middleware
{
    public static class ExceptionMiddleware
    {
        public static WebApplication UseCustomExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(configure =>
            {
                configure.Run(async httpContext =>
                {
                    var exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();
                    var ex = exceptionHandlerPathFeature?.Error;
                    if (ex != null)
                    {
                        httpContext.Response.StatusCode = StatusCodes.Status200OK;
                        httpContext.Response.ContentType = "application/json; charset=utf-8";
                        await JsonSerializer.SerializeAsync(httpContext.Response.Body,
                            CommonResultExtension.Fail(CommonConstants.SYSTEM_ERR_0001), 
                            new JsonSerializerOptions
                        {
                            // 首字母小写
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        });
                    }
                });
            });
            return app;
        }
    }
}