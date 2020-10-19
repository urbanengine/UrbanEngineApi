using System;
using UrbanEngine.Core.Enums;

namespace UrbanEngine.Core.Specifications.Events
{
    public interface IEventFilter
    {
		/// <summary>
		/// used to filter to a specific event
		/// </summary>
		long? EventId { get; }
        /// <summary>
        /// start date of event
        /// </summary>
        DateTimeOffset? StartDate { get; }
        /// <summary>
        /// end date of event
        /// </summary>
        DateTimeOffset? EndDate { get; }
        /// <summary>
        /// event type
        /// </summary>
        EventType EventType { get; }
        /// <summary>
        /// whether to filter to a specific organizer
        /// </summary>
        string OrganizerId { get; }
        /// <summary>
        /// whether to include deleted events in the results
        /// </summary>
        bool? IsDeleted { get; }
		/// <summary>
		/// whether to filter to a specific room
		/// </summary>
		long? RoomId { get; }
    }
}