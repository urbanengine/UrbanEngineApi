using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.Users;
using UrbanEngine.Core.Messages.Users;
using UrbanEngine.Core.Models.Users;
using UrbanEngine.Core.Specifications.Users;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Handlers.Users
{
	public class GetUsersHandler : IRequestHandler<GetUsersMessage, QueryResult<IEnumerable<UserListItemDto>>>
	{
		private readonly IUserManager _manager;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public GetUsersHandler(IUserManager manager, IMapper mapper, ILogger<GetUsersHandler> logger)
		{
			_manager = manager;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<QueryResult<IEnumerable<UserListItemDto>>> Handle(GetUsersMessage request, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof(GetUsersHandler)} - Handler - Start");

			_logger.LogInformation("generating specification from request");
			var specification = new UserSpecification(request);

			_logger.LogInformation("retrieving result from manager");
			var result = await _manager.GetAsync(specification);

			_logger.LogInformation($"mapping result to dto");
			var data = _mapper.Map<IEnumerable<UserEntity>, IEnumerable<UserListItemDto>>(result);

			_logger.LogInformation($"creating {nameof(QueryResult)} from mapped data");
			var queryResult = QueryResult<IEnumerable<UserListItemDto>>.New(data);

			_logger.LogInformation($"{nameof(GetUsersHandler)} - Handler - End");

			return queryResult;
		}
	}
}
