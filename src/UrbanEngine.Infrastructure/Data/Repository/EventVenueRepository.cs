using UrbanEngine.Core.Entities;

namespace UrbanEngine.Infrastructure.Data.Repository
{
    public class EventVenueRepository : EfRepository<EventVenueEntity>
    {
        public EventVenueRepository(UrbanEngineDbContext dbContext) 
            : base(dbContext) { }
    }
}
