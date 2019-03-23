using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;

namespace UrbanEngine.Core.Application.Schedules
{
    public interface IScheduleService
    {
        Task<ScheduleResult<Event>> ScheduleEventAsync(Event eventDetail);

        Task<ScheduleResult<EventSession>> ScheduleEventSessionAsync(long eventId, EventSession sessionDetail);

        Task<bool> DeleteEventAsync(long eventId, string reason);

        Task<bool> DeleteEventSessionAsync(long eventId, long sessionId, string reason);

        Task<IEnumerable<ScheduleResult<Event>>> ListScheduledEventsAsync(ScheduleFilter scheduleFilter);

        Task<IEnumerable<ScheduleResult<EventSession>>> ListEventSessionsAsync(long eventId);
    }
}
