using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Exceptions;
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
			var roomIsAvailable = await IsRoomAvailableAsync(entity.RoomId.Value, entity.StartDate.Value, entity.EndDate.Value);
			if(!roomIsAvailable)
				throw new RoomUnavailableException($"roomId: {entity.RoomId}", entity.StartDate, entity.EndDate);

			return await base.CreateAsync(entity);
		}

		public async override Task<EventEntity> UpdateAsync(EventEntity entity)
		{
			var roomIsAvailable = await IsRoomAvailableAsync(entity.RoomId.Value, entity.StartDate.Value, entity.EndDate.Value);
			if(!roomIsAvailable)
				throw new RoomUnavailableException($"roomId: {entity.RoomId}", entity.StartDate, entity.EndDate);

			return await base.UpdateAsync(entity);
		}

		public async Task<bool> IsRoomAvailableAsync(long roomId, DateTimeOffset startDateTime, DateTimeOffset endDateTime)
		{ 
			var specification = new EventByRoomSpecification(roomId, startDateTime, endDateTime);
			var result = await Repository.AnyAsync(specification);
			return !result; // if no results room should be free
		}
	}
}
