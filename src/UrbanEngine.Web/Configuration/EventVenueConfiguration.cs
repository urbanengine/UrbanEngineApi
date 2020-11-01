namespace UrbanEngine.Web.Configuration
{
	using Microsoft.AspNet.OData.Builder;
	using Microsoft.AspNetCore.Mvc;
	using UrbanEngine.Core.Entities;

	/// <summary>
	/// Represents the configuration for Event Venues
	/// </summary>
	public class EventVenueConfiguration : IModelConfiguration
	{
		/// <inheritdoc />
		public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
		{
			// register the entity that is used by the odata controller
			var eventVenues = builder
				.EntitySet<EventVenueEntity>("EventVenues");

			var roomsEntityType = builder
				.EntitySet<RoomEntity>("Rooms");

			// key
			eventVenues.EntityType.HasKey(p => p.Id);
			
			// indicate which odata actions are allowed
			eventVenues.EntityType.Select().Page().Count().Filter().OrderBy();

			// indicate anything to not expose to odata endpoints publicly
			eventVenues.EntityType.Ignore(p => p.Events);
			eventVenues.EntityType.Ignore(p => p.Rooms);

			//roomsEntityType.Ignore(p => p.Events);
			//roomsEntityType.Ignore(p => p.Venue);

			// if version specific stuff you can check the version
			// if(apiVersion > ApiVersions.V1) { }
		}
	}
}
