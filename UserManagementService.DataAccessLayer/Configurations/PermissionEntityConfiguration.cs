using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.DataAccessLayer.Configurations
{
    public class PermissionEntityConfiguration : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> entity)
        {
            entity.ToTable("permissions");

            entity.HasKey(p => p.Id)
                .HasName("pk_permissions_id");

            entity.Property(p => p.Id)
                .HasColumnName("id");

            entity.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired();
        }
    }
}