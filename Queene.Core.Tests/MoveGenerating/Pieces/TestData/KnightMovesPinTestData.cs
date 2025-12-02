using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class KnightMovesPinTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Pinned knight in file
            yield return new object[] {
                "7k/7n/8/8/8/8/7R/3K4 b - - 0 1",
                new List<Move>()
            };
            //Pinned knight diagonal
            yield return new object[] {
                "8/4Q3/8/8/1n6/k7/8/3K4 b - - 0 1",
                new List<Move>()
            };
            //Two knights, one pinned in rank
            yield return new object[] {
                "4n3/8/8/1kn4R/8/8/8/3K4 b - - 0 1",
                new List<Move>
                {
                    new Move(59, 42, MoveTypeEnum.Move),
                    new Move(59, 44, MoveTypeEnum.Move),
                    new Move(59, 49, MoveTypeEnum.Move),
                    new Move(59, 53, MoveTypeEnum.Move)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
