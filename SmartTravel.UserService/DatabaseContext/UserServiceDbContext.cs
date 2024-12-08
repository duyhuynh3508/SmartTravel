using Microsoft.EntityFrameworkCore;
using SmartTravel.Shared.Entities;

namespace SmartTravel.UserService.DatabaseContext
{
    public class UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("tblUsers");
                entity.HasKey(e => e.UserId);

                entity.HasOne<RoleEntity>()
                    .WithMany()
                    .HasForeignKey(e => e.RoleId);
            });

            modelBuilder.Entity<RoleEntity>(entity =>
            {
                entity.ToTable("tblRoles");
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId)
                      .ValueGeneratedNever();
            });

            modelBuilder.Entity<PermissionEntity>(entity =>
            {
                entity.ToTable("tblPermissions");
                entity.HasKey(e => e.PermissionId);

                entity.HasOne<RoleEntity>()
                    .WithMany()
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
