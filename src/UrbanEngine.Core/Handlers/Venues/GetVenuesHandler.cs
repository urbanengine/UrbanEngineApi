using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.Venues;
using UrbanEngine.Core.Messages.Venues;
using UrbanEngine.Core.Models.Venues;
using UrbanEngine.Core.Specifications.Venues;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Venues
{
    public class GetVenuesHandler : IRequestHandler<GetEventVenuesMessage, QueryResult<IEnumerable<EventVenueListItemDto>>>
    {
        private readonly IEventVenueManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetVenuesHandler(IEventVenueManager manager, IMapper mapper, ILogger<GetVenuesHandler> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResult<IEnumerable<EventVenueListItemDto>>> Handle(GetEventVenuesMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GetVenuesHandler)} - Handler - Start");

            _logger.LogInformation("generating specification from request");
            var specification = new EventVenueSpecification(request);

            _logger.LogInformation("retrieving result from manager");
            var result = await _manager.GetAsync(specification);

            _logger.LogInformation($"mapping result to dto");
            var data = _mapper.Map<IEnumerable<EventVenueEntity>, IEnumerable<EventVenueListItemDto>>(result);

            _logger.LogInformation($"creating {nameof(QueryResult)} from mapped data");
            var queryResult = QueryResult<IEnumerable<EventVenueListItemDto>>.New(data);

            _logger.LogInformation($"{nameof(GetVenuesHandler)} - Handler - End");

            return queryResult;
        }
    }
}
