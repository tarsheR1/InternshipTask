using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Configurations;
using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Entities.Users;

namespace UserManagementService.DataAccessLayer.Persistence
{
    public class UserManagementDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> contextOptions) 
            : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenEntityConfiguration());
        }
    }
}