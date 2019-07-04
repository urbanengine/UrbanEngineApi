using Microsoft.EntityFrameworkCore;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;

namespace UrbanEngine.Infrastructure.Persistence.Data.Repository
{
    public class EventVenueRepository : EfRepository<EventVenue>
    {
        public EventVenueRepository(DbContext dbContext) : base(dbContext) { }
    }
}
