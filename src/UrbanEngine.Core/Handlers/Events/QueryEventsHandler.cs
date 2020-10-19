using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.Events;
using UrbanEngine.Core.Messages.Events;
using UrbanEngine.Core.Specifications.Events;

namespace UrbanEngine.Core.Handlers.Events
{
	public sealed class QueryEventsHandler : IRequestHandler<QueryEventsMessage, IQueryable<EventEntity>>
	{
		private readonly IEventManager _manager;
		private readonly ILogger _logger;

		public QueryEventsHandler(IEventManager manager, ILogger<QueryEventsHandler> logger)
		{
			_manager = manager;
			_logger = logger;
		}

		public Task<IQueryable<EventEntity>> Handle(QueryEventsMessage request, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof( QueryEventsHandler )} - Handler - Start");
			
            _logger.LogInformation("generating specification from request");
            var specification = new EventSpecification(request);

			_logger.LogInformation("generating IQueryable from specification");
			var queryable = _manager.Query(specification);
			
			_logger.LogInformation($"{nameof( QueryEventsHandler )} - Handler - End");

			return Task.FromResult(queryable);
		}
	}
}
