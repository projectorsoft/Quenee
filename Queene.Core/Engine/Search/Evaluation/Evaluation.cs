using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating;
using Queene.Core.MovesGenerating.Pieces;
using System;

namespace Queene.Core.Engine.Search.Evaluation
{
    public static class Evaluation
    {
        public static readonly int[] SideToMoveMultiplier = [-1, 1];
        public static int Evaluate(BoardContext context)
        {
            var random = new Random();

            var pawnsScore = Pawn.Value * (context.PieceTypeList[Player.White][(byte)PieceTypeEnum.Pawn].Count() - context.PieceTypeList[Player.Black][(byte)PieceTypeEnum.Pawn].Count());
            var rooksScore = Rook.Value * (context.PieceTypeList[Player.White][(byte)PieceTypeEnum.Rook].Count() - context.PieceTypeList[Player.Black][(byte)PieceTypeEnum.Rook].Count());
            var queensScore = Queen.Value * (context.PieceTypeList[Player.White][(byte)PieceTypeEnum.Queen].Count() - context.PieceTypeList[Player.Black][(byte)PieceTypeEnum.Queen].Count());
            var knightsScore = Knight.Value * (context.PieceTypeList[Player.White][(byte)PieceTypeEnum.Knight].Count() - context.PieceTypeList[Player.Black][(byte)PieceTypeEnum.Knight].Count());
            var bishopsScore = Bishop.Value * (context.PieceTypeList[Player.White][(byte)PieceTypeEnum.Bishop].Count() - context.PieceTypeList[Player.Black][(byte)PieceTypeEnum.Bishop].Count());
            var kingScore = King.Value * (context.PieceTypeList[Player.White][(byte)PieceTypeEnum.King].Count() - context.PieceTypeList[Player.Black][(byte)PieceTypeEnum.King].Count());

            return (pawnsScore + rooksScore + queensScore + knightsScore + bishopsScore) * SideToMoveMultiplier[context.Player.Current];
        }
    }
}
