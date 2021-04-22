using Aiirh.Basic.Cache;
using Aiirh.Basic.Security;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aiirh.Basic
{
    public static class Configuration
    {
        public static void AddAiirhBasic(this IServiceCollection services, Action<AiirhRegistrationOptions> options = null)
        {
            services.AddMemoryCache();
            Http.Initialization.RegisterServices(services);
            if (options == null)
            {
                return;
            }

            var optionsToUse = new AiirhRegistrationOptions();
            options.Invoke(optionsToUse);

            if (!string.IsNullOrWhiteSpace(optionsToUse.CryptoServicePassPhrase))
            {
                CryptoService.Init(optionsToUse.CryptoServicePassPhrase);
            }

            MemoryCacheManager.Init(optionsToUse.DisableCache);
        }
    }

    public class AiirhRegistrationOptions
    {
        public string CryptoServicePassPhrase { get; set; }
        public bool DisableCache { get; set; }
    }
}
