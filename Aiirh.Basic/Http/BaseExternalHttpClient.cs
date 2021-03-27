using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Aiirh.Basic.Cache;
using Aiirh.Basic.Http.Extensions;
using Aiirh.Basic.Utilities;

namespace Aiirh.Basic.Http
{
    public abstract class BaseExternalHttpClient<T> : BaseHttpClient
    {
        private readonly HttpClientSettings _clientSettings;

        protected BaseExternalHttpClient(
            IHttpClientBuilder httpClientBuilder,
            IMemoryCacheManager cacheManager,
            HttpClientSettings clientSettings) : base(
            cacheManager,
            httpClientBuilder,
            typeof(T).Name)
        {
            _clientSettings = clientSettings;
        }

        protected async Task<(TResponse response, TErrResponse error)> GetAsync<TResponse, TErrResponse>(HttpClientParameters parameters, ResponseFormat responseFormat, bool forceSkipMessageLog)
        {
            var conf = await GetHttpClientInitArgsForSite(parameters);
            var description = GetHttpDescription(parameters, RequestMethod.Get, conf);
            var client = CreateClient(conf);
            var response = await client.SendAsJsonAsync(description.Url, default(object), description, conf.SkipMessageLog || forceSkipMessageLog).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var respResult = await response.Content.ReadAsSpecifiedFormatAsync<TResponse>(responseFormat).ConfigureAwait(false);
                return (respResult, default);
            }

            var resp = await HandleExceptionAsync<TErrResponse>(response, responseFormat).ConfigureAwait(false);
            return (default, resp);
        }

        protected async Task<(TResponse response, TErrResponse error)> PostAsJsonAsync<TRequest, TResponse, TErrResponse>(TRequest request, HttpClientParameters parameters, ResponseFormat responseFormat)
        {
            var conf = await GetHttpClientInitArgsForSite(parameters);
            var description = GetHttpDescription(parameters, RequestMethod.Post, conf);
            var client = CreateClient(conf);
            var response = await client.SendAsJsonAsync(description.Url, request, description, conf.SkipMessageLog).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadAsSpecifiedFormatAsync<TResponse>(responseFormat).ConfigureAwait(false), default);
            }
            var resp = await HandleExceptionAsync<TErrResponse>(response, responseFormat).ConfigureAwait(false);
            return (default, resp);
        }

        protected async Task<(TResponse response, TErrResponse error)> PostAsFormAsync<TResponse, TErrResponse>(ICollection<KeyValuePair<string, string>> request, HttpClientParameters parameters, ResponseFormat responseFormat)
        {
            var conf = await GetHttpClientInitArgsForSite(parameters);
            var description = GetHttpDescription(parameters, RequestMethod.Post, conf);
            var client = CreateClient(conf);
            var response = await client.SendAsFormAsync(description.Url, request, description, conf.SkipMessageLog).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadAsSpecifiedFormatAsync<TResponse>(responseFormat).ConfigureAwait(false), default);
            }
            var resp = await HandleExceptionAsync<TErrResponse>(response, responseFormat).ConfigureAwait(false);
            return (default, resp);
        }

        protected async Task<(TResponse response, TErrResponse error)> PostAsXmlAsync<TRequest, TResponse, TErrResponse>(TRequest request, HttpClientParameters parameters, ResponseFormat responseFormat)
        {
            var conf = await GetHttpClientInitArgsForSite(parameters);
            var description = GetHttpDescription(parameters, RequestMethod.Post, conf);
            var client = CreateClient(conf);
            var response = await client.SendAsXmlAsync(description.Url, request, description, conf.SkipMessageLog).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadAsSpecifiedFormatAsync<TResponse>(responseFormat).ConfigureAwait(false), default);
            }
            var resp = await HandleExceptionAsync<TErrResponse>(response, responseFormat).ConfigureAwait(false);
            return (default, resp);
        }

        protected async Task<(TResponse response, TErrResponse error)> PutAsJsonAsync<TRequest, TResponse, TErrResponse>(TRequest request, HttpClientParameters parameters, ResponseFormat responseFormat)
        {
            var conf = await GetHttpClientInitArgsForSite(parameters);
            var description = GetHttpDescription(parameters, RequestMethod.Put, conf);

            var client = CreateClient(conf);
            var response = await client.SendAsJsonAsync(description.Url, request, description, conf.SkipMessageLog).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadAsSpecifiedFormatAsync<TResponse>(responseFormat).ConfigureAwait(false), default);
            }
            var resp = await HandleExceptionAsync<TErrResponse>(response, responseFormat).ConfigureAwait(false);
            return (default, resp);
        }

        protected HttpRequestDescription GetHttpDescription(HttpClientParameters parameters, RequestMethod requestMethod, HttpClientInitializationArgs conf)
        {
            var url = conf.BaseUrl.BuildUrl(parameters.UrlSegment, parameters.UrlParams);
            var description = new HttpRequestDescription
            {
                FromSystem = _clientSettings.FromSystem,
                ToSystem = _clientSettings.ToSystem,
                Action = parameters.Action,
                Reference1 = parameters.Reference1,
                Reference2 = parameters.Reference2,
                Url = url,
                AuthenticationHeader = parameters.AuthenticationHeader,
                DefaultHttpHeaders = conf.RequestHeaderValues.Merge(parameters.RequestSpecificHeaders).ToDictionary(),
                Method = requestMethod
            };
            return description;
        }

        private static async Task<TResult> HandleExceptionAsync<TResult>(HttpResponseMessage responseMsg, ResponseFormat responseFormat)
        {
            return await responseMsg.TryParseResultAsync<TResult>(responseFormat).ConfigureAwait(false);
        }

        protected abstract Task<HttpClientInitializationArgs> GetHttpClientInitArgsForSite(HttpClientParameters parameters);
    }

    public class HttpClientSettings
    {
        public string FromSystem { get; }
        public string ToSystem { get; }

        public HttpClientSettings(string fromSystem, string toSystem)
        {
            FromSystem = fromSystem;
            ToSystem = toSystem;
        }
    }
}