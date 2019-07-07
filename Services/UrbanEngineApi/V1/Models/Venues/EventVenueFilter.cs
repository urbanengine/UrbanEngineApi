using UrbanEngine.Core.Application.Venues;
using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Services.UrbanEngineApi.V1.Models.Venues
{
    /// <summary>
    /// filters and paginates event venue search results
    /// </summary>
    public class EventVenueFilter : PagingParameters, IEventVenueFilter
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

        #endregion
    }
}
