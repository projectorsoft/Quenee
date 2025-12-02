using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class KingMovesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Corners
            yield return new object[] {
                "8/8/8/8/8/K7/8/7k b - - 0 1",
                new List<Move> { new Move(0, 1, MoveTypeEnum.Move), new Move(0, 8, MoveTypeEnum.Move), new Move(0, 9, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/K7/8/8/k7 b - - 0 1",
                new List<Move> { new Move(7, 6, MoveTypeEnum.Move), new Move(7, 14, MoveTypeEnum.Move), new Move(7, 15, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "k7/8/8/8/8/K7/8/8 b - - 0 1",
                new List<Move> { new Move(63, 62, MoveTypeEnum.Move), new Move(63, 54, MoveTypeEnum.Move), new Move(63, 55, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "7k/8/8/8/8/K7/8/8 b - - 0 1",
                new List<Move> { new Move(56, 57, MoveTypeEnum.Move), new Move(56, 48, MoveTypeEnum.Move), new Move(56, 49, MoveTypeEnum.Move) }
            };
            // Corners + blocking own pieces
            yield return new object[] {
                "8/8/8/8/8/8/6bp/4K1nk b - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "8/8/8/8/8/8/nb6/kp2K3 b - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "kp6/nb6/8/8/8/8/8/4K3 b - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "6bk/6pr/8/8/8/8/8/4K3 b - - 0 1",
                new List<Move> { }
            };
            // Borders
            yield return new object[] {
                "8/8/8/8/8/K7/8/6k1 b - - 0 1",
                new List<Move>
                {
                    new Move(1, 0, MoveTypeEnum.Move),
                    new Move(1, 2, MoveTypeEnum.Move),
                    new Move(1, 8, MoveTypeEnum.Move),
                    new Move(1, 9, MoveTypeEnum.Move),
                    new Move(1, 10, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "5k2/8/8/8/8/K7/8/8 b - - 0 1",
                new List<Move>
                {
                    new Move(58, 57, MoveTypeEnum.Move),
                    new Move(58, 59, MoveTypeEnum.Move),
                    new Move(58, 49, MoveTypeEnum.Move),
                    new Move(58, 50, MoveTypeEnum.Move),
                    new Move(58, 51, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/8/8/7k/8/K7/8/8 b - - 0 1",
                new List<Move>
                {
                    new Move(32, 24, MoveTypeEnum.Move),
                    new Move(32, 25, MoveTypeEnum.Move),
                    new Move(32, 33, MoveTypeEnum.Move),
                    new Move(32, 41, MoveTypeEnum.Move),
                    new Move(32, 40, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/8/8/8/k7/8/8/4K3 b - - 0 1",
                new List<Move>
                {
                    new Move(31, 23, MoveTypeEnum.Move),
                    new Move(31, 22, MoveTypeEnum.Move),
                    new Move(31, 30, MoveTypeEnum.Move),
                    new Move(31, 38, MoveTypeEnum.Move),
                    new Move(31, 39, MoveTypeEnum.Move)
                }
            };
            // Borders + blocking own pieces
            yield return new object[] {
                "8/8/8/8/8/8/5pbp/2K2nkr b - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "8/8/6pp/6bk/6nr/8/8/2K5 b - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "3nkr2/3pbp2/8/8/8/8/8/2K5 b - - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "8/8/8/pp6/kb6/nr6/8/2K5 b - - 0 1",
                new List<Move> { }
            };
            // All possible directions
            yield return new object[] {
                "8/8/8/8/8/8/6k1/3K4 b - - 0 1",
                new List<Move>
                {
                    new Move(9, 0, MoveTypeEnum.Move),
                    new Move(9, 1, MoveTypeEnum.Move),
                    new Move(9, 2, MoveTypeEnum.Move),
                    new Move(9, 8, MoveTypeEnum.Move),
                    new Move(9, 10, MoveTypeEnum.Move),
                    new Move(9, 16, MoveTypeEnum.Move),
                    new Move(9, 17, MoveTypeEnum.Move),
                    new Move(9, 18, MoveTypeEnum.Move)
                }
            };
            // All possible directions + blocking own pieces
            yield return new object[] {
                "8/8/8/8/8/5ppp/5bkq/2K2nrr b - - 0 1",
                new List<Move> { }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
