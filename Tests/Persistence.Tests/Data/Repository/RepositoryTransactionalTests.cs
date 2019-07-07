using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanEngine.Infrastructure.Persistence.Data.Repository;
using UrbanEngine.Tests.Persistence.Tests.TestHelpers;
using UrbanEngine.Tests.Persistence.Tests.TestHelpers.Scopes;

namespace UrbanEngine.Tests.Persistence.Tests.Data.Repository
{
    [TestClass]
    public class RepositoryTransactionalTests
    {
        [TestMethod]
        public async Task CreateAsync_Should_CreateNewItem()
        {
            var newEntity = new FakeEntity(4, "NewItem", "Test");
            var expectedResult = newEntity.Id;

            using (var scope = new DefaultScope())
            {
                var result = await scope.InstanceUnderTest.CreateAsync(newEntity);
                var actualResult = result?.Id;
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        public async Task UpdateAsync_Should_UpdateExistingItem()
        {
            FakeEntity itemToUpdate = null;
            using (var scope = new DefaultScope())
            {
                itemToUpdate = await scope.InstanceUnderTest.GetByIdAsync(1);
                itemToUpdate.Name = "Updated Name";

                var result = await scope.InstanceUnderTest.UpdateAsync(itemToUpdate);
                Assert.IsTrue(result == 1);
            } 
        }

        private class DefaultScope : RepositoryTestScope<TestTransactionalRepository>
        {
            public DefaultScope()
            {
                InstanceUnderTest = new TestTransactionalRepository(GetDbContext());
            }
        }

        // inherint from EfRepository to test all the inherited functionality
        private class TestTransactionalRepository : EfRepository<FakeEntity>
        {
            public TestTransactionalRepository(UrbanEngineTestDbContext dbContext)
                : base(dbContext) { }
        }
    }
}
