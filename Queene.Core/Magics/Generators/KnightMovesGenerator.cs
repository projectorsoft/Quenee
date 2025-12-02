using System;

namespace QueeneEngine.Engine.Magics.Generators
{
	internal static class KnightMovesGenerator
    {
		public static ulong[] GeneratetMoves()
		{
			ulong[] result = new ulong[64];
            for (ulong i = 0; i < 64; i++)
			{
                ulong moves = 0;

                int tmp = (int)i % 8;

				if ((int)(i - 6) >= 0 && Math.Abs(tmp - ((int)(i - 6) % 8)) < 6)
					moves |= (ulong)Math.Pow(2, i - 6);
				if ((int)(i - 10) >= 0 && Math.Abs(tmp - ((int)(i - 10) % 8)) < 6)
					moves |= (ulong)Math.Pow(2, i - 10);
				if ((int)(i - 15) >= 0 && Math.Abs(tmp - ((int)(i - 15) % 8)) < 6)
					moves |= (ulong)Math.Pow(2, i - 15);
				if ((int)(i - 17) >= 0 && Math.Abs(tmp - ((int)(i - 17) % 8)) < 6)
					moves |= (ulong)Math.Pow(2, i - 17);
				if ((int)(i + 6) < 64 && Math.Abs(tmp - ((int)(i + 6) % 8)) < 6)
					moves |= (ulong)Math.Pow(2, i + 6);
				if ((int)(i + 10) < 64 && Math.Abs(tmp - ((int)(i + 10) % 8)) < 6)
					moves |= (ulong)Math.Pow(2, i + 10);
				if ((int)(i + 15) < 64 && Math.Abs(tmp - ((int)(i + 15) % 8)) < 6)
					moves |= (ulong)Math.Pow(2, i + 15);
				if ((int)(i + 17) < 64 && Math.Abs(tmp - ((int)(i + 17) % 8)) < 6)
					moves |= (ulong)Math.Pow(2, i + 17);

				result[i] = moves;
			}

			return result;
		}
	}
}
