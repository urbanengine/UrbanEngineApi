namespace UrbanEngine.Tests.TestData {
    using Microsoft.EntityFrameworkCore;
    using System;

    public class TestDbContext : DbContext {
        public TestDbContext( DbContextOptions options )
            : base( options ) { }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            // let base take care of any initial items 
            base.OnModelCreating( modelBuilder );

            // configure each entity 
            ConfigureEntities( modelBuilder );

            // seed data 
            SeedData( modelBuilder );
        }

        protected virtual void ConfigureEntities( ModelBuilder modelBuilder ) {
            modelBuilder.Entity<FooEntity>();
            modelBuilder.Entity<BarEntity>();
        }

        protected void SeedData( ModelBuilder modelBuilder ) {

            modelBuilder.Entity<FooEntity>().HasData(
                new FooEntity { Id = 1, Value = "lorem" },
                new FooEntity { Id = 2, Value = "ipsum" },
                new FooEntity { Id = 3, Value = "dolor" },
                new FooEntity { Id = 4, Value = "sit" },
                new FooEntity { Id = 5, Value = "amet" } );

            var now = DateTime.Now;
            modelBuilder.Entity<BarEntity>().HasData(
                new BarEntity { Id = 1, Name = "John", DateCreated = now.AddDays( -20 ) },
                new BarEntity { Id = 2, Name = "Mary", DateCreated = now.AddDays( -10 ) },
                new BarEntity { Id = 3, Name = "Rebecca", DateCreated = now.AddDays( -5 ) } );

        }
    }
}
