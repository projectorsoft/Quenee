using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class PawnEnPassantValidMovesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { //capture enPassant when king in check
                "rnq1kbnr/pp2pppp/5p2/b1pP4/3K4/8/8/RNBQ1BNR w - c6 0 1",
                new List<Move> { new Move(36, 45, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] { //capture enPassant and block check
                "8/8/8/1k6/3Pp3/8/8/4KQ2 b - d3 0 1",
                new List<Move> { new Move(27, 20, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] { //only king escape moves possible
                "8/8/8/8/1k1Pp3/8/8/3KQ3 b - d3 0 1",
                new List<Move>()
            };
            yield return new object[] { //discovered check for white
                "8/8/8/K2pP2q/8/8/8/3k4 w - d6 0 1",
                new List<Move>()
            };
            yield return new object[] { //discovered check for black
                "8/8/8/8/k2Pp2Q/8/8/3K4 b - d3 0 1",
                new List<Move>()
            };
            yield return new object[] { //all enPassant possible
                "8/8/8/8/k1pPp2Q/8/8/3K4 b - d3 0 1",
                new List<Move> { new Move(27, 20, MoveTypeEnum.EnPassante), new Move(29, 20, MoveTypeEnum.EnPassante) }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
