using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrbanEngine.IntegrationTests.Extensions;
using UrbanEngine.IntegrationTests.Models;
using UrbanEngine.IntegrationTests.Utils;

namespace UrbanEngine.IntegrationTests.Controllers
{
	[TestClass]
    public class AboutTests
	{
		private IConfiguration _configuration;
		private readonly HttpClientHelper _helper;

		public AboutTests()
		{
			_helper = new HttpClientHelper();
		}

		[TestInitialize]
		public void Init()
		{
			_configuration = new ConfigurationBuilder()
				.AddJsonFile( "appsettings.test.json" )
				.Build();
		}

		#region Tests

		/// <summary>
		/// Verifies that the API returns a 401 Unauthorized if no access token is provided in the request
		/// </summary>
		[TestMethod]
		public async Task UnAuthorizedAccess()
		{
			// Arrange
			var client = _helper.Client;

			// Act
			var response = await client.GetAsync( "/about" );

			// Assert
			Assert.AreEqual( HttpStatusCode.Unauthorized, response.StatusCode );
		}

		/// <summary>
		/// Verifies the response being returned when getting the token is not null
		/// </summary>
		[TestMethod]
		public async Task TestGetToken_IsNotNull()
		{
			// Arrange
			var client = new HttpClient();
			var json = new Auth0Request()
			{
				Audience = _configuration[ "Auth0:Audience" ],
				Authority = _configuration[ "Auth0:Authority" ],
				ClientId = _configuration[ "Auth0:ClientId" ],
				ClientSecret = _configuration[ "Auth0:ClientSecret" ],
				GrantType = _configuration[ "Auth0:GrantType" ]
			};

			var payload = json.ToJson();
			var content = new StringContent( payload, Encoding.UTF8, "application/json" );
			var url = $"{json.Authority}/oauth/token";

			// Act
			var response = await client.PostAsync( url, content );
			var deserializedResponse = await response.Content.ReadAsAsync<Auth0Response>();

			// Assert
			Assert.IsNotNull( deserializedResponse.AccessToken );
		}

		/// <summary>
		/// Verifies the token in the repsonse is a Bearer token
		/// </summary>
		[TestMethod]
		public async Task VerifyTokenType_IsBearer()
		{
			// Arrange
			var client = new HttpClient();
			var json = new Auth0Request()
			{
				Audience = _configuration[ "Auth0:Audience" ],
				Authority = _configuration[ "Auth0:Authority" ],
				ClientId = _configuration[ "Auth0:ClientId" ],
				ClientSecret = _configuration[ "Auth0:ClientSecret" ],
				GrantType = _configuration[ "Auth0:GrantType" ]
			};

			var payload = json.ToJson();
			var content = new StringContent( payload, Encoding.UTF8, "application/json" );
			var url = $"{json.Authority}/oauth/token";

			// Act
			var response = await client.PostAsync( url, content );
			var deserializedResponse = await response.Content.ReadAsAsync<Auth0Response>();

			// Assert
			Assert.AreEqual( "Bearer", deserializedResponse.TokenType );
		}

		/// <summary>
		/// Verifies that the API returns a 401 Unauthorized if no access token is provided in the request
		/// </summary>
		[TestMethod]
		public async Task AuthorizedAccess()
		{
			// Arrange
			var client = _helper.Client;
			var token = await GetToken();

			// Act
			client.DefaultRequestHeaders.TryAddWithoutValidation( "Authorization", string.Format( "Bearer {0}", token ) );
			var response = await client.GetAsync( "/about" ); // this throws an InvalidOperationException

			// Assert
			Assert.AreEqual( HttpStatusCode.OK, response.StatusCode );
		}

		#endregion

		#region Private Methods

		private async Task<string> GetToken()
		{
			var client = new HttpClient();
			string token = string.Empty;
			var json = new Auth0Request()
			{
				Audience = _configuration[ "Auth0:Audience" ],
				Authority = _configuration[ "Auth0:Authority" ],
				ClientId = _configuration[ "Auth0:ClientId" ],
				ClientSecret = _configuration[ "Auth0:ClientSecret" ],
				GrantType = _configuration[ "Auth0:GrantType" ]
			};

			var payload = json.ToJson();
			var content = new StringContent( payload, Encoding.UTF8, "application/json" );
			var url = $"{json.Authority}/oauth/token";

			// Act
			var response = await client.PostAsync( url, content );


			if(response.IsSuccessStatusCode)
			{
				var deserializedResponse = await response.Content.ReadAsAsync<Auth0Response>();
				token = deserializedResponse.AccessToken;
			}

			return token;
		}

		#endregion
	}
}
