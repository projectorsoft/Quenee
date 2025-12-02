using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class BishopMovesPinTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Rank pin
            yield return new object[] {
                "8/Q2bk3/8/8/8/8/8/3K4 b - - 0 1",
                new List<Move>()
            };
            //File pin
            yield return new object[] {
                "8/5Q2/8/8/8/5b2/5k2/3K4 b - - 0 1",
                new List<Move>()
            };
            //Rank pin
            yield return new object[] {
                "8/8/7Q/8/8/8/3b4/K1k5 b - - 0 1",
                new List<Move>
                {
                    new Move(12, 19, MoveTypeEnum.Move),
                    new Move(12, 26, MoveTypeEnum.Move),
                    new Move(12, 33, MoveTypeEnum.Move)
                }
            };
            //Three bishops, two pinned and one free to move
            yield return new object[] {
                "7Q/b7/8/4b3/3k4/4b3/8/K5B1 b - - 0 1",
                new List<Move>
                {
                    new Move(19, 10, MoveTypeEnum.Move),
                    new Move(35, 42, MoveTypeEnum.Move),
                    new Move(35, 49, MoveTypeEnum.Move),
                    new Move(55, 37, MoveTypeEnum.Move),
                    new Move(55, 46, MoveTypeEnum.Move),
                    new Move(55, 62, MoveTypeEnum.Move)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
