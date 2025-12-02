using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class PawnWhitePromotionCapturesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "1r6/P7/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(55, 62, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(55, 62, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(55, 62, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(55, 62, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "r7/1P6/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(54, 63, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(54, 63, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(54, 63, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(54, 63, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "2r5/1P6/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(54, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(54, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(54, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(54, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "r1b5/1P6/8/7k/8/8/7K/8 w KQkq - 1 1",
                new List<Move>
                {
                    new Move(54, 63, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(54, 63, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(54, 63, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(54, 63, MoveTypeEnum.Promotion, PieceTypeEnum.Queen),
                    new Move(54, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(54, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(54, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(54, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "1r6/2P5/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(53, 62, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(53, 62, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(53, 62, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(53, 62, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "3r4/2P5/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(53, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(53, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(53, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(53, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "1r1b4/2P5/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(53, 62, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(53, 62, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(53, 62, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(53, 62, MoveTypeEnum.Promotion, PieceTypeEnum.Queen),
                    new Move(53, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(53, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(53, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(53, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "2r5/3P4/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(52, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(52, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(52, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(52, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "4r3/3P4/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(52, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(52, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(52, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(52, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "2r1b3/3P4/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(52, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(52, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(52, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(52, 61, MoveTypeEnum.Promotion, PieceTypeEnum.Queen),
                    new Move(52, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(52, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(52, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(52, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "3r4/4P3/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(51, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(51, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(51, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(51, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "5r2/4P3/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(51, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(51, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(51, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(51, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "3r1b2/4P3/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(51, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(51, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(51, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(51, 60, MoveTypeEnum.Promotion, PieceTypeEnum.Queen),
                    new Move(51, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(51, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(51, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(51, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "4r3/5P2/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(50, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(50, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(50, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(50, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "6b1/5P2/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(50, 57, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(50, 57, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(50, 57, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(50, 57, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "4r1b1/5P2/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(50, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(50, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(50, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(50, 59, MoveTypeEnum.Promotion, PieceTypeEnum.Queen),
                    new Move(50, 57, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(50, 57, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(50, 57, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(50, 57, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "5r2/6P1/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(49, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(49, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(49, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(49, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "7b/6P1/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(49, 56, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(49, 56, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(49, 56, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(49, 56, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "5r1b/6P1/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(49, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(49, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(49, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(49, 58, MoveTypeEnum.Promotion, PieceTypeEnum.Queen),
                    new Move(49, 56, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(49, 56, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(49, 56, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(49, 56, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
            yield return new object[] {
                "6r1/7P/8/7k/8/7K/8/8 w KQkq - 0 1",
                new List<Move>
                {
                    new Move(48, 57, MoveTypeEnum.Promotion, PieceTypeEnum.Knight),
                    new Move(48, 57, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop),
                    new Move(48, 57, MoveTypeEnum.Promotion, PieceTypeEnum.Rook),
                    new Move(48, 57, MoveTypeEnum.Promotion, PieceTypeEnum.Queen)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
