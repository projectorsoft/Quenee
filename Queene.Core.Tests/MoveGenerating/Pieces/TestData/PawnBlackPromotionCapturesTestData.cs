using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class PawnBlackPromotionCapturesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "8/8/8/7k/8/7K/p7/1R6 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(15, 6, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(15, 6, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(15, 6, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(15, 6, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/1p6/R7 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(14, 7, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(14, 7, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(14, 7, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(14, 7, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/1p6/2B5 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(14, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(14, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(14, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(14, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/1p6/R1B5 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(14, 7, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(14, 7, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(14, 7, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(14, 7, MoveTypeEnum.Promotion, PieceTypeEnum.Queen),
                    new Move(14, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(14, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(14, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(14, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/2p5/1R6 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(13, 6, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(13, 6, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(13, 6, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(13, 6, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/7k/8/8/7K/2p5/3B4 b KQkq - 1 2",
                new List<Move>
                {
                    new Move(13, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(13, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(13, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(13, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/7k/8/8/7K/2p5/1R1B4 b KQkq - 1 2",
                new List<Move>
                {
                    new Move(13, 6, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(13, 6, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(13, 6, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(13, 6, MoveTypeEnum.Promotion, PieceTypeEnum.Queen),
                    new Move(13, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(13, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(13, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(13, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/3p4/2R5 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(12, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(12, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(12, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(12, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/3p4/4B3 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(12, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(12, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(12, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(12, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/3p4/2R1B3 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(12, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(12, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(12, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(12, 5, MoveTypeEnum.Promotion, PieceTypeEnum.Queen),
                    new Move(12, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(12, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(12, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(12, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/4p3/3R4 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(11, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(11, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(11, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(11, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/4p3/5B2 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(11, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(11, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(11, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(11, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/4p3/3R1B2 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(11, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(11, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(11, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(11, 4, MoveTypeEnum.Promotion, PieceTypeEnum.Queen),
                    new Move(11, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(11, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(11, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(11, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/5p2/4R3 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(10, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(10, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(10, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(10, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/5p2/6B1 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(10, 1, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(10, 1, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(10, 1, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(10, 1, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/5p2/4R1B1 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(10, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(10, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(10, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(10, 3, MoveTypeEnum.Promotion, PieceTypeEnum.Queen),
                    new Move(10, 1, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(10, 1, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(10, 1, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(10, 1, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/6p1/5R2 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(9, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(9, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(9, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(9, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/6p1/7B b KQkq - 0 1",
                new List<Move>
                {
                    new Move(9, 0, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(9, 0, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(9, 0, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(9, 0, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/6p1/5R1B b KQkq - 0 1",
                new List<Move>
                {
                    new Move(9, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(9, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(9, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(9, 2, MoveTypeEnum.Promotion, PieceTypeEnum.Queen),
                    new Move(9, 0, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(9, 0, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(9, 0, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(9, 0, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/7K/7p/6B1 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(8, 1, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(8, 1, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(8, 1, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(8, 1, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
