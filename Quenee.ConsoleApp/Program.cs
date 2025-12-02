using Microsoft.Extensions.DependencyInjection;
using Quenee.ConsoleApp.Commands;
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

            //while (true)
            //{
            //    Console.WriteLine("Enter command:");
            //    var commandLine = Console.ReadLine();
            //    var commandParser = new CommandParser(commandLine);

            //    if (commandParser.CommandName.ToLower() == "exit")
            //        break;
            //    else
            //    {
            //        Console.WriteLine($"{commandParser.CommandName}:");
            //        foreach (string param in commandParser.Parameters.Keys)
            //            Console.WriteLine($"{param}={commandParser[param]}");
            //    }
            //}

            var game = scope.ServiceProvider.GetRequiredService<Game>();

            while (true)
            {
                Console.WriteLine("Enter fen:");
                string fen = Console.ReadLine();

                Console.WriteLine("Ply:");
                int ply = int.Parse(Console.ReadLine());

                game.Run(fen, ply);

                startup.DisposeServices();

                Console.ReadLine();
            }
        }
    }
}
