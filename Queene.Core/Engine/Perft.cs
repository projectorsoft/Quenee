namespace Queene.Core.Engine
{
    public class Perft
    {
        private readonly Board _board;
        private readonly PerftDetails _perftDetails;

        public PerftDetails GetDetails => _perftDetails;

        public Perft(Board board)
        {
            _board = board;
            _perftDetails = new PerftDetails();
        }

        public ulong Run(int depth, bool root = true)
        {
            var moves = _board.GenerateMoves();

            if (root)
            {
                _perftDetails.RootNodes = new RootNode[moves.Length];
                for (int i = 0; i < moves.Length; i++)
                {
                    _perftDetails.RootNodes[i] = new RootNode();
                    _perftDetails.RootNodes[i].Move = moves[i].ToString();
                    _perftDetails.RootNodes[i].Count = 1;
                }
            }

            if (depth == 1)
                return (ulong)moves.Length;

            ulong nodes = 0;
            ulong rootNodes = 0;

            for (int i = 0; i < moves.Length; i++)
            {
                var extMove = _board.MakeMove(moves[i]);

                //_perftDetails.Nodes++;

                //if (extMove.MoveType == Enums.MoveTypeEnum.Castle)
                //    _perftDetails.Castles++;
                //if (extMove.MoveType == Enums.MoveTypeEnum.EnPassante)
                //    _perftDetails.EnPassante++;
                //if (extMove.MoveType == Enums.MoveTypeEnum.Promotion)
                //    _perftDetails.Promotions++;
                //if (extMove.Captured.HasValue)
                //    _perftDetails.Captures++;

                rootNodes = Run(depth - 1, false);
                nodes = nodes + rootNodes;

                if (root)
                {
                    _perftDetails.RootNodes[i].Move = extMove.ToString();
                    _perftDetails.RootNodes[i].Count = rootNodes;
                }

                _board.UnmakeMove(extMove);
            }

            return nodes;
        }
    }
}
