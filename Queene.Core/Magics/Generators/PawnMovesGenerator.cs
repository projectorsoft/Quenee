using System;

namespace QueeneEngine.Engine.Magics.Generators
{
	internal static class PawnMovesGenerator
	{
		public static ulong[] GeneratetWhiteMoves()
		{
			ulong[] result = new ulong[64];
            for (UInt64 i = 0; i < 64; i++)
			{
                ulong moves = 0;

                if (i > 7 && i < 57)
				{
					if (i >= 8 && i < 16)
					{
						moves |= (UInt64)Math.Pow(2, i + 8);
						moves |= (UInt64)Math.Pow(2, i + 16);
					}
					else
						moves |= (UInt64)Math.Pow(2, i + 8);
				}
				result[i] = moves;
			}

			return result;
		}

		public static ulong[] GeneratetWhiteCaptures()
		{
			ulong[] result = new ulong[64];
            for (UInt64 i = 0; i < 64; i++)
			{
                ulong moves = 0;
                if (i >= 0 && i < 57)
				{
					if (i % 8 == 0)
					{
						moves |= (UInt64)Math.Pow(2, i + 9);
					}
					else
						if (i % 8 < 7)
						{
							moves |= (UInt64)Math.Pow(2, i + 7);
							moves |= (UInt64)Math.Pow(2, i + 9);
						}
						else
							moves |= (UInt64)Math.Pow(2, i + 7);
				}
				result[i] = moves;
			}

			return result;
		}

		public static ulong[] GeneratetBlackMoves()
		{
			ulong[] result = new ulong[64];
            for (UInt64 i = 0; i < 64; i++)
			{
                ulong moves = 0;

                if (i > 7 && i < 56)
				{
					if (i >= 48 && i <= 55)
					{
						moves |= (UInt64)Math.Pow(2, i - 8);
						moves |= (UInt64)Math.Pow(2, i - 16);
					}
					else
						moves |= (UInt64)Math.Pow(2, i - 8);
				}
				result[i] = moves;
			}

			return result;
		}

		public static ulong[] GeneratetBlackCaptures()
		{
			ulong[] result = new ulong[64];
            for (UInt64 i = 0; i < 64; i++)
			{
                ulong moves = 0;
                if (i > 7 && i <= 64)
				{
					if (i % 8 == 0)
					{
						moves |= (UInt64)Math.Pow(2, i - 7);
					}
					else
						if (i % 8 < 7)
						{
							moves |= (UInt64)Math.Pow(2, i - 7);
							moves |= (UInt64)Math.Pow(2, i - 9);
						}
						else
							moves |= (UInt64)Math.Pow(2, i - 9);
				}
				result[i] = moves;
			}

			return result;
		}
	}
}
