using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanEngineApi.Integration.Tests.Model {
    public class Auth0RequestModel {
        public string audience { get; set; }
        public string authority { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
    }
}
