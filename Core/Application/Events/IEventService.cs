using System.Threading.Tasks;
using UrbanEngine.Core.Common.Results;

namespace UrbanEngine.Core.Application.Events
{
    public interface IEventService
    {
        Task<QueryResult> GetEventAsync<TProjected>(long eventId)
            where TProjected : IEventModel, new();

        Task<QueryResult> GetEventsAsync<TProjected>(IEventFilter filter)
            where TProjected : IEventModel, new();

        Task<CommandResultWithData> CreateEventAsync(IEventModel eventModel);

        Task<CommandResultWithData> UpdateEventAsync(long eventId, IEventModel eventVenue);

        Task<CommandResult> DeleteEventAsync(long eventId);
    }
}
