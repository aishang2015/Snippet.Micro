using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Snippet.Micro.Rbac.App.Data.Entity.Enums;

namespace Snippet.Micro.Rbac.App.Data.Entity.RBAC
{
    public class SnippetAdminUser : IdentityUser<int>
    {
        [Comment("头像")]
        public string Avatar { get; set; }

        [Comment("姓名")]
        public string RealName { get; set; }

        [Comment("性别")]
        public Gender Gender { get; set; }

        [Comment("电话")]
        public new string PhoneNumber { get; set; }

        [Comment("是否激活")]
        public bool IsActive { get; set; }
    }
}