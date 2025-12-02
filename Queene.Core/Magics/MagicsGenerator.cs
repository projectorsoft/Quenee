using Queene.Core.Enums;
using Queene.Core.Magics;
using QueeneEngine.Common;
using QueeneEngine.Engine.Magics.Generators;
using QueeneEngine.Engine.Utils;
using QueeneEngine.Helpers.Bitwise;
using System;

namespace QueeneEngine.Engine.Magics
{
    public static class MagicsGenerator
    {
        private static readonly byte[] _rookBits = {
          12, 11, 11, 11, 11, 11, 11, 12,
          11, 10, 10, 10, 10, 10, 10, 11,
          11, 10, 10, 10, 10, 10, 10, 11,
          11, 10, 10, 10, 10, 10, 10, 11,
          11, 10, 10, 10, 10, 10, 10, 11,
          11, 10, 10, 10, 10, 10, 10, 11,
          11, 10, 10, 10, 10, 10, 10, 11,
          12, 11, 11, 11, 11, 11, 11, 12
        };

        private static readonly byte[] _bishopBits = {
          6, 5, 5, 5, 5, 5, 5, 6,
          5, 5, 5, 5, 5, 5, 5, 5,
          5, 5, 7, 7, 7, 7, 5, 5,
          5, 5, 7, 9, 9, 7, 5, 5,
          5, 5, 7, 9, 9, 7, 5, 5,
          5, 5, 7, 7, 7, 7, 5, 5,
          5, 5, 5, 5, 5, 5, 5, 5,
          6, 5, 5, 5, 5, 5, 5, 6
        };

        public static MagicResult[] ComputeMagics(SliderTypeEnum sliderType)
        {
            var magics = new MagicResult[64];

            for (byte square = 0; square < 64; square++)
                magics[square] = ComputeMagicResult(square, sliderType);

            return magics;
        }

        private static MagicResult ComputeMagicResult(byte square, SliderTypeEnum sliderType)
        {
            var mask = sliderType == SliderTypeEnum.Bishop ? BishopMovesGenerator.GetMask(square) : RookMovesGenerator.GetMask(square);
            var maskOnesCount = BitwiseHelper.CountOnes(mask);

            var permutationsCount = PermutationsGenerator.PermutationsCount(maskOnesCount);
            var permutations = PermutationsGenerator.GenerateMaskPermutations(mask).ToArray();
            var Used = new UInt64[permutationsCount];
            var attacks = GenerateAttacks(square, permutations, permutationsCount, sliderType);

            for (int k = 0; k < 100000000; k++)
            {
                var magic = GenerateRandomNumber();
                var count = BitwiseHelper.CountOnes((mask * magic) & 0xFF00000000000000UL);

                if (count < 6) continue;

                for (int i = 0; i < permutationsCount; i++)
                    Used[i] = 0UL;

                var fail = false;

                for (int i = 0; !fail && i < permutationsCount; i++)
                {
                    var bits = sliderType == SliderTypeEnum.Bishop ? _bishopBits[square] : _rookBits[square];
                    var index = Transform(permutations[i], magic, bits);

                    if (Used[index] == 0)
                        Used[index] = attacks[i];
                    else
                        if (Used[index] != attacks[i])
                        fail = true;
                }

                if (!fail)
                    return new MagicResult
                    {
                        Attacks = Used,
                        Magic = magic,
                        Mask = mask,
                        Shift =(byte)(64 - maskOnesCount)
                    };
            }

            return null;
        }

        private static int Transform(UInt64 mask, UInt64 magic, byte bits) => (int)((mask * magic) >> (64 - bits));

        private static ulong GenerateRandomNumber()
        {
            var rnd = new Random();

            return rnd.NextULong() & rnd.NextULong() & rnd.NextULong();
        }

        private static ulong[] GenerateAttacks(byte square, ulong[] permutations, int permutationsCount, SliderTypeEnum sliderType)
        {
            var attacks = new ulong[permutationsCount];

            for (int i = 0; i < permutationsCount; i++)
                attacks[i] = sliderType
                    == SliderTypeEnum.Bishop ? BishopMovesGenerator.AttackFromSquare(square, permutations[i]) : RookMovesGenerator.AttackFromSquare(square, permutations[i]);

            return attacks;
        }
    }
}
