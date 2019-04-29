using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using urban_engine_api;

namespace UrbanEngineApi.Integration.Tests.Helpers {
    public class HttpClientHelper
    {
        public HttpClient Client { get; set; }
        private TestServer _server;

        public HttpClientHelper()
        {
            SetupClient();
        }

        private void SetupClient()
        {
            _server = new TestServer( new WebHostBuilder().UseStartup<Startup>() );

            Client = _server.CreateClient();
        }
    }
}
