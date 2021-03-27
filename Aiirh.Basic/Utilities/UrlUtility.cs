using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aiirh.Basic.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Aiirh.Basic.Utilities
{
    public static class UrlUtility
    {
        public static string BuildUrl(this string baseUrl, string method, Dictionary<string, string> urlParams)
        {
            return baseUrl.BuildUrl(method, null, urlParams);
        }

        public static string BuildUrl(this string baseUrl, string method, ApiVersion apiVersion, Dictionary<string, string> urlParams)
        {
            var regex = new Regex("/v\\d+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var endUrl = baseUrl;
            if (apiVersion != null)
            {
                if (regex.IsMatch(endUrl))
                {
                    if (apiVersion.Force)
                    {
                        endUrl = regex.Replace(endUrl, $"/v{apiVersion.Version}");
                    }
                }
                else
                {
                    endUrl = endUrl + (endUrl.EndsWith("/") ? string.Empty : "/") + $"v{apiVersion.Version}";
                }
            }

            if (!string.IsNullOrWhiteSpace(method))
            {
                endUrl = endUrl + (endUrl.EndsWith("/") ? string.Empty : "/") + method;
            }
            if (urlParams != null && urlParams.Count > 0)
            {
                endUrl = QueryHelpers.AddQueryString(endUrl, urlParams);
            }
            return endUrl;
        }
    }
}
