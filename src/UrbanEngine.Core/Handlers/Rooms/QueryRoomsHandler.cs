using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.Rooms;
using UrbanEngine.Core.Messages.Rooms;
using UrbanEngine.Core.Specifications.Rooms;

namespace UrbanEngine.Core.Handlers.Rooms
{
	public sealed class QueryRoomsHandler : IRequestHandler<QueryRoomMessage, IQueryable<RoomEntity>>
	{
		private readonly IRoomManager _manager;
		private readonly ILogger _logger;

		public QueryRoomsHandler(IRoomManager manager, ILogger<QueryRoomsHandler> logger)
		{
			_manager = manager;
			_logger = logger;
		}

		public Task<IQueryable<RoomEntity>> Handle(QueryRoomMessage request, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof( QueryRoomsHandler )} - Handler - Start");
			
            _logger.LogInformation("generating specification from request");
            var specification = new RoomSpecification(request);

			_logger.LogInformation("generating IQueryable from specification");
			var queryable = _manager.Query(specification);
			
			_logger.LogInformation($"{nameof( QueryRoomsHandler )} - Handler - End");

			return Task.FromResult(queryable);
		}
	}
}
