using Microsoft.Extensions.DependencyInjection;
using Queene.Core;
using System;

namespace Quenee.ConsoleApp
{
    public class Startup
    {
        private readonly IServiceCollection _serviceCollection;

        public IServiceProvider ServiceProvider { get; private set; }

        public Startup()
        {
            _serviceCollection = new ServiceCollection();
        }

        public void ConfigureServices()
        {
            QueeneIocModule.RegisterServices(_serviceCollection);
            _serviceCollection.AddSingleton<Game>();

            ServiceProvider = _serviceCollection.BuildServiceProvider(true);
        }

        public void DisposeServices()
        {
            if (ServiceProvider == null)
            {
                return;
            }
            if (ServiceProvider is IDisposable)
            {
                ((IDisposable)ServiceProvider).Dispose();
            }
        }
    }
}
