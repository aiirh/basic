using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aiirh.Crypto;

public static class Configuration
{
    public static IServiceCollection AddAiirhCrypto(this IServiceCollection services, Action<AiirhCryptoOptions> options = null)
    {
        if (options == null)
        {
            return services;
        }
        var optionsToUse = new AiirhCryptoOptions();
        options.Invoke(optionsToUse);
        CryptographyTools.Init(optionsToUse.PassPhrase);
        return services;
    }
}

public class AiirhCryptoOptions
{
    public string PassPhrase { get; set; }
}