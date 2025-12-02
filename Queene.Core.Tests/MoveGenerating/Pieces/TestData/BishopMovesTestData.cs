using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class BishopMovesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Blocking move when king in check
            yield return new object[] {
                "8/4k3/8/N7/7Q/4b3/8/3K4 b - - 0 1",
                new List<Move> { new Move(19, 33, MoveTypeEnum.Move) }
            };
            //Pinned
            yield return new object[] {
                "8/7q/8/8/4B3/8/4k3/1K6 w - - 0 1",
                new List<Move>
                {
                    new Move(27, 13, MoveTypeEnum.Move),
                    new Move(27, 20, MoveTypeEnum.Move),
                    new Move(27, 34, MoveTypeEnum.Move),
                    new Move(27, 41, MoveTypeEnum.Move)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
