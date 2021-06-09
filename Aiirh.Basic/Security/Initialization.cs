using Aiirh.Basic.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Aiirh.Basic.Security
{
    internal static class Initialization
    {
        internal static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ApiSignatureAuthorizationFilter>();
            services.AddSingleton<IApiSignatureManager, ApiSignatureManager>();
        }
    }
}
