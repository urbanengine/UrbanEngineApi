using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Core.Application.Schedules
{
    public interface IScheduleService
    {
        Task<ScheduleResult<Event>> ScheduleEventAsync(Event eventDetail);

        Task<ScheduleResult<EventSession>> ScheduleEventSessionAsync(long eventId, EventSession sessionDetail);

        Task<Event> GetEventDetail(long eventId);

        Task<bool> DeleteEventAsync(long eventId, string reason);

        Task<bool> DeleteEventSessionAsync(long eventId, long sessionId, string reason);

        Task<IEnumerable<Event>> ListScheduledEventsAsync(ScheduleFilter scheduleFilter, IPagingParameters paging = null);

        Task<IEnumerable<ScheduleResult<EventSession>>> ListEventSessionsAsync(long eventId);
    }
}
