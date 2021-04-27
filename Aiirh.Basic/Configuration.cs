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
            Initialization.RegisterServices(services);
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
            ApiSignatureManager.Init(optionsToUse.ApiSignatureOptions);
            MemoryCacheManager.Init(optionsToUse.DisableCache);
        }
    }

    public class AiirhRegistrationOptions
    {
        public string CryptoServicePassPhrase { get; set; }

        public bool DisableCache { get; set; } = false;

        public ApiSignatureOptions ApiSignatureOptions { get; set; } = new ApiSignatureOptions();
    }

    public class ApiSignatureOptions
    {
        public bool Disabled { get; set; } = false;
        public byte ApiSignatureValidityTimeInSeconds { get; set; } = 15;
    }
}
