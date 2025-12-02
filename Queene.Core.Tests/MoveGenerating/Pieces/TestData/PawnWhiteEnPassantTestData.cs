using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class PawnWhiteEnPassantTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "rnbqkb1r/1ppppppp/5n2/pP6/8/8/P1PPPPPP/RNBQKBNR w - a6 0 3",
                new List<Move> { new Move(38, 47, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkb1r/p1pppppp/7n/Pp6/8/8/1PPPPPPP/RNBQKBNR w - b6 0 3",
                new List<Move> { new Move(39, 46, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkb1r/p1pppppp/5n2/PpP5/8/8/1P1PPPPP/RNBQKBNR w - b6 0 5",
                new List<Move> { new Move(39, 46, MoveTypeEnum.EnPassante), new Move(37, 46, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkb1r/pp1ppppp/5n2/1Pp5/8/8/P1PPPPPP/RNBQKBNR w - c6 0 3",
                new List<Move> { new Move(38, 45, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkb1r/pp1ppppp/5n2/1PpP4/8/8/P1P1PPPP/RNBQKBNR w - c6 0 5",
                new List<Move> { new Move(38, 45, MoveTypeEnum.EnPassante), new Move(36, 45, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkb1r/ppp1pppp/7n/2Pp4/8/8/PP1PPPPP/RNBQKBNR w - d6 0 3",
                new List<Move> { new Move(37, 44, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkb1r/ppp1pppp/7n/2PpP3/8/8/PP1P1PPP/RNBQKBNR w - d6 0 5",
                new List<Move> { new Move(37, 44, MoveTypeEnum.EnPassante), new Move(35, 44, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkb1r/pppp1ppp/7n/3Pp3/8/8/PPP1PPPP/RNBQKBNR w - e6 0 3",
                new List<Move> { new Move(36, 43, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkb1r/pppp1ppp/7n/3PpP2/8/8/PPP1P1PP/RNBQKBNR w - e6 0 5",
                new List<Move> { new Move(36, 43, MoveTypeEnum.EnPassante), new Move(34, 43, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkb1r/ppppp1pp/7n/4Pp2/8/8/PPPP1PPP/RNBQKBNR w - f6 0 3",
                new List<Move> { new Move(35, 42, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkb1r/ppppp1pp/7n/4PpP1/8/8/PPPP1P1P/RNBQKBNR w - f6 0 5",
                new List<Move> { new Move(35, 42, MoveTypeEnum.EnPassante), new Move(33, 42, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "r1bqkbnr/pppppp1p/n7/5Pp1/8/8/PPPPP1PP/RNBQKBNR w - g6 0 3",
                new List<Move> { new Move(34, 41, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "r1bqkbnr/pppppp1p/n7/5PpP/8/8/PPPPP1P1/RNBQKBNR w - g6 0 5",
                new List<Move> { new Move(34, 41, MoveTypeEnum.EnPassante), new Move(32, 41, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkb1r/ppppppp1/5n2/6Pp/8/8/PPPPPP1P/RNBQKBNR w - h6 0 3",
                new List<Move> { new Move(33, 40, MoveTypeEnum.EnPassante) }
            };
            yield return new object[] {
                "rnbqkb1r/pppppp1p/5n2/6pP/8/8/PPPPPPP1/RNBQKBNR w - g6 0 3",
                new List<Move> { new Move(32, 41, MoveTypeEnum.EnPassante) }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
