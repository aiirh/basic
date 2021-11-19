using Aiirh.WebTools.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace Aiirh.WebTools.Http
{
    internal static class Initialization
    {
        internal static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpClientBuilder, HttpClientBuilder>();
            services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();
        }
    }
}
