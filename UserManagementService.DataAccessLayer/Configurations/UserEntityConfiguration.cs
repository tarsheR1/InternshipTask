using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.DataAccessLayer.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> entity)
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
        }
    }
}