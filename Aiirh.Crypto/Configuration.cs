using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aiirh.Crypto
{
    public static class Configuration
    {
        public static void AddAiirhCrypto(this IServiceCollection services, Action<AiirhCryptoOptions> options = null)
        {
            if (options == null)
            {
                return;
            }
            var optionsToUse = new AiirhCryptoOptions();
            options.Invoke(optionsToUse);
            CryptographyTools.Init(optionsToUse.PassPhrase);
        }
    }

    public class AiirhCryptoOptions
    {
        public string PassPhrase { get; set; }
    }
}
