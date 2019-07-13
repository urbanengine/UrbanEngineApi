using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
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

        public async Task<QueryResult> GetVenuesAsync<TProjected>(IEventVenueFilter filter) 
            where TProjected : IEventVenueModel, new()
        {
            _logger.LogDebug("GetVenuesAsync - {filter}", filter);

            var selector = new TProjected(); 
            var specification = new EventVenueSpecification(filter, selector);
            _logger.LogDebug("Specification created to filter results - {specification}", specification);

            var data = await _repository.ListAsync(specification); 
            _logger.LogDebug("EventVenues found {count}", data.Count);

            var result = QueryResult<IEnumerable<IEventVenueModel>>.New(data); 
            return result;
        }

        public async Task<CommandResultWithData> CreateVenueAsync(IEventVenueModel eventVenue)
        {
            _logger.LogDebug("CreateVenueAsync - {input}", eventVenue); 
            return await CreateOrUpdateVenue(eventVenue);
        }

        public async Task<CommandResultWithData> UpdateVenueAsync(long eventVenueId, IEventVenueModel eventVenue)
        { 
            _logger.LogDebug("UpdateVenueAsync - id {id}, venue {input}", eventVenueId, eventVenue);
             
            if (eventVenueId <= 0)
                throw new ArgumentException($"{nameof(eventVenueId)} must be greater than 0");

            return await CreateOrUpdateVenue(eventVenue, eventVenueId);
        }

        public async Task<CommandResult> DeleteVenueAsync(long eventVenueId)
        {
            _logger.LogDebug("DeleteVenueAsync {id}", eventVenueId);

            if (eventVenueId <= 0)
                throw new ArgumentException($"{nameof(eventVenueId)} must be greater than 0");

            _logger.LogDebug("retrieving entity by id");
            var entity = await _repository.GetByIdAsync(eventVenueId);
            if (entity == null)
                throw new KeyNotFoundException($"unable to find an event venue with id {eventVenueId}");

            // perform a "soft" delete but updating IsDeleted flag
            entity.IsDeleted = true;

            _logger.LogDebug("performing soft delete by updating entity IsDeleted flag for id {id}", eventVenueId);
            var itemsUpdated = await _repository.UpdateAsync(entity);

            var result = itemsUpdated > 0 ?
                new CommandResult($"event venue {eventVenueId} marked as deleted", 200, true) :
                new CommandResult($"unable to mark {eventVenueId} as deleted", 500, false);

            return result;
        }

        private async Task<CommandResultWithData> CreateOrUpdateVenue(IEventVenueModel eventVenue, long? eventVenueId = null)
        {
            _logger.LogDebug("CreateOrUpdateVenue - {input}", eventVenue);

            if (eventVenue == null)
                throw new ArgumentNullException($"{nameof(eventVenue)} cannot be null");

            _logger.LogDebug("convert model to domain entity");
            var entity = eventVenue.ToDomainEntity();

            EventVenue createdOrUpdatedEntity;
            if (eventVenueId > 0)
            {
                _logger.LogDebug("update entity in database with id {id}", eventVenueId);

                var itemsUpdated = await _repository.UpdateAsync(entity);
                createdOrUpdatedEntity = itemsUpdated > 0 ? entity : null;
            }
            else
            {
                _logger.LogDebug("create entity in database");
                createdOrUpdatedEntity = await _repository.CreateAsync(entity);
            }

            _logger.LogDebug("convert created entity to model");
            var model = eventVenue.FromDomainEntity(createdOrUpdatedEntity);

            _logger.LogDebug("create command result to return to client");
            var result = createdOrUpdatedEntity?.Id > 0 ?
                new CommandResultWithData(model, "event venue created", 200, true) :
                new CommandResultWithData(null, message: "failed to create event venue", statusCode: 0, success: false); 

            return result;
        }
    }
}
