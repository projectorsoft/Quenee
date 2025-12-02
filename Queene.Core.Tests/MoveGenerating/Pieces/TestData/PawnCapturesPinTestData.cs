using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class PawnCapturesPinTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //File pin
            yield return new object[] {
                "1k6/1p6/2R5/8/8/1Q6/8/K7 b - - 0 1",
                new List<Move>()
            };
            //Diagonal pin
            yield return new object[] {
                "8/k7/1p6/N7/8/8/5Q2/K7 b - - 0 1",
                new List<Move>()
            };
            //Diagonal pin with capture pinner
            yield return new object[] {
                "8/k7/1p6/N1Q5/8/8/8/K7 b - - 0 1",
                new List<Move>
                {
                    new Move(46, 37, MoveTypeEnum.Move)
                }
            };
            //Diagonal pin enPassant capture
            yield return new object[] {
                "8/8/8/k7/1pP5/8/8/K3Q3 b - c3 0 1",
                new List<Move>
                {
                    new Move(30, 21, MoveTypeEnum.EnPassante)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
