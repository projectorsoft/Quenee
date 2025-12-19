using Queene.Core.Consts;
using Queene.Core.Enums;
using Queene.Core.Magics;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.PiecesList;
using Queene.Core.Utils;
using QueeneEngine.Helpers.Bitwise;
using System.Runtime.CompilerServices;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void GenerateMoves(MagicResult[] magics, MoveGenerationTypeEnum generationType, SliderTypeEnum sliderType)
        {
            ulong attacks;

            for (int i = 0; i < _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceType].Count(); i++)
            {
                _square = _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceType].GetAtIndex(i);
                attacks = GetAttacks(_square, _bitBoardContext.OccupiedSquares, magics);

                if (generationType == MoveGenerationTypeEnum.All)
                    _moves = attacks & _bitBoardContext.EmptySquares;

                _captures = attacks & _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)PieceTypeEnum.All];

                if (_bitBoardContext.Attackers != 0)
                {
                    _moves &= _bitBoardContext.CheckedSquares;
                    _captures &= _bitBoardContext.Attackers;
                }

                if (_bitBoardContext.Pinners != 0)
                {
                    if ((_bitBoardContext.PinnedSquares & Powers.powersOfTwo[_square]) != 0)
                    {
                        var pinners = attacks & _bitBoardContext.Pinners;

                        if (pinners == 0)
                            _moves = _captures = 0;
                        else
                            while (pinners > 0)
                            {
                                var pinnerSquare = BitwiseHelper.FastBitScanForward(pinners);
                                var masksBeetwenSquares = GetMaskBeetwenSquares(pinnerSquare, sliderType);

                                pinners ^= Powers.powersOfTwo[pinnerSquare];

                                if ((masksBeetwenSquares & Powers.powersOfTwo[_square]) == 0)
                                {
                                    if (pinners == 0)
                                        _moves = _captures = 0;

                                    continue;
                                }

                                _moves &= (masksBeetwenSquares ^ _bitBoardContext.PinnedSquares);
                                _captures &= (_bitBoardContext.Pinners & Powers.powersOfTwo[pinnerSquare]);

                                break;
                            }
                    }
                }

                AddMoves(_moves | _captures, _square, MoveTypeEnum.Move);
            }
        }

        private ulong GetMaskBeetwenSquares(byte pinnerSquare, SliderTypeEnum sliderType)
        {
            if (sliderType == SliderTypeEnum.Bishop)
                return SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresDiagonals[pinnerSquare][_bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.King].GetAtIndex(0)];

            return SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresRanksAndFiles[pinnerSquare][_bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.King].GetAtIndex(0)];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetAttacks(byte square, ulong occupied, MagicResult[] magics)
        {
            occupied &= magics[square].Mask;
            occupied *= magics[square].Magic;
            occupied >>= magics[square].Shift;
            return magics[square].Attacks[occupied];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetXRayAttacks(ulong occcupied, ulong blockers, byte square, MagicResult[] magics)
        {
            ulong attacks = GetAttacks(square, occcupied, magics);
            blockers &= attacks;
            return attacks ^ GetAttacks(square, occcupied ^ blockers, magics);
        }
    }
}
