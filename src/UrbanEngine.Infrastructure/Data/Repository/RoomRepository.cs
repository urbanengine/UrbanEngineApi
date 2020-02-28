using Microsoft.EntityFrameworkCore;
using UrbanEngine.Core.Entities;

namespace UrbanEngine.Infrastructure.Data.Repository
{
	public class RoomRepository : EfRepository<RoomEntity>
	{
		public RoomRepository(UrbanEngineDbContext dbContext) 
			: base(dbContext) { }
	}
}
