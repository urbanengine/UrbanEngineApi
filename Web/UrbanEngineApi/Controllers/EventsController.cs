using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrbanEngine.Core.Application.Schedules;

namespace UrbanEngineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public EventsController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
        
        [Route("")]
        [HttpPost]
        public async Task<IActionResult> ScheduleEventAsync([FromBody]EventDetailModel eventDetail)
        {
            var result = await _scheduleService.ScheduleEventAsync(eventDetail);
            return Ok(result);
        }

        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> ListScheduledEvents([FromQuery]EventsFilterModel filter)
        {
            var result = await _scheduleService.ListScheduledEventsAsync(filter);
            return Ok(result);
        }
         
        [Route("{eventId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEventAsync(long eventId, string reason)
        {
            var result = await _scheduleService.DeleteEventAsync(eventId, reason);
            return Ok(result);
        }

        [Route("{eventId}/sessions")]
        [HttpPost]
        public async Task<IActionResult> ScheduleEventSessionAsync(long eventId, [FromBody]SessionDetailModel sessionDetail)
        {
            var result = await _scheduleService.ScheduleEventSessionAsync(eventId, sessionDetail);
            return Ok(result);
        }

        [Route("{eventId}/sessions/{sessionId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEventSessionAsync(long eventId, long sessionId, string reason)
        {
            var result = await _scheduleService.DeleteEventSessionAsync(eventId, sessionId, reason);
            return Ok(result);
        }

        [Route("{eventId}/sessions/list")]
        [HttpGet]
        public async Task<IActionResult> ListEventSessionsAsync(long eventId)
        {
            var result = await _scheduleService.ListEventSessionsAsync(eventId);
            return Ok(result);
        }
    }
}