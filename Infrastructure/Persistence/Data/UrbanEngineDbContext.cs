using Microsoft.EntityFrameworkCore;

namespace UrbanEngine.Infrastructure.Persistence.Data
{
    public class UrbanEngineDbContext : DbContext
    {
        public string SchemaName { get; set; } = "ue";

        public UrbanEngineDbContext(DbContextOptions options) 
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // set a default schema 
            if(!string.IsNullOrEmpty(SchemaName))
                modelBuilder.HasDefaultSchema(SchemaName);

            // apply configurations
            ApplyConfigurations(modelBuilder);

            // apply seed data
            ApplySeedData(modelBuilder);

            // call to base to handle any remaining 
            base.OnModelCreating(modelBuilder);
        }

        protected virtual void ApplySeedData(ModelBuilder modelBuilder)
        {
            SeedDataGenerator.Instance.ApplySeedData(modelBuilder);
        }

        protected virtual void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            // look for all configuration in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UrbanEngineDbContext).Assembly);
        }
    }
}
