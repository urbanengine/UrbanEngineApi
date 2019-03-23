using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;

namespace UrbanEngine.Core.Application.Schedules
{
    public class ScheduleService : IScheduleService
    {
        #region Local Fields

        private readonly IAsyncRepository<Event> _eventRepository; 

        #endregion

        #region Constructors

        public ScheduleService(IAsyncRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        #endregion

        #region Implementation of IScheduleService

        public Task<ScheduleResult<Event>> ScheduleEventAsync(Event eventDetail)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteEventAsync(long eventId, string reason)
        {
            throw new System.NotImplementedException();
        }
        
        public Task<IEnumerable<ScheduleResult<Event>>> ListScheduledEventsAsync(ScheduleFilter filter)
        {
            throw new System.NotImplementedException();
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
