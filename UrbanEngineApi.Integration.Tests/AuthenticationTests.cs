using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Moq;
using Newtonsoft.Json.Linq;

using UrbanEngine.Web.Controllers;

namespace UrbanEngineApi.Integration.Tests
{
    [TestClass]
    public class AuthenticationTests
    {
        private IConfiguration _configuration;

        [TestInitialize]
        public void Init()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
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
            var mock = new Mock<ILogger<AboutController>>();
            var controller = new AboutController( mock.Object );

            // Act
            var actionResult = controller.GetVersion();
            var contentResult = actionResult as ContentResult;

            // Assert
            Assert.AreEqual( HttpStatusCode.Unauthorized, contentResult.StatusCode );
        }

        /// <summary>
        /// Verifies that the API returns a 401 Unauthorized if no access token is provided in the request
        /// </summary>
        [TestMethod]
        public async Task TestGetToken()
        {
            var client = new HttpClient();
            var bodyString = $@"{{""client_id"":""{_configuration["Auth0:ClientId"]}"", ""client_secret"":""{_configuration["Auth0:ClientSecret"]}"", ""audience"":""{_configuration["Auth0:Audience"]}"", ""grant_type"":""client_credentials""}}";
            var response = await client.PostAsync($"{_configuration["Auth0:Authority"]}/oauth/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));

            Assert.AreEqual( HttpStatusCode.OK, response.StatusCode );

            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse( responseString );

            // Verify that a non-null token has been received and that the type of token is Bearer
            Assert.IsNotNull( (string) responseJson["access_token"] );
            Assert.AreEqual( "Bearer", (string) responseJson["token_type"] );
        }

        /// <summary>
        /// Verifies that the API returns a 401 Unauthorized if no access token is provided in the request
        /// </summary>
        [TestMethod]
        public async Task AuthorizedAccess()
        {
            //// Arrange
            //var mock = new Mock<ILogger<AboutController>>();
            //ILogger<AboutController> logger = mock.Object;


            //// Act
            //var controller = new AboutController( logger );
            //var result = controller.GetHeaderValue( "27" );
            //var response = controller.GetVersion() as OkObjectResult;

            // Arrange
            var client = new HttpClient();
            var token = await GetToken();

            // Act
            var requestMessage = new HttpRequestMessage( HttpMethod.Get, "/about" );
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue( "Bearer", token );
            var response = await client.SendAsync( requestMessage );

            // Assert
            Assert.AreEqual( HttpStatusCode.OK, response.StatusCode );

            // Arrange Again
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JArray.Parse( responseString );

            // Assert again
            Assert.AreEqual( 4, responseJson.Count );
        }

        #endregion

        #region Private Methods

        private async Task<string> GetToken()
        {
            var client = new HttpClient();
            string token = "";
            var bodyString = $@"{{""client_id"":""{_configuration["Auth0:ClientId"]}"", ""client_secret"":""{_configuration["Auth0:ClientSecret"]}"", ""audience"":""{_configuration["Auth0:Audience"]}"", ""grant_type"":""client_credentials""}}";
            var response = await client.PostAsync($"{_configuration["Auth0:Authority"]}oauth/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));

            if ( response.IsSuccessStatusCode )
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var responseJson = JObject.Parse( responseString );
                token = ( string ) responseJson[ "access_token" ];
            }

            return token;
        }

        #endregion
    }
}
