using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class BishopCapturesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Captures attacker from behind when king is in check
            yield return new object[] {
                "8/2k5/8/N7/5Q2/2r5/7b/3K4 b - - 0 1",
                new List<Move> { new Move(8, 26, MoveTypeEnum.Move) }
            };
            //Blocking piece captures attacker on the same diagonal when king is in check
            yield return new object[] {
                "8/8/6k1/5b2/8/8/8/1Q1K4 b - - 0 1",
                new List<Move> 
                { 
                    new Move(34, 6, MoveTypeEnum.Move)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
