using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Messages.Rooms;
using UrbanEngine.Core.Models.Rooms;

namespace UrbanEngine.Web.Controllers.Rooms
{
    /// <summary>
    /// manage and query information about rooms
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[ApiVersion("1.0")]
	public class RoomsCommandsController : ControllerBase
	{
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public RoomsCommandsController(IMediator mediator)
        {
            _mediator = mediator;
        }
		
        /// <summary>
        /// create a new Room
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRoomAsync([FromQuery]RoomDetailDto data)
        {
            var message = new SaveRoomMessage
            {
                Detail = data,
                Action = ActionType.Create
            };
            var result = await _mediator.Send(message);
            return Ok(result);
        }

        /// <summary>
        /// create a new Room
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRoomAsync([FromQuery]RoomDetailDto data)
        {
            var message = new SaveRoomMessage
            {
                Detail = data,
                Action = ActionType.Update
            };
            var result = await _mediator.Send(message);
            return Ok(result);
        }

        /// <summary>
        /// delete a Room
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteRoomAsync([FromQuery]DeleteRoomMessage message)
        {
            var result = await _mediator.Send(message);
            return Ok(result);
        }
	}
}
