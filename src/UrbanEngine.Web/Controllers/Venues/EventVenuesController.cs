using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Messages.Venues;

namespace UrbanEngine.Web.Controllers.Venues
{
	/// <summary>
	/// performs any queries relating to Event Venues
	/// </summary>
	[Route("odata/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
	public class EventVenuesController : ODataController
	{
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public EventVenuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// get event venue for specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
		[EnableQuery]
        public async Task<SingleResult<EventVenueEntity>> GetVenueByIdAsync(long id)
        {
            var result = await _mediator.Send(new QueryVenuesMessage { EventVenueId = id });
            return new SingleResult<EventVenueEntity>(result);
        }

        /// <summary>
        /// retrieves a list of event venues based on specified filter
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
		[EnableQuery]
        public async Task<IQueryable<EventVenueEntity>> GetVenuesAsync([FromQuery]QueryVenuesMessage message)
        {
            var result = await _mediator.Send(message);
            return result;
        }
	}
}
