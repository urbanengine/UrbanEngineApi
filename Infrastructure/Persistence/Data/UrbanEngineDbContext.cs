using Microsoft.EntityFrameworkCore;

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
    }
}
