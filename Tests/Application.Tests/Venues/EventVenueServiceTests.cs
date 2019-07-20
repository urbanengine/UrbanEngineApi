using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrbanEngine.Core.Application.Venues;
using UrbanEngine.Tests.Application.TestHelpers.Scopes;
using Moq;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;

namespace UrbanEngine.Tests.Application.Venues
{
    [TestClass]
    public class EventVenueServiceTests
    {
        [TestMethod]
        public async Task GetVenuesAsync_Should_GetFilteredVenues()
        {
            var result = await Task.FromResult(false);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CreateVenueAsync_Should_AddVenue()
        {
            var result = await Task.FromResult(false);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateVenueAsync_Should_UpdateExistingVenue()
        {
            var result = await Task.FromResult(false);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteVenueAsync_Should_UpdateIsDeleted()
        {
            var result = await Task.FromResult(false);
            Assert.IsTrue(result);
        }

        private sealed class DefaultScope : TestScope<IEventVenueService>
        {
            public Mock<IEventVenueRepository> MockRepository { get; }

            public DefaultScope()
            {
                var mockLogger = GetMockLogger();
                MockRepository = GetMockRepository();

                InstanceUnderTest = new EventVenueService(MockRepository.Object, mockLogger.Object);
            }

            private Mock<IEventVenueRepository> GetMockRepository()
            {
                var mockRepository = new Mock<IEventVenueRepository>();

                //mockRepository.Setup(s=>s.ListAsync())

                return mockRepository;
            }

            private Mock<ILogger<EventVenueService>> GetMockLogger()
            {
                return new Mock<ILogger<EventVenueService>>();
            }
        }
    }
}
