using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class PawnMovesPinTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Rank pin
            yield return new object[] {
                "8/8/1kpR4/8/8/8/8/K7 b - - 0 1",
                new List<Move>()
            };
            //Rank pin double move possible
            yield return new object[] {
                "8/1kp2R2/8/8/8/8/8/K7 b - - 0 1",
                new List<Move>()
            };
            //File pin
            yield return new object[] {
                "8/8/k7/p7/8/8/R7/K7 b - - 0 1",
                new List<Move>
                {
                    new Move(39, 31, MoveTypeEnum.Move)
                }
            };
            //File pin double move possible
            yield return new object[] {
                "1k6/1p6/8/8/8/1R6/8/K7 b - - 0 1",
                new List<Move>
                {
                    new Move(54, 46, MoveTypeEnum.Move),
                    new Move(54, 38, MoveTypeEnum.Move)
                }
            };
            //File pin double move possible
            yield return new object[] {
                "1k6/1p6/8/1R6/8/8/8/K7 b - - 0 1",
                new List<Move>
                {
                    new Move(54, 46, MoveTypeEnum.Move)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
