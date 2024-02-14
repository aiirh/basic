using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aiirh.DatabaseTools.Utilities
{
    public static class ServiceLocator
    {
        private static IServiceProvider _serviceProvider;

        public static void AddServiceLocator(this IServiceCollection serviceCollection)
        {
            _serviceProvider ??= serviceCollection.BuildServiceProvider();
        }

        public static TService GetService<TService>()
        {
            if (_serviceProvider == null)
            {
                throw new Exception("ServiceLocator is not initialized");
            }

            return (TService)_serviceProvider.GetService(typeof(TService));
        }
    }
}
