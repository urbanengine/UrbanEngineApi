using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Managers.Events;
using UrbanEngine.Core.Messages.Events;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Events
{
    public class DeleteEventHandler : IRequestHandler<DeleteEventMessage, CommandResult>
    {
        private readonly IEventManager _manager;
        private readonly ILogger _logger;

        public DeleteEventHandler(IEventManager manager, ILogger<GetEventsHandler> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<CommandResult> Handle(DeleteEventMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(DeleteEventHandler)} - Handler - Start");
            
            _logger.LogInformation("deleting entity");
            var result = await _manager.DeleteAsync(request.Id, softDelete: true);
            
            _logger.LogInformation($"creating {nameof(CommandResult)} from result");
            var commandResult = result ?
                new CommandResult($"event {request.Id} marked as deleted", 200, true) :
                new CommandResult($"unable to mark event {request.Id} as deleted", 500, false);

            _logger.LogInformation($"{nameof(DeleteEventHandler)} - Handler - End");

            return commandResult;
        }
    }
}
