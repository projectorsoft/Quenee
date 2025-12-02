using Queene.Core.Enums;
using System.Collections.Generic;

namespace Queene.Core.Consts
{
    internal static class BoardConsts
	{
		public const byte MAX_MOVES_NUMBER = 128;

		public const ulong W_PAWNS_STARTING_POS = 0x000000000000ff00;
		public const ulong W_ROOKS_STARTING_POS = 0x000000000000081;
		public const ulong W_BISHOPS_STARTING_POS = 0x000000000000024;
		public const ulong W_KNIGHTS_STARTING_POS = 0x000000000000042;
		public const ulong W_QUEENS_STARTING_POS = 0x0000000000000010;
		public const ulong W_KING_STARTING_POS = 0x0000000000000008;

		public const ulong B_PAWNS_STARTING_POS = 0x00ff000000000000;
		public const ulong B_ROOKS_STARTING_POS = 0x8100000000000000;
		public const ulong B_BISHOPS_STARTING_POS = 0x2400000000000000;
		public const ulong B_KNIGHTS_STARTING_POS = 0x4200000000000000;
		public const ulong B_QUEENS_STARTING_POS = 0x1000000000000000;
		public const ulong B_KING_STARTING_POS = 0x0800000000000000;

		public const ulong RANK_8_FULL_STATE = 0xff00000000000000;
		public const ulong RANK_7_FULL_STATE = 0xff000000000000;
		public const ulong RANK_6_FULL_STATE = 0xff0000000000;
		public const ulong RANK_5_FULL_STATE = 0xff00000000;
		public const ulong RANK_4_FULL_STATE = 0xff000000;
		public const ulong RANK_3_FULL_STATE = 0xff0000;
		public const ulong RANK_2_FULL_STATE = 0xff00;
		public const ulong RANK_1_FULL_STATE = 0xff;

		public static readonly ulong[] RankFullState = { 
			0x00000000000000FF, 0x000000000000FF00, 0x0000000000FF0000, 0x00000000FF000000,
			0x000000FF00000000, 0x0000FF0000000000, 0x00FF000000000000, 0xFF00000000000000
		};

		public static readonly ulong[] FileFullState = {
			0x8080808080808080, 0x4040404040404040, 0x2020202020202020, 0x1010101010101010,
			0x808080808080808 , 0x404040404040404, 0x202020202020202, 0x101010101010101
		};

		public static readonly ulong[] DiagFullState = {
			0x8040201008040201, 0x4020100804020100, 0x2010080402010000, 0x1008040201000000,
			0x0804020100000000, 0x0402010000000000, 0x0201000000000000, 0x0,
			0x80402010080402, 0x804020100804, 0x8040201008, 0x80402010,
			0x804020, 0x8040, 0x0
		};

		public static readonly ulong[] AntyDiagFullState = {
			0x0, 0x4080000000000000, 0x2040800000000000, 0x1020408000000000,
			0x810204080000000, 0x408102040800000, 0x204081020408000, 0x102040810204080,
			0x1020408102040, 0x10204081020, 0x102040810, 0x1020408,
			0x10204, 0x102, 0x0
		};

		public static readonly string[] SQUARES_NAMES = new string[] {
			"H1", "G1", "F1", "E1", "D1", "C1", "B1", "A1",
			"H2", "G2", "F2", "E2", "D2", "C2", "B2", "A2",
			"H3", "G3", "F3", "E3", "D3", "C3", "B3", "A3",
			"H4", "G4", "F4", "E4", "D4", "C4", "B4", "A4",
			"H5", "G5", "F5", "E5", "D5", "C5", "B5", "A5",
			"H6", "G6", "F6", "E6", "D6", "C6", "B6", "A6",
			"H7", "G7", "F7", "E7", "D7", "C7", "B7", "A7",
			"H8", "G8", "F8", "E8", "D8", "C8", "B8", "A8"
		};

		public static readonly byte[] SQUARES_REVERSED = 
		{
			63, 62, 61, 60, 59, 58, 57, 56,
			55, 54, 53, 52, 51, 50, 49, 48,
			47, 46, 45, 44, 43, 42, 41, 40,
			39, 38, 37, 36, 35, 34, 33, 32,
			31, 30, 29, 28, 27, 26, 25, 24,
			23, 22, 21, 20, 19, 18, 17, 16,
			15, 14, 13, 12, 11, 10, 9, 8,
			7, 6, 5, 4, 3, 2, 1, 0,
		};


		public static readonly byte[] SQUARES_FORWARD = 
		{
			 0,  0,  0,  0,  0,  0,  0,  0,
			16, 17, 18, 19, 20, 21, 22, 23,
			24, 25, 26, 27, 28, 29, 30, 31,
			32, 33, 34, 35, 36, 37, 38, 39,
			40, 41, 42, 43, 44, 45, 46, 47,
			48, 49, 50, 51, 52, 53, 54, 55,
			56, 57, 58, 59, 60, 61, 62, 63,
			 0,  0,  0,  0,  0,  0,  0,  0,
		};

		public static readonly byte[] SQUARES_BACKWARD = 
		{
			0, 0, 0, 0, 0, 0, 0, 0,
			0, 1, 2, 3, 4, 5, 6, 7,
			8,  9, 10, 11, 12, 13, 14, 15,
			16, 17, 18, 19, 20, 21, 22, 23,
			24, 25, 26, 27, 28, 29, 30, 31,
			32, 33, 34, 35, 36, 37, 38, 39,
			40, 41, 42, 43, 44, 45, 46, 47,
			63, 63, 63, 63, 63, 63, 63, 63
		};

		public static readonly byte[] RANK_FROM_SQUARE =
		{
			0, 0, 0, 0, 0, 0, 0, 0,
			1, 1, 1, 1, 1, 1, 1, 1,
			2, 2, 2, 2, 2, 2, 2, 2,
			3, 3, 3, 3, 3, 3, 3, 3,
			4, 4, 4, 4, 4, 4, 4, 4,
			5, 5, 5, 5, 5, 5, 5, 5,
			6, 6, 6, 6, 6, 6, 6, 6,
			7, 7, 7, 7, 7, 7, 7, 7,
		};

		public static readonly byte[] FILE_FROM_SQUARE =
		{
			7, 6, 5, 4, 3, 2, 1, 0,
			7, 6, 5, 4, 3, 2, 1, 0,
			7, 6, 5, 4, 3, 2, 1, 0,
			7, 6, 5, 4, 3, 2, 1, 0,
			7, 6, 5, 4, 3, 2, 1, 0,
			7, 6, 5, 4, 3, 2, 1, 0,
			7, 6, 5, 4, 3, 2, 1, 0,
			7, 6, 5, 4, 3, 2, 1, 0,
		};

		public static readonly Dictionary<PieceTypeEnum, string> PIECE_TYPE_SYMBOL = new Dictionary<PieceTypeEnum, string>
		{
			{ PieceTypeEnum.Empty, "" },
			{ PieceTypeEnum.Pawn, "P" },
			{ PieceTypeEnum.Knight, "N" },
			{ PieceTypeEnum.Bishop, "B" },
			{ PieceTypeEnum.Rook, "R" },
			{ PieceTypeEnum.Queen, "Q" },
			{ PieceTypeEnum.King, "K" },
			{ PieceTypeEnum.All, "" }
		};
	}
}
