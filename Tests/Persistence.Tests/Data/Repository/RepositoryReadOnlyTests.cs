using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrbanEngine.Infrastructure.Persistence.Data.Repository;
using UrbanEngine.Tests.Persistence.Tests.TestHelpers;
using UrbanEngine.Tests.Persistence.Tests.TestHelpers.Scopes;

namespace UrbanEngine.Tests.Persistence.Tests.Data.Repository
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

            using (var scope = new DefaultScope())
            {
                var actualResult = await scope.InstanceUnderTest.CountAsync(specWithNoItems);
                Assert.AreEqual(0, actualResult);
            }
        }

        [TestMethod]
        public async Task AnyAsync_Should_BeTrue_AllItems()
        {
            var specWithItems = TestSpecification.AllItems;

            using (var scope = new DefaultScope())
            {
                var actualResult = await scope.InstanceUnderTest.AnyAsync(specWithItems);
                Assert.AreEqual(true, actualResult);
            }
        }

        [TestMethod]
        public async Task AnyAsync_Should_BeFalse_NoItems()
        {
            var specWithNoItems = TestSpecification.NoItems;

            using (var scope = new DefaultScope())
            {
                var actualResult = await scope.InstanceUnderTest.AnyAsync(specWithNoItems);
                Assert.AreEqual(false, actualResult);
            }
        }

        [TestMethod]
        public async Task FirstOrDefaultAsync_Should_Return_FirstItem()
        {
            var expectedResult = TestSeedData.FakeEntities.ElementAt(0).Id;
            var specById = new TestSpecification(p => p.Id == expectedResult); 

            using (var scope = new DefaultScope())
            {
                var result = await scope.InstanceUnderTest.FirstOrDefaultAsync(specById);
                var actualResult = result?.Id;
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public async Task SingleOrDefaultAsync_Should_Return_ItemByName()
        {
            var expectedResult = TestSeedData.FakeEntities.ElementAt(0).Name;
            var specById = new TestSpecification(p => p.Name == expectedResult);

            using (var scope = new DefaultScope())
            {
                var result = await scope.InstanceUnderTest.SingleOrDefaultAsync(specById);
                var actualResult = result?.Name;
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public async Task ListAsync_Should_Return_ExpectedItems()
        {
            var expectedResult = TestSeedData.FakeEntities.Count(p => p.IsDeleted == false); 
            var spec = new TestSpecification(p => p.IsDeleted == false);

            using (var scope = new DefaultScope())
            {
                var result = await scope.InstanceUnderTest.ListAsync(spec);
                var actualResult = result.Count();
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public async Task ListAllAsync_Should_Return_AllItems()
        {
            var expectedResult = TestSeedData.FakeEntities.Count(); 

            using (var scope = new DefaultScope())
            {
                var result = await scope.InstanceUnderTest.ListAllAsync();
                var actualResult = result.Count();
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public async Task GetByIdAsync_Should_Return_Item()
        {
            var expectedResult = TestSeedData.FakeEntities.ElementAt(1);

            using (var scope = new DefaultScope())
            {
                var result = await scope.InstanceUnderTest.GetByIdAsync(expectedResult.Id);
                Assert.AreEqual(expectedResult.Id, result.Id);
            }
        }

        private sealed class DefaultScope : RepositoryTestScope<TestReadOnlyRepository>
        {
            public DefaultScope()
            {
                InstanceUnderTest = new TestReadOnlyRepository(GetDbContext());
            }
        }

        // inherit from EfRepository to test all the inherited functionality
        private class TestReadOnlyRepository : EfRepository<FakeEntity>
        {
            public TestReadOnlyRepository(UrbanEngineTestDbContext dbContext) 
                : base(dbContext) { }
        }

    }
}