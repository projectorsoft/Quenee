using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class RookMovesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Corner A8
            yield return new object[] {
                "r7/7p/6P1/8/8/8/8/1K5k b KQkq - 0 1",
                new List<Move>
                {
                    new Move(63, 62, MoveTypeEnum.Move),
                    new Move(63, 61, MoveTypeEnum.Move),
                    new Move(63, 60, MoveTypeEnum.Move),
                    new Move(63, 59, MoveTypeEnum.Move),
                    new Move(63, 58, MoveTypeEnum.Move),
                    new Move(63, 57, MoveTypeEnum.Move),
                    new Move(63, 56, MoveTypeEnum.Move),
                    new Move(63, 55, MoveTypeEnum.Move),
                    new Move(63, 47, MoveTypeEnum.Move),
                    new Move(63, 39, MoveTypeEnum.Move),
                    new Move(63, 31, MoveTypeEnum.Move),
                    new Move(63, 23, MoveTypeEnum.Move),
                    new Move(63, 15, MoveTypeEnum.Move),
                    new Move(63, 7, MoveTypeEnum.Move)
                }
            };

            //Corner H8
            yield return new object[] {
                "7r/p7/8/P7/8/8/1K1k4/8 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(56, 63, MoveTypeEnum.Move),
                    new Move(56, 62, MoveTypeEnum.Move),
                    new Move(56, 61, MoveTypeEnum.Move),
                    new Move(56, 60, MoveTypeEnum.Move),
                    new Move(56, 59, MoveTypeEnum.Move),
                    new Move(56, 58, MoveTypeEnum.Move),
                    new Move(56, 57, MoveTypeEnum.Move),
                    new Move(56, 48, MoveTypeEnum.Move),
                    new Move(56, 40, MoveTypeEnum.Move),
                    new Move(56, 32, MoveTypeEnum.Move),
                    new Move(56, 24, MoveTypeEnum.Move),
                    new Move(56, 16, MoveTypeEnum.Move),
                    new Move(56, 8, MoveTypeEnum.Move),
                    new Move(56, 0, MoveTypeEnum.Move)
                }
            };

            //Corner H1
            yield return new object[] {
                "8/p7/8/P7/8/8/1K1k4/7r b KQkq - 0 1",
                new List<Move>
                {
                    new Move(0, 7, MoveTypeEnum.Move),
                    new Move(0, 6, MoveTypeEnum.Move),
                    new Move(0, 5, MoveTypeEnum.Move),
                    new Move(0, 4, MoveTypeEnum.Move),
                    new Move(0, 3, MoveTypeEnum.Move),
                    new Move(0, 2, MoveTypeEnum.Move),
                    new Move(0, 1, MoveTypeEnum.Move),
                    new Move(0, 56, MoveTypeEnum.Move),
                    new Move(0, 48, MoveTypeEnum.Move),
                    new Move(0, 40, MoveTypeEnum.Move),
                    new Move(0, 32, MoveTypeEnum.Move),
                    new Move(0, 24, MoveTypeEnum.Move),
                    new Move(0, 16, MoveTypeEnum.Move),
                    new Move(0, 8, MoveTypeEnum.Move)
                }
            };

            //Corner A1
            yield return new object[] {
                "8/1p6/8/1P6/8/8/1K1k4/r7 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(7, 6, MoveTypeEnum.Move),
                    new Move(7, 5, MoveTypeEnum.Move),
                    new Move(7, 4, MoveTypeEnum.Move),
                    new Move(7, 3, MoveTypeEnum.Move),
                    new Move(7, 2, MoveTypeEnum.Move),
                    new Move(7, 1, MoveTypeEnum.Move),
                    new Move(7, 0, MoveTypeEnum.Move),
                    new Move(7, 15, MoveTypeEnum.Move),
                    new Move(7, 23, MoveTypeEnum.Move),
                    new Move(7, 31, MoveTypeEnum.Move),
                    new Move(7, 39, MoveTypeEnum.Move),
                    new Move(7, 47, MoveTypeEnum.Move),
                    new Move(7, 55, MoveTypeEnum.Move),
                    new Move(7, 63, MoveTypeEnum.Move)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
