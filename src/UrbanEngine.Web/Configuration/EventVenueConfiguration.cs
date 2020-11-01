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
			var eventVenue = builder
				.EntitySet<EventVenueEntity>("EventVenues")
				.EntityType
				.HasKey(o => o.Id);

			// indicate which properties allowed to select by
			eventVenue.Select().Page().Count().Filter().OrderBy();

			// if version specific stuff you can check the version
			// if(apiVersion > ApiVersions.V1) { }
		}
	}
}
