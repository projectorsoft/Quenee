using System;

namespace QueeneEngine.Engine.Magics.Generators
{
	public static class BishopMovesGenerator
    {
		public static UInt64 GetMask(int square)
		{
			UInt64 result = 0UL;
			int rk = square / 8, fl = square % 8, r, f;

			for (r = rk + 1, f = fl + 1; r <= 6 && f <= 6; r++, f++) result |= (1UL << (f + r * 8));

			for (r = rk + 1, f = fl - 1; r <= 6 && f >= 1; r++, f--) result |= (1UL << (f + r * 8));

			for (r = rk - 1, f = fl + 1; r >= 1 && f <= 6; r--, f++) result |= (1UL << (f + r * 8));

			for (r = rk - 1, f = fl - 1; r >= 1 && f >= 1; r--, f--) result |= (1UL << (f + r * 8));

			return result;
		}

		public static UInt64 AttackFromSquare(int square, UInt64 block)
		{
			UInt64 result = 0UL;
			int rk = square / 8, fl = square % 8, r, f;

			for (r = rk + 1, f = fl + 1; r <= 7 && f <= 7; r++, f++)
			{
				result |= (1UL << (f + r * 8));
				if (((ulong)block & (1UL << (f + r * 8))) != 0)
					break;
			}

			for (r = rk + 1, f = fl - 1; r <= 7 && f >= 0; r++, f--)
			{
				result |= (1UL << (f + r * 8));
				if (((ulong)block & (1UL << (f + r * 8))) != 0)
					break;
			}

			for (r = rk - 1, f = fl + 1; r >= 0 && f <= 7; r--, f++)
			{
				result |= (1UL << (f + r * 8));
				if (((ulong)block & (1UL << (f + r * 8))) != 0)
					break;
			}

			for (r = rk - 1, f = fl - 1; r >= 0 && f >= 0; r--, f--)
			{
				result |= (1UL << (f + r * 8));
				if (((ulong)block & (1UL << (f + r * 8))) != 0)
					break;
			}

			return result;
		}
	}
}
