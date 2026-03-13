using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Pieces;

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

        private void MakeNoramlMove(ExtendedMove move)
        {
            var player = _bitBoardContext.Player.Current;

            _pieceIndex = _bitBoardContext.PieceIndices[player][move.From];
            _bitBoardContext.PieceIndices[player][move.From] = null;
            _bitBoardContext.PieceIndices[player][move.To] = _pieceIndex;

            var actualPieceType = _pieceIndex.PieceType;
            _bitBoardContext.PieceTypeList[player][(byte)actualPieceType].SetAtIndex(_pieceIndex.Index, move.To);
        }

        private void UnmakeNoramlMove(ExtendedMove move)
        {
            var player = _bitBoardContext.Player.Current;

            _pieceIndex = _bitBoardContext.PieceIndices[player][move.To];
            _bitBoardContext.PieceIndices[player][move.To] = null;
            _bitBoardContext.PieceIndices[player][move.From] = _pieceIndex;

            var actualPieceType = _pieceIndex.PieceType;
            _bitBoardContext.PieceTypeList[player][(byte)actualPieceType].SetAtIndex(_pieceIndex.Index, move.From);
        }

        private void MakeCapture(ExtendedMove move)
        {
            var opp = _bitBoardContext.Player.Oponnent;

            _pieceIndex = _bitBoardContext.PieceIndices[opp][move.To];
            var capturedType = _pieceIndex.PieceType;
            _square = _bitBoardContext.PieceTypeList[opp][(byte)capturedType].RemoveAtIndex(_pieceIndex.Index);
            _bitBoardContext.PieceIndices[opp][_square] = _pieceIndex;
            _bitBoardContext.PieceIndices[opp][move.To] = null;
        }

        private void UnmakeCapture(ExtendedMove move)
        {
            var opp = _bitBoardContext.Player.Oponnent;

            _bitBoardContext.PieceTypeList[opp][(byte)move.Captured.Value].Add(move.To);
            var index = _bitBoardContext.PieceTypeList[opp][(byte)move.Captured.Value].GetLastIndex();
            _bitBoardContext.PieceIndices[opp][move.To] = new PieceIndex(move.Captured.Value, index);
        }

        private void MakePromotionMove(ExtendedMove move)
        {
            var player = _bitBoardContext.Player.Current;

            _pieceIndex = _bitBoardContext.PieceIndices[player][move.From];
            _bitBoardContext.PieceIndices[player][move.From] = null;

            var pawnType = _pieceIndex.PieceType;
            _square = _bitBoardContext.PieceTypeList[player][(byte)pawnType].RemoveAtIndex(_pieceIndex.Index);
            _bitBoardContext.PieceIndices[player][_square] = _pieceIndex;

            _bitBoardContext.PieceTypeList[player][(byte)move.PromotedTo.Value].Add(move.To);
            var index = _bitBoardContext.PieceTypeList[player][(byte)move.PromotedTo.Value].GetLastIndex();
            _bitBoardContext.PieceIndices[player][move.To] = new PieceIndex(move.PromotedTo.Value, index);
        }

        private void UnmakePromotionMove(ExtendedMove move)
        {
            var player = _bitBoardContext.Player.Current;

            _pieceIndex = _bitBoardContext.PieceIndices[player][move.To];
            _bitBoardContext.PieceIndices[player][move.To] = null;
            _square = _bitBoardContext.PieceTypeList[player][(byte)move.PromotedTo.Value].RemoveAtIndex(_pieceIndex.Index);
            _bitBoardContext.PieceIndices[player][_square] = _pieceIndex;

            var pawnType = move.PieceType != PieceTypeEnum.Empty ? move.PieceType : PieceTypeEnum.Pawn;
            _bitBoardContext.PieceTypeList[player][(byte)pawnType].Add(move.From);
            var index = _bitBoardContext.PieceTypeList[player][(byte)pawnType].GetLastIndex();
            _bitBoardContext.PieceIndices[player][move.From] = new PieceIndex(pawnType, index);
        }

        private void MakeEnPassante(ExtendedMove move)
        {
            var opp = _bitBoardContext.Player.Oponnent;
            var capturedSquare = _bitBoardContext.Player.Current == Player.White ? (byte)(move.To - 8) : (byte)(move.To + 8);
            var pieceIndex = _bitBoardContext.PieceIndices[opp][capturedSquare];

            _bitBoardContext.PieceIndices[opp][capturedSquare] = null;

            var capturedType = pieceIndex.PieceType;
            _square = _bitBoardContext.PieceTypeList[opp][(byte)capturedType].RemoveAtIndex(pieceIndex.Index);
            _bitBoardContext.PieceIndices[opp][_square] = pieceIndex;
        }

        private void UnmakeEnPassante(ExtendedMove move)
        {
            var opp = _bitBoardContext.Player.Oponnent;
            var capturedSquare = _bitBoardContext.Player.Current == Player.White ? (byte)(move.To - 8) : (byte)(move.To + 8);

            _bitBoardContext.PieceTypeList[opp][(byte)move.Captured.Value].Add(capturedSquare);
            var index = _bitBoardContext.PieceTypeList[opp][(byte)move.Captured.Value].GetLastIndex();
            _bitBoardContext.PieceIndices[opp][capturedSquare] = new PieceIndex(move.Captured.Value, index);
        }

        private void MakeCastle(ExtendedMove move)
        {
            var player = _bitBoardContext.Player.Current;
            var rookSrcSquare = move.IsCastleKingSideMove ? Rook.KingSideSourceSquare[player] : Rook.QueenSideSourceSquare[player];
            var rookDstSquare = move.IsCastleKingSideMove ? Rook.KingSideDestinationSquare[player] : Rook.QueenSideDestinationSquare[player];
            var pieceIndex = _bitBoardContext.PieceIndices[player][rookSrcSquare];

            _bitBoardContext.PieceIndices[player][rookSrcSquare] = null;
            _bitBoardContext.PieceIndices[player][rookDstSquare] = pieceIndex;

            var rookType = pieceIndex.PieceType;
            _bitBoardContext.PieceTypeList[player][(byte)rookType].SetAtIndex(pieceIndex.Index, rookDstSquare);
        }

        private void UnmakeCastle(ExtendedMove move)
        {
            var player = _bitBoardContext.Player.Current;
            var rookSrcSquare = move.IsCastleKingSideMove ? Rook.KingSideSourceSquare[player] : Rook.QueenSideSourceSquare[player];
            var rookDstSquare = move.IsCastleKingSideMove ? Rook.KingSideDestinationSquare[player] : Rook.QueenSideDestinationSquare[player];
            var pieceIndex = _bitBoardContext.PieceIndices[player][rookDstSquare];

            _bitBoardContext.PieceIndices[player][rookDstSquare] = null;
            _bitBoardContext.PieceIndices[player][rookSrcSquare] = pieceIndex;

            var rookType = pieceIndex.PieceType;
            _bitBoardContext.PieceTypeList[player][(byte)rookType].SetAtIndex(pieceIndex.Index, rookSrcSquare);
        }
    }
}