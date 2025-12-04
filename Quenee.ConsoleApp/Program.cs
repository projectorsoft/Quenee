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

            while (true)
            {
                Console.WriteLine("Enter fen:");
                string fen = Console.ReadLine();

                if (string.IsNullOrEmpty(fen))
                    continue;

                Console.WriteLine("Ply:");
                if (!int.TryParse(Console.ReadLine(), out var ply))
                    continue;

                //prevent too big depth
                if (ply > 10)
                    continue;

                try
                {
                    game.Run(fen, ply);
                }
                catch (Exception ex)
                {
                    continue;
                }

                Console.ReadLine();
            }

            startup.DisposeServices();
        }
    }
}
