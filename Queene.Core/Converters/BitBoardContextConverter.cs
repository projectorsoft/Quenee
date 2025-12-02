using Queene.Core.Consts;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.Models;
using Queene.Core.MovesGenerating;
using QueeneEngine.Helpers.Bitwise;

namespace Queene.Core.Converters
{
    public class BitBoardContextConverter : IBitBoardContextConverter
	{
		private readonly IBoardStateConverter _boardStateConverter;
		private BitBoardContext _bitBoardContext;

		public BitBoardContextConverter(IBoardStateConverter boardStateConverter)
        {
			_boardStateConverter = boardStateConverter;
		}

		public BoardState Convert(BitBoardContext bitBoardContext)
		{
			_bitBoardContext = bitBoardContext;

			var boardState = new BoardState(_boardStateConverter)
            {
                TurnToMove = (PlayerEnum)_bitBoardContext.Player.Current,
				FullMoveNumber = _bitBoardContext.FullMoveNumber, //TODO:
				HalfMoveNumber = _bitBoardContext.HalfMoveNumber //TODO:
			};

            SetEnPassantSquare(boardState);
			SetCastlingRightsForWhite(boardState);
			SetCastlingRightsForBlack(boardState);
			SetPieces(boardState);

			return boardState;
		}

		private void SetPieces(BoardState boardState)
        {
			var squares = BitwiseHelper.GetAllSetBitsInMask(_bitBoardContext.OccupiedSquares, out var count);

			for (byte i = 0; i < count; i++)
			{
				var pieceType = _bitBoardContext.GetPieceType(squares[i], Player.White);

				if (pieceType == PieceTypeEnum.Empty)
				{
					pieceType = _bitBoardContext.GetPieceType(squares[i], Player.Black);
					boardState.BlackPieces[pieceType].Add(squares[i]);
				}
				else
					boardState.WhitePieces[pieceType].Add(squares[i]);
			}
		}

		private void SetEnPassantSquare(BoardState boardState)
        {
			if (_bitBoardContext.EnPassantSquare.HasValue)
				boardState.EnPassant = BoardConsts.SQUARES_NAMES[_bitBoardContext.EnPassantSquare.Value];
			else
				boardState.EnPassant = FenHelper.EMPTY_SECTION_CHARACTER;
		}

		private void SetCastlingRightsForBlack(BoardState boardState)
        {
			if (!_bitBoardContext.CanCastle[(byte)PlayerEnum.Black])
				boardState.BlackCastlingRights = CastleTypeEnum.None;
			else
			{
				if (_bitBoardContext.CanCastleKingSide[(byte)PlayerEnum.Black])
					boardState.BlackCastlingRights |= CastleTypeEnum.CastleKingSide;

				if (_bitBoardContext.CanCastleQueenSide[(byte)PlayerEnum.Black])
					boardState.BlackCastlingRights |= CastleTypeEnum.CastleQueenSide;
			}
		}

		private void SetCastlingRightsForWhite(BoardState boardState)
		{
			if (!_bitBoardContext.CanCastle[(byte)PlayerEnum.White])
				boardState.WhiteCastlingRights = CastleTypeEnum.None;
			else
			{
				if (_bitBoardContext.CanCastleKingSide[(byte)PlayerEnum.White])
					boardState.WhiteCastlingRights |= CastleTypeEnum.CastleKingSide;

				if (_bitBoardContext.CanCastleQueenSide[(byte)PlayerEnum.White])
					boardState.WhiteCastlingRights |= CastleTypeEnum.CastleQueenSide;
			}
		}
	}
}
