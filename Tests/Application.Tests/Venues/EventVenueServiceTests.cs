using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrbanEngine.Core.Application.Venues;
using UrbanEngine.Tests.Application.TestHelpers.Scopes;
using Moq;

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
            public DefaultScope()
            {
                InstanceUnderTest = new EventVenueService(null, null);
            }
        }
    }
}
