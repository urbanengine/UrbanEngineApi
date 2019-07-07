using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Schedules;
using UrbanEngine.Services.UrbanEngineApi.Schedules;
using UrbanEngine.Services.UrbanEngineApi.V1.Models.Schedules;

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
    }
}