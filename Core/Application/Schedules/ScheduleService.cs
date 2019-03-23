using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;
using UrbanEngine.Core.Application.SharedKernel;

namespace UrbanEngine.Core.Application.Schedules
{
    public class ScheduleService : IScheduleService
    {
        #region Local Fields

        private readonly IScheduledEventRepository _eventRepository; 

        #endregion

        #region Constructors

        public ScheduleService(IScheduledEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        #endregion

        #region Implementation of IScheduleService

        public Task<ScheduledEventModel> ScheduleEventAsync(EventDetailModel eventDetail)
        {
            throw new System.NotImplementedException();
        }

        public Task<ScheduledEventModel> DeleteEventAsync(long eventId, string reason)
        {
            throw new System.NotImplementedException();
        }
        
        public Task<IEnumerable<ScheduledEventModel>> ListScheduledEventsAsync(EventsFilterModel filter)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ScheduledEventSessionModel>> ListEventSessionsAsync(long eventId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ScheduledEventSessionModel> ScheduleEventSessionAsync(long eventId, SessionDetailModel sessionDetail)
        {
            throw new System.NotImplementedException();
        }

        public Task<ScheduledEventModel> DeleteSessionAsync(long eventId, long sessionId, string reason)
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
