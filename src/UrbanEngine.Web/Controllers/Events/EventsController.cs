using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Messages.Events;

namespace UrbanEngine.Web.Controllers.Events
{
	/// <summary>
	/// performs any queries relating to Event Venues
	/// </summary>
	[Route("odata/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
	public class EventsController : ODataController
	{
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }
		
        /// <summary>
        /// get event for specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
		[EnableQuery]
        public async Task<SingleResult<EventEntity>> GetEventByIdAsync(long id)
        {
            var result = await _mediator.Send(new QueryEventsMessage { EventId = id });
            return new SingleResult<EventEntity>(result);
        }

        /// <summary>
        /// retrieves a list of events based on specified filter
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
		[EnableQuery]
        public async Task<IQueryable<EventEntity>> GetEventsAsync([FromQuery]QueryEventsMessage message)
        {
            var result = await _mediator.Send(message);
            return result;
        }

	}
}
