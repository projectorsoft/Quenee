using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class BishopCapturesPinTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Diagonal pin
            yield return new object[] {
                "7Q/8/8/8/4k3/5b2/8/K6B b - - 0 1",
                new List<Move>
                {
                    new Move(18, 0, MoveTypeEnum.Move)
                }
            };
            //Diagonal pin
            yield return new object[] {
                "7Q/8/8/4b3/8/2k5/8/K7 b - - 0 1",
                new List<Move>
                {
                    new Move(35, 56, MoveTypeEnum.Move)
                }
            };
            //Pin in file
            yield return new object[] {
                "8/8/1k6/1b6/8/8/1R6/K7 b - - 0 1",
                new List<Move>()
            };
            //Pin in rank
            yield return new object[] {
                "8/8/8/8/8/2kb3Q/8/K7 b - - 0 1",
                new List<Move>()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
