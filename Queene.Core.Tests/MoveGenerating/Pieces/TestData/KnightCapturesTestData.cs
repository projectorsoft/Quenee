using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class KnightCapturesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Corners
            yield return new object[] {
                "8/8/8/8/1k6/6r1/2K2b2/7N w - - 0 1",
                new List<Move> { new Move(0, 10, MoveTypeEnum.Move), new Move(0, 17, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/3k4/1r6/2bK4/N7 w - - 0 1",
                new List<Move> { new Move(7, 13, MoveTypeEnum.Move), new Move(7, 22, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "N7/2b5/1r6/8/3k4/8/3K4/8 w - - 0 1",
                new List<Move> { new Move(63, 46, MoveTypeEnum.Move), new Move(63, 53, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "7N/5b2/6r1/8/3k4/8/3K4/8 w - - 0 1",
                new List<Move> { new Move(56, 41, MoveTypeEnum.Move), new Move(56, 50, MoveTypeEnum.Move) }
            };
            // Borders
            yield return new object[] {
                "8/8/8/8/3k4/5b1q/4r3/2K3N1 w - - 1 1",
                new List<Move>
                {
                    new Move(1, 11, MoveTypeEnum.Move),
                    new Move(1, 16, MoveTypeEnum.Move),
                    new Move(1, 18, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/8/8/8/3k4/1K2b1p1/3r3q/5N2 w - - 0 1",
                new List<Move>
                {
                    new Move(2, 8, MoveTypeEnum.Move),
                    new Move(2, 12, MoveTypeEnum.Move),
                    new Move(2, 17, MoveTypeEnum.Move),
                    new Move(2, 19, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "6N1/4b3/5p1q/8/3k4/1K6/3r4/8 w - - 0 1",
                new List<Move>
                {
                    new Move(57, 40, MoveTypeEnum.Move),
                    new Move(57, 42, MoveTypeEnum.Move),
                    new Move(57, 51, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "5N2/3b3r/4p1q1/8/3k4/1K6/3r4/8 w - - 0 1",
                new List<Move>
                {
                    new Move(58, 41, MoveTypeEnum.Move),
                    new Move(58, 43, MoveTypeEnum.Move),
                    new Move(58, 48, MoveTypeEnum.Move),
                    new Move(58, 52, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "5r2/7N/5q2/6p1/3k4/1K6/3r4/8 w - - 0 1",
                new List<Move>
                {
                    new Move(48, 33, MoveTypeEnum.Move),
                    new Move(48, 42, MoveTypeEnum.Move),
                    new Move(48, 58, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "6r1/5r2/7N/5q2/3k2p1/1K6/8/8 w - - 0 1",
                new List<Move>
                {
                    new Move(40, 25, MoveTypeEnum.Move),
                    new Move(40, 34, MoveTypeEnum.Move),
                    new Move(40, 50, MoveTypeEnum.Move),
                    new Move(40, 57, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "6r1/8/8/8/1r1k4/2p2K2/N7/2q5 w - - 0 1",
                new List<Move>
                {
                    new Move(15, 5, MoveTypeEnum.Move),
                    new Move(15, 21, MoveTypeEnum.Move),
                    new Move(15, 30, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/8/8/1r6/2rk4/N4K2/2p5/1q6 w - - 0 1",
                new List<Move>
                {
                    new Move(23, 6, MoveTypeEnum.Move),
                    new Move(23, 13, MoveTypeEnum.Move),
                    new Move(23, 29, MoveTypeEnum.Move),
                    new Move(23, 38, MoveTypeEnum.Move)
                }
            };
            // All possible directions
            yield return new object[] {
                "8/3b1p2/2q3b1/4N3/k1r3p1/3r1p2/5K2/8 w - - 0 1",
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
            //Second rank and file (all corners)
            yield return new object[] {
                "8/8/8/8/k4p1b/4p3/1K4N1/4r3 w - - 0 1",
                new List<Move>
                {
                    new Move(9, 3, MoveTypeEnum.Move),
                    new Move(9, 19, MoveTypeEnum.Move),
                    new Move(9, 24, MoveTypeEnum.Move),
                    new Move(9, 26, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/8/8/8/k3p1b1/3p3b/1K3N2/3r3q w - - 0 1",
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
                "8/8/8/5b1b/k3p3/6N1/1K2p3/5r1q w - - 0 1",
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
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
