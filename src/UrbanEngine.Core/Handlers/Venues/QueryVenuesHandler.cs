using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.Venues;
using UrbanEngine.Core.Messages.Venues;
using UrbanEngine.Core.Specifications.Venues;

namespace UrbanEngine.Core.Handlers.Venues
{
	public sealed class QueryVenuesHandler : IRequestHandler<QueryVenuesMessage, IQueryable<EventVenueEntity>>
	{
		private readonly IEventVenueManager _manager;
		private readonly ILogger _logger;

		public QueryVenuesHandler(IEventVenueManager manager, ILogger<QueryVenuesHandler> logger)
		{
			_manager = manager;
			_logger = logger;
		}

		public Task<IQueryable<EventVenueEntity>> Handle(QueryVenuesMessage request, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof( QueryVenuesHandler )} - Handler - Start");
			
            _logger.LogInformation("generating specification from request");
            var specification = new EventVenueSpecification(request);

			_logger.LogInformation("generating IQueryable from specification");
			var queryable = _manager.Query(specification);
			
			_logger.LogInformation($"{nameof( QueryVenuesHandler )} - Handler - End");

			return Task.FromResult(queryable);
		}
	}
}
