using Microsoft.EntityFrameworkCore;
using UrbanEngine.Infrastructure.Persistence.Data;

namespace UrbanEngine.Tests.Persistence.Tests.TestHelpers
{
    internal class UrbanEngineTestDbContext : UrbanEngineDbContext
    {
        public UrbanEngineTestDbContext(DbContextOptions options) 
            : base(options) { }

        protected override void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            // apply everything in UrbanEngineDbContext
            base.ApplyConfigurations(modelBuilder);

            // additional test project specific configurations
            modelBuilder.Entity<FakeEntity>()
                .ToTable("FakeEntity")
                .HasKey(e => e.Id);
        }

        protected override void ApplySeedData(ModelBuilder modelBuilder)
        {
            // apply any seed data as stored in test project
            TestSeedData.Instance.ApplySeedData(modelBuilder);
        }
    }
}
