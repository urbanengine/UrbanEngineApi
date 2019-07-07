using Microsoft.VisualStudio.TestTools.UnitTesting; 
using System.Linq; 
using System.Threading.Tasks;
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
            var expectedResult = TestSeedData.FakeEntities.Where(p => p.IsDeleted == false); 
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

        private class DefaultScope : RepositoryTestScope<TestReadOnlyRepository>
        {
            public DefaultScope()
            {
                InstanceUnderTest = new TestReadOnlyRepository(GetDbContext());
            }
        }

        // inherint from EfRepository to test all the inherited functionality
        private class TestReadOnlyRepository : EfRepository<FakeEntity>
        {
            public TestReadOnlyRepository(UrbanEngineTestDbContext dbContext) 
                : base(dbContext) { }
        }

    }
}