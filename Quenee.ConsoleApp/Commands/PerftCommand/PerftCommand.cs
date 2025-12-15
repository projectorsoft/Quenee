using Queene.Core;
using Queene.Core.Engine;
using Queene.Core.MovesGenerating.Hashing;
using Quenee.ConsoleApp.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Quenee.ConsoleApp.Commands.PerftCommand
{
    public class PerftCommand : ICommand<PerftCommandRequest, PerftCommandResponse>
    {
        public string Name => "Perft";

        public List<CommandParameterBase> Params => [
                new CommandParameter<int>(nameof(PerftCommandRequest.Ply), 0),
                new CommandParameter<int>(nameof(PerftCommandRequest.MaxParallelOperations), 1, required: false)
            ];

        private readonly Board _board;

        public PerftCommand(Board board)
        {
            ZorbistHash.Init();
            _board = board;
            //PerftTranspositionTable.Init();
        }

        public PerftCommandResponse Execute(PerftCommandRequest request)
        {
            Console.WriteLine($"Running perft using {request.MaxParallelOperations} tasks");

            _board.NewGame(_board.BitBoardContext.ToString());

            var perft = new ParallelPerft(_board);
            perft.OnPrintResults += PrintMoves;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var movesCount = perft.RunInParallel(request.Ply, request.MaxParallelOperations);

            stopwatch.Stop();

            Console.WriteLine($"Moves count: {movesCount} in {stopwatch.ElapsedMilliseconds} ms");

            return new PerftCommandResponse
            {
                NodesCount = movesCount,
                TotalTimeInMs = stopwatch.ElapsedMilliseconds
            };
        }

        private static void PrintMoves(string move, ulong count)
        {
            Console.WriteLine($"{move}: {count}");
        }
    }
}
