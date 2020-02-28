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
            modelBuilder.Entity<EventVenueEntity>().HasData(EventVenueSeedData());
            modelBuilder.Entity<EventEntity>().HasData(EventSeedData());
            modelBuilder.Entity<CheckInEntity>().HasData(CheckInSeedData());
			modelBuilder.Entity<RoomEntity>().HasData(RoomSeedData());
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
                new EventEntity(id: 1, name: "show256", eventType: EventType.Workshop, description: null, startDate: DateTime.Now, endDate: DateTime.Now, organizerId: null, roomId: 4 ),
                new EventEntity(id: 2, name: "Designer's Corner", eventType: EventType.Workshop, description: null, startDate: DateTime.Now, endDate: DateTime.Now, organizerId: null, roomId: 2 ),
                new EventEntity(id: 3, name: "Huntsville AI", eventType: EventType.Workshop, description: null, startDate: DateTime.Now, endDate: DateTime.Now, organizerId: null, roomId: 5 )
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

		public static RoomEntity[] RoomSeedData()
		{
			return new RoomEntity[]
			{
				new RoomEntity(1, "Cafe Conference Room", "Cafe Conference Room", null, null, 1, true),
				new RoomEntity(2, "Front Conference Room", "Front Conference Room", null, null, 1, true),
				new RoomEntity(3, "Corner Conference Room", "Corner Conference Room", null, null, 1, true),
				new RoomEntity(4, "Library", "Library", null, null, 1, false),
				new RoomEntity(5, "Training Room", "Training Room", null, null, 1, true)
			};
		}

        #endregion
    }
}
