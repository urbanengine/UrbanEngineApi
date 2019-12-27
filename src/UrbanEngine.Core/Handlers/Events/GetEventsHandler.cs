using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.Events;
using UrbanEngine.Core.Messages.Events;
using UrbanEngine.Core.Models.Events;
using UrbanEngine.Core.Specifications.Events;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Events
{
    public class GetEventsHandler : IRequestHandler<GetEventsMessage, QueryResult<IEnumerable<EventListItemDto>>>
    {
        private readonly IEventManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetEventsHandler(IEventManager manager, IMapper mapper, ILogger<GetEventsHandler> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResult<IEnumerable<EventListItemDto>>> Handle(GetEventsMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GetEventsHandler)} - Handler - Start");

            _logger.LogInformation("generating specification from request");
            var specification = new EventSpecification(request);

            _logger.LogInformation("retrieving result from manager");
            var result = await _manager.GetAsync(specification);

            _logger.LogInformation($"mapping result to dto");
            var data = _mapper.Map<IEnumerable<EventEntity>, IEnumerable<EventListItemDto>>(result);

            _logger.LogInformation($"creating {nameof(QueryResult)} from mapped data");
            var queryResult = QueryResult<IEnumerable<EventListItemDto>>.New(data);

            _logger.LogInformation($"{nameof(GetEventsHandler)} - Handler - End");

            return queryResult;
        }
    }
}
