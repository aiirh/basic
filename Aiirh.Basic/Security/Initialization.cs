using Microsoft.Extensions.DependencyInjection;

namespace Aiirh.Basic.Security
{
    internal static class Initialization
    {
        internal static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IApiSignatureManager<>), typeof(ApiSignatureManager<>));
        }
    }
}
