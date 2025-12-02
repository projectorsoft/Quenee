using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class KingMovesCheckedSquaresTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Trapped in corner
            yield return new object[] {
                "k7/8/1K6/8/5Q2/8/8/8 b - - 0 1",
                new List<Move>()
            };
            //All squares checked by enemy pieces
            yield return new object[] {
                "3Q4/7R/4k3/8/8/2B5/8/K4R2 b - - 0 1",
                new List<Move>()
            };
            //All squares checked by sorrounded enemy knight
            yield return new object[] {
                "N5N1/8/1N1k4/8/1N3N2/3N4/8/K7 b - - 0 1",
                new List<Move>()
            };
            //checked by enemy rook in file
            yield return new object[] {
                "8/8/8/5k2/8/8/8/K4R2 b - - 0 1",
                new List<Move>
                {
                    new Move(34, 25, Enums.MoveTypeEnum.Move),
                    new Move(34, 27, Enums.MoveTypeEnum.Move),
                    new Move(34, 33, Enums.MoveTypeEnum.Move),
                    new Move(34, 35, Enums.MoveTypeEnum.Move),
                    new Move(34, 41, Enums.MoveTypeEnum.Move),
                    new Move(34, 43, Enums.MoveTypeEnum.Move)
                }
            };
            //checked by enemy rook in rank
            yield return new object[] {
                "8/8/8/8/1R2k3/8/8/K7 b - - 0 1",
                new List<Move>
                {
                    new Move(27, 18, Enums.MoveTypeEnum.Move),
                    new Move(27, 19, Enums.MoveTypeEnum.Move),
                    new Move(27, 20, Enums.MoveTypeEnum.Move),
                    new Move(27, 34, Enums.MoveTypeEnum.Move),
                    new Move(27, 35, Enums.MoveTypeEnum.Move),
                    new Move(27, 36, Enums.MoveTypeEnum.Move)
                }
            };
            //checked in diagonal
            yield return new object[] {
                "8/8/8/8/8/5k2/8/K2B3B b - - 0 1",
                new List<Move>
                {
                    new Move(18, 10, Enums.MoveTypeEnum.Move),
                    new Move(18, 17, Enums.MoveTypeEnum.Move),
                    new Move(18, 19, Enums.MoveTypeEnum.Move),
                    new Move(18, 26, Enums.MoveTypeEnum.Move)
                }
            };
            //sorrounded by enemy pawns
            yield return new object[] {
                "8/8/8/8/3P1k1P/3P3P/4PPP1/K7 b - - 0 1",
                new List<Move>
                {
                    new Move(26, 34, Enums.MoveTypeEnum.Move)
                }
            };
            //pushed to the border by enemy king
            yield return new object[] {
                "8/8/8/8/3P1k1P/3P3P/4PPP1/K7 b - - 0 1",
                new List<Move>
                {
                    new Move(26, 34, Enums.MoveTypeEnum.Move)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
