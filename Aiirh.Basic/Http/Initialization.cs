using Aiirh.Basic.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace Aiirh.Basic.Http
{
    internal static class Initialization
    {
        internal static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IHttpClientBuilder, HttpClientBuilder>();
            services.AddScoped<IMemoryCacheManager, MemoryCacheManager>();
        }
    }
}
