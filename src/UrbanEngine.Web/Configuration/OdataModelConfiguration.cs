using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using UrbanEngine.Core.Entities;

namespace UrbanEngine.Web.Configuration
{
	/// <summary>
	/// configuration for odata model builder
	/// </summary>
	public sealed class OdataModelConfiguration : IModelConfiguration 
	{
		public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
		{
			// if(apiVersion < ApiVersions.V1) { }

			builder.EntitySet<CheckInEntity>("CheckInEntity");
			builder.EntitySet<EventEntity>("EventEntity");
			builder.EntitySet<EventVenueEntity>("EventVenueEntity");
			builder.EntitySet<RoomEntity>("RoomEntity");
		}
	}
}
