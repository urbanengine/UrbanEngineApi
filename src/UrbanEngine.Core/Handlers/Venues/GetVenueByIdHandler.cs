using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.Venues;
using UrbanEngine.Core.Messages.Venues;
using UrbanEngine.Core.Models.Venues;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Venues
{
    public class GetVenueByIdHandler : IRequestHandler<GetVenueByIdMessage, QueryResult<EventVenueDetailDto>>
    {
        private readonly IEventVenueManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetVenueByIdHandler(IEventVenueManager manager, IMapper mapper, ILogger<GetVenuesHandler> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResult<EventVenueDetailDto>> Handle(GetVenueByIdMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GetVenueByIdHandler)} - Handler - Start");
            
            _logger.LogInformation("retrieving result from manager");     
            var result = await _manager.GetByIdAsync(request.Id);
            
            _logger.LogInformation($"mapping result to dto");
            var data = _mapper.Map<EventVenueEntity, EventVenueDetailDto>(result);

            _logger.LogInformation($"creating {nameof(QueryResult)} from mapped data");
            var queryResult = QueryResult<EventVenueDetailDto>.New(data);

            _logger.LogInformation($"{nameof(GetVenueByIdHandler)} - Handler - End");

            return queryResult;
        }
    }
}
