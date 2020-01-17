using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.CheckIn;
using UrbanEngine.Core.Messages.CheckIn;
using UrbanEngine.Core.Models.CheckIn;
using UrbanEngine.Core.Specifications.CheckIns;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.CheckIn {
    public class GetCheckInsHandler : IRequestHandler<GetCheckInsMessage, QueryResult<IEnumerable<CheckInListItemDto>>> {
        private readonly ICheckInManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetCheckInsHandler( ICheckInManager manager, IMapper mapper, ILogger<GetCheckInsHandler> logger ) {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResult<IEnumerable<CheckInListItemDto>>> Handle( GetCheckInsMessage request, CancellationToken cancellationToken ) {
            _logger.LogInformation( $"{nameof( GetCheckInsHandler )} - Handler - Start" );

            _logger.LogInformation( "generating specification from request" );
            var specification = new CheckInSpecification( request );

            _logger.LogInformation( "retrieving result from manager" );
            var result = await _manager.GetAsync( specification );

            _logger.LogInformation( $"mapping result to dto" );
            var data = _mapper.Map<IEnumerable<CheckInEntity>, IEnumerable<CheckInListItemDto>>( result );

            _logger.LogInformation( $"creating {nameof( QueryResult )} from mapped data" );
            var queryResult = QueryResult<IEnumerable<CheckInListItemDto>>.New( data );

            _logger.LogInformation( $"{nameof( GetCheckInsHandler )} - Handler - End" );

            return queryResult;
        }
    }
}
