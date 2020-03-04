using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Messages.Events;
using UrbanEngine.Core.Models.Events;

namespace UrbanEngine.Web.Controllers
{
    /// <summary>
    /// manage and query information about events
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
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
        public async Task<IActionResult> GetEventByIdAsync(long id)
        {
            var result = await _mediator.Send(new GetEventByIdMessage { Id = id });
            return Ok(result);
        }

        /// <summary>
        /// retrieves a list of events based on specified filter
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEventsAsync([FromQuery]GetEventsMessage message)
        {
            var result = await _mediator.Send(message);
            return Ok(result);
        }

        /// <summary>
        /// create a new event
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEventAsync([FromQuery]EventDetailDto data)
        {
            var message = new SaveEventMessage
            {
                Detail = data,
                Action = ActionType.Create
            };
            var result = await _mediator.Send(message);
            return Ok(result);
        }

        /// <summary>
        /// update an existing event
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateEventAsync([FromQuery]EventDetailDto data)
        {
            var message = new SaveEventMessage
            {
                Detail = data,
                Action = ActionType.Update
            };
            var result = await _mediator.Send(message);
            return Ok(result);
        }

        /// <summary>
        /// delete a Event
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteEventAsync([FromQuery]DeleteEventMessage message)
        {
            var result = await _mediator.Send(message);
            return Ok(result);
        }
    }
}