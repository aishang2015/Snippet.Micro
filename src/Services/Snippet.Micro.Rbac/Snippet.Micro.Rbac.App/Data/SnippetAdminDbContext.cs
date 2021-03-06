using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Snippet.Micro.Rbac.App.Data.Entity.RBAC;

namespace Snippet.Micro.Rbac.App.Data
{
    public class SnippetAdminDbContext : IdentityDbContext<SnippetAdminUser, SnippetAdminRole, int>
    {
        public SnippetAdminDbContext(DbContextOptions<SnippetAdminDbContext> options) : base(options)
        {
            // 更改默认不跟踪所有实体
            // ef core 5推荐 NoTracking在多次相同查询时会返回不同的对象，NoTrackingWithIdentityResolution则会返回
            // 相同的对象
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;

            // 关闭自动检测后，实体的变化需要手动调用Update，Delete等方法去进行检测。
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Element> Elements { get; set; }

        public DbSet<ElementTree> ElementTrees { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<OrganizationTree> OrganizationTrees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<SnippetAdminUser>().ToTable("T_RBAC_User");
            builder.Entity<SnippetAdminRole>().ToTable("T_RBAC_Role");
            builder.Entity<IdentityUserRole<int>>().ToTable("T_RBAC_UserRole");
            builder.Entity<IdentityUserClaim<int>>().ToTable("T_RBAC_UserClaim");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("T_RBAC_RoleClaim");
            builder.Entity<IdentityUserLogin<int>>().ToTable("T_RBAC_UserLogin");
            builder.Entity<IdentityUserToken<int>>().ToTable("T_RBAC_UserToken");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // 打印sql参数
            optionsBuilder.EnableSensitiveDataLogging();
        }

    }
}