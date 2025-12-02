using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class PawnBlackSecondRankMovesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "8/p7/8/8/P7/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(55, 47, MoveTypeEnum.Move), new Move(55, 39, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/p7/8/P7/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(55, 47, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/p7/P7/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "8/1p6/8/8/1P6/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(54, 46, MoveTypeEnum.Move), new Move(54, 38, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/1p6/8/1P6/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(54, 46, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/1p6/1P6/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "8/2p5/8/8/2P5/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(53, 45, MoveTypeEnum.Move), new Move(53, 37, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/2p5/8/2P5/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(53, 45, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/2p5/2P5/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "8/3p4/8/8/3P4/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(52, 44, MoveTypeEnum.Move), new Move(52, 36, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/3p4/8/3P4/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(52, 44, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/3p4/3P4/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "8/4p3/8/8/4P3/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(51, 43, MoveTypeEnum.Move), new Move(51, 35, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/4p3/8/4P3/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(51, 43, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/5p2/5P2/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "8/5p2/8/8/5P2/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(50, 42, MoveTypeEnum.Move), new Move(50, 34, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/5p2/8/5P2/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(50, 42, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/5p2/5P2/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "8/6p1/8/8/6P1/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(49, 41, MoveTypeEnum.Move), new Move(49, 33, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/6p1/8/6P1/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(49, 41, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/6p1/6P1/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "8/7p/8/8/7P/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(48, 40, MoveTypeEnum.Move), new Move(48, 32, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/7p/8/7P/8/8/8/K6k b KQkq - 0 1",
                new List<Move> { new Move(48, 40, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "8/7p/7P/8/8/8/8/K6k b KQkq - 0 1",
                new List<Move>()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
