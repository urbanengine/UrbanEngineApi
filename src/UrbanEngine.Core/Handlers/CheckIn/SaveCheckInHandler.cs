using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Managers.CheckIn;
using UrbanEngine.Core.Messages.CheckIn;
using UrbanEngine.Core.Models.CheckIn;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.CheckIn {
    public class SaveCheckInHandler : IRequestHandler<SaveCheckInMessage, CommandResultWithData> {
        private readonly ICheckInManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SaveCheckInHandler( ICheckInManager manager, IMapper mapper, ILogger<GetCheckInsHandler> logger ) {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CommandResultWithData> Handle( SaveCheckInMessage request, CancellationToken cancellationToken ) {
            _logger.LogInformation( $"{nameof( SaveCheckInHandler )} - Handler - Start" );

            _logger.LogInformation( "mapping dto to entity" );
            var entity = _mapper.Map<CheckInDetailDto, CheckInEntity>( request.Detail );

            CheckInEntity savedEntity;
            if( request.Action == ActionType.Update ) {
                _logger.LogInformation( "updating entity", entity.Id );
                savedEntity = await _manager.UpdateAsync( entity );
            } else if( request.Action == ActionType.Create ) {
                _logger.LogInformation( "inserting entity" );
                savedEntity = await _manager.CreateAsync( entity );
            } else {
                throw new NotSupportedException( $"{request.Action.Name} is not supported for this request" );
            }

            _logger.LogInformation( "mapping result to dto" );
            var data = _mapper.Map<CheckInEntity, CheckInDetailDto>( savedEntity );

            _logger.LogInformation( "creating command result" );
            var result = data?.Id > 0 ?
                new CommandResultWithData( data, $"CheckIn {request.Action.Name}", 200, true ) :
                new CommandResultWithData( null, message: $"failed to {request.Action.Name} CheckIn", statusCode: 0, success: false );

            _logger.LogInformation( $"{nameof( SaveCheckInHandler )} - Handler - Start" );

            return result;
        }
    }
}
