using UrbanEngine.Core.Entities;

namespace UrbanEngine.Infrastructure.Data.Repository
{
    public class EventRepository : EfRepository<EventEntity>
    {
        public EventRepository(UrbanEngineDbContext dbContext) 
            : base(dbContext) { }
    }
}
