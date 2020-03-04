using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Managers.Users;
using UrbanEngine.Core.Messages.Users;
using UrbanEngine.Core.Models.Users;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Users
{
	public class SaveUserHandler : IRequestHandler<SaveUserMessage, CommandResultWithData>
	{
		private readonly IUserManager _manager;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public SaveUserHandler(IUserManager manager, IMapper mapper, ILogger<GetUsersHandler> logger)
		{
			_manager = manager;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<CommandResultWithData> Handle(SaveUserMessage request, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof(SaveUserHandler)} - Handler - Start");

			_logger.LogInformation("mapping dto to entity");
			var entity = _mapper.Map<UserDetailDto, UserEntity>(request.Detail);

			UserEntity savedEntity;
			if(request.Action == ActionType.Update)
			{
				_logger.LogInformation("updating entity", entity.Id);
				savedEntity = await _manager.UpdateAsync(entity);
			}
			else if(request.Action == ActionType.Create)
			{
				_logger.LogInformation("inserting entity");
				savedEntity = await _manager.CreateAsync(entity);
			}
			else
			{
				throw new NotSupportedException($"{request.Action.Name} is not supported for this request");
			}

			_logger.LogInformation("mapping result to dto");
			var data = _mapper.Map<UserEntity, UserDetailDto>(savedEntity);

			_logger.LogInformation("creating command result");
			var result = data?.Id > 0 ?
				new CommandResultWithData(data, $"User {request.Action.Name}", 200, true) :
				new CommandResultWithData(null, message: $"failed to {request.Action.Name} User", statusCode: 0, success: false);

			_logger.LogInformation($"{nameof(SaveUserHandler)} - Handler - Start");

			return result;
		}
	}
}
