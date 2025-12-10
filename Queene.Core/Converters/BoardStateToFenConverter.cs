using Queene.Core.Consts;
using Queene.Core.Enums;
using Queene.Core.Fen;

namespace Queene.Core.Converters
{
    public class BoardStateToFenConverter : IBoardStateConverter
    {
		public string PiecesPart { get; private set; }
		public string PlayerPart { get; private set; }
        public string CastlingsPart { get; private set; }
        public string EnPassantePart { get; private set; }
		public string HalfMovePart { get; private set; }
		public string FullMovePart { get; private set; }

		private BoardState _boardState;

		public string Convert(BoardState boardState)
        {
			_boardState = boardState;

			PiecesPart = ConvertPieces();
			PlayerPart = ConvertPlayer();
			CastlingsPart = ConvertCatlingRights();
			EnPassantePart = ConvertEnPassante();
			FullMovePart = _boardState.FullMoveNumber.ToString();
			HalfMovePart = _boardState.HalfMoveNumber.ToString();

			return $"{PiecesPart} {PlayerPart} {CastlingsPart} {EnPassantePart} {HalfMovePart} {FullMovePart}";
		}

        private string ConvertPieces()
        {
			var pieces = "";
			var counter = 0;

			for (int i = 63; i >= 0; i--)
			{
				if (TryGetPieceOnSquare((byte)i, out var pieceTypeSymbol))
				{
					if (counter > 0)
						pieces += counter;

					pieces += pieceTypeSymbol;
					counter = 0;
				}
				else
					counter++;

				if (i % 8 == 0)
				{
					if (counter > 0)
						pieces += counter;

					pieces += "/";
					counter = 0;
				}
			}

			return pieces.TrimEnd('/');
		}

		private bool TryGetPieceOnSquare(byte square, out string pieceTypeSymbol)
		{
			for (var pieceType = PieceTypeEnum.Knight; pieceType <= PieceTypeEnum.King; pieceType++)
			{
				if (_boardState.WhitePieces[pieceType].Contains(square))
				{
					pieceTypeSymbol = BoardConsts.PIECE_TYPE_SYMBOL[pieceType].ToUpperInvariant();
					return true;
				}

				if (_boardState.BlackPieces[pieceType].Contains(square))
				{
					pieceTypeSymbol = BoardConsts.PIECE_TYPE_SYMBOL[pieceType].ToLowerInvariant();
					return true;
				}
			}

			pieceTypeSymbol = BoardConsts.PIECE_TYPE_SYMBOL[PieceTypeEnum.Empty];
			return false;
		}

		private string ConvertPlayer()
        {
			return _boardState.TurnToMove == PlayerEnum.White ? "w" : "b";
        }

		private string ConvertCatlingRights()
        {
			if (_boardState.WhiteCastlingRights == CastleTypeEnum.None &&
				_boardState.BlackCastlingRights == CastleTypeEnum.None)
				return "-";

			var castlings = "";

			if (_boardState.WhiteCastlingRights == CastleTypeEnum.Both)
				castlings = "KQ";
			else
				if (_boardState.WhiteCastlingRights == CastleTypeEnum.CastleKingSide)
					castlings = "K";
			else
				if (_boardState.WhiteCastlingRights == CastleTypeEnum.CastleQueenSide)
				castlings = "Q";

			if (_boardState.BlackCastlingRights == CastleTypeEnum.Both)
				castlings += "kq";
			else
				if (_boardState.BlackCastlingRights == CastleTypeEnum.CastleKingSide)
				castlings += "k";
			else
				if (_boardState.BlackCastlingRights == CastleTypeEnum.CastleQueenSide)
				castlings += "q";

			return castlings;
		}

		private string ConvertEnPassante()
        {
			return _boardState.EnPassant;
        }
	}
}
