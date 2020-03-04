using Newtonsoft.Json;

namespace UrbanEngine.IntegrationTests.Extensions
{
	public static class StringExtensions
	{
		public static string ToJson( this object obj )
		{
			return JsonConvert.SerializeObject( obj );
		}

		public static string ToJson( this object obj, int recursionDepth )
		{
			return JsonConvert.SerializeObject( obj, new JsonSerializerSettings
			{
				MaxDepth = recursionDepth
			} );
		}

		public static T FromJson<T>( this object obj )
		{
			return JsonConvert.DeserializeObject<T>( obj as string );
		}
	}
}
