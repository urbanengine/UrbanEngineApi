using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
        /// Verifies that the API returns a 401 Unauthorized if non access token is provided in the request
        /// </summary>
        [TestMethod]
        public void UnAuthorizedAccess()
        {
            var mock = new Mock<ILogger<AboutController>>();
            ILogger<AboutController> logger = mock.Object;

            var controller = new AboutController( logger );

            var response = controller.GetVersion() as OkObjectResult;

            Assert.AreEqual( HttpStatusCode.Unauthorized, response.StatusCode );
        }

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

        [TestMethod]
        public async Task AuthorizedAccess()
        {
            var client = new HttpClient();
            var token = await GetToken();

            var requestMessage = new HttpRequestMessage( HttpMethod.Get, "/about" );
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue( "Bearer", token );
            var booksResponse = await client.SendAsync( requestMessage );

            Assert.AreEqual( HttpStatusCode.OK, booksResponse.StatusCode );

            var bookResponseString = await booksResponse.Content.ReadAsStringAsync();
            var bookResponseJson = JArray.Parse( bookResponseString );
            Assert.AreEqual( 4, bookResponseJson.Count );
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
