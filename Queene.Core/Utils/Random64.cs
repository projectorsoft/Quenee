using System;

namespace QueeneEngine.Common
{
	public static class Random64
    {
		public static ulong NextULong(this Random rnd)
		{
			var part1 = (ulong)rnd.Next() << 32;
			var part2 = (ulong)rnd.Next();

			return part1 | part2;
		}
    }
}
