using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MakeUnmakeMove.TestData
{
    public class PawnDoubleMoveTestData: IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            for (byte square = 8; square < 16; square++)
                yield return new object[] {
                    "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KkQq - 0 1", square
                };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
