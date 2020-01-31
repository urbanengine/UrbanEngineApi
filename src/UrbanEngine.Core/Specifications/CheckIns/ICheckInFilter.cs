using System;
using UrbanEngine.SharedKernel.Paging;

namespace UrbanEngine.Core.Specifications.CheckIns
{
    public interface ICheckInFilter : IPagingParameters
    {
        /// <summary>
		/// id of the event
		/// </summary>
        long? EventId { get; set; }
        /// <summary>
		/// id of the user
		/// </summary>
        long? UserId { get; set; }
        /// <summary>
        /// date and time user checked in at the event
        /// </summary>
        DateTimeOffset? CheckedInAt { get; set; }
        /// <summary>
        /// whether to include deleted events in the results
        /// </summary>
        bool? IsDeleted { get; }
    }
}
