using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace UrbanEngine.Web.Configuration
{
	/// <summary>
	/// represents the model configuration for all configurations
	/// </summary>
	public class AllConfigurations : IModelConfiguration
	{
		/// <inheritdoc /> 
		public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
		{
			// builder.Function( "Ping" ).Returns<string>().Parameter<string>( "Input" );
		}
	}
}
