using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.CheckIn;
using UrbanEngine.Core.Messages.CheckIn;
using UrbanEngine.Core.Specifications.CheckIns;

namespace UrbanEngine.Core.Handlers.CheckIn
{
	public sealed class QueryCheckInsHandler : IRequestHandler<QueryCheckInsMessage, IQueryable<CheckInEntity>>
	{
		private readonly ICheckInManager _manager;
        private readonly ILogger _logger;

        public QueryCheckInsHandler( ICheckInManager manager, ILogger<QueryCheckInsHandler> logger ) {
            _manager = manager;
            _logger = logger;
        }

		public Task<IQueryable<CheckInEntity>> Handle(QueryCheckInsMessage request, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof( QueryCheckInsHandler )} - Handler - Start");
			
            _logger.LogInformation("generating specification from request");
            var specification = new CheckInSpecification(request);

			_logger.LogInformation("generating IQueryable from specification");
			var queryable = _manager.Query(specification);
			
			_logger.LogInformation($"{nameof( QueryCheckInsHandler )} - Handler - End");

			return Task.FromResult(queryable);
		}
	}
}
