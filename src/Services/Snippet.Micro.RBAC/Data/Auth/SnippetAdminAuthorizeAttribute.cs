using Microsoft.AspNetCore.Mvc;

namespace Snippet.Micro.RBAC.Data.Auth
{
    public class SnippetAdminAuthorizeAttribute : TypeFilterAttribute
    {
        public SnippetAdminAuthorizeAttribute() : base(typeof(SnippetAdminAuthorizeFilter))
        {
        }
    }
}