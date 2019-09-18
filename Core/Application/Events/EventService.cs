using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;
using UrbanEngine.Core.Common.Results;

namespace UrbanEngine.Core.Application.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;
        private readonly ILogger _logger;

        public EventService(IEventRepository repository, ILogger<EventService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CommandResultWithData> CreateEventAsync(IEventModel eventModel)
        {
            _logger.LogDebug("CreateEventAsync - {input}", eventModel); 
            return await CreateOrUpdateEvent(eventModel);
        }

        public async Task<CommandResult> DeleteEventAsync(long eventId)
        {
            _logger.LogDebug("DeleteEventAsync {id}", eventId);

            if (eventId <= 0)
                throw new ArgumentException($"{nameof(eventId)} must be greater than 0");

            _logger.LogDebug("retrieving entity by id");
            var entity = await _repository.GetByIdAsync(eventId);
            if (entity == null)
                throw new KeyNotFoundException($"unable to find an event with id {eventId}");

            // perform a "soft" delete but updating IsDeleted flag
            entity.IsDeleted = true;

            _logger.LogDebug("performing soft delete by updating entity IsDeleted flag for id {id}", eventId);
            var itemsUpdated = await _repository.UpdateAsync(entity);

            var result = itemsUpdated > 0 ?
                new CommandResult($"event {eventId} marked as deleted", 200, true) :
                new CommandResult($"unable to mark {eventId} as deleted", 500, false);

            return result;
        }

        public async Task<QueryResult> GetEventAsync<TProjected>(long eventId) where TProjected : IEventModel, new()
        {
            _logger.LogDebug("GetEventAsync - {eventId}", eventId);

            var selector = new TProjected();
            var specification = new EventSpecification(p => p.Id == eventId, selector);

            var data = await _repository.FirstOrDefaultAsync(specification);
            _logger.LogDebug("Event found {found}", data != null);

            var result = QueryResult<IEventModel>.New(data); 
            return result;
        }

        public async Task<QueryResult> GetEventsAsync<TProjected>(IEventFilter filter) where TProjected : IEventModel, new()
        {
            _logger.LogDebug("GetEventsAsync - {filter}", filter);

            var selector = new TProjected(); 
            var specification = new EventSpecification(filter, selector);
            _logger.LogDebug("Specification created to filter results - {specification}", specification);

            var data = await _repository.ListAsync(specification); 
            _logger.LogDebug("Events found {count}", data.Count);

            var result = QueryResult<IEnumerable<IEventModel>>.New(data); 
            return result;
        }

        public async Task<CommandResultWithData> UpdateEventAsync(long eventId, IEventModel eventModel)
        {
            _logger.LogDebug("UpdateEventAsync - id {id}, event {input}", eventId, eventModel);
             
            if (eventId <= 0)
                throw new ArgumentException($"{nameof(eventId)} must be greater than 0");

            return await CreateOrUpdateEvent(eventModel, eventId);
        }

        
        private async Task<CommandResultWithData> CreateOrUpdateEvent(IEventModel eventModel, long? eventId = null)
        {
            _logger.LogDebug("CreateOrUpdateEvent - {input}", eventModel);

            if (eventModel == null)
                throw new ArgumentNullException($"{nameof(eventModel)} cannot be null");

            _logger.LogDebug("convert model to domain entity");
            var entity = eventModel.ToDomainEntity(id: eventId);

            Event createdOrUpdatedEntity;
            var action = "";
            if (eventId > 0)
            {
                _logger.LogDebug("update entity in database with id {id}", eventId);

                var itemsUpdated = await _repository.UpdateAsync(entity);
                createdOrUpdatedEntity = itemsUpdated > 0 ? entity : null;
                action = "updated";
            }
            else
            {
                _logger.LogDebug("create entity in database");
                createdOrUpdatedEntity = await _repository.CreateAsync(entity);
                action = "created";
            }

            _logger.LogDebug($"convert {action} entity to model");
            var model = eventModel.FromDomainEntity(createdOrUpdatedEntity);

            _logger.LogDebug("create command result to return to client");
            var result = createdOrUpdatedEntity?.Id > 0 ?
                new CommandResultWithData(model, $"event {action}", 200, true) :
                new CommandResultWithData(null, message: $"failed to {action} event", statusCode: 0, success: false); 

            return result;
        }
    }
}
