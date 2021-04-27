using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Aiirh.Basic.Cache;
using Aiirh.Basic.Http.Extensions;
using Aiirh.Basic.Logging;
using Aiirh.Basic.Security;
using Aiirh.Basic.Utilities;

namespace Aiirh.Basic.Http
{
    public interface IInternalHttpClient
    {
        Task<TResponse> GetAsync<TResponse>(InternalComponentHttpClientParameters parameters);
        Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(TRequest request, InternalComponentHttpClientParameters parameters);
    }

    public abstract class BaseInternalHttpClient<TClient, TApiType> : BaseHttpClient, IInternalHttpClient where TApiType : Enum
    {
        private readonly InternalHttpClientParameters<TApiType> _clientParameters;
        private readonly IApiSignatureManager _apiSignatureManager;
        protected readonly ILogger _logger;

        protected string SettingName => "InternalComponentSettings";

        protected BaseInternalHttpClient(
            IHttpClientBuilder httpClientBuilder,
            InternalHttpClientParameters<TApiType> clientParameters,
            IMemoryCacheManager cacheManager,
            IApiSignatureManager apiSignatureManager,
            ILogger logger) : base(
            cacheManager,
            httpClientBuilder,
            typeof(TClient).Name)
        {
            _apiSignatureManager = apiSignatureManager;
            _logger = logger;
            _clientParameters = clientParameters;
        }

        public async Task<TResponse> GetAsync<TResponse>(InternalComponentHttpClientParameters parameters)
        {
            var sw = Stopwatch.StartNew();
            var conf = await GetHttpClientInitArgs();
            var baseUrl = conf.BaseUrl;
            var apiVersion = parameters.ApiVersion;
            var url = baseUrl.BuildUrl(parameters.UrlSegment, apiVersion, parameters.UrlParams);
            _logger.Info($"InternalHttpClient:{_clientName}", $@"Action {parameters.Action} started [Url=""{url}"", Reference1=""{parameters.Reference1}""]");
            var signedUrl = _apiSignatureManager.AppendSignature(_clientParameters.ApiType.ToString(), url);
            var description = new HttpRequestDescription
            {
                FromSystem = _clientParameters.FromSystem,
                ToSystem = _clientParameters.ToSystem,
                Action = parameters.Action,
                Reference1 = parameters.Reference1,
                Reference2 = parameters.Reference2,
                Url = url,
                Method = RequestMethod.Get
            };

            var client = CreateClient(conf);
            var response = await client.SendAsJsonAsync(signedUrl, default(object), description, conf.SkipMessageLog).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsJsonAsync<TResponse>().ConfigureAwait(false);
                _logger.Info($"InternalHttpClient:{_clientName}", $@"Action {parameters.Action} completed [Url = ""{url}"", Reference1=""{parameters.Reference1}""]. Time elapsed: {sw.ElapsedAndReset()}");
                return result;
            }

            var resp = await response.Content.ReadAsStringAsync();
            throw new Exception(resp);
        }

        public async Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(TRequest request, InternalComponentHttpClientParameters parameters)
        {
            var sw = Stopwatch.StartNew();
            var conf = await GetHttpClientInitArgs();
            var baseUrl = conf.BaseUrl;
            var apiVersion = parameters.ApiVersion;
            var url = baseUrl.BuildUrl(parameters.UrlSegment, apiVersion, parameters.UrlParams);
            _logger.Info($"InternalHttpClient:{_clientName}", $@"Action {parameters.Action} started [Url = ""{url}"", Reference1=""{parameters.Reference1}""]");
            var signedUrl = _apiSignatureManager.AppendSignature(_clientParameters.ApiType.ToString(), url);
            var description = new HttpRequestDescription
            {
                FromSystem = _clientParameters.FromSystem,
                ToSystem = _clientParameters.ToSystem,
                Action = parameters.Action,
                Reference1 = parameters.Reference1,
                Reference2 = parameters.Reference2,
                Url = url,
                Method = RequestMethod.Post
            };

            var client = CreateClient(conf);
            var response = await client.SendAsJsonAsync(signedUrl, request, description, conf.SkipMessageLog).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsJsonAsync<TResponse>().ConfigureAwait(false);
                _logger.Info($"InternalHttpClient:{_clientName}", $@"Action {parameters.Action} completed [Url = ""{url}"", Reference1=""{parameters.Reference1}""]. Time elapsed: {sw.ElapsedAndReset()}");
                return result;
            }
            var resp = await response.Content.ReadAsStringAsync();
            throw new Exception(resp);
        }

        protected abstract Task<HttpClientInitializationArgs> GetHttpClientInitArgs();
    }

    public class InternalHttpClientParameters<T> where T : Enum
    {
        public string FromSystem { get; }
        public string ToSystem { get; }
        public T ApiType { get; }

        public InternalHttpClientParameters(string fromSystem, string toSystem, T apiType)
        {
            FromSystem = fromSystem;
            ToSystem = toSystem;
            ApiType = apiType;
        }
    }
}

