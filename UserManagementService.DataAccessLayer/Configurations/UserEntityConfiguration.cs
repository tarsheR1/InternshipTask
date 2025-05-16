using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Entities.Users;

namespace UserManagementService.DataAccessLayer.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> entity)
        {
            entity.ToTable("users", schema: "dbo"); 

            entity.HasKey(u => u.Id)
                .HasName("pk_users_id");

            entity.Property(u => u.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("NEWID()"); 
            entity.Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired()
                .HasComment("Электронная почта пользователя");

            entity.HasIndex(u => u.Email)
                .HasDatabaseName("ix_users_email")
                .IsUnique();

            entity.Property(u => u.PasswordHash)
                .HasColumnName("password_hash")
                .HasMaxLength(255)
                .IsRequired()
                .HasComment("Хеш пароля пользователя");

            entity.Property(u => u.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired()
                .HasComment("Имя пользователя");

            entity.Property(u => u.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired()
                .HasComment("Фамилия пользователя");

            entity.Property(u => u.MiddleName)
                .HasColumnName("middle_name")
                .HasMaxLength(100)  
                .HasComment("Отчество пользователя (если есть)");

            entity.Property(u => u.Phone)
                .HasColumnName("phone")
                .HasMaxLength(15)
                .HasComment("Номер телефона пользователя");

            entity.Property(u => u.EmailConfirmationToken)
                .HasColumnName("email_confirmation_token")
                .HasMaxLength(100)
                .HasComment("Токен для подтверждения email");

            entity.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()") 
                .HasComment("Дата и время создания записи");

            entity.Property(u => u.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(false) 
                .HasComment("Флаг активности пользователя");

            entity.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "user_roles",
                    j => j
                        .HasOne<RoleEntity>()
                        .WithMany()
                        .HasForeignKey("role_id")
                        .HasConstraintName("fk_user_roles_role_id")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<UserEntity>()
                        .WithMany()
                        .HasForeignKey("user_id")
                        .HasConstraintName("fk_user_roles_user_id")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("user_id", "role_id")
                            .HasName("pk_user_roles");
                        j.HasComment("Таблица связи пользователей и ролей");
                    });
        }
    }
}