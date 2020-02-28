using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.Rooms;
using UrbanEngine.Core.Messages.Rooms;
using UrbanEngine.Core.Models.Rooms;
using UrbanEngine.Core.Specifications.Rooms;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Rooms
{
    public class GetRoomsHandler : IRequestHandler<GetRoomsMessage, QueryResult<IEnumerable<RoomListItemDto>>>
    {
        private readonly IRoomManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetRoomsHandler(IRoomManager manager, IMapper mapper, ILogger<GetRoomsHandler> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResult<IEnumerable<RoomListItemDto>>> Handle(GetRoomsMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GetRoomsHandler)} - Handler - Start");

            _logger.LogInformation("generating specification from request");
            var specification = new RoomSpecification(request);

            _logger.LogInformation("retrieving result from manager");
            var result = await _manager.GetAsync(specification);

            _logger.LogInformation($"mapping result to dto");
            var data = _mapper.Map<IEnumerable<RoomEntity>, IEnumerable<RoomListItemDto>>(result);

            _logger.LogInformation($"creating {nameof(QueryResult)} from mapped data");
            var queryResult = QueryResult<IEnumerable<RoomListItemDto>>.New(data);

            _logger.LogInformation($"{nameof(GetRoomsHandler)} - Handler - End");

            return queryResult;
        }
    }
}
