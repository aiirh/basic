using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aiirh.DatabaseTools
{
    public static class Configuration
    {
        public static void AddAiirhDatabaseTools(this IServiceCollection services, Action<AiirhDatabaseToolsRegistrationOptions> options = null)
        {
        }
    }

    public class AiirhDatabaseToolsRegistrationOptions
    {
    }
}
