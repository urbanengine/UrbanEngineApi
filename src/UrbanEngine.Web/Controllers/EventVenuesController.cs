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
        /// retrieves a list of event venues based on specified filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetVenuesAsync([FromQuery]GetEventVenuesMessage filter)
        {
            var result = await _mediator.Send(filter);
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
            var message = new SaveEventVenueMessage
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
            var message = new SaveEventVenueMessage
            {
                Detail = data,
                Action = ActionType.Update
            };
            var result = await _mediator.Send(message);
            return Ok(result);
        }
    }
}