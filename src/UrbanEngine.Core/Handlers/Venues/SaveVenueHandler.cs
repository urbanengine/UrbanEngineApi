using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Managers.Venues;
using UrbanEngine.Core.Messages.Venues;
using UrbanEngine.Core.Models.Venues;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Venues
{
    public class SaveVenueHandler : IRequestHandler<SaveEventVenueMessage, CommandResultWithData>
    {
        private readonly IEventVenueManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SaveVenueHandler(IEventVenueManager manager, IMapper mapper, ILogger<GetVenuesHandler> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommandResultWithData> Handle(SaveEventVenueMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(SaveVenueHandler)} - Handler - Start");

            _logger.LogInformation("mapping dto to entity");
            var entity = _mapper.Map<EventVenueDetailDto, EventVenueEntity>(request.Detail);

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

            _logger.LogInformation("mapping result to dto");
            var data = _mapper.Map<EventVenueEntity, EventVenueDetailDto>(savedEntity);

            _logger.LogInformation("creating command result");
            var result = new CommandResultWithData<EventVenueDetailDto>(data, "", null, null);

            _logger.LogInformation($"{nameof(SaveVenueHandler)} - Handler - Start");

            return result;
        }
    }
}
