using MediatR;
using System;
using System.Collections.Generic;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Models.Events;
using UrbanEngine.Core.Specifications.Events;
using UrbanEngine.SharedKernel.Paging;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Events
{
    /// <summary>
    /// filters and paginates event search results
    /// </summary>
    public class GetEventsMessage : PagingParameters, IRequest<QueryResult<IEnumerable<EventListItemDto>>>, IEventFilter
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

		/// <summary>
		/// whether to filter by a specific room
		/// </summary>
		public long? RoomId { get; set; }
	}
}
