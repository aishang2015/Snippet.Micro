using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Snippet.Micro.Identity.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().ToTable("T_RBAC_User");
            builder.Entity<AppRole>().ToTable("T_RBAC_Role");
            builder.Entity<IdentityUserRole<int>>().ToTable("T_RBAC_UserRole");
            builder.Entity<IdentityUserClaim<int>>().ToTable("T_RBAC_UserClaim");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("T_RBAC_RoleClaim");
            builder.Entity<IdentityUserLogin<int>>().ToTable("T_RBAC_UserLogin");
            builder.Entity<IdentityUserToken<int>>().ToTable("T_RBAC_UserToken");
        }
    }
}
