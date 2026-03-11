using Microsoft.Extensions.Logging;
using Queene.Core;
using Queene.Core.Engine.Search;
using Quenee.ConsoleApp.Commands.Abstract;
using Quenee.ConsoleApp.Commands.PerftCommand;
using System.Collections.Generic;
using System.Diagnostics;

namespace Quenee.ConsoleApp.Commands.SearchCommand
{
    public class SearchCommand : ICommand<SearchCommandRequest, SearchCommandResponse>
    {
        private readonly IQueeneGame _game;
        private readonly ILogger<SearchCommand> _logger;

        public string Name => "search";

        public List<CommandParameterBase> Params => [
                new CommandParameter<int>(nameof(PerftCommandRequest.Ply), 0)
            ];

        public SearchCommand(IQueeneGame game,
            ILogger<SearchCommand> logger)
        {
            _game = game;
            _logger = logger;
        }

        public SearchCommandResponse Execute(SearchCommandRequest request)
        {
            _logger.Log(LogLevel.Information, $"Searching for the best move... \r\n");

            var search = new ParallelSearch(_game);
            search.OnPrintResults += PrintMoves;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var bestMove = search.RunInParallel(request.Ply);

            stopwatch.Stop();

            //_logger.Log(LogLevel.Information, $"Moves count: {search.Stats.NodesSearched} in {stopwatch.ElapsedMilliseconds} ms");
            _logger.Log(LogLevel.Information, $"Best move: {bestMove} \r\n");

            return new SearchCommandResponse
            {
                //NodesCount = search.Stats.NodesSearched,
                TotalTimeInMs = stopwatch.ElapsedMilliseconds
            };
        }

        private void PrintMoves(string move, long count, string pv)
        {
            _logger.Log(LogLevel.Information, $"{move}: {count} -> {pv} \r\n");
        }
    }
}
