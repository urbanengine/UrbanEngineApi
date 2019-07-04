using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Core.Application.Venues
{
    /// <summary>
    /// filters and paginates event venue search results
    /// </summary>
    public interface IEventVenueFilter : IPagingParameters
    {
        /// <summary>
        /// region venue is located in
        /// </summary>
        string Region { get; }

        /// <summary>
        /// city venue is located in
        /// </summary>
        string City { get; }

        /// <summary>
        /// state venue is located in
        /// </summary>
        string State { get; }

        /// <summary>
        /// postal code venue is located in
        /// </summary>
        string PostalCode { get; }

        /// <summary>
        /// whether to include delete venues in the results
        /// </summary>
        bool? IsDeleted { get; }
    }
}
