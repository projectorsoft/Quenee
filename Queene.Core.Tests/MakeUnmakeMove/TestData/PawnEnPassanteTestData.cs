using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MakeUnmakeMove.TestData
{
    class PawnEnPassanteTestData: IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "rnbqkb1r/p1pppppp/7n/Pp6/8/8/1PPPPPPP/RNBQKBNR w KQkq B6 0 3", 39
            };
            yield return new object[] {
                "rnbqkb1r/1ppppppp/7n/pP6/8/8/P1PPPPPP/RNBQKBNR w KQkq A6 0 3", 38
            };
            yield return new object[] {
                "rnbqkb1r/pp1ppppp/7n/1Pp5/8/8/P1PPPPPP/RNBQKBNR w KQkq C6 0 3", 38
            };
            yield return new object[] {
                "rnbqkb1r/p1pppppp/7n/1pP5/8/8/PP1PPPPP/RNBQKBNR w KQkq B6 0 3", 37
            };
            yield return new object[] {
                "rnbqkb1r/p1pppppp/7n/1pP5/8/8/PP1PPPPP/RNBQKBNR w KQkq B6 0 3", 37
            };
            yield return new object[] {
                "rnbqkb1r/pp1ppppp/7n/2pP4/8/8/PPP1PPPP/RNBQKBNR w KQkq C6 0 3", 36
            };
            yield return new object[] {
                "rnbqkb1r/pppp1ppp/7n/3Pp3/8/8/PPP1PPPP/RNBQKBNR w KQkq E6 0 3", 36
            };
            yield return new object[] {
                "rnbqkb1r/ppp1pppp/7n/3pP3/8/8/PPPP1PPP/RNBQKBNR w KQkq D6 0 3", 35
            };
            yield return new object[] {
                "rnbqkb1r/ppppp1pp/7n/4Pp2/8/8/PPPP1PPP/RNBQKBNR w KQkq F6 0 3", 35
            };
            yield return new object[] {
                "rnbqkb1r/pppp1ppp/7n/4pP2/8/8/PPPPP1PP/RNBQKBNR w KQkq E6 0 3", 34
            };
            yield return new object[] {
                "rnbqkb1r/pppppp1p/7n/5Pp1/8/8/PPPPP1PP/RNBQKBNR w KQkq G6 0 3", 34
            };
            yield return new object[] {
                "r1bqkbnr/ppppp1pp/n7/5pP1/8/8/PPPPPP1P/RNBQKBNR w KQkq F6 0 3", 33
            };
            yield return new object[] {
                "r1bqkbnr/ppppppp1/n7/6Pp/8/8/PPPPPP1P/RNBQKBNR w KQkq H6 0 3", 33
            };
            yield return new object[] {
                "r1bqkbnr/pppppp1p/n7/6pP/8/8/PPPPPPP1/RNBQKBNR w KQkq G6 0 3", 32
            };
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
