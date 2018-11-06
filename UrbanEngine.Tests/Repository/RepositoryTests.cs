namespace UrbanEngine.Tests.Repository {
    using Microsoft.EntityFrameworkCore; 
    using Infrastructure.Repository; 
    using TestData;
    using Xunit;
    using System.Linq;

    public class RepositoryTests {
 
        readonly IRepository _repository;

        public RepositoryTests() {
            _repository = GetInMemoryRepository();
        }

        private IRepository GetInMemoryRepository() { 
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase( "InMemoryDatabase" );

            TestDbContext dbContext = new TestDbContext( builder.Options );
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            return new Repository( dbContext ); 
        }

        [Fact]
        public async void Should_GetSingle_ExistingRecord() {
            int expectedId = 1;  

            // find an existing record by id 
            var result = await _repository.SingleOrDefaultAsync<FooEntity>( p => p.Id == expectedId ); 
            Assert.Equal( expectedId, result?.Id ?? 0 );
        }

        [Fact]
        public async void Should_GetById() {
            long id = 2;

            var result = await _repository.GetByIdAsync<FooEntity>( id );
            Assert.Equal( id, result?.Id );
        }

        [Fact]
        public async void Should_Find_ByKey() {
            long id = 1;

            var result = await _repository.FindAsync<FooEntity>( id );
            Assert.Equal( id, result?.Id ); 
        }

        [Fact]
        public async void Should_GetList() {
            // find any records that contain letter e
            var result = await _repository.ListAsync<FooEntity>( p => p.Value.Contains( "e" ) );
            Assert.True( result.Count() > 1 );
        }

        [Fact]
        public void Should_Find_Set() {
            var dbSet = _repository.Set<FooEntity>();
            Assert.NotNull( dbSet );
        }
         
        [Fact]
        public async void Should_Create_NewRecord() {
            var newEntity = new FooEntity { Id = 6, Value = "consectetur" };

            // create the record, should indicate 1 record added in the result 
            var result = await _repository.CreateAsync( newEntity );
            Assert.Equal( 1, result );

            // query to make sure the record exists 
            var query = await _repository.SingleOrDefaultAsync<FooEntity>( p => p.Id == newEntity.Id );
            Assert.Equal( newEntity.Value, query.Value );
        }

        [Fact]
        public async void Should_Update_ExistingRecord() {
            var existingValue = "lorem";

            // query by value
            var existingEntity = await _repository.FirstOrDefaultAsync<FooEntity>( p => p.Value == existingValue );
            Assert.Equal( existingValue, existingEntity?.Value );

            // change the value, update, and ensure record was updated
            existingEntity.Value = "Maecenas";
            var updateResult = await _repository.UpdateAsync( existingEntity );
            Assert.Equal( 1, updateResult );
        }

        [Fact]
        public async void Should_ThrowException_ForUpdate_RecordNotExists() {
            // try to update some record where primary key does not exist
            var entityThatDoesNotExist = new FooEntity { Id = 99999999, Value = "lorem" }; 
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>( () => _repository.UpdateAsync( entityThatDoesNotExist ) );
        }

        [Fact]
        public async void Should_Delete_ExistingRecord() {
            var entity = new FooEntity { Id = 2, Value = "ipsum" };

            // delete existing record
            var result = await _repository.DeleteAsync( entity );
            Assert.Equal( 1, result );
        }

        [Fact]
        public async void Should_ThrowException_ForDelete_RecordNotExists() {
            // try to update some record where primary key does not exist
            var entityThatDoesNotExist = new FooEntity { Id = 99999999, Value = "lorem" };
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>( () => _repository.DeleteAsync( entityThatDoesNotExist ) );
        }
         
    } 
}
