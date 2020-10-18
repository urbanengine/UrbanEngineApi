using System.Linq;
using MediatR;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Specifications.Venues;

namespace UrbanEngine.Core.Messages.Venues
{
	public sealed class QueryVenuesMessage : IRequest<IQueryable<EventVenueEntity>>, IEventVenueFilter
	{
		public long? EventVenueId { get; set; }

		public string Region { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string PostalCode { get; set; }

		public bool? IsDeleted { get; set; }
	}
}
