using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Snippet.Micro.Rbac.App.Data.Entity.RBAC
{
    public class SnippetAdminRole : IdentityRole<int>
    {
        [Comment("编码")]
        public string Code { get; set; }

        [Comment("备注")]
        public string Remark { get; set; }

        [Comment("是否激活")]
        public bool IsActive { get; set; }
    }
}