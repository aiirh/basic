using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Aiirh.WebTools.Http
{
    public class HttpClientParameters
    {
        public string Action { get; }
        public string Reference1 { get; }
        public string Reference2 { get; set; }
        public string UrlSegment { get; }

        public Dictionary<string, string> UrlParams { get; set; }
        public AuthenticationHeaderValue AuthenticationHeader { get; set; }
        public Dictionary<string, string> RequestSpecificHeaders { get; set; }

        public HttpClientParameters(string urlSegment, string action, string reference1)
        {
            UrlSegment = urlSegment;
            Action = action;
            Reference1 = reference1;
        }
    }

    public class InternalComponentHttpClientParameters : HttpClientParameters
    {
        public ApiVersion ApiVersion { get; }

        public InternalComponentHttpClientParameters(string urlSegment, string action, string reference1, int apiVersion, bool force = false) : base(urlSegment, action, reference1)
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

        public BasicAuthHttpClientParameters(string urlSegment, string action, string reference1) : base(urlSegment, action, reference1)
        {
        }
    }
}
