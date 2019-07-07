using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Specifications;
using UrbanEngine.Infrastructure.Persistence.Data.Repository;
using UrbanEngine.Tests.Persistence.Tests.TestHelpers;
using UrbanEngine.Tests.Persistence.Tests.TestHelpers.Scopes;

namespace Persistence.Tests.Data.Repository
{
    [TestClass]
    public class RepositoryReadOnlyTests
    {
        [TestMethod]
        public async Task CountAsync_Should_BeGreaterThanZero_AllItems()
        {
            var specWithItems = TestSpecification.AllItems;
            var expectedResult = TestSeedData.FakeEntities.Count();

            using (var scope = new DefaultScope())
            {
                var actualResult = await scope.InstanceUnderTest.CountAsync(specWithItems);
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public async Task CountAsync_Should_BeZero_NoItems()
        {
            var specWithNoItems = TestSpecification.NoItems;
            var expectedResult = 0;

            using (var scope = new DefaultScope())
            {
                var actualResult = await scope.InstanceUnderTest.CountAsync(specWithNoItems);
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public async Task AnyAsync_Should_BeTrue_AllItems()
        {
            var specWithItems = TestSpecification.AllItems;
            var expectedResult = true;

            using (var scope = new DefaultScope())
            {
                var actualResult = await scope.InstanceUnderTest.AnyAsync(specWithItems);
                Assert.AreEqual(expectedResult, actualResult);
            }
        }


        [TestMethod]
        public async Task AnyAsync_Should_BeFalse_NoItems()
        {
            var specWithNoItems = TestSpecification.NoItems;
            var expectedResult = false;

            using (var scope = new DefaultScope())
            {
                var actualResult = await scope.InstanceUnderTest.AnyAsync(specWithNoItems);
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        private class DefaultScope : RepositoryTestScope<TestRepository>
        {
            public DefaultScope()
            {
                InstanceUnderTest = new TestRepository(GetDbContext());
            }
        }

        private class TestRepository : EfRepository<FakeEntity>
        {
            public TestRepository(UrbanEngineTestDbContext dbContext) : base(dbContext)
            {
            }
        }

    }
}