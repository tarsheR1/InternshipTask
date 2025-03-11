using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

using EventManagementService.Domain.Models;

namespace EventManagementService.Infrastracture.Persistence
{
    public class EventDbContext : DbContext
    {
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }

        public EventDbContext(DbContextOptions<EventDbContext> options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventEntity>().ToTable("events");
                   
            modelBuilder.Entity<EventEntity>(entity =>
            {
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Id).HasColumnName("event_id"); 
                entity.Property(e => e.Title).HasColumnName("event_title"); 
                entity.Property(e => e.Description).HasColumnName("event_description");
                entity.Property(e => e.Date).HasColumnName("event_date");
                entity.Property(e => e.Location).HasColumnName("event_location");

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Events)
                    .HasForeignKey(e => e.CategoryId);
            });


            modelBuilder.Entity<EventEntity>().ToTable("categories");

            modelBuilder.Entity<CategoryEntity>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).HasColumnName("category_id");
                entity.Property(c => c.Title).HasColumnName("category_title");
            });
        }
    }
}
