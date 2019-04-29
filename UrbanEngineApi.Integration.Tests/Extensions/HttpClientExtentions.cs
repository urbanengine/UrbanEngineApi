using System.Net.Http.Headers;

namespace UrbanEngineApi.Integration.Tests.Extensions
{
    public static class HttpClientExtentions
    {
        public static AuthenticationHeaderValue ToAuthBearerHeaderValue(this string token)
        {
            return new AuthenticationHeaderValue("Bearer", "=" + token);
        }
    }
}
