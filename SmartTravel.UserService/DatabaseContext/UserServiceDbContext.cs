using Microsoft.EntityFrameworkCore;
using SmartTravel.UserService.Entities;

namespace SmartTravel.UserService.DatabaseContext
{
    public class UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
    }
}
