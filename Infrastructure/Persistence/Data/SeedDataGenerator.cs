using Microsoft.EntityFrameworkCore;
using UrbanEngine.Core.Application.Entities;
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
                new EventVenue(1, "CoWorking Night", new Location { Address = "3001 9th Avenue Southwest", City = "Huntsville", State = "AL", Zip = "35805" })
            };
        }

        #endregion
    }
}
