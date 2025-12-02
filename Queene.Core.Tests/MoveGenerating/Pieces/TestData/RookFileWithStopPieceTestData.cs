using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class RookFileWithStopPieceTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //A file
            yield return new object[] {
                "1k6/8/8/8/8/8/p7/RK6 w KQkq - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "1k6/8/8/8/8/p7/8/RK6 w KQkq - 0 1",
                new List<Move> { new Move(7, 15, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "1k6/8/8/8/p7/8/8/RK6 w KQkq - 0 1",
                new List<Move> { new Move(7, 15, MoveTypeEnum.Move), new Move(7, 23, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "1k6/8/8/p7/8/8/8/RK6 w KQkq - 0 1",
                new List<Move> { new Move(7, 15, MoveTypeEnum.Move), new Move(7, 23, MoveTypeEnum.Move), new Move(7, 31, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "1k6/8/p7/8/8/8/8/RK6 w KQkq - 0 1",
                new List<Move> { new Move(7, 15, MoveTypeEnum.Move), new Move(7, 23, MoveTypeEnum.Move), new Move(7, 31, MoveTypeEnum.Move), new Move(7, 39, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "1k6/p7/8/8/8/8/8/RK6 w KQkq - 0 1",
                new List<Move> { new Move(7, 15, MoveTypeEnum.Move), new Move(7, 23, MoveTypeEnum.Move), new Move(7, 31, MoveTypeEnum.Move), new Move(7, 39, MoveTypeEnum.Move), new Move(7, 47, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "pk6/8/8/8/8/8/8/RK6 w KQkq - 0 1",
                new List<Move> { new Move(7, 15, MoveTypeEnum.Move), new Move(7, 23, MoveTypeEnum.Move), new Move(7, 31, MoveTypeEnum.Move), new Move(7, 39, MoveTypeEnum.Move), new Move(7, 47, MoveTypeEnum.Move), new Move(7, 55, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "1k6/8/8/8/8/8/8/RK6 w KQkq - 0 1",
                new List<Move> { new Move(7, 15, MoveTypeEnum.Move), new Move(7, 23, MoveTypeEnum.Move), new Move(7, 31, MoveTypeEnum.Move), new Move(7, 39, MoveTypeEnum.Move), new Move(7, 47, MoveTypeEnum.Move), new Move(7, 55, MoveTypeEnum.Move), new Move(7, 63, MoveTypeEnum.Move) }
            };

            //B file
            yield return new object[] {
                "8/k7/8/8/8/8/1p6/KRP5 w KQkq - 0 1",
                new List<Move> { }
            };
            yield return new object[] {
                "8/k7/8/8/8/1p6/8/KRP5 w KQkq - 0 1",
                new List<Move> { new Move(6, 14, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/k7/8/8/1p6/8/8/KRP5 w KQkq - 0 1",
                new List<Move> { new Move(6, 14, MoveTypeEnum.Move), new Move(6, 22, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/k7/8/1p6/8/8/8/KRP5 w KQkq - 0 1",
                new List<Move> { new Move(6, 14, MoveTypeEnum.Move), new Move(6, 22, MoveTypeEnum.Move), new Move(6, 30, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/k7/1p6/8/8/8/8/KRP5 w KQkq - 0 1",
                new List<Move> { new Move(6, 14, MoveTypeEnum.Move), new Move(6, 22, MoveTypeEnum.Move), new Move(6, 30, MoveTypeEnum.Move), new Move(6, 38, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/kp6/8/8/8/8/8/KRP5 w KQkq - 0 1",
                new List<Move> { new Move(6, 14, MoveTypeEnum.Move), new Move(6, 22, MoveTypeEnum.Move), new Move(6, 30, MoveTypeEnum.Move), new Move(6, 38, MoveTypeEnum.Move), new Move(6, 46, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "1p6/k7/8/8/8/8/8/KRP5 w KQkq - 0 1",
                new List<Move> { new Move(6, 14, MoveTypeEnum.Move), new Move(6, 22, MoveTypeEnum.Move), new Move(6, 30, MoveTypeEnum.Move), new Move(6, 38, MoveTypeEnum.Move), new Move(6, 46, MoveTypeEnum.Move), new Move(6, 54, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "k7/8/8/8/8/8/8/PRK5 w KQkq - 0 1",
                new List<Move> { new Move(6, 14, MoveTypeEnum.Move), new Move(6, 22, MoveTypeEnum.Move), new Move(6, 30, MoveTypeEnum.Move), new Move(6, 38, MoveTypeEnum.Move), new Move(6, 46, MoveTypeEnum.Move), new Move(6, 54, MoveTypeEnum.Move), new Move(6, 62, MoveTypeEnum.Move) }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
