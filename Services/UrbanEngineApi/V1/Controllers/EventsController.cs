using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Application.Events;
using UrbanEngine.Services.UrbanEngineApi.V1.Models.Events;

namespace UrbanEngine.Services.UrbanEngineApi.V1.Controllers
{
    /// <summary>
    /// manage and query event information
    /// </summary>
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
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

/*
        private readonly IScheduleService _scheduleService;
        private readonly ILogger _logger;

        /// <summary>
        /// create a new instanace of controller
        /// </summary>
        /// <param name="scheduleService"></param>
        /// <param name="logger"></param>
        public EventsController(IScheduleService scheduleService, ILogger<EventsController> logger)
        {
            _scheduleService = scheduleService;
            _logger = logger;
        }
        
        /// <summary>
        /// types of events
        /// </summary>
        /// <returns></returns>
        [HttpGet("types")]
        public IActionResult ListEventTypes()
        {
            return Ok(EventType.List);
        }

        /// <summary>
        /// schedule an event
        /// </summary>
        /// <param name="eventDetail"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ScheduleEventAsync([FromBody]EventDetailModel eventDetail)
        {
            if (!(eventDetail?.IsValid ?? false))
                throw new ArgumentException(eventDetail?.GetErrorMessage() ?? $"{nameof(eventDetail)} was not found, cannot be null");

            var result = await _scheduleService.ScheduleEventAsync(EventDetailModel.ToDomainEntity(eventDetail));
            return Ok(result);
        }

        /// <summary>
        /// list events for a specified filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> ListScheduledEventsAsync([FromQuery]ScheduleFilter filter)
        {
            var result = await _scheduleService.ListScheduledEventsAsync(filter);
            return Ok(result); 
        }
        
        /// <summary>
        /// get details about a specific event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventDetail(long eventId)
        {
            var result = await _scheduleService.GetEventDetail(eventId);
            var model = EventDetailModel.FromDomainEntity(result); 
            return Ok(model); 
        }

        /// <summary>
        /// delete a specified event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEventAsync(long eventId, string reason)
        {
            if (eventId <= 0)
                throw new ArgumentException($"{nameof(eventId)} must be greater than 0");  

            var result = await _scheduleService.DeleteEventAsync(eventId, reason);
            return Ok(result);
        }
        
        /// <summary>
        /// add sessions for an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="sessionDetail"></param>
        /// <returns></returns>
        [HttpPost("{eventId}/sessions")]
        public async Task<IActionResult> ScheduleEventSessionAsync(long eventId, [FromBody]SessionDetailModel sessionDetail)
        {
            if (eventId <= 0)
                throw new ArgumentException($"{nameof(eventId)} must be greater than 0");

            if (!(sessionDetail?.IsValid ?? false))
                throw new ArgumentException(sessionDetail?.GetErrorMessage() ?? $"{nameof(sessionDetail)} was not found, cannot be null");
             
            var result = await _scheduleService.ScheduleEventSessionAsync(eventId, sessionDetail?.AsSessionToSchedule());
            return Ok(result);
        }
        
        /// <summary>
        /// delete an event session
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="sessionId"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpDelete("{eventId}/sessions/{sessionId}")]
        public async Task<IActionResult> DeleteEventSessionAsync(long eventId, long sessionId, string reason)
        {
            if (eventId <= 0)
                throw new ArgumentException($"{nameof(eventId)} must be greater than 0");
             
            var result = await _scheduleService.DeleteEventSessionAsync(eventId, sessionId, reason);
            return Ok(result);
        }
        
        /// <summary>
        /// list event sessions for a specified event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("{eventId}/sessions/list")]
        public async Task<IActionResult> ListEventSessionsAsync(long eventId)
        {
            if (eventId <= 0)
                throw new ArgumentException($"{nameof(eventId)} must be greater than 0");

            var result = await _scheduleService.ListEventSessionsAsync(eventId);
            return Ok(result);
        } 
 */