using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Queene.Core;
using Quenee.ConsoleApp.ConsoleFormatters;
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

            _serviceCollection.AddLogging((builder) =>
            {
                builder.AddConsole(options =>
                {
                    options.FormatterName = QueeneConsoleFormatter.FormatterName;
                });
                builder.AddConsoleFormatter<QueeneConsoleFormatter,  QueeneConsoleFormatterOptions>(options =>
                {
                    options.ExcludeNotes = false;
                });
                builder.SetMinimumLevel(LogLevel.Information);
            });

            ServiceProvider = _serviceCollection.BuildServiceProvider(true);
        }
    }
}
