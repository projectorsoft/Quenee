using Queene.Core;
using Queene.Core.Converters;
using Queene.Core.Engine;
using Queene.Core.MovesGenerating.Hashing;
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
            ZorbistHash.Init();
        }

        public void Run(string fen, int depth)
        {
            Perft(fen, depth);
        }

        private void Perft(string fen, int depth)
        {
            _board.NewGame(fen);

            var perft = new ParallelPerft(_board);
            perft.OnPrintResults += PrintMoves;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var movesCount = perft.RunInParallel(depth, Environment.ProcessorCount);

            stopwatch.Stop();

            Console.WriteLine($"Moves count: {movesCount} in {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void PrintMoves(string move, ulong count)
        {
            Console.WriteLine($"{move}: {count}");
        }
    }
}
