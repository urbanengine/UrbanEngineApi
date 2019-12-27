using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.Events;
using UrbanEngine.Core.Messages.Events;
using UrbanEngine.Core.Models.Events;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Events
{
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdMessage, QueryResult<EventDetailDto>>
    {
        private readonly IEventManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetEventByIdHandler(IEventManager manager, IMapper mapper, ILogger<GetEventsHandler> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResult<EventDetailDto>> Handle(GetEventByIdMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GetEventByIdHandler)} - Handler - Start");
            
            _logger.LogInformation("retrieving result from manager");     
            var result = await _manager.GetByIdAsync(request.Id);
            
            _logger.LogInformation($"mapping result to dto");
            var data = _mapper.Map<EventEntity, EventDetailDto>(result);

            _logger.LogInformation($"creating {nameof(QueryResult)} from mapped data");
            var queryResult = QueryResult<EventDetailDto>.New(data);

            _logger.LogInformation($"{nameof(GetEventByIdHandler)} - Handler - End");

            return queryResult;
        }
    }
}
