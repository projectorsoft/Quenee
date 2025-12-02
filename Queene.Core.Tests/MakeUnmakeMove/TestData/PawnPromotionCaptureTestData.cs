using Queene.Core.Enums;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MakeUnmakeMove.TestData
{
    public class PawnPromotionCaptureTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            for (byte square = 48; square < 56; square++)
            {
                yield return new object[] {
                    "rrrrrrrr/PPPPPPPP/8/5k2/8/8/8/3K4 w KQkq - 0 1", square, PieceTypeEnum.Bishop
                };
                yield return new object[] {
                    "rrrrrrrr/PPPPPPPP/8/5k2/8/8/8/3K4 w KQkq - 0 1", square, PieceTypeEnum.Knight
                };
                yield return new object[] {
                    "rrrrrrrr/PPPPPPPP/8/5k2/8/8/8/3K4 w KQkq - 0 1", square, PieceTypeEnum.Rook
                };
                yield return new object[] {
                    "rrrrrrrr/PPPPPPPP/8/5k2/8/8/8/3K4 w KQkq - 0 1", square, PieceTypeEnum.Queen
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
