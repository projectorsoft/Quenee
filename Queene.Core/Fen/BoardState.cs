using Queene.Core.Converters;
using Queene.Core.Enums;
using System;
using System.Collections.Generic;

namespace Queene.Core.Fen
{
    public class BoardState
	{
		public PlayerEnum TurnToMove { get; set; }
		public CastleTypeEnum WhiteCastlingRights { get; set; }
		public CastleTypeEnum BlackCastlingRights { get; set; }
		public string EnPassant { get; set; }
		public byte HalfMoveNumber { get; set; }
		public byte FullMoveNumber { get; set; }

		public Dictionary<PieceTypeEnum, List<byte>> WhitePieces { get; set; }
		public Dictionary<PieceTypeEnum, List<byte>> BlackPieces { get; set; }

		private readonly IBoardStateConverter _boardStateConverter;

		public BoardState(IBoardStateConverter boardStateConverter)
		{
			_boardStateConverter = boardStateConverter;

			WhitePieces = new Dictionary<PieceTypeEnum, List<byte>>();
			BlackPieces = new Dictionary<PieceTypeEnum, List<byte>>();

			foreach (int value in Enum.GetValues(typeof(PieceTypeEnum)))
			{
				var e = (PieceTypeEnum)value;

				WhitePieces.Add(e, new List<byte>());
				BlackPieces.Add(e, new List<byte>());
			}

			WhiteCastlingRights = BlackCastlingRights = CastleTypeEnum.None;
		}

        public override string ToString()
        {
			return _boardStateConverter.Convert(this);
		}
    }
}
