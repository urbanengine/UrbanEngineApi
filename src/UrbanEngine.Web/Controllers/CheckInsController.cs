using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Messages.CheckIn;
using UrbanEngine.Core.Models.CheckIn;

namespace UrbanEngine.Web.Controllers
{
    /// <summary>
    /// manage and query information about CheckIns
    /// </summary>
    [Route( "api/[controller]" )]
    [ApiController]
    public class CheckInsController: ControllerBase {
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public CheckInsController( IMediator mediator ) {
            _mediator = mediator;
        }

        /// <summary>
        /// get CheckIn for specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetCheckInByIdAsync( long id ) {
            var result = await _mediator.Send( new GetCheckInByIdMessage { Id = id } );
            return Ok( result );
        }

        /// <summary>
        /// retrieves a list of CheckIns based on specified filter
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCheckInsAsync( [FromQuery]GetCheckInsMessage message ) {
            var result = await _mediator.Send( message );
            return Ok( result );
        }

        /// <summary>
        /// create a new CheckIn
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCheckInAsync( [FromQuery]CheckInDetailDto data ) {
            var message = new SaveCheckInMessage {
                Detail = data,
                Action = ActionType.Create
            };
            var result = await _mediator.Send( message );
            return Ok( result );
        }

        /// <summary>
        /// create a new CheckIn
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCheckInAsync( [FromQuery]CheckInDetailDto data ) {
            var message = new SaveCheckInMessage {
                Detail = data,
                Action = ActionType.Update
            };
            var result = await _mediator.Send( message );
            return Ok( result );
        }

        /// <summary>
        /// delete a CheckIn
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCheckInAsync( [FromQuery]DeleteCheckInMessage message ) {
            var result = await _mediator.Send( message );
            return Ok( result );
        }
    }
}
