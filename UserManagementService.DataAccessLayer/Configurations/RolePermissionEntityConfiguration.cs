using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementService.DataAccessLayer.Entities.Relations;

namespace UserManagementService.DataAccessLayer.Configurations
{
    public class RolePermissionEntityConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
    {
        public void Configure(EntityTypeBuilder<RolePermissionEntity> entity)
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
        }
    }
}