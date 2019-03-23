using System.Collections.Generic;
using System.Threading.Tasks;

namespace UrbanEngine.Core.Application.Schedules
{
    public interface IScheduleService
    {
        Task<ScheduledEventModel> ScheduleEventAsync(EventDetailModel eventDetail);

        Task<ScheduledEventSessionModel> ScheduleEventSessionAsync(long eventId, SessionDetailModel sessionDetail);

        Task<bool> DeleteEventAsync(long eventId, string reason);

        Task<bool> DeleteEventSessionAsync(long eventId, long sessionId, string reason);

        Task<IEnumerable<ScheduledEventModel>> ListScheduledEventsAsync(EventsFilterModel filter);

        Task<IEnumerable<ScheduledEventSessionModel>> ListEventSessionsAsync(long eventId);
    }
}
