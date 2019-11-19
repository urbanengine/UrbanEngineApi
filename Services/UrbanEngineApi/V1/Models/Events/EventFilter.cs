using System;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Events;
using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Services.UrbanEngineApi.V1.Models.Events
{
    /// <summary>
    /// filters and paginates event search results
    /// </summary>
    public class EventFilter : PagingParameters, IEventFilter
    {
        /// <summary>
        /// start date of the event
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// end date of the event
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// event type
        /// </summary>
        public EventType EventType { get; set; }

        /// <summary>
        /// venue id
        /// </summary>
        public long? VenueId { get; set; }

        /// <summary>
        /// organizer id
        /// </summary>
        public string OrganizerId { get; set; }

        /// <summary>
        /// whether to include deleted events in the results
        /// </summary>
        public bool? IsDeleted { get; set; }
    }
}
