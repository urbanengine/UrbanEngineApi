using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Managers.Events;
using UrbanEngine.Core.Messages.Events;
using UrbanEngine.Core.Models.Events;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Events
{
    public class SaveEventHandler : IRequestHandler<SaveEventMessage, CommandResultWithData>
    {
        private readonly IEventManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SaveEventHandler(IEventManager manager, IMapper mapper, ILogger<GetEventsHandler> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommandResultWithData> Handle(SaveEventMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(SaveEventHandler)} - Handler - Start");

            _logger.LogInformation("mapping dto to entity");
            var entity = _mapper.Map<EventDetailDto, EventEntity>(request.Detail);

            EventEntity savedEntity;
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
            var data = _mapper.Map<EventEntity, EventDetailDto>(savedEntity);

            _logger.LogInformation("creating command result");
            var result = data?.Id > 0 ?
                new CommandResultWithData(data, $"event {request.Action.Name}", 200, true) :
                new CommandResultWithData(null, message: $"failed to {request.Action.Name} event", statusCode: 0, success: false); 

            _logger.LogInformation($"{nameof(SaveEventHandler)} - Handler - Start");

            return result;
        }
    }
}
