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

            //if (depth == 0)
            //    return 1;

            //var entry = PerftTranspositionTable.Get(_board.BitBoardContext.Hash, depth);

            //if (entry != null)
            //    return entry.Value;

            //if (PerftTranspositionTable.TryGet(_board.BitBoardContext.Hash, depth, out ulong nodesCached))
            //    return nodesCached;

            ulong nodes = 0;

            for (int i = 0; i < moves.Length; i++)
            {
                var extMove = _board.MakeMove(moves[i]);

                nodes = nodes + Run(depth - 1);

                _board.UnmakeMove(extMove);
            }

            //PerftTranspositionTable.Add(_board.BitBoardContext.Hash, depth, nodes);

            return nodes;
        }
    }
}
