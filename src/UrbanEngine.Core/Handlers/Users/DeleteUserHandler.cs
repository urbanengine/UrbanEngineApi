using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Managers.Users;
using UrbanEngine.Core.Messages.Users;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Users
{
	public class DeleteUserHandler : IRequestHandler<DeleteUserMessage, CommandResult>
	{
		private readonly IUserManager _manager;
		private readonly ILogger _logger;

		public DeleteUserHandler(IUserManager manager, ILogger<GetUsersHandler> logger)
		{
			_manager = manager;
			_logger = logger;
		}

		public async Task<CommandResult> Handle(DeleteUserMessage request, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof(DeleteUserHandler)} - Handler - Start");

			_logger.LogInformation("deleting entity");
			var result = await _manager.DeleteAsync(request.Id, softDelete: true);

			_logger.LogInformation($"creating {nameof(CommandResult)} from result");
			var commandResult = result ?
				new CommandResult($"User {request.Id} marked as deleted", 200, true) :
				new CommandResult($"unable to mark User {request.Id} as deleted", 500, false);

			_logger.LogInformation($"{nameof(DeleteUserHandler)} - Handler - End");

			return commandResult;
		}
	}
}
