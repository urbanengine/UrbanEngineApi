using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Application.Events;
using UrbanEngine.Services.UrbanEngineApi.Models.Events;

namespace UrbanEngine.Services.UrbanEngineApi.Controllers
{
    /// <summary>
    /// manage and query event information
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _service;
        private readonly ILogger<EventsController> _logger;

        public EventsController(IEventService service, ILogger<EventsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{EventId}")]
        public async Task<IActionResult> GetEventsAsync(long EventId)
        {
            if (EventId < 0)
                throw new ArgumentException($"{nameof(EventId)} must be greater than 0");

            var result = await _service.GetEventAsync<EventDetailModel>(EventId); 
            return Ok(result);
        }

        /// <summary>
        /// retrieves a list of event Events based on specified filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEventsAsync([FromQuery]EventFilter filter)
        {
            var result = await _service.GetEventsAsync<EventListItemModel>(filter);
            return Ok(result);
        }

        /// <summary>
        /// create a new event Events
        /// </summary>
        /// <param name="EventDetail"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEventsAsync([FromBody]EventDetailModel EventDetail)
        {
            if (!(EventDetail?.IsValid ?? false))
                throw new ArgumentException(EventDetail?.GetErrorMessage() ?? $"{nameof(EventDetail)} was not found, cannot be null");

            var result = await _service.CreateEventAsync(EventDetail);
            return Ok(result);
        }

        /// <summary>
        /// updates an existing event Events
        /// </summary>
        /// <param name="EventId"></param>
        /// <param name="EventDetail"></param>
        /// <returns></returns>
        [HttpPut("{EventId}")]
        public async Task<IActionResult> UpdateEventsAsync(long EventId, [FromBody]EventDetailModel EventDetail)
        {
            if (EventId < 0)
                throw new ArgumentException($"{nameof(EventId)} must be greater than 0");
            if (!(EventDetail?.IsValid ?? false))
                throw new ArgumentException(EventDetail?.GetErrorMessage() ?? $"{nameof(EventDetail)} was not found, cannot be null");

            var result = await _service.UpdateEventAsync(EventId, EventDetail);
            return Ok(result);
        }

        /// <summary>
        /// performs a soft delete of an event Events marking it as deleted
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        [HttpDelete("{EventId}")]
        public async Task<IActionResult> DeleteEventsAsync(long EventId)
        {
            if (EventId < 0)
                throw new ArgumentException($"{nameof(EventId)} must be greater than 0");

            var result = await _service.DeleteEventAsync(EventId);
            return Ok(result);
        }
    }
}