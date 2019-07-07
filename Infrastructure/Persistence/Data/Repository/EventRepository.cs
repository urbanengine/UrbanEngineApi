using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;

namespace UrbanEngine.Infrastructure.Persistence.Data.Repository
{
    public class EventRepository : EfRepository<Event>, IEventRepository
    {
        public EventRepository(UrbanEngineDbContext dbContext) : base(dbContext) { }
    }
}
