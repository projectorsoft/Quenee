using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.PiecesList;
using QueeneEngine.Engine.Magics;
using QueeneEngine.Helpers.Bitwise;
using System.Runtime.CompilerServices;

namespace Queene.Core.MovesGenerating.Pieces
{
    public class Rook : Slider, IPiece
    {
        public override PieceTypeEnum PieceType => PieceTypeEnum.Rook;

        public static readonly byte[] KingSideSourceSquare = new byte[] { 56, 0 };
        public static readonly byte[] QueenSideSourceSquare = new byte[] { 63, 7 };
        public static readonly byte[] KingSideDestinationSquare = new byte[] { 58, 2 };
        public static readonly byte[] QueenSideDestinationSquare = new byte[] { 60, 4 };

        public Rook(BitBoardContext bitBoardContext,
            MovesContainer movesContainer,
            IList<Move> movesList,
            IPiecesListService piecesListService)
            : base(bitBoardContext, movesContainer, movesList, piecesListService)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GenerateMoves(MoveGenerationTypeEnum generationType)
        {
            GenerateMoves(_movesContainer.RookMagics, generationType, SliderTypeEnum.Rook);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void MakeMove(ExtendedMove move)
        {
            if (_bitBoardContext.CanCastle[_bitBoardContext.Player.Current])
            {
                if (_bitBoardContext.CanCastleKingSide[_bitBoardContext.Player.Current] && move.From == KingSideSourceSquare[_bitBoardContext.Player.Current])
                {
                    move.KingSideCastleBreak = true;
                    _bitBoardContext.SetKingSideCastleAllowance(_bitBoardContext.Player.Current, false);
                }
                else if (_bitBoardContext.CanCastleQueenSide[_bitBoardContext.Player.Current] && move.From == QueenSideSourceSquare[_bitBoardContext.Player.Current])
                {
                    move.QueenSideCastleBreak = true;
                    _bitBoardContext.SetQueenSideCastleAllowance(_bitBoardContext.Player.Current, false);
                }
            }

            base.MakeMove(move);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void UnmakeMove(ExtendedMove move)
        {
            if (move.KingSideCastleBreak)
                _bitBoardContext.SetKingSideCastleAllowance(_bitBoardContext.Player.Current, true);
            else if (move.QueenSideCastleBreak)
                _bitBoardContext.SetQueenSideCastleAllowance(_bitBoardContext.Player.Current, true);

            base.UnmakeMove(move);
        }

        public static bool IsRookOnKingSideInitialPosition(BitBoardContext bitBoardContext)
        {
            return BitwiseHelper.IsSet(bitBoardContext.Pieces[bitBoardContext.Player.Current][(byte)PieceTypeEnum.Rook], KingSideSourceSquare[bitBoardContext.Player.Current]);
        }

        public static bool IsRookOnQueenSideInitialPosition(BitBoardContext bitBoardContext)
        {
            return BitwiseHelper.IsSet(bitBoardContext.Pieces[bitBoardContext.Player.Current][(byte)PieceTypeEnum.Rook], QueenSideSourceSquare[bitBoardContext.Player.Current]);
        }
    }
}
