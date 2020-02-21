using Microsoft.EntityFrameworkCore;
using System;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;

namespace UrbanEngine.Infrastructure.Data
{
    public interface ISeedDataGenerator
    {
        void ApplySeedData(ModelBuilder modelBuilder);
    }

    internal class SeedDataGenerator : ISeedDataGenerator {
        #region Singleton Support

        private static readonly Lazy<SeedDataGenerator> _instance =
            new Lazy<SeedDataGenerator>( new SeedDataGenerator() );

        private SeedDataGenerator() { }

        public static SeedDataGenerator Instance => _instance.Value;

        #endregion

        public void ApplySeedData( ModelBuilder modelBuilder ) {
            modelBuilder.Entity<EventVenueEntity>().HasData( EventVenueSeedData() );
            modelBuilder.Entity<EventEntity>().HasData( EventSeedData() );
            modelBuilder.Entity<CheckInEntity>().HasData( CheckInSeedData() );
        }

        #region Seed Data


        public static EventVenueEntity[] EventVenueSeedData()
        {
            return new EventVenueEntity[]
            {
                new EventVenueEntity(1, "Huntsville West") {
                    Address = "3001 9th Avenue Southwest",
                    City = "Huntsville",
                    State = "AL",
                    PostalCode = "35805",
                    Region = RegionType.SouthernRegion,
                    Country = "United States"
                }
            };
        }

        public static EventEntity[] EventSeedData() {
            return new EventEntity[]
            {
                new EventEntity(id: 1, name: "show256", eventType: EventType.Workshop, description: null, startDate: DateTime.Now, endDate: DateTime.Now, organizerId: null, venueId: 1 )
            };
        }

        public static CheckInEntity[] CheckInSeedData() {
            return new CheckInEntity[]
            {
                new CheckInEntity()
                {
                    Id = 1,
                    DateCreated = DateTime.Now,
                    CheckedInAt = DateTime.Now,
                    EventId = 1,
                    IsDeleted = false 
                }
            };
        }

        #endregion
    }
}
