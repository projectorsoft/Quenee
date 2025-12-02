using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MakeUnmakeMove.TestData
{
    public class PawnSingleMoveTestData: IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            for (byte square = 8; square < 16; square++)
                yield return new object[] {
                    "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KkQq - 0 1", square
                };
            yield return new object[] {
                "3k4/8/8/3P3P/P1P1P1P1/1P3P2/8/3K4 w KQkq - 0 1", 32
            };
            yield return new object[] {
                "3k4/8/8/3P3P/P1P1P1P1/1P3P2/8/3K4 w KQkq - 0 1", 25
            };
            yield return new object[] {
                "3k4/8/8/3P3P/P1P1P1P1/1P3P2/8/3K4 w KQkq - 0 1", 18
            };
            yield return new object[] {
                "3k4/8/8/3P3P/P1P1P1P1/1P3P2/8/3K4 w KQkq - 0 1", 27
            };
            yield return new object[] {
                "3k4/8/8/3P3P/P1P1P1P1/1P3P2/8/3K4 w KQkq - 0 1", 36
            };
            yield return new object[] {
                "3k4/8/8/3P3P/P1P1P1P1/1P3P2/8/3K4 w KQkq - 0 1", 29
            };
            yield return new object[] {
                "3k4/8/8/3P3P/P1P1P1P1/1P3P2/8/3K4 w KQkq - 0 1", 22
            };
            yield return new object[] {
                "3k4/8/8/3P3P/P1P1P1P1/1P3P2/8/3K4 w KQkq - 0 1", 31
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
