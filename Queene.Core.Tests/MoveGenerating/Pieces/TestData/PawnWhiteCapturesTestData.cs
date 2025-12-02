using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class PawnWhiteCapturesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "8/8/8/8/8/1n6/P7/K6k w KQkq - 0 1",
                new List<Move> { new Move(15, 22, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/p7/1P6/K6k w KQkq - 0 1",
                new List<Move> { new Move(14, 23, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/2n5/1P6/K6k w KQkq - 0 1",
                new List<Move> { new Move(14, 21, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/p1n5/1P6/K6k w KQkq - 0 1",
                new List<Move> { new Move(14, 23, MoveTypeEnum.Move), new Move(14, 21, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/1n6/2P5/K6k w KQkq - 0 1",
                new List<Move> { new Move(13, 22, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/3n4/2P5/K6k w KQkq - 0 1",
                new List<Move> { new Move(13, 20, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/1b1n4/2P5/K6k w KQkq - 0 1",
                new List<Move> { new Move(13, 22, MoveTypeEnum.Move), new Move(13, 20, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/2b5/3P4/K6k w KQkq - 0 1",
                new List<Move> { new Move(12, 21, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/4n3/3P4/K6k w KQkq - 0 1",
                new List<Move> { new Move(12, 19, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/2b1n3/K2P4/7k w - - 1 1",
                new List<Move> { new Move(12, 21, MoveTypeEnum.Move), new Move(12, 19, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/3n4/4P3/K6k w KQkq - 0 1",
                new List<Move> { new Move(11, 20, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/5n2/4P3/K6k w KQkq - 0 1",
                new List<Move> { new Move(11, 18, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/3b1n2/4P3/K6k w KQkq - 0 1",
                new List<Move> { new Move(11, 20, MoveTypeEnum.Move), new Move(11, 18, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/4b3/5P2/K6k w KQkq - 0 1",
                new List<Move> { new Move(10, 19, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/6b1/5P2/K6k w KQkq - 0 1",
                new List<Move> { new Move(10, 17, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/4b1n1/5P2/K6k w KQkq - 0 1",
                new List<Move> { new Move(10, 19, MoveTypeEnum.Move), new Move(10, 17, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/5b2/6P1/K6k w KQkq - 0 1",
                new List<Move> { new Move(9, 18, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/7b/6P1/K6k w KQkq - 0 1",
                new List<Move> { new Move(9, 16, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/5r1b/6P1/K6k w KQkq - 0 1",
                new List<Move> { new Move(9, 18, MoveTypeEnum.Move), new Move(9, 16, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/8/8/8/8/6q1/7P/K6k w KQkq - 0 1",
                new List<Move> { new Move(8, 17, MoveTypeEnum.Move) }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
