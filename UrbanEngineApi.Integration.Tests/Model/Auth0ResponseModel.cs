using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanEngineApi.Integration.Tests.Model {
    public class Auth0ResponseModel
    {
        public string access_token { get; set; }
        public string scopes { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
    }
}
