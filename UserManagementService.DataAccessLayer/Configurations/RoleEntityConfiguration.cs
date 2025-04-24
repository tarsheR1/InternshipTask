using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementService.DataAccessLayer.Entities.Role;

namespace UserManagementService.DataAccessLayer.Configurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> entity)
        {
            entity.ToTable("roles");

            entity.HasKey(r => r.Id)
                .HasName("pk_roles_id");

            entity.Property(r => r.Id)
                .HasColumnName("id");

            entity.Property(r => r.Name)
                .HasColumnName("name")
                .IsRequired();
        }
    }
}