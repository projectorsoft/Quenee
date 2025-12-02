using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class RookCapturesPinTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Diagonal pin
            yield return new object[] {
                "3k4/4r3/8/8/7B/8/8/3K4 b - - 0 1",
                new List<Move>()
            };
            //file pin
            yield return new object[] {
                "3k4/8/3r4/8/8/8/3Q4/3K4 b - - 0 1",
                new List<Move>
                {
                    new Move(44, 12, MoveTypeEnum.Move)
                }
            };
            //rank pin
            yield return new object[] {
                "8/8/8/1R3r1k/8/8/8/3K4 b - - 0 1",
                new List<Move>
                {
                    new Move(34, 38, MoveTypeEnum.Move)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
