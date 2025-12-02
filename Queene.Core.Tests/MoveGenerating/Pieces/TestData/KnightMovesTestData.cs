using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class KnightMovesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Corners
            yield return new object[] {
                "8/8/8/8/2k5/8/2K5/7N w - - 0 1",
                new List<Move> { new Move(0, 10, MoveTypeEnum.Move), new Move(0, 17, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/2k5/6R1/3K1B2/N7 w - - 0 1",
                new List<Move> { new Move(7, 13, MoveTypeEnum.Move), new Move(7, 22, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "N7/8/2K5/1B6/3k4/8/8/8 w - - 0 1",
                new List<Move> { new Move(63, 46, MoveTypeEnum.Move), new Move(63, 53, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "7N/8/2K5/1B6/3k4/8/8/8 w - - 0 1",
                new List<Move> { new Move(56, 41, MoveTypeEnum.Move), new Move(56, 50, MoveTypeEnum.Move) }
            };
            // Corners + blocking own pieces
            yield return new object[] {
                "8/8/8/8/2k5/6R1/2K2B2/7N w - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "8/8/8/8/3k4/1B6/2K5/N7 w - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "N7/2K5/1B6/8/3k4/8/8/8 w - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "7N/5K2/6B1/8/3k4/8/8/8 w - - 0 1",
                new List<Move> { }
            };
            // Borders
            yield return new object[] {
                "8/5K2/6B1/8/3k4/8/8/6N1 w - - 0 1",
                new List<Move>
                {
                    new Move(1, 11, MoveTypeEnum.Move),
                    new Move(1, 16, MoveTypeEnum.Move),
                    new Move(1, 18, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/8/8/8/3k1KB1/1R5P/8/5N2 w - - 0 1",
                new List<Move>
                {
                    new Move(2, 8, MoveTypeEnum.Move),
                    new Move(2, 12, MoveTypeEnum.Move),
                    new Move(2, 17, MoveTypeEnum.Move),
                    new Move(2, 19, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "6N1/8/8/8/8/1k6/8/4K3 w - - 0 1",
                new List<Move>
                {
                    new Move(57, 40, MoveTypeEnum.Move),
                    new Move(57, 42, MoveTypeEnum.Move),
                    new Move(57, 51, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "5N2/8/8/8/3k1KB1/1R5P/8/8 w - - 0 1",
                new List<Move>
                {
                    new Move(58, 41, MoveTypeEnum.Move),
                    new Move(58, 43, MoveTypeEnum.Move),
                    new Move(58, 48, MoveTypeEnum.Move),
                    new Move(58, 52, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/7N/8/8/3k1KB1/1R5P/8/8 w - - 0 1",
                new List<Move>
                {
                    new Move(48, 33, MoveTypeEnum.Move),
                    new Move(48, 42, MoveTypeEnum.Move),
                    new Move(48, 58, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/8/7N/8/3k1K2/1R3B1P/8/8 w - - 0 1",
                new List<Move>
                {
                    new Move(40, 25, MoveTypeEnum.Move),
                    new Move(40, 34, MoveTypeEnum.Move),
                    new Move(40, 50, MoveTypeEnum.Move),
                    new Move(40, 57, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/8/8/8/3k1K2/1R3B1P/N7/8 w - - 0 1",
                new List<Move>
                {
                    new Move(15, 5, MoveTypeEnum.Move),
                    new Move(15, 21, MoveTypeEnum.Move),
                    new Move(15, 30, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/8/8/8/3k1K2/NR3B1P/8/8 w - - 0 1",
                new List<Move>
                {
                    new Move(23, 6, MoveTypeEnum.Move),
                    new Move(23, 13, MoveTypeEnum.Move),
                    new Move(23, 29, MoveTypeEnum.Move),
                    new Move(23, 38, MoveTypeEnum.Move)
                }
            };
            // Borders + blocking own pieces
            yield return new object[] {
                "8/8/8/8/8/5K1R/3kP3/6N1 w - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "8/8/8/8/8/1k2K1R1/3P3R/5N2 w - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "8/8/8/8/6R1/1k3K2/7N/5R2 w - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "8/8/8/8/4R1B1/1k1R3Q/5N2/3P3K w - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "6N1/4R3/5K1R/8/8/1k6/8/6B1 w - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "5N2/3R3B/4K1R1/8/8/1k6/8/8 w - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "2R5/N6B/2K5/1R6/8/1k6/8/8 w - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "1B6/2R5/N7/2K5/1R6/1k6/8/8 w - - 0 1",
                new List<Move> { }
            };
            // All possible directions
            yield return new object[] {
                "8/8/1K6/4N3/8/1k6/8/8 w - - 0 1",
                new List<Move>
                {
                    new Move(35, 18, MoveTypeEnum.Move),
                    new Move(35, 20, MoveTypeEnum.Move),
                    new Move(35, 29, MoveTypeEnum.Move),
                    new Move(35, 25, MoveTypeEnum.Move),
                    new Move(35, 45, MoveTypeEnum.Move),
                    new Move(35, 41, MoveTypeEnum.Move),
                    new Move(35, 50, MoveTypeEnum.Move),
                    new Move(35, 52, MoveTypeEnum.Move)
                }
            };
            // All possible directions + blocking own pieces
            yield return new object[] {
                "8/3B1R2/2K3P1/4N3/2R3P1/1k1Q1B2/8/8 w - - 0 1",
                new List<Move> { }
            };
            //Second rank and file (all corners)
            yield return new object[] {
                "8/8/8/8/8/1k6/6N1/7K w - - 0 1",
                new List<Move>
                {
                    new Move(9, 3, MoveTypeEnum.Move),
                    new Move(9, 19, MoveTypeEnum.Move),
                    new Move(9, 24, MoveTypeEnum.Move),
                    new Move(9, 26, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/8/8/8/8/1k6/5N1K/8 w - - 0 1",
                new List<Move>
                {
                    new Move(10, 0, MoveTypeEnum.Move),
                    new Move(10, 4, MoveTypeEnum.Move),
                    new Move(10, 16, MoveTypeEnum.Move),
                    new Move(10, 20, MoveTypeEnum.Move),
                    new Move(10, 25, MoveTypeEnum.Move),
                    new Move(10, 27, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/8/8/8/8/1k4N1/8/4K3 w - - 0 1",
                new List<Move>
                {
                    new Move(17, 0, MoveTypeEnum.Move),
                    new Move(17, 2, MoveTypeEnum.Move),
                    new Move(17, 11, MoveTypeEnum.Move),
                    new Move(17, 27, MoveTypeEnum.Move),
                    new Move(17, 32, MoveTypeEnum.Move),
                    new Move(17, 34, MoveTypeEnum.Move)
                }
            };
            //Second rank and file (all corners) + blocking own pieces
            yield return new object[] {
                "8/8/8/8/5R1B/1k2Q3/6N1/4K3 w - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "8/8/8/8/4R1B1/1k1Q3P/5N2/3K3R w - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "8/8/8/5B1P/4R3/1k4N1/4Q3/5K1R w - - 0 1",
                new List<Move> { }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
