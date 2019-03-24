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
            modelBuilder.Entity<Event>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
