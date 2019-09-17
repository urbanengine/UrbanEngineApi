using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public Task<CommandResultWithData> CreateEventAsync(IEventModel eventModel)
        {
            throw new NotImplementedException();
        }

        public Task<CommandResult> DeleteEventAsync(long eventId)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult> GetEventAsync<TProjected>(long eventId) where TProjected : IEventModel, new()
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult> GetEventsAsync<TProjected>(IEventFilter filter) where TProjected : IEventModel, new()
        {
            throw new NotImplementedException();
        }

        public Task<CommandResultWithData> UpdateEventAsync(long eventId, IEventModel eventVenue)
        {
            throw new NotImplementedException();
        }
    }
}
