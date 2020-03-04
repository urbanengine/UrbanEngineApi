using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanEngine.IntegrationTests.Models
{
	public class Auth0Request
	{
		public string Audience { get; set; }
		public string Authority { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string GrantType { get; set; }
	}
}
