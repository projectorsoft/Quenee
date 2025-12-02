using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class PawnBlackCapturesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "8/p7/1Q6/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(55, 46, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/1p6/Q7/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(54, 47, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/1p6/2Q5/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(54, 45, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/1p6/Q1P5/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(54, 47, MoveTypeEnum.Move), new Move(54, 45, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/2p5/1Q6/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(53, 46, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/2p5/3Q4/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(53, 44, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/2p5/1N1Q4/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(53, 46, MoveTypeEnum.Move), new Move(53, 44, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/3p4/2N5/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(52, 45, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/3p4/4N3/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(52, 43, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/3p4/2N1Q3/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(52, 45, MoveTypeEnum.Move), new Move(52, 43, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/4p3/3N4/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(51, 44, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/4p3/5N2/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(51, 42, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/4p3/3B1N2/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(51, 44, MoveTypeEnum.Move), new Move(51, 42, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/5p2/4B3/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(50, 43, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/5p2/6B1/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(50, 41, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/5p2/4B1N1/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(50, 43, MoveTypeEnum.Move), new Move(50, 41, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/6p1/5B2/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(49, 42, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/6p1/7B/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(49, 40, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/6p1/5P1B/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(49, 42, MoveTypeEnum.Move), new Move(49, 40, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/7p/6P1/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(48, 41, MoveTypeEnum.Move) }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
