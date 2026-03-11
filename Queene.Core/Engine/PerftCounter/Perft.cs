using Queene.Core.MovesGenerating.Hashing;

namespace Queene.Core.Engine.PerftCounter
{
    public class Perft
    {
        private readonly Board _board;

        public Perft(Board board)
        {
            _board = board;
        }

        public ulong Run(int depth)
        {
            var moves = _board.GenerateMoves();

            if (depth == 1)
                return (ulong)moves.Length;

            //if (PerftTranspositionTable.TryGet(_board.BoardContext.Hash, depth, out ulong cachedNodes))
            //    return cachedNodes;

            ulong nodes = 0;

            for (int i = 0; i < moves.Length; i++)
            {
                var extMove = _board.MakeMove(moves[i]);

                nodes += Run(depth - 1);

                _board.UnmakeMove(extMove);
            }

            //PerftTranspositionTable.Store(_board.BoardContext.Hash, depth, nodes);

            return nodes;
        }
    }
}
