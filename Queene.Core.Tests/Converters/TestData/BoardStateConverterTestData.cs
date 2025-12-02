using Queene.Core.Enums;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.Converters.TestData
{
    public class BoardStateConverterTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                //starting position
                new Dictionary<PieceTypeEnum, List<byte>>
                {
                    { PieceTypeEnum.Pawn, new List<byte> { 8, 9, 10, 11, 12, 13, 14, 15 } },
                    { PieceTypeEnum.Rook, new List<byte> { 0, 7 } },
                    { PieceTypeEnum.Knight, new List<byte> { 1, 6 } },
                    { PieceTypeEnum.Bishop, new List<byte> { 2, 5 } },
                    { PieceTypeEnum.Queen, new List<byte> { 4 } },
                    { PieceTypeEnum.King, new List<byte> { 3 } }
                },
                new Dictionary<PieceTypeEnum, List<byte>>
                {
                    { PieceTypeEnum.Pawn, new List<byte> { 48, 49, 50, 51, 52, 53, 54, 55 } },
                    { PieceTypeEnum.Rook, new List<byte> { 56, 63 } },
                    { PieceTypeEnum.Knight, new List<byte> { 57, 62 } },
                    { PieceTypeEnum.Bishop, new List<byte> { 58, 61 } },
                    { PieceTypeEnum.Queen, new List<byte> { 60 } },
                    { PieceTypeEnum.King, new List<byte> { 59 } }
                },
                "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR",
            };

            yield return new object[]
            {
                new Dictionary<PieceTypeEnum, List<byte>>
                {
                    { PieceTypeEnum.Pawn, new List<byte> { 8, 9, 10, 13, 14, 15, 27, 36 } },
                    { PieceTypeEnum.Rook, new List<byte> { 0, 7 } },
                    { PieceTypeEnum.Knight, new List<byte> { 21, 35 } },
                    { PieceTypeEnum.Bishop, new List<byte> { 11, 12 } },
                    { PieceTypeEnum.Queen, new List<byte> { 18 } },
                    { PieceTypeEnum.King, new List<byte> { 3 } }
                },
                new Dictionary<PieceTypeEnum, List<byte>>
                {
                    { PieceTypeEnum.Pawn, new List<byte> { 50, 52, 53, 55, 41, 43, 30, 16 } },
                    { PieceTypeEnum.Rook, new List<byte> { 56, 63 } },
                    { PieceTypeEnum.Knight, new List<byte> { 46, 42 } },
                    { PieceTypeEnum.Bishop, new List<byte> { 49, 47 } },
                    { PieceTypeEnum.Queen, new List<byte> { 51 } },
                    { PieceTypeEnum.King, new List<byte> { 59 } }
                },
                "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R"
            };

            yield return new object[]
            {
                new Dictionary<PieceTypeEnum, List<byte>>
                {
                    { PieceTypeEnum.Pawn, new List<byte> { 9, 11, 38 } },
                    { PieceTypeEnum.Rook, new List<byte> { 30 } },
                    { PieceTypeEnum.Knight, new List<byte> { } },
                    { PieceTypeEnum.Bishop, new List<byte> { } },
                    { PieceTypeEnum.Queen, new List<byte> { } },
                    { PieceTypeEnum.King, new List<byte> { 39 } }
                },
                new Dictionary<PieceTypeEnum, List<byte>>
                {
                    { PieceTypeEnum.Pawn, new List<byte> { 26, 44, 53 } },
                    { PieceTypeEnum.Rook, new List<byte> { 32 } },
                    { PieceTypeEnum.Knight, new List<byte> { } },
                    { PieceTypeEnum.Bishop, new List<byte> { } },
                    { PieceTypeEnum.Queen, new List<byte> { } },
                    { PieceTypeEnum.King, new List<byte> { 24 } }
                },
                "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8"
            };

            yield return new object[]
            {
                new Dictionary<PieceTypeEnum, List<byte>>
                {
                    { PieceTypeEnum.Pawn, new List<byte> { } },
                    { PieceTypeEnum.Rook, new List<byte> {} },
                    { PieceTypeEnum.Knight, new List<byte> { } },
                    { PieceTypeEnum.Bishop, new List<byte> { } },
                    { PieceTypeEnum.Queen, new List<byte> { } },
                    { PieceTypeEnum.King, new List<byte> { 21 } }
                },
                new Dictionary<PieceTypeEnum, List<byte>>
                {
                    { PieceTypeEnum.Pawn, new List<byte> { } },
                    { PieceTypeEnum.Rook, new List<byte> { } },
                    { PieceTypeEnum.Knight, new List<byte> { } },
                    { PieceTypeEnum.Bishop, new List<byte> { } },
                    { PieceTypeEnum.Queen, new List<byte> { } },
                    { PieceTypeEnum.King, new List<byte> { 27 } }
                },
                "8/8/8/8/4k3/2K5/8/8"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
