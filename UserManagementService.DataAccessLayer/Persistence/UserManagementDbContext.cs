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

        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(u => u.Id)
                    .HasName("pk_users_id");

                entity.Property(u => u.Id)
                    .HasColumnName("id");

                entity.Property(u => u.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(u => u.PasswordHash)
                    .HasColumnName("password_hash")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(u => u.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(u => u.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(u => u.MiddleName)
                    .HasColumnName("middle_name")
                    .HasMaxLength(100);

                entity.Property(u => u.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(15);

                entity.Property(u => u.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime2");

                entity.Property(u => u.IsActive)
                    .HasColumnName("is_active");  
            });

            modelBuilder.Entity<RoleEntity>(entity =>
            {
                entity.ToTable("roles");

                entity.HasKey(r => r.Id)
                    .HasName("pk_roles_id");

                entity.Property(r => r.Id)
                    .HasColumnName("id");

                entity.Property(r => r.Name)
                    .HasColumnName("name")
                    .IsRequired();
            });

            modelBuilder.Entity<PermissionEntity>(entity =>
            {
                entity.ToTable("permissions");

                entity.HasKey(p => p.Id)
                    .HasName("pk_permissions_id");

                entity.Property(p => p.Id)
                    .HasColumnName("id");

                entity.Property(p => p.Name)
                    .HasColumnName("name")
                    .IsRequired();
            });

            modelBuilder.Entity<RefreshTokenEntity>(entity =>
            {
                entity.ToTable("refresh_tokens");

                entity.HasKey(rt => rt.Id)
                    .HasName("pk_refresh_tokens_id");

                entity.Property(rt => rt.Id)
                    .HasColumnName("id");

                entity.Property(rt => rt.UserId)
                    .HasColumnName("user_id");

                entity.Property(rt => rt.Token)
                    .HasColumnName("token")
                    .IsRequired();

                entity.Property(rt => rt.Expires)
                    .HasColumnName("expires")
                    .HasColumnType("datetime2");

                entity.Property(rt => rt.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime2");

                entity.Property(rt => rt.Revoked)
                    .HasColumnName("revoked")
                    .HasColumnType("datetime2");

                entity.HasOne(rt => rt.User)
                    .WithMany()
                    .HasForeignKey(rt => rt.UserId)
                    .HasConstraintName("fk_refresh_tokens_user_id");
            });

            modelBuilder.Entity<UserRoleEntity>(entity =>
            {
                entity.ToTable("user_roles");

                entity.HasKey(ur => new { ur.UserId, ur.RoleId })
                    .HasName("pk_user_roles_user_id_role_id");

                entity.Property(ur => ur.UserId)
                    .HasColumnName("user_id");

                entity.Property(ur => ur.RoleId)
                    .HasColumnName("role_id");

                entity.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .HasConstraintName("fk_user_roles_user_id");

                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .HasConstraintName("fk_user_roles_role_id");
            });

            modelBuilder.Entity<RolePermissionEntity>(entity =>
            {
                entity.ToTable("role_permissions");

                entity.HasKey(rp => new { rp.RoleId, rp.PermissionId })
                    .HasName("pk_role_permissions_role_id_permission_id");

                entity.Property(rp => rp.RoleId)
                    .HasColumnName("role_id");

                entity.Property(rp => rp.PermissionId)
                    .HasColumnName("permission_id");

                entity.HasOne(rp => rp.Role)
                    .WithMany(r => r.RolePermissions)
                    .HasForeignKey(rp => rp.RoleId)
                    .HasConstraintName("fk_role_permissions_role_id");

                entity.HasOne(rp => rp.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(rp => rp.PermissionId)
                    .HasConstraintName("fk_role_permissions_permission_id");
            });
        }
    }
}