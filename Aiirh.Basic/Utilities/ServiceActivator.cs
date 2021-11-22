using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aiirh.Basic.Utilities
{
    public static class ServiceActivator
    {
        private static IServiceProvider _serviceProvider;

        public static void AddServiceActivator(this IApplicationBuilder app)
        {
            _serviceProvider ??= app.ApplicationServices;
        }

        public static IServiceScope GetScope(IServiceProvider serviceProvider = null)
        {
            var provider = serviceProvider ?? _serviceProvider;
            return provider?.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }
    }
}
