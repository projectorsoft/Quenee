using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class PawnWhiteSecondRankMovesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "K6k/8/8/8/8/8/P7/8 w KQkq - 0 1",
                new List<Move> { new Move(15, 23, MoveTypeEnum.Move), new Move(15, 31, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/p7/8/P7/8 w KQkq - 0 1",
                new List<Move> { new Move(15, 23, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/8/p7/P7/8 w KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "K6k/8/8/1p6/8/8/1P6/8 w KQkq - 0 1",
                new List<Move> { new Move(14, 22, MoveTypeEnum.Move), new Move(14, 30, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/1p6/8/1P6/8 w KQkq - 0 1",
                new List<Move> { new Move(14, 22, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/8/1p6/1P6/8 w KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "K6k/8/8/2p5/8/8/2P5/8 w KQkq - 0 1",
                new List<Move> { new Move(13, 21, MoveTypeEnum.Move), new Move(13, 29, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/2p5/8/2P5/8 w KQkq - 0 1",
                new List<Move> { new Move(13, 21, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/8/2p5/2P5/8 w KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "K6k/8/8/3p4/8/8/3P4/8 w KQkq - 0 1",
                new List<Move> { new Move(12, 20, MoveTypeEnum.Move), new Move(12, 28, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/3p4/8/3P4/8 w KQkq - 0 1",
                new List<Move> { new Move(12, 20, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/8/3p4/3P4/8 w KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "K6k/8/8/4p3/8/8/4P3/8 w KQkq - 0 1",
                new List<Move> { new Move(11, 19, MoveTypeEnum.Move), new Move(11, 27, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/4p3/8/4P3/8 w KQkq - 0 1",
                new List<Move> { new Move(11, 19, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/8/4p3/4P3/8 w KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "K6k/8/8/5p2/8/8/5P2/8 w KQkq - 0 1",
                new List<Move> { new Move(10, 18, MoveTypeEnum.Move), new Move(10, 26, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/5p2/8/5P2/8 w KQkq - 0 1",
                new List<Move> { new Move(10, 18, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/8/5p2/5P2/8 w KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "K6k/8/8/6p1/8/8/6P1/8 w KQkq - 0 1",
                new List<Move> { new Move(9, 17, MoveTypeEnum.Move), new Move(9, 25, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/6p1/8/6P1/8 w KQkq - 0 1",
                new List<Move> { new Move(9, 17, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/8/6p1/6P1/8 w KQkq - 0 1",
                new List<Move>()
            };
            yield return new object[] {
                "K6k/8/8/7p/8/8/7P/8 w KQkq - 0 1",
                new List<Move> { new Move(8, 16, MoveTypeEnum.Move), new Move(8, 24, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/7p/8/7P/8 w KQkq - 0 1",
                new List<Move> { new Move(8, 16, MoveTypeEnum.Move) }
            };
            yield return new object[] {
                "K6k/8/8/8/8/7p/7P/8 w KQkq - 0 1",
                new List<Move>()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
