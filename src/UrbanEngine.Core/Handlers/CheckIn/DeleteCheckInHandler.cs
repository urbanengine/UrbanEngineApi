using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Managers.CheckIn;
using UrbanEngine.Core.Messages.CheckIn;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.CheckIn
{
    public class DeleteCheckInHandler : IRequestHandler<DeleteCheckInMessage, CommandResult>
    {
        private readonly ICheckInManager _manager;
        private readonly ILogger _logger;

        public DeleteCheckInHandler( ICheckInManager manager, ILogger<GetCheckInsHandler> logger ) {
            _manager = manager;
            _logger = logger;
        }

        public async Task<CommandResult> Handle( DeleteCheckInMessage request, CancellationToken cancellationToken ) {
            _logger.LogInformation( $"{nameof( DeleteCheckInHandler )} - Handler - Start" );

            _logger.LogInformation( "deleting entity" );
            var result = await _manager.DeleteAsync( request.Id, softDelete: true );

            _logger.LogInformation( $"creating {nameof( CommandResult )} from result" );
            var commandResult = result ?
                new CommandResult( $"CheckIn {request.Id} marked as deleted", 200, true ) :
                new CommandResult( $"unable to mark CheckIn {request.Id} as deleted", 500, false );

            _logger.LogInformation( $"{nameof( DeleteCheckInHandler )} - Handler - End" );

            return commandResult;
        }
    }
}
