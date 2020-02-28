using System.Threading.Tasks;
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

		public override Task<EventEntity> CreateAsync(EventEntity entity)
		{
			return base.CreateAsync(entity);
		}

		public bool IsRoomAvailable()
		{
			return false;
		}
	}
}
