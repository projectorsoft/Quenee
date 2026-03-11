using Queene.Core.Consts;
using Queene.Core.Converters;
using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Pieces;
using Queene.Core.MovesGenerating.PiecesList;
using QueeneEngine.Helpers.Bitwise;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Queene.Core.MovesGenerating
{
    public class BoardContext
	{
		public ulong[][] Pieces { get; }
		public ulong EmptySquares { get; set; }
		public ulong OccupiedSquares { get; set; }
		public byte? EnPassantSquare { get; set; }
		public ulong Attackers { get; set; }
		public ulong CheckedSquares { get; set; }
		public ulong Pinners { get; set; }
		public ulong PinnedSquares { get; set; }
		public Player Player { get; }
		public bool[] CanCastle { get; }
		public bool[] CanCastleKingSide { get; }
		public bool[] CanCastleQueenSide { get; }
		
		public byte FullMoveNumber { get; set; }
		public byte HalfMoveNumber { get; set; }

		public PieceTypeList[][] PieceTypeList { get; }
		public PieceIndex[][] PieceIndices { get; }

		public ulong OpponentRooksAndQueens { get; private set; }
		public ulong OpponentBishopsAndQueens { get; private set; }
		public ulong Hash {  get; set; }


		private readonly IBitBoardContextConverter _bitBoardContextConverter;

		public BoardContext(IBitBoardContextConverter bitBoardContextConverter)
		{
			_bitBoardContextConverter = bitBoardContextConverter;

			Pieces = new ulong[2][];
			Pieces[0] = new ulong[8];
			Pieces[1] = new ulong[8];

			PieceTypeList = new PieceTypeList[2][];
			PieceTypeList[0] = new PieceTypeList[6];
			PieceTypeList[1] = new PieceTypeList[6];

			PieceIndices = new PieceIndex[2][];
			PieceIndices[0] = new PieceIndex[64];
			PieceIndices[1] = new PieceIndex[64];

			CanCastle = new bool[2];
			CanCastleKingSide = new bool[2];
			CanCastleQueenSide = new bool[2];

			CanCastle[0] = CanCastle[1] = false;
			CanCastleKingSide[0] = CanCastleKingSide[1] = false;
			CanCastleQueenSide[0] = CanCastleQueenSide[1] = false;

			Player = new Player(PlayerEnum.White);

			InitPiecesList();
		}

		public bool IsCheck => Attackers > 0;

		public void InitPiecesList()
        {
			for (int square = 0; square < 64; square++)
			{
				PieceIndices[(byte)PlayerEnum.Black][square] = null;
				PieceIndices[(byte)PlayerEnum.White][square] = null;
			}

			for (PieceTypeEnum pieceType = PieceTypeEnum.Knight; pieceType <= PieceTypeEnum.King; pieceType++)
			{
				InitPiecesList(PlayerEnum.Black, pieceType);
				InitPiecesList(PlayerEnum.White, pieceType);
			}
		}

		public void InitPiecesList(PlayerEnum player, PieceTypeEnum pieceType)
		{
			var piecesList = PieceTypeList[(byte)player][(byte)pieceType] = new PieceTypeList(pieceType);
			ulong mask = Pieces[(byte)player][(byte)pieceType];

			byte square;

			while (mask > 0)
			{
				square = BitwiseHelper.FastBitScanForward(mask);

				piecesList.Add(square);
				PieceIndices[(byte)player][square] = new PieceIndex(pieceType, piecesList.GetLastIndex());

				mask ^= Powers.powersOfTwo[square];
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetAllCastlesAllowance(byte player, bool value)
		{
			CanCastle[player] = CanCastleKingSide[player] = CanCastleQueenSide[player] = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetCastleAllowance(byte player, bool value)
        {
			CanCastle[player] = value;
		}

		//Only when rook moved
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetKingSideCastleAllowance(byte player, bool value)
		{
			CanCastleKingSide[player] = value;
        }

		//Only when rook moved
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetQueenSideCastleAllowance(byte player, bool value)
		{
			CanCastleQueenSide[player] = value;
		}

		public void FixCastlingRights()
        {
			FixCastlingRights(Player.Current);
			FixCastlingRights(Player.Oponnent);
		}

		private void FixCastlingRights(byte player)
        {
			if (PieceTypeList[player][(byte)PieceTypeEnum.King].GetAtIndex(0) != King.InitialSquare[player])
				SetAllCastlesAllowance(player, false);
			else
			{
				var rooks = BitwiseHelper.GetAllSetBitsInMask(Pieces[player][(byte)PieceTypeEnum.Rook], out var _);
				if (!rooks.Any(s => s == Rook.KingSideSourceSquare[player]))
					SetKingSideCastleAllowance(player, false);

				if (!rooks.Any(s => s == Rook.QueenSideSourceSquare[player]))
					SetQueenSideCastleAllowance(player, false);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public PieceTypeEnum GetPieceType(int square, byte playerByte)
		{
            var pieceIndex = PieceIndices[playerByte][square];

            if (pieceIndex != null)
                return pieceIndex.PieceType;

            return PieceTypeEnum.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PieceTypeEnum? GetCapturedPieceType(int square, byte playerByte)
        {
            if (BitwiseHelper.IsSet(Pieces[playerByte][(byte)PieceTypeEnum.All], square))
                return GetPieceType(square, playerByte);

            return null;
        }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetOpponnentSliders()
		{
			OpponentRooksAndQueens = Pieces[Player.Oponnent][(byte)PieceTypeEnum.Rook] | Pieces[Player.Oponnent][(byte)PieceTypeEnum.Queen];
			OpponentBishopsAndQueens = Pieces[Player.Oponnent][(byte)PieceTypeEnum.Bishop] | Pieces[Player.Oponnent][(byte)PieceTypeEnum.Queen];
		}

		public string PrintPieces()
		{
			var sb = new StringBuilder();

			for (PlayerEnum player = PlayerEnum.Black; player <= PlayerEnum.White; player++)
			{
				sb.AppendLine();
                sb.AppendLine($"{player.ToString()}:");
				for (PieceTypeEnum pieceType = PieceTypeEnum.Knight; pieceType <= PieceTypeEnum.King; pieceType++)
					sb.AppendLine(PieceTypeList[(byte)player][(byte)pieceType].ToString());
			}

			return sb.ToString();
        }

        public string PrintPiecesIndicies()
        {
            var sb = new StringBuilder();

            for (PlayerEnum player = PlayerEnum.Black; player <= PlayerEnum.White; player++)
            {
                sb.AppendLine();
                sb.AppendLine($"{player.ToString()}:");
                for (int i=0; i < 64; i++)
					if (PieceIndices[(byte)player][i] != null)
						sb.AppendLine(PieceIndices[(byte)player][i].ToString());
            }

            return sb.ToString();
        }

        public override string ToString()
		{
			var boardState = _bitBoardContextConverter?.Convert(this);

			return boardState.ToString();
		}
    }
}
