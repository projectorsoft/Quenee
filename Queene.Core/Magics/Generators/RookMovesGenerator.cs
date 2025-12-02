using System;

namespace QueeneEngine.Engine.Magics.Generators
{
	public static class RookMovesGenerator
    {
		public static UInt64 GetMask(int square)
		{
			UInt64 result = 0UL;
			int rk = square / 8, fl = square % 8, r, f;

			for (r = rk + 1; r <= 6; r++) result |= (1UL << (fl + r * 8));

			for (r = rk - 1; r >= 1; r--) result |= (1UL << (fl + r * 8));

			for (f = fl + 1; f <= 6; f++) result |= (1UL << (f + rk * 8));

			for (f = fl - 1; f >= 1; f--) result |= (1UL << (f + rk * 8));

			return result;
		}

		public static UInt64 AttackFromSquare(int square, UInt64 block)
		{
			UInt64 result = 0UL;
			int rk = square / 8, fl = square % 8, r, f;

			for (r = rk + 1; r <= 7; r++)
			{
				result |= (1UL << (fl + r * 8));
				if (((ulong)block & (ulong)(1UL << (fl + r * 8))) != 0)
					break;
			}

			for (r = rk - 1; r >= 0; r--)
			{
				result |= (1UL << (fl + r * 8));
				if (((ulong)block & (ulong)(1UL << (fl + r * 8))) != 0)
					break;
			}

			for (f = fl + 1; f <= 7; f++)
			{
				result |= (1UL << (f + rk * 8));
				if (((ulong)block & (ulong)(1UL << (f + rk * 8))) != 0)
					break;
			}

			for (f = fl - 1; f >= 0; f--)
			{
				result |= (1UL << (f + rk * 8));
				if (((ulong)block & (ulong)(1UL << (f + rk * 8))) != 0)
					break;
			}

			return result;
		}
	}
}
