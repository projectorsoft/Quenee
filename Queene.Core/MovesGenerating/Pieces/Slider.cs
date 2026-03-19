using Queene.Core.Consts;
using Queene.Core.Enums;
using Queene.Core.Magics;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.PiecesList;
using Queene.Core.Utils;
using QueeneEngine.Helpers.Bitwise;

namespace Queene.Core.MovesGenerating.Pieces
{
    public abstract class Slider : PieceBase
    {
        public Slider(BoardContext bitBoardContext,
            IList<Move> movesList,
            IPiecesListService piecesListService,
            ZorbistHash zorbistHash)
            : base(bitBoardContext, movesList, piecesListService, zorbistHash)
        {
        }

        protected void GenerateMoves(MagicResult[] magics, MoveGenerationTypeEnum generationType, SliderTypeEnum sliderType)
        {
            var player = _bitBoardContext.Player.Current;
            var opp = _bitBoardContext.Player.Oponnent;
            var pieceList = _bitBoardContext.PieceTypeList[player][(byte)PieceType];
            var count = pieceList.Count();
            var occupied = _bitBoardContext.OccupiedSquares;
            var empty = _bitBoardContext.EmptySquares;
            var attackers = _bitBoardContext.Attackers;
            var checkedSquares = _bitBoardContext.CheckedSquares;
            var pinnersAll = _bitBoardContext.Pinners;
            var pinnedSquares = _bitBoardContext.PinnedSquares;
            var oppAll = _bitBoardContext.Pieces[opp][(byte)PieceTypeEnum.All];
            var kingSquare = _bitBoardContext.PieceTypeList[player][(byte)PieceTypeEnum.King].GetAtIndex(0);

            for (int i = 0; i < count; i++)
            {
                _moves = 0;
                _captures = 0;

                _square = pieceList.GetAtIndex(i);

                var attacks = GetAttacks(occupied, magics[_square]);

                if (generationType == MoveGenerationTypeEnum.All)
                    _moves = attacks & empty;

                _captures = attacks & oppAll;

                if (attackers != 0)
                {
                    _moves &= checkedSquares;
                    _captures &= attackers;
                }

                if (pinnersAll != 0 && (pinnedSquares & Powers.powersOfTwo[_square]) != 0)
                {
                    var pinners = attacks & pinnersAll;

                    if (pinners == 0)
                    {
                        _moves = 0;
                        _captures = 0;
                    }
                    else
                    {
                        var valid = false;
                        while (pinners != 0)
                        {
                            var pinnerSquare = BitwiseHelper.FastBitScanForward(pinners);
                            pinners ^= Powers.powersOfTwo[pinnerSquare];

                            ulong masksBetween;
                            if (sliderType == SliderTypeEnum.Bishop)
                                masksBetween = SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresDiagonals[pinnerSquare][kingSquare];
                            else
                                masksBetween = SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresRanksAndFiles[pinnerSquare][kingSquare];

                            if ((masksBetween & Powers.powersOfTwo[_square]) == 0)
                                continue;

                            _moves &= (masksBetween ^ pinnedSquares);
                            _captures &= (_bitBoardContext.Pinners & Powers.powersOfTwo[pinnerSquare]);

                            valid = true;
                            break;
                        }

                        if (!valid)
                        {
                            _moves = 0;
                            _captures = 0;
                        }
                    }
                }

                var combined = _moves | _captures;
                if (combined != 0)
                    AddMoves(combined, _square, MoveTypeEnum.Move);
            }
        }

        private ulong GetMaskBeetwenSquares(byte pinnerSquare, SliderTypeEnum sliderType)
        {
            if (sliderType == SliderTypeEnum.Bishop)
                return SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresDiagonals[pinnerSquare][_bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.King].GetAtIndex(0)];

            return SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresRanksAndFiles[pinnerSquare][_bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.King].GetAtIndex(0)];
        }

        public static ulong GetAttacks(ulong occupied, MagicResult magic)
        {
            occupied &= magic.Mask;
            occupied *= magic.Magic;
            occupied >>= magic.Shift;
            return magic.Attacks[occupied];
        }

        public static ulong GetXRayAttacks(ulong occcupied, ulong blockers, MagicResult magic)
        {
            ulong attacks = GetAttacks(occcupied, magic);
            blockers &= attacks;
            return attacks ^ GetAttacks(occcupied ^ blockers, magic);
        }
    }
}
