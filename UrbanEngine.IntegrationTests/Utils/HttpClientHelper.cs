using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using UrbanEngine.Web;

namespace UrbanEngine.IntegrationTests.Utils
{
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
