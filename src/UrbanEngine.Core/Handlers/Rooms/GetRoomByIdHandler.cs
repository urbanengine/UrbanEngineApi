using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.Rooms;
using UrbanEngine.Core.Messages.Rooms;
using UrbanEngine.Core.Models.Rooms;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Rooms
{
    public class GetRoomByIdHandler : IRequestHandler<GetRoomByIdMessage, QueryResult<RoomDetailDto>>
    {
        private readonly IRoomManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetRoomByIdHandler(IRoomManager manager, IMapper mapper, ILogger<GetRoomsHandler> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResult<RoomDetailDto>> Handle(GetRoomByIdMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GetRoomByIdHandler)} - Handler - Start");
            
            _logger.LogInformation("retrieving result from manager");     
            var result = await _manager.GetByIdAsync(request.Id);
            
            _logger.LogInformation($"mapping result to dto");
            var data = _mapper.Map<RoomEntity, RoomDetailDto>(result);

            _logger.LogInformation($"creating {nameof(QueryResult)} from mapped data");
            var queryResult = QueryResult<RoomDetailDto>.New(data);

            _logger.LogInformation($"{nameof(GetRoomByIdHandler)} - Handler - End");

            return queryResult;
        }
    }
}
