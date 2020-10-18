using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Messages.CheckIn;
using UrbanEngine.Core.Models.CheckIn;

namespace UrbanEngine.Web.Controllers.CheckIn
{
	/// <summary>
	/// performs any commands (Create, Update, Delete) relating to a check in
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[ApiVersion("1.0")]
    public class CheckInCommandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public CheckInCommandsController(IMediator mediator) {
            _mediator = mediator;
        }

        /// <summary>
        /// create a new CheckIn
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCheckInAsync([FromQuery]CheckInDetailDto data) {
            var message = new SaveCheckInMessage {
                Detail = data,
                Action = ActionType.Create
            };
            var result = await _mediator.Send(message);
            return Ok(result);
        }

        /// <summary>
        /// update existing CheckIn
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCheckInAsync([FromQuery]CheckInDetailDto data) {
            var message = new SaveCheckInMessage {
                Detail = data,
                Action = ActionType.Update
            };
            var result = await _mediator.Send(message);
            return Ok(result);
        }

        /// <summary>
        /// delete a CheckIn
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCheckInAsync([FromQuery]DeleteCheckInMessage message) {
            var result = await _mediator.Send(message);
            return Ok(result);
        }
    }
}