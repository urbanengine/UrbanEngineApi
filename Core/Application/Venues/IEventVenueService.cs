using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Common.Results;

namespace UrbanEngine.Core.Application.Venues
{
    public interface IEventVenueService
    {
        Task<QueryResult> GetVenues(EventVenueFilter filter);
    }
}
