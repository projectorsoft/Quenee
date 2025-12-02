using QueeneEngine.Helpers.Bitwise;
using System.Collections.Generic;
using System.Linq;

namespace QueeneEngine.Engine.Utils
{
    public static class PermutationsGenerator
    {
        public static int PermutationsCount(int n) => 1 << n;

        /// <summary>
        /// Generates a list of bitboard permutations (but only these bits where mask was set).
        /// </summary>
        /// <param name="mask">The mask (bits where permutation will be applied)</param>
        /// <returns>The list of the permutations for the specified mask.</returns>
        public static List<ulong> GenerateMaskPermutations(ulong mask)
        {
            var permutations = new List<ulong>();

            var bitIndexes = BitwiseHelper.GetAllSetBitsInMask(mask, out var count)
                                 .Take(count)
                                 .ToList();

            var permutationsCount = 1 << count;

            for (var i = 0; i < permutationsCount; i++)
                permutations.Add(GeneratePermutation((ulong)i, bitIndexes));

            return permutations;
        }

        private static ulong GeneratePermutation(ulong mask, List<byte> bitIndexes)
        {
            var permutation = 0ul;

            while (mask != 0)
            {
                var index = BitwiseHelper.FastBitScanForward(mask);
                mask = BitwiseHelper.ClearBitAtIndex(mask, index);

                permutation |= 1ul << bitIndexes[index];
            }

            return permutation;
        }
    }
}
