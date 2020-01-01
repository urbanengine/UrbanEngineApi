using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Managers.Venues;
using UrbanEngine.Core.Messages.Venues;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Venues
{
    public class DeleteVenueHandler : IRequestHandler<DeleteVenueMessage, CommandResult>
    {
        private readonly IEventVenueManager _manager;
        private readonly ILogger _logger;

        public DeleteVenueHandler(IEventVenueManager manager, ILogger<GetVenuesHandler> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<CommandResult> Handle(DeleteVenueMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(DeleteVenueHandler)} - Handler - Start");
            
            _logger.LogInformation("deleting entity");
            var result = await _manager.DeleteAsync(request.Id, softDelete: true);
            
            _logger.LogInformation($"creating {nameof(CommandResult)} from result");
            var commandResult = result ?
                new CommandResult($"event venue {request.Id} marked as deleted", 200, true) :
                new CommandResult($"unable to mark {request.Id} as deleted", 500, false);

            _logger.LogInformation($"{nameof(DeleteVenueHandler)} - Handler - End");

            return commandResult;
        }
    }
}
