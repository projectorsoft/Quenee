using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class RookCapturesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Captures A1
            yield return new object[] {
                "N7/8/8/8/8/8/1K1k4/r6B b KQkq - 0 1",
                new List<Move>
                {
                    new Move(7, 63, MoveTypeEnum.Move),
                    new Move(7, 0, MoveTypeEnum.Move)
                }
            };

            //Captures A8
            yield return new object[] {
                "r6B/8/8/8/8/8/1K1k4/N7 b KQkq - 0 1",
                new List<Move>
                {
                    new Move(63, 7, MoveTypeEnum.Move),
                    new Move(63, 56, MoveTypeEnum.Move)
                }
            };

            //Captures H8
            yield return new object[] {
                "N6r/8/8/8/8/8/1K1k4/7B b KQkq - 0 1",
                new List<Move>
                {
                    new Move(56, 63, MoveTypeEnum.Move),
                    new Move(56, 0, MoveTypeEnum.Move)
                }
            };

            //Captures H1
            yield return new object[] {
                "7N/8/8/8/8/8/1K1k4/B6r b KQkq - 0 1",
                new List<Move>
                {
                    new Move(0, 56, MoveTypeEnum.Move),
                    new Move(0, 7, MoveTypeEnum.Move)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
