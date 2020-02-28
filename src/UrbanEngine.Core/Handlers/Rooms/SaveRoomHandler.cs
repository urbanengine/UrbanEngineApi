using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Managers.Rooms;
using UrbanEngine.Core.Messages.Rooms;
using UrbanEngine.Core.Models.Rooms;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Rooms
{
    public class SaveRoomHandler : IRequestHandler<SaveRoomMessage, CommandResultWithData>
    {
        private readonly IRoomManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SaveRoomHandler(IRoomManager manager, IMapper mapper, ILogger<GetRoomsHandler> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommandResultWithData> Handle(SaveRoomMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(SaveRoomHandler)} - Handler - Start");

            _logger.LogInformation("mapping dto to entity");
            var entity = _mapper.Map<RoomDetailDto, RoomEntity>(request.Detail);

            RoomEntity savedEntity;
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
            var data = _mapper.Map<RoomEntity, RoomDetailDto>(savedEntity);

            _logger.LogInformation("creating command result");
            var result = data?.Id > 0 ?
                new CommandResultWithData(data, $"Room {request.Action.Name}", 200, true) :
                new CommandResultWithData(null, message: $"failed to {request.Action.Name} Room", statusCode: 0, success: false); 

            _logger.LogInformation($"{nameof(SaveRoomHandler)} - Handler - Start");

            return result;
        }
    }
}
