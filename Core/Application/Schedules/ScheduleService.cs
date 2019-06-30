using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;
using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Core.Application.Schedules
{
    public class ScheduleService : IScheduleService
    {
        #region Local Fields

        private readonly IEventRepository _eventRepository;
        private readonly ILogger _logger;

        #endregion

        #region Constructors

        public ScheduleService(IEventRepository eventRepository, ILogger<ScheduleService> logger)
        {
            _eventRepository = eventRepository;
            _logger = logger;
        }

        #endregion

        #region Implementation of IScheduleService

        public async Task<ScheduleResult<Event>> ScheduleEventAsync(Event eventDetail)
        {
            _logger.LogDebug("ScheduleEventAsync - eventDetail: {eventDetail}", eventDetail);

            var scheduledEvent = await _eventRepository.CreateAsync(eventDetail);

            ScheduleResult<Event> scheduleResult = scheduledEvent?.Id > 0 ?
                new ScheduleResult<Event>(scheduledEvent, "event created", true) :
                new ScheduleResult<Event>(null, "failed to create event", false);

            return scheduleResult;
        }

        public Task<bool> DeleteEventAsync(long eventId, string reason)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Event> GetEventDetail(long eventId)
        {
            _logger.LogDebug("GetEventDetail - eventId: {eventId}", eventId);

            var eventDetail = await _eventRepository.GetByIdAsync(eventId);
            return eventDetail;
        }
        
        public async Task<IEnumerable<Event>> ListScheduledEventsAsync(ScheduleFilter filter, IPagingParameters paging = null)
        {
            _logger.LogDebug("ListScheduledEventsAsync - filter: {filter}, paging: {paging}", filter, paging);

            var specification = new ScheduleFilterSpecification(filter, paging);

            var scheduledEvents = await _eventRepository.ListAsync(specification);
            return scheduledEvents;
        }

        public Task<IEnumerable<ScheduleResult<EventSession>>> ListEventSessionsAsync(long eventId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ScheduleResult<EventSession>> ScheduleEventSessionAsync(long eventId, EventSession sessionDetail)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteSessionAsync(long eventId, long sessionId, string reason)
        {
            throw new System.NotImplementedException();
        }

        Task<bool> IScheduleService.DeleteEventAsync(long eventId, string reason)
        {
            throw new System.NotImplementedException();
        }

        Task<bool> IScheduleService.DeleteEventSessionAsync(long eventId, long sessionId, string reason)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
