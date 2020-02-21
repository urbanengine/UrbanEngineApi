using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.CheckIn;
using UrbanEngine.Core.Messages.CheckIn;
using UrbanEngine.Core.Models.CheckIn;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.CheckIn {
    public class GetCheckInByIdHandler : IRequestHandler<GetCheckInByIdMessage, QueryResult<CheckInDetailDto>> {
        private readonly ICheckInManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetCheckInByIdHandler( ICheckInManager manager, IMapper mapper, ILogger<GetCheckInsHandler> logger )
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QueryResult<CheckInDetailDto>> Handle( GetCheckInByIdMessage request, CancellationToken cancellationToken )
        {
            _logger.LogInformation( $"{nameof( GetCheckInByIdHandler )} - Handler - Start" );

            _logger.LogInformation( "retrieving result from manager" );
            var result = await _manager.GetByIdAsync( request.Id );

            _logger.LogInformation( $"mapping result to dto" );
            var data = _mapper.Map<CheckInEntity, CheckInDetailDto>( result );

            _logger.LogInformation( $"creating {nameof( QueryResult )} from mapped data" );
            var queryResult = QueryResult<CheckInDetailDto>.New( data );

            _logger.LogInformation( $"{nameof( GetCheckInByIdHandler )} - Handler - End" );

            return queryResult;
        }
    }
}
