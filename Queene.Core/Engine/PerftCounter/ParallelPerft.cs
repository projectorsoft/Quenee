using System.Threading;
using System.Threading.Tasks;

namespace Queene.Core.Engine.PerftCounter
{
    public class ParallelPerft(IQueeneGame game)
    {
        private readonly IQueeneGame _game = game;

        public delegate void PrintResults(string move, ulong count);
        public event PrintResults OnPrintResults;

        public ulong RunInParallel(int depth, int maxParallelOperations)
        {
            var moves = _game.GenerateMoves();

            ulong count = 0;
            var fen = _game.GetFen();

            var result = Parallel.ForEach(moves, new ParallelOptions { MaxDegreeOfParallelism = maxParallelOperations }, (move, state, index) =>
            {
                var board = _game.CloneBoard();
                var extMove = board.MakeMove(move);

                var perft = new Perft(board);
                var nodes = perft.Run(depth - 1);

                Interlocked.Add(ref count, nodes);

                OnPrintResults?.Invoke(move.ToString(), nodes);
            });

            return count;
        }
    }
}
