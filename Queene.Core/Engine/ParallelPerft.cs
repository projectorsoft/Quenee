using Queene.Core.Converters;
using System.Threading;
using System.Threading.Tasks;

namespace Queene.Core.Engine
{
    public class ParallelPerft(Board board)
    {
        private readonly Board _board = board;

        public delegate void PrintResults(string move, ulong count);
        public event PrintResults OnPrintResults;

        public ulong RunInParallel(int depth, int maxParallelOperations)
        {
            var moves = _board.GenerateMoves();

            ulong count = 0;
            var fen = _board.BitBoardContext.ToString();

            var result = Parallel.ForEach(moves, new ParallelOptions { MaxDegreeOfParallelism = maxParallelOperations }, (move, state, index) =>
            {
                var board = new Board(new BitBoardContextConverter(new BoardStateToFenConverter()));
                board.NewGame(fen);
                var extMove = board.MakeMove(move);

                var perft = new Perft(board);
                var nodes = perft.Run(depth - 1);

                Interlocked.Add(ref count, nodes);

                OnPrintResults?.Invoke(move.ToString(), count);
            });

            return count;
        }
    }
}
