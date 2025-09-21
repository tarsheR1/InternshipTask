using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EventManagementService.Domain.Entities;

namespace EventManagementService.Infrastructure.DataAccess.Configurations
{
    public class EventEntityConfiguration : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder.ToTable("events");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("event_id");
            builder.Property(e => e.Title)
                .HasColumnName("event_title")
                .HasMaxLength(30);
            builder.Property(e => e.Description)
                .HasColumnName("event_description")
                .HasMaxLength(100);
            builder.Property(e => e.Date)
                .HasColumnName("event_date");
            builder.Property(e => e.Location)
                .HasColumnName("event_location")
                .HasMaxLength(40);
            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at");
            builder.Property(e => e.IsActive)
                .HasColumnName("is_active");

            builder.HasMany(e => e.Categories)
                .WithMany(c => c.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "event_categories",
                    j => j.HasOne<CategoryEntity>().WithMany().HasForeignKey("category_id"),
                    j => j.HasOne<EventEntity>().WithMany().HasForeignKey("event_id")
                );
        }
    }
}
