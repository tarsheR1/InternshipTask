using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using EventManagementService.Domain.Models;
using EventManagementService.DataAccess.Persistence.Configurations;
using EventManagementService.Infrastructure.DataAccess.Configurations;

namespace EventManagementService.DataAccess.Persistence
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

            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());

            modelBuilder.ApplyConfiguration(new EventEntityConfiguration());
        }
    }
}
