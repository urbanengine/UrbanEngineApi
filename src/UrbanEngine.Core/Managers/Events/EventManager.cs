using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Specifications.Events;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.SharedKernel.Managers;

namespace UrbanEngine.Core.Managers.Events
{
    public class EventManager : ManagerBase<EventEntity>, IEventManager
    {
        public EventManager(IAsyncRepository<EventEntity> repository, ILogger<EventManager> logger) 
            : base(repository, logger) { }

		public async override Task<EventEntity> CreateAsync(EventEntity entity)
		{
			var roomIsAvailable = await IsRoomAvailableAsync(entity);
			if(!roomIsAvailable)
				throw new Exception(""); // TODO: create a custom exception and catch in the Handler

			return await base.CreateAsync(entity);
		}

		public async Task<bool> IsRoomAvailableAsync(EventEntity entity)
		{ 
			var specification = new EventSpecification(null); // TODO: make a custom specification for filtering by room

			var result = await Repository.AnyAsync(specification);
			return result;
		}
	}
}
