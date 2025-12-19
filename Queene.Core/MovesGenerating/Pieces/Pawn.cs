using Queene.Core.Consts;
using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.PiecesList;
using Queene.Core.Utils;
using QueeneEngine.Engine.Magics;
using QueeneEngine.Helpers.Bitwise;
using System.Runtime.CompilerServices;

namespace Queene.Core.MovesGenerating.Pieces
{
    public class Pawn : PieceBase, IPiece
    {
        public override PieceTypeEnum PieceType => PieceTypeEnum.Pawn;
        public const int Value = 100;

        private readonly ulong[] EnPassantShiftMasks = [0x1c0000000, 0x1c000000000];
        public readonly ulong[] PromotionRank = [BoardConsts.RANK_1_FULL_STATE, BoardConsts.RANK_8_FULL_STATE];

        public Pawn(BoardContext bitBoardContext,
            IList<Move> movesList,
            IPiecesListService piecesListService,
            ZorbistHash zorbistHash)
            : base(bitBoardContext, movesList, piecesListService, zorbistHash)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GenerateMoves(MoveGenerationTypeEnum generationType)
        {
            GenerateEnPassant();

            for (int i = 0; i < _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.Pawn].Count(); i++)
            {
                _moves = 0;
                _square = _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.Pawn].GetAtIndex(i);

