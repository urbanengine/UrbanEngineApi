using Microsoft.EntityFrameworkCore;

namespace UrbanEngine.Infrastructure.Persistence.Data
{
    public class UrbanEngineDbContext : DbContext
    {
        public string SchemaName { get; private set; } = "ue";

        public UrbanEngineDbContext(DbContextOptions<UrbanEngineDbContext> options) 
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // set a default schema 
            modelBuilder.HasDefaultSchema(SchemaName);

            // look for all configuration in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UrbanEngineDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
