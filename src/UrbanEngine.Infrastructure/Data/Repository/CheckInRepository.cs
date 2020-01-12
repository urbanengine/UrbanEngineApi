using UrbanEngine.Core.Entities;

namespace UrbanEngine.Infrastructure.Data.Repository
{
    public class CheckInRepository : EfRepository<CheckInEntity>
    {
        public CheckInRepository( UrbanEngineDbContext dbContext )
            : base( dbContext ) { }
    }
}
