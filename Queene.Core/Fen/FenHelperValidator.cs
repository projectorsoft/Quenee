using Queene.Core.Consts;
using Queene.Core.Exceptions;
using System.Globalization;
using System.Linq;

namespace Queene.Core.Fen
{
    public static class FenHelperValidator
    {
		public static readonly string InvalidFen = "Fen string should not be empty";
		public static readonly string InvalidFenFormat = "Fen string has invalid format";
		public static readonly string InvalidMoveSection = "'{0}' is not valid number";
		public static readonly string InvalidEnpassantSection = "Invalid enpassant section";
		public static readonly string InvalidPlayerSection = "Invalid player section";
		public static readonly string InvalidPiecesSection = "Invalid pieces section";
		public static readonly string InvalidCastlingSection = "Invalid castling section";

		private static char _player;

		public static void Validate(string fen)
		{
			if (string.IsNullOrWhiteSpace(fen))
				throw new FenValidationException(InvalidFen);

			var sections = fen.Trim().Split(" ");

			if (sections.Length < 4)
				throw new FenValidationException(InvalidFenFormat);

			VaidatePiecesSection(sections[0]);
			ValidatePlayer(sections[1]);
			ValidateCastleRights(sections[2]);
			ValidateEnPassantSquare(sections[3]);

			if (sections.Length >= 5)
				ValidateMove(sections[4]);

			if (sections.Length >= 6)
				ValidateMove(sections[5]);
		}

		private static void ValidateEnPassantSquare(string enPassantSection)
		{
			if (enPassantSection.Length > 2)
				throw new FenValidationException(InvalidEnpassantSection);

			if (enPassantSection.Length == 1 && enPassantSection[0] != '-')
				throw new FenValidationException(InvalidEnpassantSection);

			if (enPassantSection.Length == 2 && !BoardConsts.SQUARES_NAMES.Contains(enPassantSection.ToUpper(CultureInfo.InvariantCulture)))
				throw new FenValidationException(InvalidEnpassantSection);

			//if (enPassantSection[0] != '-')
			//{
			//	if (_player == 'w' && !BoardConsts.SQUARES_NAMES.Where(s => s[1] == '6').Contains(enPassantSection.ToUpper()))
			//		throw new FenValidationException(InvalidEnpassantSection);

			//	if (_player == 'b' && !BoardConsts.SQUARES_NAMES.Where(s => s[1] == '3').Contains(enPassantSection.ToUpper()))
			//		throw new FenValidationException(InvalidEnpassantSection);
			//}
		}

		private static void ValidateCastleRights(string castlesSection)
		{
			if (castlesSection.Length > 4)
				throw new FenValidationException(InvalidCastlingSection);

			if (castlesSection.Contains('-', System.StringComparison.InvariantCultureIgnoreCase) && castlesSection.Length != 1)
				throw new FenValidationException(InvalidCastlingSection);

			var result = true;

			foreach (var section in castlesSection)
			{
				result &= FenHelper.FEN_ALLOWED_CASTLES_CHARACTERS.Contains(section, System.StringComparison.InvariantCultureIgnoreCase);
			}

			//if (!result)
			//	throw new FenValidationException(InvalidCastlingSection);
		}

		private static void ValidatePlayer(string playerSection)
		{
			if (playerSection.Length != 1 || !"wb".Contains(playerSection, System.StringComparison.InvariantCultureIgnoreCase))
				throw new FenValidationException(InvalidPlayerSection);

			_player = playerSection.ToLower(CultureInfo.InvariantCulture)[0];
		}

		private static void VaidatePiecesSection(string piecesSection)
		{
			var parts = piecesSection.Split('/');

			if (parts.Length != 8)
				throw new FenValidationException(InvalidPiecesSection);

			var result = true;

			foreach (var c in piecesSection)
			{
				result &= (char.IsDigit(c) || FenHelper.FEN_ALLOWED_PIECES_CHARACTERS.Contains(c, System.StringComparison.InvariantCultureIgnoreCase));
			}

			if (!result)
				throw new FenValidationException(InvalidPiecesSection);

			var wKing = piecesSection.Where(x => x == 'K').Count();
			var bKing = piecesSection.Where(x => x == 'k').Count();

			if (wKing != 1 || bKing != 1)
				throw new FenValidationException(InvalidPiecesSection);

			foreach (var part in parts)
				ValidatePiecesSectionPart(part);
		}

		private static void ValidatePiecesSectionPart(string piecePart)
		{
			var count = 0;

			foreach (var c in piecePart)
			{
				if (char.IsDigit(c))
				{
					count += byte.Parse(c.ToString(), CultureInfo.InvariantCulture.NumberFormat);
				}
				else
					count++;
			}

			if (count != 8)
				throw new FenValidationException(InvalidPiecesSection);
		}

		private static void ValidateMove(string moveSection)
		{
			if (!byte.TryParse(moveSection, out _))
				throw new FenValidationException(string.Format(CultureInfo.InvariantCulture, InvalidMoveSection, nameof(moveSection)));
		}
	}
}
