using System.Threading.Tasks;
using UrbanEngine.Core.Common.Results;

namespace UrbanEngine.Core.Application.Venues
{
    public interface IEventVenueService
    {
        Task<QueryResult> GetVenuesAsync<TProjected>(IEventVenueFilter filter)
            where TProjected : IEventVenueModel, new();

        Task<CommandResultWithData> CreateVenueAsync(IEventVenueModel eventVenue);

        Task<CommandResultWithData> UpdateVenueAsync(long eventVenueId, IEventVenueModel eventVenue);

        Task<CommandResult> DeleteVenueAsync(long eventVenueId);
    }
}
