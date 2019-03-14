using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.SharedKernel;

namespace UrbanEngine.Core.Application.Schedules
{
    public interface IScheduleService
    {
        Task<ScheduledEventModel> ScheduleEventAsync(EventDetailModel eventDetail);

        Task<ScheduledEventSessionModel> ScheduleEventSessionAsync(long eventId, SessionDetailModel sessionDetail);

        Task<bool> DeleteEventAsync(long eventId, string reason);

        Task<bool> DeleteSessionAsync(long sessionId, string reason);

        Task<IEnumerable<ScheduledEventModel>> ListScheduledEventsAsync(DateTimeRange dateTimeRange);

        Task<IEnumerable<ScheduledEventSessionModel>> ListEventSessionsAsync(long eventId);
    }
}
