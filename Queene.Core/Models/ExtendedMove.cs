using Queene.Core.Consts;
using Queene.Core.Enums;
using Queene.Core.MovesGenerating;
using System;

namespace Queene.Core.Models
{
    public struct ExtendedMove
    {
        public Move Move { get; private set; }
        public byte From { get; private set; }
        public byte To { get; private set; }
        public PieceTypeEnum PieceType { get; private set; }
        public PieceTypeEnum? Captured { get; private set; }
        public PieceTypeEnum? PromotedTo { get; private set; }
        public MoveTypeEnum MoveType { get; private set; }
        public int Value { get; set; }

        public bool CastleBreak { get; set; }

        /// <summary>
        /// Set to true when rook breaks king side castle rule
        /// </summary>
        public bool KingSideCastleBreak { get; set; }

        /// <summary>
        /// Set to true when rook breaks queen side castle rule
        /// </summary>
        public bool QueenSideCastleBreak { get; set; }

        public byte? EnPassanteSquare { get; set; }
        public byte? CaptureSquare { get; set; }

        public bool IsCastleKingSideMove => MoveType == MoveTypeEnum.Castle && To < From;
        public bool IsCastleQueenSideMove => MoveType == MoveTypeEnum.Castle && To > From;

        public ExtendedMove(Move move, BoardContext context)
        {
            Move = move;
            From = move.GetFromSquare();
            To = move.GetToSquare();
            MoveType = move.GetMoveType();

            if (MoveType == MoveTypeEnum.Promotion)
                PromotedTo = move.GetPromotionPieceType();
            else
                PromotedTo = PieceTypeEnum.Empty;

            //KingSideCastleBreak = false;
            //QueenSideCastleBreak = false;
            PieceType = context.GetPieceType(From, context.Player.Current);

            if (MoveType == MoveTypeEnum.EnPassante)
            {
                CaptureSquare = context.Player.Current == Player.White ? BoardConsts.SQUARES_BACKWARD[To] : BoardConsts.SQUARES_FORWARD[To];
                Captured = context.GetCapturedPieceType(CaptureSquare.Value, context.Player.Oponnent);
            }
            else
                if (MoveType != MoveTypeEnum.Castle)
                {
                    Captured = context.GetCapturedPieceType(To, context.Player.Oponnent);

                    if (Captured != null)
                        CaptureSquare = To;
                }

            EnPassanteSquare = context.EnPassantSquare;
        }

        public bool IsPawnDoubleMove()
        {
            if (PieceType != PieceTypeEnum.Pawn)
                return false;

            return Math.Abs(To - From) == 16;
        }

        public override string ToString()
        {
            //if (MoveType == MoveTypeEnum.Castle)
            //    return IsCastleKingSideMove ? "O-O" : "O-O-O";

            //if (Captured.HasValue)
            //{
            //    var str = $"{BoardConsts.PIECE_TYPE_SYMBOL[PieceType]}{BoardConsts.SQUARES_NAMES[From]}x{BoardConsts.PIECE_TYPE_SYMBOL[Captured.Value]}{BoardConsts.SQUARES_NAMES[To]}({Captured})";

            //    if (PromotedTo.HasValue)
            //        str += $":{BoardConsts.PIECE_TYPE_SYMBOL[PromotedTo.Value]}";

            //    return str;
            //}

            //return $"{BoardConsts.PIECE_TYPE_SYMBOL[PieceType]}{BoardConsts.SQUARES_NAMES[From]}-{BoardConsts.SQUARES_NAMES[To]}";

            return $"{BoardConsts.SQUARES_NAMES[From]}{BoardConsts.SQUARES_NAMES[To]}";
        }
    }
}
