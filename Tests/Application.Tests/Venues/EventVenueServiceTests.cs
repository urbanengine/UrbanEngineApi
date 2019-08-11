using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrbanEngine.Core.Application.Venues;
using UrbanEngine.Tests.Application.TestHelpers.Scopes;
using Moq;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;
using UrbanEngine.Core.Common.Paging;
using UrbanEngine.Core.Common.Results;

namespace UrbanEngine.Tests.Application.Venues
{
    [TestClass]
    public class EventVenueServiceTests
    {
        #region GetVenue Tests

        [TestMethod]
        public async Task GetVenueAsync_Should_Call_FirstOrDefaultAsync_WithSpecification()
        {
            var scope = new DefaultScope();

            var result = await scope.InstanceUnderTest.GetVenueAsync<EventVenueModelFake>(1);

            scope.MockRepository.Verify(v => v.FirstOrDefaultAsync(It.IsAny<EventVenueSpecification>()), Times.Once);
        }
         
        [TestMethod]
        public async Task GetVenueAsync_Should_Return_QueryResultType()
        {
            var scope = new DefaultScope();

            var result = await scope.InstanceUnderTest.GetVenueAsync<EventVenueModelFake>(scope.ModelFake.Id); 

            Assert.IsInstanceOfType(result, typeof(QueryResult));
        }

        [TestMethod]
        public async Task GetVenueAsync_Should_ReturnExpected()
        {
            var scope = new DefaultScope();

            var id = scope.ModelFake.Id;

            var result = await scope.InstanceUnderTest.GetVenueAsync<EventVenueModelFake>(id);
            var data = result?.Data as EventVenueModelFake;

            Assert.AreEqual(id, data?.Id);
        }
         
        [TestMethod]
        public async Task GetVenueAsync_Should_Return_DataAsExpectedType()
        {
            var scope = new DefaultScope();

            var result = await scope.InstanceUnderTest.GetVenueAsync<EventVenueModelFake>(scope.ModelFake.Id); 

            Assert.IsInstanceOfType(result?.Data, typeof(EventVenueModelFake));
        }

        #endregion

        #region GetVenues Tests
         
        [TestMethod]
        public async Task GetVenuesAsync_Should_Call_ListAsync_WithSpecification()
        {
            var scope = new DefaultScope();
            
            var filter = new EventVenueFilterFake();

            var result = await scope.InstanceUnderTest.GetVenuesAsync<EventVenueModelFake>(filter);

            scope.MockRepository.Verify(v => v.ListAsync(It.IsAny<EventVenueSpecification>()), Times.Once);
        }
        
        [TestMethod]
        public async Task GetVenuesAsync_Should_Return_QueryResultType()
        {
            var scope = new DefaultScope();
            
            var filter = new EventVenueFilterFake();

            var result = await scope.InstanceUnderTest.GetVenuesAsync<EventVenueModelFake>(filter); 

            Assert.IsInstanceOfType(result, typeof(QueryResult));
        }
        
        [TestMethod]
        public async Task GetVenuesAsync_Should_Return_DataAsExpectedType()
        {
            var scope = new DefaultScope();
            
            var filter = new EventVenueFilterFake();

            var result = await scope.InstanceUnderTest.GetVenuesAsync<EventVenueModelFake>(filter); 

            Assert.IsInstanceOfType(result?.Data, typeof(IReadOnlyList<EventVenueModelFake>));
        }

        [TestMethod]
        public async Task GetVenuesAsync_Should_BePaged()
        {
            var scope = new DefaultScope();
            
            var filter = new EventVenueFilterFake();

            var result = await scope.InstanceUnderTest.GetVenuesAsync<EventVenueModelFake>(filter); 

            Assert.IsTrue(result?.Paging.IsPaged ?? false);
        }
        #endregion
        
        #region CreateVenue Tests

        [TestMethod]
        public async Task CreateVenueAsync_Should_Call_CreateAsync_WithEntity()
        {
            var scope = new DefaultScope();

            var newItem = new EventVenueModelFake();

            var result = await scope.InstanceUnderTest.CreateVenueAsync(newItem);

            scope.MockRepository.Verify(v => v.CreateAsync(It.IsAny<EventVenue>()), Times.Once);
        }
        
