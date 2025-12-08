using Microsoft.Extensions.DependencyInjection;
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

            var game = scope.ServiceProvider.GetRequiredService<Game>();

            startup.DisposeServices();

            while (true)
            {
                Console.Write("Enter fen: ");
                string fen = Console.ReadLine();

                if (string.IsNullOrEmpty(fen))
                {
                    Console.WriteLine();
                    continue;
                }

                Console.Write("Ply: ");
                if (!int.TryParse(Console.ReadLine(), out var ply))
                {
                    Console.WriteLine();
                    continue;
                }

                //prevent too big depth
                if (ply > 10)
                {
                    Console.WriteLine();
                    continue;
                }

                try
                {
                    game.Run(fen, ply);
                }
                catch (Exception ex)
                {
                    continue;
                }

                Console.WriteLine();
            }
        }
    }
}
