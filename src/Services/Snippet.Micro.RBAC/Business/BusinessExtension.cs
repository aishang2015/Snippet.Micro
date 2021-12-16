using Snippet.Micro.RBAC.Business.Hubs;

namespace Snippet.Micro.RBAC.Business
{
    public static class BusinessExtension
    {
        public static IEndpointRouteBuilder MapHubs(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<BroadcastHub>("/broadcast");
            return endpoints;
        }
    }
}