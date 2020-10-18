using MediatR;
using System.Collections.Generic;
using UrbanEngine.Core.Models.Venues;
using UrbanEngine.Core.Specifications.Venues;
using UrbanEngine.SharedKernel.Paging;
using UrbanEngine.SharedKernel.Results;

namespace UrbanEngine.Core.Messages.Venues
{
    /// <summary>
    /// filters and paginates event venue search results
    /// </summary>
    public class GetVenuesMessage : PagingParameters, IEventVenueFilter, IRequest<QueryResult<IEnumerable<EventVenueListItemDto>>>
    {
        #region IEventVenueFilter Members

        /// <summary>
        /// region venue is located in
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// city venue is located in
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// state venue is located in
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// postal code venue is located in
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// whether to include delete venues in the results
        /// </summary>
        public bool? IsDeleted { get; set; }

		/// <summary>
		/// filter by a specific event venue id
		/// </summary>
		public long? EventVenueId { get; set; }

		#endregion
	}
}
