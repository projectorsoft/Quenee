using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class RookMovesPinTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Diagonal pin
            yield return new object[] {
                "3k4/4r3/8/8/7B/8/8/3K4 b - - 0 1",
                new List<Move>()
            };
            //File pin
            yield return new object[] {
                "3k4/8/3r4/8/8/8/3Q4/3K4 b - - 0 1",
                new List<Move>
                {
                    new Move(44, 52, MoveTypeEnum.Move),
                    new Move(44, 36, MoveTypeEnum.Move),
                    new Move(44, 28, MoveTypeEnum.Move),
                    new Move(44, 20, MoveTypeEnum.Move)
                }
            };
            //Rank pin
            yield return new object[] {
                "8/8/8/1R3r1k/8/8/8/3K4 b - - 0 1",
                new List<Move>
                {
                    new Move(34, 33, MoveTypeEnum.Move),
                    new Move(34, 35, MoveTypeEnum.Move),
                    new Move(34, 36, MoveTypeEnum.Move),
                    new Move(34, 37, MoveTypeEnum.Move)
                }
            };
            //Three rooks, two pinned and one free to move
            yield return new object[] {
                "8/3r4/3k1r1Q/2r5/8/B7/8/3K4 b - - 0 1",
                new List<Move>
                {
                    new Move(42, 41, MoveTypeEnum.Move),
                    new Move(42, 43, MoveTypeEnum.Move),
                    new Move(52, 48, MoveTypeEnum.Move),
                    new Move(52, 49, MoveTypeEnum.Move),
                    new Move(52, 50, MoveTypeEnum.Move),
                    new Move(52, 51, MoveTypeEnum.Move),
                    new Move(52, 53, MoveTypeEnum.Move),
                    new Move(52, 54, MoveTypeEnum.Move),
                    new Move(52, 55, MoveTypeEnum.Move),
                    new Move(52, 60, MoveTypeEnum.Move)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
