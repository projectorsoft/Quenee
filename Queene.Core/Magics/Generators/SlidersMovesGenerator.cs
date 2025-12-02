using Queene.Core.Enums;

namespace QueeneEngine.Engine.Magics.Generators
{
	internal static class SlidersMovesGenerator
    {
		// positve left, negative right shifts
		private static readonly int[] shift = { 9, 1, -7, -8, -9, -1, 7, 8 };

		private static readonly ulong[] avoidWrap =
		{
		   0xfefefefefefefe00,
		   0xfefefefefefefefe,
		   0x00fefefefefefefe,
		   0x00ffffffffffffff,
		   0x007f7f7f7f7f7f7f,
		   0x7f7f7f7f7f7f7f7f,
		   0x7f7f7f7f7f7f7f00,
		   0xffffffffffffff00,
		};

		//public static ulong GenerateMovesByDirection(ulong sliders, ulong empty, AttackDirectionsEnum dir8)
		//{
		//	ulong fill = OccludedFill(sliders, empty, (int)dir8);

		//	return ShiftOne(fill, (int)dir8);
		//}

		private static ulong OccludedFill(ulong sliders, ulong empty, int dir8)
		{
			int r = shift[dir8]; // {+-1,7,8,9}

			empty &= avoidWrap[dir8];
			sliders |= empty & RotateLeft(sliders, r);

			empty &= RotateLeft(empty, r);
			sliders |= empty & RotateLeft(sliders, 2 * r);

			empty &= RotateLeft(empty, 2 * r);
			sliders |= empty & RotateLeft(sliders, 4 * r);

			return sliders;
		}

		private static ulong ShiftOne(ulong bitboard, int dir8)
		{
			int r = shift[dir8]; // {+-1,7,8,9}

			return RotateLeft(bitboard, r) & avoidWrap[dir8];
		}

		private static ulong RotateLeft(ulong bitboard, int shift)
		{
			return (bitboard << shift) | (bitboard >> (64 - shift));
		}
	}
}
