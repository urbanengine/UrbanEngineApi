using UrbanEngine.Core.Entities;

namespace UrbanEngine.Infrastructure.Data.Repository
{
	public class UserRepository : EfRepository<UserEntity>
	{
		public UserRepository(UrbanEngineDbContext dbContext)
			: base(dbContext) { }
	}
}
