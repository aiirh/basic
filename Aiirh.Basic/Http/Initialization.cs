using Microsoft.Extensions.DependencyInjection;

namespace Aiirh.Basic.Http
{
    public static class Initialization
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IHttpClientBuilder, HttpClientBuilder>();
        }
    }
}
