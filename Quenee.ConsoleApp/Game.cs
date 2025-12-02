using Queene.Core;
using Queene.Core.Converters;
using Queene.Core.Engine;
using System;
using System.Diagnostics;

namespace Quenee.ConsoleApp
{
    public class Game
    {
        private readonly Board _board;

        public Game(IBitBoardContextConverter bitBoardContextConverter)
        {
            _board = new Board(bitBoardContextConverter);
        }

        public void Run(string fen, int depth)
        {
            Perft(fen, depth);
        }

        private void Perft(string fen, int depth)
        {
            _board.NewGame(fen);

            var perft = new Perft(_board);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var movesCount = perft.Run(depth, true);

            stopwatch.Stop();

            var details = perft.GetDetails;

            for (int i = 0; i < details.RootNodes.Length; i++)
            {
                Console.WriteLine($"{details.RootNodes[i].Move}: {details.RootNodes[i].Count}");
            }

            Console.WriteLine($"Moves count: {movesCount} in {stopwatch.ElapsedMilliseconds} ms");

            //Console.WriteLine($"Captures count: {detail.Captures}");
            //Console.WriteLine($"EnPassante count: {detail.EnPassante}");
            //Console.WriteLine($"Castles count: {detail.Castles}");
            //Console.WriteLine($"Promotions count: {detail.Promotions}");
        }
    }
}
