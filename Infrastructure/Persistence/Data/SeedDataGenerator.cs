using Microsoft.EntityFrameworkCore;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;

namespace UrbanEngine.Infrastructure.Persistence.Data
{
    internal static class SeedDataGenerator
    {
        public static void ApplySeedData(this ModelBuilder modelBuilder)
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
