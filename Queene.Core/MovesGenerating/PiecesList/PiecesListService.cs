using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Pieces;
using System.Runtime.CompilerServices;

namespace Queene.Core.MovesGenerating.PiecesList
{
    public class PiecesListService : IPiecesListService
    {
        private readonly BoardContext _bitBoardContext;

        private PieceIndex _pieceIndex;
        private byte _square;

        public PiecesListService(BoardContext bitBoardContext)
        {
            _bitBoardContext = bitBoardContext;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MakeMove(ExtendedMove move)
        {
            if (move.MoveType == MoveTypeEnum.Promotion)
                MakePromotionMove(move);
            else
                MakeNoramlMove(move);

            if (move.Captured.HasValue)
            {
                if (move.MoveType == MoveTypeEnum.EnPassante)
                    MakeEnPassante(move);
                else
                    MakeCapture(move);
            }

            if (move.MoveType == MoveTypeEnum.Castle)
                MakeCastle(move);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UnmakeMove(ExtendedMove move)
        {
            if (move.MoveType == MoveTypeEnum.Promotion)
                UnmakePromotionMove(move);
            else
                UnmakeNoramlMove(move);

            if (move.Captured.HasValue)
            {
                if (move.MoveType == MoveTypeEnum.EnPassante)
                    UnmakeEnPassante(move);
                else
                    UnmakeCapture(move);
            }

            if (move.MoveType == MoveTypeEnum.Castle)
                UnmakeCastle(move);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MakeNoramlMove(ExtendedMove move)
        {
            _pieceIndex = _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][move.From];
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][move.From] = null;
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][move.To] = _pieceIndex;

            _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)move.PieceType].SetAtIndex(_pieceIndex.Index, move.To);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UnmakeNoramlMove(ExtendedMove move)
        {
            _pieceIndex = _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][move.To];
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][move.To] = null;
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][move.From] = _pieceIndex;

            _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)move.PieceType].SetAtIndex(_pieceIndex.Index, move.From);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MakeCapture(ExtendedMove move)
        {
            _pieceIndex = _bitBoardContext.PieceIndices[_bitBoardContext.Player.Oponnent][move.To];
            _square = _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Oponnent][(byte)move.Captured.Value].RemoveAtIndex(_pieceIndex.Index);
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Oponnent][_square] = _pieceIndex;
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Oponnent][move.To] = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UnmakeCapture(ExtendedMove move)
        {
            _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Oponnent][(byte)move.Captured.Value].Add(move.To);
            var index = _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Oponnent][(byte)move.Captured.Value].GetLastIndex();
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Oponnent][move.To] = new PieceIndex(move.Captured.Value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MakePromotionMove(ExtendedMove move)
        {
            _pieceIndex = _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][move.From];
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][move.From] = null;
            _square = _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)move.PieceType].RemoveAtIndex(_pieceIndex.Index);
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][_square] = _pieceIndex;

            _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)move.PromotedTo.Value].Add(move.To);
            var index = _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)move.PromotedTo.Value].GetLastIndex();
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][move.To] = new PieceIndex(move.PromotedTo.Value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UnmakePromotionMove(ExtendedMove move)
        {
            _pieceIndex = _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][move.To];
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][move.To] = null;
            _square = _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)move.PromotedTo.Value].RemoveAtIndex(_pieceIndex.Index);
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][_square] = _pieceIndex;

            _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)move.PieceType].Add(move.From);
            var index = _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)move.PieceType].GetLastIndex();
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][move.From] = new PieceIndex(move.PieceType, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MakeEnPassante(ExtendedMove move)
        {
            var capturedSquare = _bitBoardContext.Player.Current == Player.White ? (byte)(move.To - 8) : (byte)(move.To + 8);
            var pieceIndex = _bitBoardContext.PieceIndices[_bitBoardContext.Player.Oponnent][capturedSquare];
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Oponnent][capturedSquare] = null;
            _square = _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Oponnent][(byte)move.Captured.Value].RemoveAtIndex(pieceIndex.Index);
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Oponnent][_square] = pieceIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UnmakeEnPassante(ExtendedMove move)
        {
            var capturedSquare = _bitBoardContext.Player.Current == Player.White ? (byte)(move.To - 8) : (byte)(move.To + 8);

            _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Oponnent][(byte)move.Captured.Value].Add(capturedSquare);
            var index = _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Oponnent][(byte)move.Captured.Value].GetLastIndex();
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Oponnent][capturedSquare] = new PieceIndex(move.Captured.Value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MakeCastle(ExtendedMove move)
        {
            var rookSrcSquare = move.IsCastleKingSideMove ? Rook.KingSideSourceSquare[_bitBoardContext.Player.Current] : Rook.QueenSideSourceSquare[_bitBoardContext.Player.Current];
            var rookDstSquare = move.IsCastleKingSideMove ? Rook.KingSideDestinationSquare[_bitBoardContext.Player.Current] : Rook.QueenSideDestinationSquare[_bitBoardContext.Player.Current];

            var pieceIndex = _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][rookSrcSquare];
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][rookSrcSquare] = null;
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][rookDstSquare] = pieceIndex;

            _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.Rook].SetAtIndex(pieceIndex.Index, rookDstSquare);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UnmakeCastle(ExtendedMove move)
        {
            var rookSrcSquare = move.IsCastleKingSideMove ? Rook.KingSideSourceSquare[_bitBoardContext.Player.Current] : Rook.QueenSideSourceSquare[_bitBoardContext.Player.Current];
            var rookDstSquare = move.IsCastleKingSideMove ? Rook.KingSideDestinationSquare[_bitBoardContext.Player.Current] : Rook.QueenSideDestinationSquare[_bitBoardContext.Player.Current];

            var pieceIndex = _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][rookDstSquare];
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][rookDstSquare] = null;
            _bitBoardContext.PieceIndices[_bitBoardContext.Player.Current][rookSrcSquare] = pieceIndex;

            _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.Rook].SetAtIndex(pieceIndex.Index, rookSrcSquare);
        }
    }
}