using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Snippet.Micro.Common.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetUserName(this HttpContext httpContext)
        {
            return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        }
    }
}
