using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class KingCapturesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Corners
            yield return new object[] {
                "8/8/8/8/8/8/6NN/2K3Nk b - - 0 1",
                new List<Move> { new Move(0, 1, MoveTypeEnum.Move), new Move(0, 8, MoveTypeEnum.Move), new Move(0, 9, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/8/NN6/kN1K4 b - - 0 1",
                new List<Move> { new Move(7, 6, MoveTypeEnum.Move), new Move(7, 14, MoveTypeEnum.Move), new Move(7, 15, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "kN6/NN6/8/8/8/8/8/3K4 b - - 0 1",
                new List<Move> { new Move(63, 62, MoveTypeEnum.Move), new Move(63, 54, MoveTypeEnum.Move), new Move(63, 55, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "6Nk/6NN/8/8/8/8/8/3K4 b - - 0 1",
                new List<Move> { new Move(56, 57, MoveTypeEnum.Move), new Move(56, 48, MoveTypeEnum.Move), new Move(56, 49, MoveTypeEnum.Move) }
            };
            // Borders
            yield return new object[] {
                "8/8/8/2K5/8/8/6PP/6Nk b - - 0 1",
                new List<Move>
                {
                    new Move(0, 1, MoveTypeEnum.Move),
                    new Move(0, 8, MoveTypeEnum.Move),
                    new Move(0, 9, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "6Nk/6NN/8/8/8/8/8/1N2K3 b - - 0 1",
                new List<Move>
                {
                    new Move(56, 48, MoveTypeEnum.Move),
                    new Move(56, 49, MoveTypeEnum.Move),
                    new Move(56, 57, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "kN6/NN6/8/2K5/8/8/8/8 b - - 0 1",
                new List<Move>
                {
                    new Move(63, 62, MoveTypeEnum.Move),
                    new Move(63, 55, MoveTypeEnum.Move),
                    new Move(63, 54, MoveTypeEnum.Move)
                }
            };
            yield return new object[] {
                "8/8/8/2K5/8/8/PP6/kN6 b - - 0 1",
                new List<Move>
                {
                    new Move(7, 6, MoveTypeEnum.Move),
                    new Move(7, 14, MoveTypeEnum.Move),
                    new Move(7, 15, MoveTypeEnum.Move)
                }
            };
            // Maximum number of captures
            yield return new object[] {
                "8/8/3PPP2/4k3/4B3/8/8/1N2K3 b - - 0 1",
                new List<Move>
                {
                    new Move(35, 27, MoveTypeEnum.Move),
                    new Move(35, 42, MoveTypeEnum.Move),
                    new Move(35, 43, MoveTypeEnum.Move),
                    new Move(35, 44, MoveTypeEnum.Move)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
