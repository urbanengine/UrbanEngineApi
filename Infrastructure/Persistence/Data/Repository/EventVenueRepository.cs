using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;

namespace UrbanEngine.Infrastructure.Persistence.Data.Repository
{
    public class EventVenueRepository : EfRepository<EventVenue>, IEventVenueRepository
    {
        public EventVenueRepository(UrbanEngineDbContext dbContext) : base(dbContext) { }
    }
}
