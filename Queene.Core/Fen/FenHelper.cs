using Queene.Core.Converters;
using Queene.Core.Enums;
using Queene.Core.Exceptions;
using System.Globalization;

namespace Queene.Core.Fen
{
    public static class FenHelper
	{
		public const string FEN_INITIAL_START_POSITION = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
		public const string FEN_ALLOWED_PIECES_CHARACTERS = "PRNBQKprnbqk/";
		public const string FEN_ALLOWED_CASTLES_CHARACTERS = "KQkq";
		public const string EMPTY_SECTION_CHARACTER = "-";

		public static BoardState CreateBoardState(string fen)
		{
			var result = new BoardState(new BoardStateToFenConverter());

			if (string.IsNullOrWhiteSpace(fen))
				throw new FenValidationException("Invalid Fen string");

			FenHelperValidator.Validate(fen);

			var sections = fen.Split(" ");

			HandlePieces(sections[0], result);
			HandlePlayer(sections[1], result);
			HandleCastlingRights(sections[2], result);
			HandleEnPassantSquare(sections[3], result);

			if (sections.Length >= 5 && byte.TryParse(sections[4], out byte halfMoveNumber))
				result.HalfMoveNumber = halfMoveNumber;

			if (sections.Length >= 6 && byte.TryParse(sections[5], out byte fullMoveNumber))
				result.FullMoveNumber = fullMoveNumber;

			return result;
		}

		private static string CheckAndAddNotRequiredSections(string fen)
		{
			var sections = fen.TrimEnd().Split(" ");

			if (sections.Length == 4)
				fen += "0 0";

			if (sections.Length == 5)
				fen += "0";

			return fen;
		}

		private static void HandleEnPassantSquare(string enPassanteSquarePart, BoardState result)
		{
			if (enPassanteSquarePart == EMPTY_SECTION_CHARACTER)
				return;

			result.EnPassant = enPassanteSquarePart;
		}

		private static void HandleCastlingRights(string castlingPart, BoardState result)
		{
			if (castlingPart == EMPTY_SECTION_CHARACTER)
			{
				result.WhiteCastlingRights = result.BlackCastlingRights = CastleTypeEnum.None;
				return;
			}

			foreach (var c in castlingPart)
			{
				switch(c)
				{
					case 'K':
						if (result.WhiteCastlingRights == CastleTypeEnum.None)
							result.WhiteCastlingRights = CastleTypeEnum.CastleKingSide;
						else result.WhiteCastlingRights = CastleTypeEnum.Both;
						break;
					case 'k':
						if (result.BlackCastlingRights == CastleTypeEnum.None)
							result.BlackCastlingRights = CastleTypeEnum.CastleKingSide;
						else result.BlackCastlingRights = CastleTypeEnum.Both;
						break;
					case 'Q':
						if (result.WhiteCastlingRights == CastleTypeEnum.None)
							result.WhiteCastlingRights = CastleTypeEnum.CastleQueenSide;
						else result.WhiteCastlingRights = CastleTypeEnum.Both;
						break;
					case 'q':
						if (result.BlackCastlingRights == CastleTypeEnum.None)
							result.BlackCastlingRights = CastleTypeEnum.CastleQueenSide;
						else result.BlackCastlingRights = CastleTypeEnum.Both;
						break;
				}
			}
		}

		private static void HandlePlayer(string playerPart, BoardState result)
		{
			result.TurnToMove = playerPart == "w" ? PlayerEnum.White : PlayerEnum.Black;
		}

		private static void HandlePieces(string piecesPart, BoardState result)
		{
			byte index = 63;

			foreach (var c in piecesPart)
			{
				if (char.IsDigit(c))
				{
					index -= byte.Parse(c.ToString(), CultureInfo.InvariantCulture.NumberFormat);
					continue;
				}

				switch (c)
				{
					case 'P':
						result.WhitePieces[PieceTypeEnum.Pawn].Add(index--);
						break;
					case 'p':
						result.BlackPieces[PieceTypeEnum.Pawn].Add(index--);
						break;
					case 'R':
						result.WhitePieces[PieceTypeEnum.Rook].Add(index--);
						break;
					case 'r':
						result.BlackPieces[PieceTypeEnum.Rook].Add(index--);
						break;
					case 'N':
						result.WhitePieces[PieceTypeEnum.Knight].Add(index--);
						break;
					case 'n':
						result.BlackPieces[PieceTypeEnum.Knight].Add(index--);
						break;
					case 'B':
						result.WhitePieces[PieceTypeEnum.Bishop].Add(index--);
						break;
					case 'b':
						result.BlackPieces[PieceTypeEnum.Bishop].Add(index--);
						break;
					case 'Q':
						result.WhitePieces[PieceTypeEnum.Queen].Add(index--);
						break;
					case 'q':
						result.BlackPieces[PieceTypeEnum.Queen].Add(index--);
						break;
					case 'K':
						result.WhitePieces[PieceTypeEnum.King].Add(index--);
						break;
					case 'k':
						result.BlackPieces[PieceTypeEnum.King].Add(index--);
						break;
					case '/':
						continue;
				}
			}
		}
	}
}