                if (_bitBoardContext.Player.Current == Player.White)
                {
                    if (generationType == MoveGenerationTypeEnum.All 
                        && !BitwiseHelper.IsSet(_bitBoardContext.OccupiedSquares, BoardConsts.SQUARES_FORWARD[_square]))
                        _moves = MovesContainer.PawnWhiteMoves[_square] & _bitBoardContext.EmptySquares;

                    _captures = MovesContainer.PawnWhiteCaptures[_square] & _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)PieceTypeEnum.All];
                }
                else
                {
                    if (generationType == MoveGenerationTypeEnum.All 
                        && !BitwiseHelper.IsSet(_bitBoardContext.OccupiedSquares, BoardConsts.SQUARES_BACKWARD[_square]))
                        _moves = MovesContainer.PawnBlackMoves[_square] & _bitBoardContext.EmptySquares;

                    _captures = MovesContainer.PawnBlackCaptures[_square] & _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)PieceTypeEnum.All];
                }

                if (_bitBoardContext.Attackers != 0)
                {
                    _moves &= _bitBoardContext.CheckedSquares;
                    _captures &= _bitBoardContext.Attackers;
                }

                if ((_moves | _captures) != 0)
                {
                    if ((_bitBoardContext.PinnedSquares & Powers.powersOfTwo[_square]) != 0)
                    {
                        var pinners = _bitBoardContext.Pinners;

                        while (pinners != 0)
                        {
                            var pinnerSquare = BitwiseHelper.FastBitscanRevers(pinners);
                            pinners ^= Powers.powersOfTwo[pinnerSquare];

                            var masksBetweenSquares = SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresRanksAndFiles[pinnerSquare][_bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.King].GetAtIndex(0)];

                            if ((masksBetweenSquares & Powers.powersOfTwo[_square]) != 0)
                            {
                                _moves &= masksBetweenSquares;
                                _captures = 0;

                                break;
                            }
                            else
                            {
                                masksBetweenSquares = SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresDiagonals[pinnerSquare][_bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.King].GetAtIndex(0)];

                                if ((masksBetweenSquares & Powers.powersOfTwo[_square]) != 0)
                                {
                                    _moves = 0;
                                    _captures &= _bitBoardContext.Pinners & Powers.powersOfTwo[pinnerSquare];

                                    break;
                                }
                            }
                        }
                    }
                }

                Generate(generationType, _moves, _captures, _square);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void MakeMove(ExtendedMove move)
        {
            //enPassant
            if (move.MoveType == MoveTypeEnum.EnPassante)
            {
                var capturedSquare = _bitBoardContext.Player.Current == Player.White ? BoardConsts.SQUARES_BACKWARD[move.To] : BoardConsts.SQUARES_FORWARD[move.To];
                _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)PieceTypeEnum.Pawn] ^= Powers.powersOfTwo[capturedSquare];
                _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)PieceTypeEnum.All] ^= Powers.powersOfTwo[capturedSquare];

                ComputeHash();
            }

            base.MakeMove(move);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void UnmakeMove(ExtendedMove move)
        {
            base.UnmakeMove(move);

            //enPassant
            if (move.MoveType == MoveTypeEnum.EnPassante)
            {
                var capturedSquare = _bitBoardContext.Player.Current == Player.White ? BoardConsts.SQUARES_BACKWARD[move.To] : BoardConsts.SQUARES_FORWARD[move.To];
                _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)PieceTypeEnum.Pawn] |= Powers.powersOfTwo[capturedSquare];
                _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)PieceTypeEnum.All] |= Powers.powersOfTwo[capturedSquare];

                ComputeHash();
            }
        }

        private void ComputeHash()
        {
            if (_bitBoardContext.Player.Current == Player.White)
                _bitBoardContext.Hash ^= _zorbistHash.EnPassante[Player.White][_bitBoardContext.EnPassantSquare.Value - 40];
            else
                _bitBoardContext.Hash ^= _zorbistHash.EnPassante[Player.Black][_bitBoardContext.EnPassantSquare.Value - 16];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Generate(MoveGenerationTypeEnum generationType, ulong moves, ulong captures, byte square)
        {
            if (generationType == MoveGenerationTypeEnum.All)
            {
                if ((moves & PromotionRank[_bitBoardContext.Player.Current]) != 0)
                    GeneratePromotions(moves, square);
                else
                    AddMoves(moves, square, MoveTypeEnum.Move);
            }

            if ((captures & PromotionRank[_bitBoardContext.Player.Current]) != 0)
                GeneratePromotions(captures, square);
            else
                AddMoves(captures, square, MoveTypeEnum.Move);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GeneratePromotions(ulong mask, byte square)
        {
            byte toSquare;

            while (mask > 0)
            {
                toSquare = BitwiseHelper.FastBitScanForward(mask);

                _movesList.Add(new Move(square, toSquare, MoveTypeEnum.Promotion, PieceTypeEnum.Knight));
                _movesList.Add(new Move(square, toSquare, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop));
                _movesList.Add(new Move(square, toSquare, MoveTypeEnum.Promotion, PieceTypeEnum.Rook));
                _movesList.Add(new Move(square, toSquare, MoveTypeEnum.Promotion, PieceTypeEnum.Queen));

                mask ^= Powers.powersOfTwo[toSquare];
            }
        }

        private void GenerateEnPassant()
        {
            if (_bitBoardContext.EnPassantSquare.HasValue)
            {
                ulong shift = EnPassantShiftMasks[_bitBoardContext.Player.Current] >> BoardConsts.FILE_FROM_SQUARE[_bitBoardContext.EnPassantSquare.Value];

                if (_bitBoardContext.Attackers != 0)
                {
                    bool cantBlock = (Powers.powersOfTwo[_bitBoardContext.EnPassantSquare.Value] & _bitBoardContext.CheckedSquares) == 0;
                    bool cantCapture = (shift & _bitBoardContext.Attackers) == 0;

                    if (cantBlock && cantCapture)
                        return;
                }

                ulong mask = shift & _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.Pawn];

                mask &= _bitBoardContext.Player.Current == Player.White
                    ? BoardConsts.RANK_5_FULL_STATE
                    : BoardConsts.RANK_4_FULL_STATE;

                byte square;

                while (mask > 0)
                {
                    square = BitwiseHelper.FastBitScanForward(mask);
                    mask ^= Powers.powersOfTwo[square];

                    if (CheckEnPassantMoveMakesDiscoveredCheck(square))
                        continue;

                    _movesList.Add(new Move(square, _bitBoardContext.EnPassantSquare.Value, MoveTypeEnum.EnPassante));
                }
            }
        }

        private bool CheckEnPassantMoveMakesDiscoveredCheck(byte square)
        {
            //clear oponnent pawn
            ulong occupiedSquares = _bitBoardContext.OccupiedSquares;
            occupiedSquares ^= _bitBoardContext.Player.Current == Player.White
                ? Powers.powersOfTwo[BoardConsts.SQUARES_BACKWARD[_bitBoardContext.EnPassantSquare.Value]]
                : Powers.powersOfTwo[BoardConsts.SQUARES_FORWARD[_bitBoardContext.EnPassantSquare.Value]];

            //clear current player pawn
            occupiedSquares ^= Powers.powersOfTwo[square];
            occupiedSquares ^= Powers.powersOfTwo[_bitBoardContext.EnPassantSquare.Value];

            ulong rookAttacked = Slider.GetAttacks(_bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.King].GetAtIndex(0), occupiedSquares, MovesContainer.RookMagics);
            ulong bishopAttacked = Slider.GetAttacks(_bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.King].GetAtIndex(0), occupiedSquares, MovesContainer.BishopMagics);

            return ((rookAttacked & _bitBoardContext.OpponentRooksAndQueens) != 0) 
                || ((bishopAttacked & _bitBoardContext.OpponentBishopsAndQueens) != 0);
        }
    }
}
