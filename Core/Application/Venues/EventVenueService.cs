using Microsoft.Extensions.Logging;
using System;
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

        public async Task<CommandResultWithData> CreateVenue(IEventVenueModel eventVenue)
        {
            _logger.LogDebug("CreateVenue - {input}", eventVenue);

            if (eventVenue == null)
                throw new ArgumentNullException($"{nameof(eventVenue)} cannot be null");

            _logger.LogDebug("convert model to domain entity");
            var entity = eventVenue.ToDomainEntity();

            _logger.LogDebug("create entity in database");
            var createdEntity = await _repository.CreateAsync(entity);

            _logger.LogDebug("convert created entity to model");
            var model = eventVenue.FromDomainEntity(createdEntity);

            _logger.LogDebug("create command result to return to client");
            var result = createdEntity?.Id > 0 ?
                new CommandResultWithData(model, "event venue created", 200, true) :
                new CommandResultWithData(null, message: "failed to create event venue", statusCode: 0, success: false); 

            return result;
        }
    }
}
