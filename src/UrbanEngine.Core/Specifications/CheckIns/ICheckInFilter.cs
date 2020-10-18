using System;

namespace UrbanEngine.Core.Specifications.CheckIns
{
    public interface ICheckInFilter
    {
		/// <summary>
		/// filter for a specific id
		/// </summary>
		long? CheckInId { get; }
        /// <summary>
		/// id of the event
		/// </summary>
        long? EventId { get; }
        /// <summary>
		/// id of the user
		/// </summary>
        long? UserId { get; }
        /// <summary>
        /// date and time user checked in at the event
        /// </summary>
        DateTimeOffset? CheckedInAt { get; }
        /// <summary>
        /// whether to include deleted events in the results
        /// </summary>
        bool? IsDeleted { get; }
    }
}