        [TestMethod]
        public async Task CreateVenueAsync_Should_Return_CommandResultType()
        {
            var scope = new DefaultScope();

            var newItem = new EventVenueModelFake();

            var result = await scope.InstanceUnderTest.CreateVenueAsync(newItem);

            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        #endregion

        #region UpdateVenue Tests

        [TestMethod]
        public async Task UpdateVenueAsync_Should_Call_UpdateAsync_WithEntity()
        {
            var scope = new DefaultScope();

            var itemToUpdate = new EventVenueModelFake();

            var result = await scope.InstanceUnderTest.UpdateVenueAsync(scope.ModelFake.Id, itemToUpdate);

            scope.MockRepository.Verify(v => v.UpdateAsync(It.IsAny<EventVenue>()), Times.Once);
        }
        
        [TestMethod]
        public async Task UpdateVenueAsync_Should_Return_CommandResultType()
        {
            var scope = new DefaultScope();

            var result = await scope.InstanceUnderTest.UpdateVenueAsync(scope.ModelFake.Id, scope.ModelFake);

            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public async Task UpdateVenueAsync_WithNoId_Should_ThrowArgumentException()
        {
            var scope = new DefaultScope();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() => 
                scope.InstanceUnderTest.UpdateVenueAsync(0, scope.ModelFake));
        }

        #endregion

        #region DeleteVenue Tests

        [TestMethod]
        public async Task DeleteVenueAsync_Should_Call_UpdateAsync_WithEntity_SoftDelete()
        {
            var scope = new DefaultScope();

            var result = await scope.InstanceUnderTest.DeleteVenueAsync(scope.ModelFake.Id);

            scope.MockRepository.Verify(v => v.UpdateAsync(It.IsAny<EventVenue>()), Times.Once);
        }
         
        [TestMethod]
        public async Task DeleteVenueAsync_Should_Call_GetByIdAsync()
        {
            var scope = new DefaultScope();

            var result = await scope.InstanceUnderTest.DeleteVenueAsync(scope.ModelFake.Id);

            scope.MockRepository.Verify(v => v.GetByIdAsync(scope.ModelFake.Id), Times.Once);
        }
         
        [TestMethod]
        public async Task DeleteVenueAsync_Should_Return_CommandResultType()
        {
            var scope = new DefaultScope();

            var result = await scope.InstanceUnderTest.DeleteVenueAsync(scope.ModelFake.Id);

            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
        
        [TestMethod]
        public async Task DeleteVenueAsync_WithNoId_Should_ThrowArgumentException()
        {
            var scope = new DefaultScope();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() => 
                scope.InstanceUnderTest.DeleteVenueAsync(0));
        }

        #endregion

        #region EventVenue Tests Setup and Mocks

        private sealed class DefaultScope : TestScope<IEventVenueService>
        {
            public Mock<IEventVenueRepository> MockRepository { get; }

            public EventVenueModelFake ModelFake => new EventVenueModelFake{ Id = 1 };

            public DefaultScope()
            {
                var mockLogger = GetMockLogger();
                MockRepository = GetMockRepository();

                InstanceUnderTest = new EventVenueService(MockRepository.Object, mockLogger.Object);
            }

            private Mock<IEventVenueRepository> GetMockRepository()
            {
                var mockRepository = new Mock<IEventVenueRepository>();

                mockRepository
                    .Setup(s => s.ListAsync(It.IsAny<EventVenueSpecification>()))
                    .ReturnsAsync((EventVenueSpecification spec) =>
                    {
                        var data = new List<EventVenueModelFake> {ModelFake};
                        return new PageableReadOnlyList<EventVenueModelFake>(
                            data,
                            spec.Skip,
                            spec.Take,
                            data.Count);
                    });


            mockRepository
                    .Setup(s => s.FirstOrDefaultAsync(It.IsAny<EventVenueSpecification>()))
                    .ReturnsAsync((EventVenueSpecification spec) => ModelFake);

                mockRepository
                    .Setup(s => s.GetByIdAsync(It.IsAny<object>()))
                    .ReturnsAsync((long id) => new EventVenue(id, "Test"));

                return mockRepository;
            }

            private Mock<ILogger<EventVenueService>> GetMockLogger()
            {
                return new Mock<ILogger<EventVenueService>>();
            }
        }

        private class EventVenueModelFake : IEventVenueModel
        {
            public long Id { get; set; }

            public Expression<Func<EventVenue, IEventVenueModel>> Projection => x => FromDomainEntity(x);
            
            public EventVenue ToDomainEntity(long? id = null)
            {
                return new EventVenue(id ?? 0, "Test");
            }

            public IEventVenueModel FromDomainEntity(EventVenue eventVenue)
            {
                return new EventVenueModelFake();
            }
        }

        private class EventVenueFilterFake : IEventVenueFilter
        {
            public int? PageNumber => 1;
            public int? PageSize => 10;
            public bool? DisablePaging => false;
            public int GetTakeValue() => 10;

            public int GetSkipValue() => 0;

            public string Region { get; }
            public string City { get; }
            public string State { get; }
            public string PostalCode { get; }
            public bool? IsDeleted { get; }
        }
        #endregion
    }
}
