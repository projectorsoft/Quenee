using System;

namespace QueeneEngine.Engine.Magics.Generators
{
	internal static class KingMovesGenerator
    {
		public static ulong[] GeneratetMoves()
		{
			ulong[] result = new ulong[64];
            for (ulong i = 0; i < 64; i++)
			{
                ulong moves = 0;

                int tmp = (int)i % 8;

				if ((int)(i - 1) >= 0 && Math.Abs(tmp - ((int)(i - 1) % 8)) < 2)
					moves |= (ulong)Math.Pow(2, i - 1);
				if ((int)(i - 7) >= 0 && Math.Abs(tmp - ((int)(i - 7) % 8)) < 2)
					moves |= (ulong)Math.Pow(2, i - 7);
				if ((int)(i - 8) >= 0 && Math.Abs(tmp - ((int)(i - 8) % 8)) < 2)
					moves |= (ulong)Math.Pow(2, i - 8);
				if ((int)(i - 9) >= 0 && Math.Abs(tmp - ((int)(i - 9) % 8)) < 2)
					moves |= (ulong)Math.Pow(2, i - 9);
				if ((int)(i + 1) < 64 && Math.Abs(tmp - ((int)(i + 1) % 8)) < 2)
					moves |= (ulong)Math.Pow(2, i + 1);
				if ((int)(i + 7) < 64 && Math.Abs(tmp - ((int)(i + 7) % 8)) < 2)
					moves |= (ulong)Math.Pow(2, i + 7);
				if ((int)(i + 8) < 64 && Math.Abs(tmp - ((int)(i + 8) % 8)) < 2)
					moves |= (ulong)Math.Pow(2, i + 8);
				if ((int)(i + 9) < 64 && Math.Abs(tmp - ((int)(i + 9) % 8)) < 2)
					moves |= (ulong)Math.Pow(2, i + 9);

				result[i] = moves;
			}

			return result;
		}
	}
}
