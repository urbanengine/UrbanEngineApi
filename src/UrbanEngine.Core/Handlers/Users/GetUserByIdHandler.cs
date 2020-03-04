using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.Users;
using UrbanEngine.Core.Messages.Users;
using UrbanEngine.Core.Models.Users;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Users
{
	public class GetUserByIdHandler : IRequestHandler<GetUserByIdMessage, QueryResult<UserDetailDto>>
	{
		private readonly IUserManager _manager;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public GetUserByIdHandler(IUserManager manager, IMapper mapper, ILogger<GetUsersHandler> logger)
		{
			_manager = manager;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<QueryResult<UserDetailDto>> Handle(GetUserByIdMessage request, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof(GetUserByIdHandler)} - Handler - Start");

			_logger.LogInformation("retrieving result from manager");
			var result = await _manager.GetByIdAsync(request.Id);

			_logger.LogInformation($"mapping result to dto");
			var data = _mapper.Map<UserEntity, UserDetailDto>(result);

			_logger.LogInformation($"creating {nameof(QueryResult)} from mapped data");
			var queryResult = QueryResult<UserDetailDto>.New(data);

			_logger.LogInformation($"{nameof(GetUserByIdHandler)} - Handler - End");

			return queryResult;
		}
	}
}
