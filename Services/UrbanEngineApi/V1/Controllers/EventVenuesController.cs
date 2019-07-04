using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Venues;

namespace UrbanEngineApi.V1.Controllers
{
    /// <summary>
    /// manage and query information about event venues
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventVenuesController : ControllerBase
    {
        private readonly IEventVenueService _service;
        private readonly ILogger<EventVenuesController> _logger;

        /// <summary>
        /// create a new instance of controller
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public EventVenuesController(IEventVenueService service, ILogger<EventVenuesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// retrieves a list of event venues based on specified filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetVenues([FromQuery]EventVenueFilter filter)
        {
            var result = await _service.GetVenues(filter);
            return Ok(result);
        }
    }
}