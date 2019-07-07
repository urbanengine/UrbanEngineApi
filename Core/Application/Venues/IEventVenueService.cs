using System.Threading.Tasks;
using UrbanEngine.Core.Common.Results;

namespace UrbanEngine.Core.Application.Venues
{
    public interface IEventVenueService
    {
        Task<QueryResult> GetVenues<TProjected>(IEventVenueFilter filter)
            where TProjected : IEventVenueModel, new();

        Task<CommandResultWithData> CreateVenue(IEventVenueModel eventVenue);

        Task<CommandResultWithData> UpdateVenue(long eventVenueId, IEventVenueModel eventVenue);

        Task<CommandResult> DeleteVenue(long eventVenueId);
    }
}
