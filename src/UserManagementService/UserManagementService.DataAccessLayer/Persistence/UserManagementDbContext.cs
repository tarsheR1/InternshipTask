using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.DataAccessLayer.Persistence
{
    public class UserManagementDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<RolePermissionEntity> RolePermissions { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> contextOptions)
            : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UserEntity configuration
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middle_name")
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active"); 

            });

            // RefreshTokenEntity configuration
            modelBuilder.Entity<RefreshTokenEntity>(entity =>
            {
                entity.ToTable("refresh_tokens");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasMaxLength(500);

                entity.Property(e => e.Expires)
                    .HasColumnName("expires");

                entity.Property(e => e.Created)
                    .HasColumnName("created");

                entity.Property(e => e.Revoked)
                    .HasColumnName("revoked");

                // Relationship with User
                entity.HasOne(rt => rt.User)
                    .WithMany(u => u.RefreshTokens)
                    .HasForeignKey(rt => rt.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // RoleEntity configuration
            modelBuilder.Entity<RoleEntity>(entity =>
            {
                entity.ToTable("roles");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            // PermissionEntity configuration
            modelBuilder.Entity<PermissionEntity>(entity =>
            {
                entity.ToTable("permissions");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            // UserRoleEntity configuration (many-to-many)
            modelBuilder.Entity<UserRoleEntity>(entity =>
            {
                entity.ToTable("user_roles");
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id");

                entity.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // RolePermissionEntity configuration (many-to-many)
            modelBuilder.Entity<RolePermissionEntity>(entity =>
            {
                entity.ToTable("role_permissions");
                entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id");

                entity.Property(e => e.PermissionId)
                    .HasColumnName("permission_id");

                entity.HasOne(rp => rp.Role)
                    .WithMany(r => r.RolePermissions)
                    .HasForeignKey(rp => rp.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rp => rp.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(rp => rp.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
