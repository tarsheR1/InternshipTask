using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementService.DataAccessLayer.Entities.Users;

namespace UserManagementService.DataAccessLayer.Configurations
{
    public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> entity)
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
        }
    }
}