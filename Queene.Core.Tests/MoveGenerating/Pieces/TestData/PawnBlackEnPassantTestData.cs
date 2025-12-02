using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    class PawnBlackEnPassantTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "rnbqkbnr/p1pppppp/8/8/Pp6/8/1PPPPPPP/RNBQKBNR b - a3 0 3",
                new List<Move> { new Move(30, 23, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/1ppppppp/8/8/pP6/8/P1PPPPPP/RNBQKBNR b - b3 0 3",
                new List<Move> { new Move(31, 22, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/1p1ppppp/8/8/pPp5/8/P1PPPPPP/RNBQKBNR b - b3 0 5",
                new List<Move> { new Move(31, 22, MoveTypeEnum.EnPassante), new Move(29, 22, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/p1pppppp/8/8/1pP5/8/PP1PPPPP/RNBQKBNR b - c3 0 3",
                new List<Move> { new Move(30, 21, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/p1p1pppp/8/8/1pPp4/8/PP1PPPPP/RNBQKBNR b - c3 0 5",
                new List<Move> { new Move(30, 21, MoveTypeEnum.EnPassante), new Move(28, 21, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/pp1ppppp/8/8/2pP4/8/PPP1PPPP/RNBQKBNR b - d3 0 3",
                new List<Move> { new Move(29, 20, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/pp1p1ppp/8/8/2pPp3/8/PPP1PPPP/RNBQKBNR b - d3 0 5",
                new List<Move> { new Move(29, 20, MoveTypeEnum.EnPassante), new Move(27, 20, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/ppp1pppp/8/8/3pP3/8/PPPP1PPP/RNBQKBNR b - e3 0 3",
                new List<Move> { new Move(28, 19, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/ppp1p1pp/8/8/3pPp2/8/PPPP1PPP/RNBQKBNR b - e3 0 5",
                new List<Move> { new Move(28, 19, MoveTypeEnum.EnPassante), new Move(26, 19, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/pppp1ppp/8/8/4pP2/8/PPPPP1PP/RNBQKBNR b - f3 0 3",
                new List<Move> { new Move(27, 18, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/pppp1p1p/8/8/4pPp1/8/PPPPP1PP/RNBQKBNR b - f3 0 5",
                new List<Move> { new Move(27, 18, MoveTypeEnum.EnPassante), new Move(25, 18, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/ppppp1pp/8/8/5pP1/8/PPPPPP1P/RNBQKBNR b - g3 0 3",
                new List<Move> { new Move(26, 17, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/ppppp1p1/8/8/5pPp/8/PPPPPP1P/RNBQKBNR b - g3 0 5",
                new List<Move> { new Move(26, 17, MoveTypeEnum.EnPassante), new Move(24, 17, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/pppppp1p/8/8/6pP/8/PPPPPPP1/RNBQKBNR b - h3 0 3",
                new List<Move> { new Move(25, 16, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkbnr/ppppppp1/8/8/6Pp/8/PPPPPP1P/RNBQKBNR b - g3 0 3",
                new List<Move> { new Move(24, 17, MoveTypeEnum.EnPassante) }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
