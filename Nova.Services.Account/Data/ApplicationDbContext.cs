using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nova.Services.Account.Models;

namespace Nova.Services.Account.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("tb_users");
            builder.Entity<IdentityRole>().ToTable("tb_roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("tb_user_roles").HasKey(r => new { r.UserId, r.RoleId });// 使用 IdentityUserRole<int> 时会生成重复的表
            builder.Entity<IdentityUserClaim<string>>().ToTable("tb_user_claims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("tb_user_logins").HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
            builder.Entity<IdentityRoleClaim<string>>().ToTable("tb_role_claims");
            builder.Entity<IdentityUserToken<string>>().ToTable("tb_user_tokens").HasKey(t => new { t.LoginProvider, t.Name, t.UserId });
        }
    }
}
