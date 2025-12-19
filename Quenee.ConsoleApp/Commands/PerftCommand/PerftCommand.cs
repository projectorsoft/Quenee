using Microsoft.Extensions.Logging;
using Queene.Core;
using Queene.Core.Engine.PerftCounter;
using Quenee.ConsoleApp.Commands.Abstract;
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

        private readonly IQueeneGame _game;
        private readonly ILogger<PerftCommand> _logger;

        public PerftCommand(IQueeneGame game,
            ILogger<PerftCommand> logger)
        {
            _game = game;
            _logger = logger;
        }

        public PerftCommandResponse Execute(PerftCommandRequest request)
        {
            _logger.Log(LogLevel.Information, $"Running perft using {request.MaxParallelOperations} tasks");

            var perft = new ParallelPerft(_game);
            perft.OnPrintResults += PrintMoves;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var movesCount = perft.RunInParallel(request.Ply, request.MaxParallelOperations);

            stopwatch.Stop();

            _logger.Log(LogLevel.Information, $"Moves count: {movesCount} in {stopwatch.ElapsedMilliseconds} ms");

            return new PerftCommandResponse
            {
                NodesCount = movesCount,
                TotalTimeInMs = stopwatch.ElapsedMilliseconds
            };
        }

        private void PrintMoves(string move, ulong count)
        {
            _logger.Log(LogLevel.Information, $"{move}: {count}");
        }
    }
}
