using Microsoft.AspNetCore.Builder;
using System.Linq;
using System.Security.Claims;

namespace Snippet.Micro.Common.Midlewares
{
    public static class MiddlewareExtension
    {
        private const string AuthHeaderKey = "identity_username";

        private static List<string> FilteredPath = new List<string>
        {
            "/api/health",
        };

        /// <summary>
        /// 将认证信息放入header，为各个微服务使用
        /// </summary>
        /// <returns></returns>
        public static WebApplication UseAuthToHeader(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                var userName = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (!string.IsNullOrEmpty(userName))
                {
                    context.Request.Headers.Add(AuthHeaderKey, userName);
                }
                await next.Invoke();
            });
            return app;
        }

        /// <summary>
        /// 从header中拿取认证信息
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplication UseHeaderToAuth(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                if (!FilteredPath.Contains(context.Request.Path))
                {
                    var isSuccess = context.Request.Headers.TryGetValue(AuthHeaderKey, out var userName);
                    if (isSuccess)
                    {
                        context.User.Identities.First()?.AddClaim(new Claim(ClaimTypes.Name, userName));
                    }
                }
                await next.Invoke();
            });
            return app;
        }
    }
}
