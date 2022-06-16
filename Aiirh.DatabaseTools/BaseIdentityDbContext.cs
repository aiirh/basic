using Aiirh.DatabaseTools.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Aiirh.DatabaseTools
{
    public class BaseIdentityDbContext<TApplicationUser, TApplicationRole> : IdentityDbContext<TApplicationUser, TApplicationRole, string>, IHasCurrentUserId where TApplicationUser : IdentityUser, IApplicationUser where TApplicationRole : IdentityRole, IApplicationRole
    {
        public string CurrentUserId { get; set; }

        public BaseIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TApplicationUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TApplicationUser>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TApplicationRole>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TApplicationRole>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }

        public override int SaveChanges()
        {
            this.UpdateAuditEntities();
            this.UpdateGuidKeyEntities();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.UpdateAuditEntities();
            this.UpdateGuidKeyEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.UpdateAuditEntities();
            this.UpdateGuidKeyEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.UpdateAuditEntities();
            this.UpdateGuidKeyEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
