using Queene.Core.Consts;
using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.PiecesList;
using QueeneEngine.Engine.Magics;

namespace Queene.Core.MovesGenerating.Pieces
{
    public class Knight : PieceBase, IPiece
    {
        public override PieceTypeEnum PieceType => PieceTypeEnum.Knight;
        public const int Value = 300;

        public Knight(BoardContext bitBoardContext,
            IList<Move> movesList,
            IPiecesListService piecesListService,
            ZorbistHash zorbistHash)
            : base(bitBoardContext, movesList, piecesListService, zorbistHash)
        {
        }

        public void GenerateMoves(MoveGenerationTypeEnum generationType)
        {
            var player = _bitBoardContext.Player.Current;
            var opp = _bitBoardContext.Player.Oponnent;
            var knightList = _bitBoardContext.PieceTypeList[player][(byte)PieceType];
            var count = knightList.Count();
            var occupied = _bitBoardContext.OccupiedSquares;
            var empty = _bitBoardContext.EmptySquares;
            var attackers = _bitBoardContext.Attackers;
            var checkedSquares = _bitBoardContext.CheckedSquares;
            var pinnedSquares = _bitBoardContext.PinnedSquares;
            var oppAll = _bitBoardContext.Pieces[opp][(byte)PieceTypeEnum.All];

            for (int i = 0; i < count; i++)
            {
                var square = knightList.GetAtIndex(i);

                var attacks = MovesContainer.KnightMoves[square];

                ulong movesLocal = 0;
                if (generationType == MoveGenerationTypeEnum.All)
                    movesLocal = attacks & empty;

                ulong capturesLocal = attacks & oppAll;

                if (attackers != 0)
                {
                    movesLocal &= checkedSquares;
                    capturesLocal &= attackers;
                }

                if ((pinnedSquares & Powers.powersOfTwo[square]) != 0)
                {
                    movesLocal = 0;
                    capturesLocal = 0;
                }

                var combined = movesLocal | capturesLocal;
                if (combined != 0)
                    AddMoves(combined, (byte)square, MoveTypeEnum.Move);
            }
        }
    }
}
