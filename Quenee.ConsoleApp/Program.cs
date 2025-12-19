using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quenee.ConsoleApp.Commands;
using Quenee.ConsoleApp.Commands.Exceptions;
using System;

namespace Quenee.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var startup = new Startup();
            startup.ConfigureServices();

            using var scope = startup.ServiceProvider.CreateScope();
            var parser = new CommandParser(scope.ServiceProvider);

            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            while (true)
            {
                logger.Log(LogLevel.Information, "Type command (or help to see list of available commands): ");
                var command = Console.ReadLine();

                try
                {
                    parser.Execute(command);
                }
                catch (CommandParserException ex)
                {
                    logger.Log(LogLevel.Information, ex.InnerException?.InnerException?.Message);
                }

                Console.WriteLine();
            }
        }
    }
}
