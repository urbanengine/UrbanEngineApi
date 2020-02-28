using Microsoft.EntityFrameworkCore;
using UrbanEngine.Core.Entities;

namespace UrbanEngine.Infrastructure.Data.Repository
{
	public class RoomRepository : EfRepository<RoomEntity>
	{
		public RoomRepository(DbContext dbContext) 
			: base(dbContext) { }
	}
}
