using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.SharedKernel.Managers;

namespace UrbanEngine.Core.Managers.Venues
{
    public class EventVenueManager : ManagerBase<EventVenueEntity>, IEventVenueManager
    {
        public EventVenueManager(IAsyncRepository<EventVenueEntity> repository, ILogger<ManagerBase<EventVenueEntity>> logger) 
            : base(repository, logger) { }
    }
}