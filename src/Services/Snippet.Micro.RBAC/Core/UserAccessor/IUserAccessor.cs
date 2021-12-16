using System.Security.Claims;

namespace Snippet.Micro.RBAC.Core.UserAccessor
{
    public interface IUserAccessor
    {
        public ClaimsPrincipal UserInfo { get; }
        public string UserName { get; }
    }
}