using Microsoft.Extensions.DependencyInjection;
using Quenee.ConsoleApp.Commands;
using Quenee.ConsoleApp.Commands.Exceptions;
using System;

namespace Quenee.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var startup = new Startup();
            startup.ConfigureServices();
            using var scope = startup.ServiceProvider.CreateScope();

            var parser = new CommandParser(scope.ServiceProvider);

            while (true)
            {
                Console.Write("Type command: ");
                var command = Console.ReadLine();

                try
                {
                    parser.Execute(command);
                }
                catch (CommandParserException ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    continue;
                }

                Console.WriteLine();
            }

            startup.DisposeServices();
        }
    }
}
