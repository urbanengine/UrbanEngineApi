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

        [HttpGet("{eventVenueId}")]
        public async Task<IActionResult> GetVenueAsync(long eventVenueId)
        {
            if (eventVenueId < 0)
                throw new ArgumentException($"{nameof(eventVenueId)} must be greater than 0");

            var result = await _service.GetVenueAsync<EventVenueDetailModel>(eventVenueId); 
            return Ok(result);
        }

        /// <summary>
        /// retrieves a list of event venues based on specified filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetVenuesAsync([FromQuery]EventVenueFilter filter)
        {
            var result = await _service.GetVenuesAsync<EventVenueListItemModel>(filter);
            return Ok(result);
        }

        /// <summary>
        /// create a new event venue
        /// </summary>
        /// <param name="eventVenueDetail"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateVenueAsync([FromBody]EventVenueDetailModel eventVenueDetail)
        {
            if (!(eventVenueDetail?.IsValid ?? false))
                throw new ArgumentException(eventVenueDetail?.GetErrorMessage() ?? $"{nameof(eventVenueDetail)} was not found, cannot be null");

            var result = await _service.CreateVenueAsync(eventVenueDetail);
            return Ok(result);
        }

        /// <summary>
        /// updates an existing event venue
        /// </summary>
        /// <param name="eventVenueDetail"></param>
        /// <returns></returns>
        [HttpPut("{eventVenueId}")]
        public async Task<IActionResult> UpdateVenueAsync(long eventVenueId, [FromBody]EventVenueDetailModel eventVenueDetail)
        {
            if (eventVenueId < 0)
                throw new ArgumentException($"{nameof(eventVenueId)} must be greater than 0");
            if (!(eventVenueDetail?.IsValid ?? false))
                throw new ArgumentException(eventVenueDetail?.GetErrorMessage() ?? $"{nameof(eventVenueDetail)} was not found, cannot be null");

            var result = await _service.UpdateVenueAsync(eventVenueId, eventVenueDetail);
            return Ok(result);
        }

        /// <summary>
        /// performs a soft delete of an event venue marking it as deleted
        /// </summary>
        /// <param name="eventVenueId"></param>
        /// <returns></returns>
        [HttpDelete("{eventVenueId}")]
        public async Task<IActionResult> DeleteVenueAsync(long eventVenueId)
        {
            if (eventVenueId < 0)
                throw new ArgumentException($"{nameof(eventVenueId)} must be greater than 0");

            var result = await _service.DeleteVenueAsync(eventVenueId);
            return Ok(result);
        }
    }
}