using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrbanEngine.Core.Messages.Venues;

namespace UrbanEngine.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventVenuesController : ControllerBase
    {
        private readonly IMediator _mediator;

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

    }
}