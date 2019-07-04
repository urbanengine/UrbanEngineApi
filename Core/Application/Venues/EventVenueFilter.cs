using System;
using System.Linq.Expressions;
using LinqKit;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Common.Filters;
using UrbanEngine.Core.Common.Paging;

namespace UrbanEngine.Core.Application.Venues
{
    /// <summary>
    /// filters and paginates event venue search results
    /// </summary>
    public class EventVenueFilter : PagingParameters, IFilter<EventVenue>, IPagingParameters
    {
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

        public Expression<Func<EventVenue, bool>> GetExpression()
        {
            var predicate = PredicateBuilder.New<EventVenue>();

            if (!string.IsNullOrEmpty(City))
                predicate = predicate.Or(p => p.City.ToLower().Trim() == City.ToLower().Trim());

            if (!string.IsNullOrEmpty(State))
                predicate = predicate.Or(p => p.State.ToLower().Trim() == State.ToLower().Trim());

            if (!string.IsNullOrEmpty(PostalCode))
                predicate = predicate.Or(p => p.PostalCode.ToLower().Trim() == PostalCode.ToLower().Trim());

            if (!string.IsNullOrEmpty(Region) && RegionType.TryFromName(Region, out var regionTypeFromName))
                predicate = predicate.Or(p => p.Region == regionTypeFromName);
            else if (!string.IsNullOrEmpty(Region) && int.TryParse(Region, out var regionValue) && RegionType.TryFromValue(regionValue, out var regionTypeFromValue))
                predicate = predicate.Or(p => p.Region == regionTypeFromValue);

            return predicate;
        }
    }
}
