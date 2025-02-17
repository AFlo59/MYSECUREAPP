using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySecureApp.Models;

namespace MySecureApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity, Role, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserPublic> UserPublics { get; set; }
        public DbSet<UserMetier> UserMetiers { get; set; }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.GetType().GetProperty("CreatedAt")?.SetValue(entry.Entity, DateTime.UtcNow);
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.GetType().GetProperty("UpdatedAt")?.SetValue(entry.Entity, DateTime.UtcNow);
                }
            }

            return base.SaveChanges();
        }
    }
}