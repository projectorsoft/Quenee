using Queene.Core.Models;
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

            if (moves == null || moves.Length == 0)
                return 0UL;

            ulong count = 0;
            var po = new ParallelOptions { MaxDegreeOfParallelism = maxParallelOperations };

            Parallel.ForEach<Move, (Board board, Perft perft)>(
                source: moves,
                parallelOptions: po,
                localInit: () =>
                {
                    var localBoard = _game.CloneBoard();
                    var localPerft = new Perft(localBoard);
                    return (localBoard, localPerft);
                },
                body: (move, loopState, local) =>
                {
                    var extMove = local.board.MakeMove(move);
                    ulong nodes = 1;
                    if (depth > 1)
                        nodes = local.perft.Run(depth - 1);

                    Interlocked.Add(ref count, nodes);

                    var handler = OnPrintResults;

                    if (handler != null)
                        handler(move.ToString(), nodes);

                    local.board.UnmakeMove(extMove);

                    return local;
                },
                localFinally: local =>
                {
                });

            return count;
        }
    }
}
