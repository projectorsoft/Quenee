using Queene.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.MoveGenerating.Pieces.TestData
{
    public class KingCapturesCheckedSquaresTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //Check in file, enemy piece behind
            yield return new object[] {
                "8/8/8/4b3/4K3/8/8/k3r3 w - - 0 1",
                new List<Move>()
            };
            //Checked in file, can capture other piece
            yield return new object[] {
                "8/8/8/5b2/4K3/8/8/k3r3 w - - 0 1",
                new List<Move>
                {
                    new Move(27, 34, Enums.MoveTypeEnum.Move)
                }
            };
            //Check in rank, enemy piece behind
            yield return new object[] {
                "8/8/8/8/2r1Kb2/8/8/k7 w - - 0 1",
                new List<Move>()
            };
            //checked by enemy rook in file
            yield return new object[] {
                "8/8/8/8/2r1K3/3b4/8/k7 w - - 0 1",
                new List<Move>
                {
                    new Move(27, 20, Enums.MoveTypeEnum.Move)
                }
            };
            //Check in diagonal, enemy piece behind
            yield return new object[] {
                "8/8/8/5r2/4K3/8/8/kb6 w - - 0 1",
                new List<Move>()
            };
            //Check in diagonal, enemy piece behind, can capture attacker
            yield return new object[] {
                "8/8/8/5r2/4K3/3b4/8/k7 w - - 0 1",
                new List<Move>
                {
                    new Move(27, 20, Enums.MoveTypeEnum.Move)
                }
            };
            //Enemy rook protected by knight
            yield return new object[] {
                "8/8/8/8/1n2K3/3r4/8/k7 w - - 0 1",
                new List<Move>()
            };
            //Three pawns to capture
            yield return new object[] {
                "8/8/8/3p1p2/4K3/4p3/8/k7 w - - 0 1",
                new List<Move>
                {
                    new Move(27, 19, Enums.MoveTypeEnum.Move),
                    new Move(27, 34, Enums.MoveTypeEnum.Move),
                    new Move(27, 36, Enums.MoveTypeEnum.Move)
                }
            };
            //Two pawns protected by third pawn (free to capture)
            yield return new object[] {
                "8/8/8/4p3/3pKp2/8/6n1/k7 w - - 0 1",
                new List<Move>
                {
                    new Move(27, 35, Enums.MoveTypeEnum.Move)
                }
            };
            //All pawns protected
            yield return new object[] {
                "8/8/6n1/4p3/3pKp2/8/8/k7 w - - 0 1",
                new List<Move>()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
