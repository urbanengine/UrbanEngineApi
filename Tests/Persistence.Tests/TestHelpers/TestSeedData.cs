using Microsoft.EntityFrameworkCore;
using System;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Infrastructure.Persistence.Data;

namespace UrbanEngine.Tests.Persistence.Tests.TestHelpers
{
    /// <summary>
    /// mock data used for verifying tests
    /// </summary>
    internal class TestSeedData : ISeedDataGenerator
    {
        private static readonly Lazy<TestSeedData> _instance = new Lazy<TestSeedData>(new TestSeedData());

        private TestSeedData() { }

        public static TestSeedData Instance => _instance.Value;

        public void ApplySeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FakeEntity>().HasData(FakeEntities);
            modelBuilder.Entity<EventVenue>().HasData(EventVenues);
        }

        #region All Test Data 

        #region FakeEntity

        public static FakeEntity[] FakeEntities => new FakeEntity[]
        {
            new FakeEntity(1, "Foo", "Bar"),
            new FakeEntity(2, "Lorem", "Ipsum"),
            new FakeEntity(3, "Salut", "Dolor")
        };

        #endregion

        #region EventVenue

        public static EventVenue[] EventVenues => new[]
        {
            new EventVenue(1, "Test Venue 1"),
            new EventVenue(2, "Test Venue 2"),
            new EventVenue(3, "Test Venue 3")
        };

        #endregion

        #endregion
    }
}
