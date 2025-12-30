using Queene.Core.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Queene.Core.Engine.Search
{
    public class ParallelSearch(IQueeneGame game)
    {
        private readonly IQueeneGame _game = game;

        public delegate void PrintResults(string move, long count, string pv);
        public event PrintResults OnPrintResults;

        public ExtendedMove RunInParallel(int depth)
        {
            var moves = _game.GenerateMoves();

            long count = 0;
            var fen = _game.GetFen();

            ExtendedMove[] extMoves = new ExtendedMove[moves.Length];

            var result = Parallel.ForEach(moves, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (move, state, index) =>
            {
                var board = _game.CloneBoard();
                var extMove = board.MakeMove(move);

                var search = new SearchEngine(board, depth - 1);
                var (score, pv) = search.Run(-SearchEngine.Infinity, SearchEngine.Infinity, depth - 1);

                extMove.Value = -score;
                extMoves[index] = extMove;
                //search.Stats.BestMoves[depth] = extMove;

                Interlocked.Add(ref count, search.Stats.NodesSearched);

                //OnPrintResults?.Invoke(move.ToString(), score, search.Stats.ToString());
                OnPrintResults?.Invoke(move.ToString(), extMove.Value, string.Concat(pv.Select(m => $"{m.ToString()}, ")).TrimEnd(','));
            });

            return extMoves.MaxBy(x => x.Value);
        }
    }
}
