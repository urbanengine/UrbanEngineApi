using Microsoft.EntityFrameworkCore;
using System;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;

namespace UrbanEngine.Infrastructure.Persistence.Data
{
    public interface ISeedDataGenerator
    {
        void ApplySeedData(ModelBuilder modelBuilder);
    }

    internal class SeedDataGenerator : ISeedDataGenerator
    {
        #region Singleton Support

        private static readonly Lazy<SeedDataGenerator> _instance =
            new Lazy<SeedDataGenerator>(new SeedDataGenerator());

        private SeedDataGenerator() { }

        public static SeedDataGenerator Instance => _instance.Value;
         
        #endregion

        public void ApplySeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventVenue>().HasData(EventVenueSeedData());
        }

        #region Seed Data

        public static EventVenue[] EventVenueSeedData()
        {
            return new EventVenue[]
            {
                new EventVenue(1, "Huntsville West") {
                    Address = "3001 9th Avenue Southwest",
                    City = "Huntsville",
                    State = "AL",
                    PostalCode = "35805",
                    Region = RegionType.SouthernRegion,
                    Country = "United States"
                }
            };
        }
        
        #endregion
    }
}
