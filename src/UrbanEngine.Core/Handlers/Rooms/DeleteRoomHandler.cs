using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UrbanEngine.Core.Managers.Rooms;
using UrbanEngine.Core.Messages.Rooms;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Rooms
{
	public class DeleteRoomHandler : IRequestHandler<DeleteRoomMessage, CommandResult>
    {
        private readonly IRoomManager _manager;
        private readonly ILogger _logger;

        public DeleteRoomHandler(IRoomManager manager, ILogger<GetRoomsHandler> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<CommandResult> Handle(DeleteRoomMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(DeleteRoomHandler)} - Handler - Start");
            
            _logger.LogInformation("deleting entity");
            var result = await _manager.DeleteAsync(request.Id, softDelete: true);
            
            _logger.LogInformation($"creating {nameof(CommandResult)} from result");
            var commandResult = result ?
                new CommandResult($"Room {request.Id} marked as deleted", 200, true) :
                new CommandResult($"unable to mark Room {request.Id} as deleted", 500, false);

            _logger.LogInformation($"{nameof(DeleteRoomHandler)} - Handler - End");

            return commandResult;
        }
    }
}
