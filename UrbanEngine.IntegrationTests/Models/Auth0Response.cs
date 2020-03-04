using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanEngine.IntegrationTests.Models
{
	public class Auth0Response
	{
		public string AccessToken { get; set; }
		public string Dcopes { get; set; }
		public string ExpiresIn { get; set; }
		public string TokenType { get; set; }
	}
}
