using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Venues;
using UrbanEngine.Services.UrbanEngineApi.V1.Models.Venues;

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
            var result = await _service.GetVenues<EventVenueModel>(filter);
            return Ok(result);
        }

        /// <summary>
        /// create a new event venue
        /// </summary>
        /// <param name="eventVenue"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateVenue([FromBody]EventVenueModel eventVenue)
        {
            if (!(eventVenue?.IsValid ?? false))
                throw new ArgumentException(eventVenue?.GetErrorMessage() ?? $"{nameof(eventVenue)} was not found, cannot be null");

            var result = await _service.CreateVenue(eventVenue);
            return Ok(result);
        }
    }
}