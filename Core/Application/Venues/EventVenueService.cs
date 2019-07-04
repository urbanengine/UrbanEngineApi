using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;
using UrbanEngine.Core.Common.Results;

namespace UrbanEngine.Core.Application.Venues
{
    public class EventVenueService : IEventVenueService
    {
        private readonly IEventVenueRepository _repository;
        private readonly ILogger _logger;

        public EventVenueService(IEventVenueRepository repository, ILogger<EventVenueService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<QueryResult> GetVenues<TProjected>(IEventVenueFilter filter) 
            where TProjected : IEventVenueModel, new()
        {
            _logger.LogDebug("GetVenues - {filter}", filter);

            var selector = new TProjected(); 
            var specification = new EventVenueSpecification(filter, selector);
            _logger.LogDebug("Specification created to filter results - {specification}", specification);

            var data = await _repository.ListAsync(specification); 
            _logger.LogDebug("EventVenues found {count}", data.Count);

            var result = QueryResult<IEnumerable<IEventVenueModel>>.New(data); 
            return result;
        }
    }
}
