using Queene.Core.Consts;
using System.Collections.Generic;
using System.Numerics;

namespace QueeneEngine.Helpers.Bitwise
{
    public static class BitwiseHelper {

		// methods of mask scanning
		public enum EnumBitscanMethod { Forward, Revers }

		private delegate byte FastBitScanMethodDelegate(ulong mask);

		private static FastBitScanMethodDelegate _scanMethod = FastBitScanForward;

		// used in Eugene Nalimov's bitScanReverse
		private static readonly int[] _ms1BTable = new int[256];

		// const used for the debrujing algorithm
		private const ulong Debruijn64 = 0x07EDD5E59A4E28C2;

		// array for the bitscan forward routine
		private readonly static byte[] _debruijn64Array = new byte[64] 
		{
			63, 0, 58, 1, 59, 47, 53, 2, 
			60, 39, 48, 27, 54, 33, 42, 3,
			61, 51, 37, 40, 49, 18, 28, 20,
			55, 30, 34, 11, 43, 14, 22, 4,
			62, 57, 46, 52, 38, 26, 32, 41,
			50, 36, 17, 19, 29, 10, 13, 21,
			56, 45, 25, 31, 35, 16, 9, 12,
			44, 24, 15, 8, 23, 7, 6, 5
		};

		static BitwiseHelper() 
		{
			InitMS1Table ();
		}

		private static void InitMS1Table()
		{
			for (int i = 0; i < 256; i++) {
				_ms1BTable[i] = (
					(i > 127) ? 7 :
					(i > 63) ? 6 :
					(i > 31) ? 5 :
					(i > 15) ? 4 :
					(i > 7) ? 3 :
					(i > 3) ? 2 :
					(i > 1) ? 1 : 0);
			}
		}

		/// <summary>
		/// Gets the index of the only one 1 inf bitbard
		/// </summary>
		/// <param name="mask">binary number</param>
		/// <returns>index</returns>
		public static byte FastBitScanForward(ulong mask)
		{
            return (byte)BitOperations.TrailingZeroCount(mask);
            //return _debruijn64Array[(((ulong)((long)mask & -(long)mask)) * Debruijn64) >> 58];
        }

		/// <summary>
		/// Gets the index of most siginificant bit set to 1
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		public static byte FastBitscanRevers(ulong mask)
		{
            return (byte)BitOperations.TrailingZeroCount(mask);

            // this is Eugene Nalimov's bitScanReverse
            // use firstOne if you can, it is faster than lastOne.
            // don't use this if bitmap = 0

            byte result = 0;
            if (mask > 0xFFFFFFFF)
            {
                mask >>= 32;
                result = 32;
            }
            if (mask > 0xFFFF)
            {
                mask >>= 16;
                result += 16;
            }
            if (mask > 0xFF)
            {
                mask >>= 8;
                result += 8;
            }
            return (byte)(result + _ms1BTable[mask]);
        }

        /// <summary>
        /// Set bits in number according to mask
        /// </summary>
        /// <param name="number"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static ulong SetBits(ulong number, ulong mask)
		{
			return number |= mask;
		}

		public static ulong SetBitAtIndex(ulong mask, byte index)
		{
			return mask |= Powers.powersOfTwo[index];
		}

		/// <summary>
		/// Clear bits in number according to mask
		/// </summary>
		/// <param name="number"></param>
		/// <param name="mask"></param>
		/// <returns></returns>
		public static ulong ClearBits(ulong number, ulong mask)
		{
			return number &= ~mask;
		}

		public static ulong ClearBitAtIndex(ulong mask, byte index)
		{
			return mask &= ~Powers.powersOfTwo[index];
		}

		/// <summary>
		/// Checks if bit on index position is set in mask
		/// </summary>
		/// <param name="mask"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static bool IsSet(ulong mask, int index)
		{
			return (mask & Powers.powersOfTwo[index]) != 0;
		}

		/// <summary>
		/// Get numbers of set bits in mask
		/// </summary>
		/// <param name="mask">binary number</param>
		/// <returns></returns>
		public static byte[] GetAllSetBitsInMask(ulong mask, out byte count)
		{
			byte[] result = new byte[64];
			byte dst = count = 0;

			while (mask > 0)
			{
				dst = _scanMethod (mask);
				result[count++] = dst;

				mask ^= Powers.powersOfTwo[dst];
			}

			return result;
		}

		/// <summary>
		/// Return number of 1's bits in mask
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		public static byte CountOnes(ulong mask)
		{
			byte dst, count = 0;

			while (mask > 0)
			{
				dst = _scanMethod (mask);
				count++;

				mask ^= Powers.powersOfTwo[dst];
			}

			return count;
		}

		public static bool IsMoreThanOneSetBits(ulong mask)
		{
			byte dst = _scanMethod(mask);

			mask ^= Powers.powersOfTwo[dst];

			return mask > 0;
		}

		public static ulong SetMask(List<byte> indexes)
		{
			ulong mask = 0;

			foreach (var square in indexes)
				mask |= Powers.powersOfTwo[square];

			return mask;
		}
	}
}