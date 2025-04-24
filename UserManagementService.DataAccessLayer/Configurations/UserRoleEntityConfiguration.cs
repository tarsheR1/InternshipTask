using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementService.DataAccessLayer.Entities.Relations;

namespace UserManagementService.DataAccessLayer.Configurations
{
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> entity)
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
        }
    }
}