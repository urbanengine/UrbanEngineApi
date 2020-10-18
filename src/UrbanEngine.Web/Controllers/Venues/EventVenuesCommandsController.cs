using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Messages.Venues;
using UrbanEngine.Core.Models.Venues;

namespace UrbanEngine.Web.Controllers.Venues
{
    /// <summary>
    /// manage information about event venues
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[ApiVersion("1.0")]
	public class EventVenuesCommandsController : ControllerBase
	{
		private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public EventVenuesCommandsController(IMediator mediator)
        {
            _mediator = mediator;
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
