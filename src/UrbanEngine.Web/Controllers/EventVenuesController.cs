using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Messages.Venues;
using UrbanEngine.Core.Models.Venues;

namespace UrbanEngine.Web.Controllers
{
    /// <summary>
    /// manage and query information about event venues
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[ApiVersion("1.0")]
    public class EventVenuesController : ControllerBase
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
        public async Task<IActionResult> GetVenueByIdAsync(long id)
        {
            var result = await _mediator.Send(new GetVenueByIdMessage { Id = id });
            return Ok(result);
        }

        /// <summary>
        /// retrieves a list of event venues based on specified filter
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetVenuesAsync([FromQuery]GetVenuesMessage message)
        {
            var result = await _mediator.Send(message);
            return Ok(result);
        }

        /// <summary>
        /// create a new event venue
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateVenueAsync([FromQuery]EventVenueDetailDto data)
        {
            var message = new SaveVenueMessage
            {
                Detail = data,
                Action = ActionType.Create
            };
            var result = await _mediator.Send(message);
            return Ok(result);
        }

        /// <summary>
        /// create a new event venue
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateVenueAsync([FromQuery]EventVenueDetailDto data)
        {
            var message = new SaveVenueMessage
            {
                Detail = data,
                Action = ActionType.Update
            };
            var result = await _mediator.Send(message);
            return Ok(result);
        }

        /// <summary>
        /// delete a venue
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteVenueAsync([FromQuery]DeleteVenueMessage message)
        {
            var result = await _mediator.Send(message);
            return Ok(result);
        }
    }
}