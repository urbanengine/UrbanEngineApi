using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanEngine.Core.Managers.CheckIn;
using UrbanEngine.Tests.Utils;
using Moq;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.Core.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace UrbanEngine.Tests.Core.Managers.CheckIn
{
    [TestClass]
    public class CheckInManagerTests
    {
        [TestMethod, TestCategory(TestCategory.Unit)]
        public async Task CheckInManager_When_GetByIdAsync_Should_Delegate_To_Repository()
        {
            // Arrange
            var scope = new DefaultScope();
            object id = 1;

            // Act
            await scope.InstanceUnderTest.GetByIdAsync(id);

            // Assert
            scope.CheckInRepositoryMock.Verify(x => x.GetByIdAsync(It.Is<object>(v => v == id)), Times.Once);
        }

        [TestMethod, TestCategory(TestCategory.Unit)]
        public async Task CheckInManager_When_GetById_Should_Return_ExpectedValue()
        {
            // Arrange
            var scope = new DefaultScope();
            var expectedId = DefaultScope.TestEntity.Id;

            // Act
            var result = await scope.InstanceUnderTest.GetByIdAsync(expectedId);

            // Assert
            Assert.AreEqual(expectedId, result.Id);
        }

        [TestMethod, TestCategory(TestCategory.Unit)]
        public async Task CheckInManager_When_IdIsNull_Should_ThrowException()
        {
            // Arrange
            var scope = new DefaultScope();

            // Act/Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                scope.InstanceUnderTest.GetByIdAsync(null));
        }

        private class DefaultScope : TestScope<ICheckInManager>
        {
            public static CheckInEntity TestEntity { get; } = new CheckInEntity
            {
                Id = 1,
                CheckedInAt = DateTime.Now,
                DateCreated = DateTime.Now,
                UserId = 1,
                EventId = 1,
                IsDeleted = false
            };

            public Mock<IAsyncRepository<CheckInEntity>> CheckInRepositoryMock { get; } = new Mock<IAsyncRepository<CheckInEntity>>();
            private Mock<ILogger<CheckInManager>> LoggerMock { get; } = new Mock<ILogger<CheckInManager>>();

            public DefaultScope()
            {
                CheckInRepositoryMock.Setup(s => s.GetByIdAsync(
                        It.Is<object>(v => (long)v == TestEntity.Id)))
                    .ReturnsAsync(TestEntity);

                InstanceUnderTest = new CheckInManager(
                    CheckInRepositoryMock.Object,
                    LoggerMock.Object);
            }
        }
    }
}
