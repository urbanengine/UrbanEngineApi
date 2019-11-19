using System;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Core.Application.Events
{
    public interface IEventFilter : IPagingParameters
    {
        /// <summary>
        /// start date of event
        /// </summary>
        DateTime? StartDate { get; }
        /// <summary>
        /// end date of event
        /// </summary>
        DateTime? EndDate { get; }
        /// <summary>
        /// event type
        /// </summary>
        EventType EventType { get; }
        /// <summary>
        /// whether to filter to a specific venue
        /// </summary>
        long? VenueId { get; }
        /// <summary>
        /// whether to filter to a specific organizer
        /// </summary>
        string OrganizerId { get; }
        /// <summary>
        /// whether to include deleted events in the results
        /// </summary>
        bool? IsDeleted { get; }
    }
}
