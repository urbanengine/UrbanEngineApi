using Microsoft.EntityFrameworkCore;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;

namespace UrbanEngine.Infrastructure.Persistence.Data
{
    public class UrbanEngineDbContext : DbContext
    {
        public UrbanEngineDbContext(DbContextOptions<UrbanEngineDbContext> options) 
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity => {
                entity.HasKey(e => e.Id);

                entity.Property(p => p.Title).IsRequired();

                entity.Property(p => p.EventType)
                    .HasConversion(
                        p => p.Value,
                        p => EventType.FromValue(p));
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
