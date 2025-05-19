using EventManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagementService.DataAccess.Persistence.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("categories");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id")
                .IsRequired(); 
                               

            builder.Property(c => c.Title)
                .HasColumnName("title")
                .HasMaxLength(30)
                .IsRequired()
                .IsFixedLength(); 
        }
    }
}