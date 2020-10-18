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
		/// <summary>
		/// apply configuration
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="apiVersion"></param>
		public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
		{
			builder.EntitySet<CheckInEntity>("CheckInEntity");
			builder.EntitySet<EventEntity>("EventEntity");
			builder.EntitySet<EventVenueEntity>("EventVenueEntity");
			builder.EntitySet<RoomEntity>("RoomEntity");
		}
	}
}
