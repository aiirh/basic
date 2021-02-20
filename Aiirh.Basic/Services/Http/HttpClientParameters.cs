using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Aiirh.Basic.Services.Http
{
    public class HttpClientParameters
    {
        public string BaseUrl { get; set; }
        public string Action { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public string UrlSegment { get; set; }

        public Dictionary<string, string> UrlParams { get; set; }
        public AuthenticationHeaderValue AuthenticationHeader { get; set; }
        public Dictionary<string, string> RequestSpecificHeaders { get; set; }
    }

    public class InternalComponentHttpClientParameters : HttpClientParameters
    {
        public ApiVersion ApiVersion { get; }

        public InternalComponentHttpClientParameters(int apiVersion, bool force = false)
        {
            ApiVersion = new ApiVersion(apiVersion, force);
        }
    }

    public class ApiVersion
    {
        public int Version { get; }

        /// <summary>
        /// If false, version will be applied only if not provided in base url (in system settings). If true - version is always set to every request
        /// </summary>
        public bool Force { get; }

        public ApiVersion(int apiVersion, bool force = false)
        {
            Version = apiVersion;
            Force = force;
        }
    }

    public class BasicAuthHttpClientParameters : HttpClientParameters
    {
        public string AuthToken { get; set; }
    }
}
