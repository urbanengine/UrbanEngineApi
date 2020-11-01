using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Managers.Venues;
using UrbanEngine.Core.Messages.Venues;

namespace UrbanEngine.Core.Handlers.Venues
{
	public class SaveVenueHandler : IRequestHandler<SaveVenueMessage, EventVenueEntity>
    {
        private readonly IEventVenueManager _manager;
        private readonly ILogger _logger;

        public SaveVenueHandler(IEventVenueManager manager, ILogger<GetVenuesHandler> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<EventVenueEntity> Handle(SaveVenueMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(SaveVenueHandler)} - Handler - Start");

            _logger.LogInformation("mapping dto to entity");
            var entity = request.Detail;

            EventVenueEntity savedEntity;
            if(request.Action == ActionType.Update)
            {
                _logger.LogInformation("updating entity", entity.Id);
                savedEntity = await _manager.UpdateAsync(entity);
            }
            else if(request.Action == ActionType.Create)
            {
                _logger.LogInformation("inserting entity");
                savedEntity = await _manager.CreateAsync(entity);
            }
            else
            {
                throw new NotSupportedException($"{request.Action.Name} is not supported for this request");
            }

            _logger.LogInformation($"{nameof(SaveVenueHandler)} - Handler - End");

            return savedEntity;
        }
    }
}
