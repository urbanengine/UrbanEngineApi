using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.SharedKernel.Managers;

namespace UrbanEngine.Core.Managers.Events
{
    public class EventManager : ManagerBase<EventEntity>, IEventManager
    {
        public EventManager(IAsyncRepository<EventEntity> repository, ILogger<EventManager> logger) 
            : base(repository, logger) { }
    }
}
