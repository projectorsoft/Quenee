using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.PiecesList;
using QueeneEngine.Engine.Magics;
using QueeneEngine.Helpers.Bitwise;

namespace Queene.Core.MovesGenerating.Pieces
{
    public class Rook : Slider, IPiece
    {
        public override PieceTypeEnum PieceType => PieceTypeEnum.Rook;
        public const int Value = 500;

        public static readonly byte[] KingSideSourceSquare = [56, 0];
        public static readonly byte[] QueenSideSourceSquare = [63, 7];
        public static readonly byte[] KingSideDestinationSquare = [58, 2];
        public static readonly byte[] QueenSideDestinationSquare = [60, 4];

        public Rook(BoardContext bitBoardContext,
            IList<Move> movesList,
            IPiecesListService piecesListService,
            ZorbistHash zorbistHash)
            : base(bitBoardContext, movesList, piecesListService, zorbistHash)
        {
        }

        public void GenerateMoves(MoveGenerationTypeEnum generationType)
        {
            GenerateMoves(MovesContainer.RookMagics, generationType, SliderTypeEnum.Rook);
        }

        public override void MakeMove(ref ExtendedMove move)
        {
            if (_bitBoardContext.CanCastle[_bitBoardContext.Player.Current])
            {
                if (_bitBoardContext.CanCastleKingSide[_bitBoardContext.Player.Current] && move.From == KingSideSourceSquare[_bitBoardContext.Player.Current])
                {
                    move.KingSideCastleBreak = true;
                    _bitBoardContext.SetKingSideCastleAllowance(_bitBoardContext.Player.Current, false);
                    _bitBoardContext.Hash ^= _zorbistHash.KingSideCastle[_bitBoardContext.Player.Current];
                }
                else if (_bitBoardContext.CanCastleQueenSide[_bitBoardContext.Player.Current] && move.From == QueenSideSourceSquare[_bitBoardContext.Player.Current])
                {
                    move.QueenSideCastleBreak = true;
                    _bitBoardContext.SetQueenSideCastleAllowance(_bitBoardContext.Player.Current, false);
                    _bitBoardContext.Hash ^= _zorbistHash.QueenSideCastle[_bitBoardContext.Player.Current];
                }
            }

            base.MakeMove(ref move);
        }

        public override void UnmakeMove(ref ExtendedMove move)
        {
            if (move.KingSideCastleBreak)
            {
                _bitBoardContext.SetKingSideCastleAllowance(_bitBoardContext.Player.Current, true);
                _bitBoardContext.Hash ^= _zorbistHash.KingSideCastle[_bitBoardContext.Player.Current];
            }
            else if (move.QueenSideCastleBreak)
            {
                _bitBoardContext.SetQueenSideCastleAllowance(_bitBoardContext.Player.Current, true);
                _bitBoardContext.Hash ^= _zorbistHash.QueenSideCastle[_bitBoardContext.Player.Current];
            }

            base.UnmakeMove(ref move);
        }

        public static bool IsRookOnKingSideInitialPosition(BoardContext bitBoardContext)
        {
            return BitwiseHelper.IsSet(bitBoardContext.Pieces[bitBoardContext.Player.Current][(byte)PieceTypeEnum.Rook], KingSideSourceSquare[bitBoardContext.Player.Current]);
        }

        public static bool IsRookOnQueenSideInitialPosition(BoardContext bitBoardContext)
        {
            return BitwiseHelper.IsSet(bitBoardContext.Pieces[bitBoardContext.Player.Current][(byte)PieceTypeEnum.Rook], QueenSideSourceSquare[bitBoardContext.Player.Current]);
        }
    }
}
