using Aiirh.Basic.Services.Cache;

namespace Aiirh.Basic.Services.Http
{

    public abstract class BaseHttpClient
    {
        private readonly IHttpClientBuilder _httpClientBuilder;
        protected readonly IMemoryCacheManager _cacheManager;
        protected readonly string _clientName;

        protected BaseHttpClient(IMemoryCacheManager cacheManager, IHttpClientBuilder httpClientBuilder, string clientName)
        {
            _cacheManager = cacheManager;
            _httpClientBuilder = httpClientBuilder;
            _clientName = clientName;
        }

        protected ExtendedHttpClient CreateClient(HttpClientInitializationArgs conf)
        {
            var cacheKey = _clientName;
            var clientFromCache = _cacheManager.GetOrAddImmortal("HttpClients", cacheKey, () => _httpClientBuilder.BuildClient(conf));
            if (!clientFromCache.IsDisposed)
            {
                return clientFromCache;
            }
            _cacheManager.Invalidate("HttpClients", cacheKey);
            return _httpClientBuilder.BuildClient(conf);
        }
    }
}
