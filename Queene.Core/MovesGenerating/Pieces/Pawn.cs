using Queene.Core.Consts;
using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.PiecesList;
using Queene.Core.Utils;
using QueeneEngine.Engine.Magics;
using QueeneEngine.Helpers.Bitwise;

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

        public void GenerateMoves(MoveGenerationTypeEnum generationType)
        {
            GenerateEnPassant();

            var player = _bitBoardContext.Player.Current;
            var opp = _bitBoardContext.Player.Oponnent;

            var pawnList = _bitBoardContext.PieceTypeList[player][(byte)PieceTypeEnum.Pawn];
            var pawnCount = pawnList.Count();
            var occupied = _bitBoardContext.OccupiedSquares;
            var empty = _bitBoardContext.EmptySquares;
            var kingSquare = _bitBoardContext.PieceTypeList[player][(byte)PieceTypeEnum.King].GetAtIndex(0);
            var pinnedSquares = _bitBoardContext.PinnedSquares;
            var pinnersAll = _bitBoardContext.Pinners;
            var attackers = _bitBoardContext.Attackers;
            var checkedSquares = _bitBoardContext.CheckedSquares;

            var isWhite = player == Player.White;
            var pawnMoves = isWhite ? MovesContainer.PawnWhiteMoves : MovesContainer.PawnBlackMoves;
            var pawnCaptures = isWhite ? MovesContainer.PawnWhiteCaptures : MovesContainer.PawnBlackCaptures;
            var forwardSquares = isWhite ? BoardConsts.SQUARES_FORWARD : BoardConsts.SQUARES_BACKWARD;

            for (int i = 0; i < pawnCount; i++)
            {
                _moves = 0;
                _captures = 0;

                _square = pawnList.GetAtIndex(i);

                if (generationType == MoveGenerationTypeEnum.All)
                {
                    var fwdSq = forwardSquares[_square];
                    if (!BitwiseHelper.IsSet(occupied, fwdSq))
                        _moves = pawnMoves[_square] & empty;
                }

                _captures = pawnCaptures[_square] & _bitBoardContext.Pieces[opp][(byte)PieceTypeEnum.All];

                if (attackers != 0)
                {
                    _moves &= checkedSquares;
                    _captures &= attackers;
                }

                if ((pinnedSquares & Powers.powersOfTwo[_square]) != 0)
                {
                    var pinners = pinnersAll;
                    while (pinners != 0)
                    {
                        var pinnerSquare = BitwiseHelper.FastBitscanRevers(pinners);
                        pinners ^= Powers.powersOfTwo[pinnerSquare];

                        var masksBetween = SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresRanksAndFiles[pinnerSquare][kingSquare];
                        if ((masksBetween & Powers.powersOfTwo[_square]) != 0)
                        {
                            _moves &= masksBetween;
                            _captures = 0;
                            break;
                        }

                        masksBetween = SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresDiagonals[pinnerSquare][kingSquare];
                        if ((masksBetween & Powers.powersOfTwo[_square]) != 0)
                        {
                            _moves = 0;
                            _captures &= _bitBoardContext.Pinners & Powers.powersOfTwo[pinnerSquare];
                            break;
                        }
                    }
                }

                if ((_moves | _captures) != 0)
                    Generate(generationType, _moves, _captures, _square);
            }
        }

        public override void MakeMove(ref ExtendedMove move)
        {
            base.MakeMove(ref move);
        }

        public override void UnmakeMove(ref ExtendedMove move)
        {
            base.UnmakeMove(ref move);
        }

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

        private void GeneratePromotions(ulong mask, byte square)
        {
            var movesList = _movesList;
            var localFrom = square;

            while (mask != 0)
            {
                var sq = BitwiseHelper.FastBitScanForward(mask);

                movesList.Add(new Move(square, sq, MoveTypeEnum.Promotion, PieceTypeEnum.Knight));
                movesList.Add(new Move(square, sq, MoveTypeEnum.Promotion, PieceTypeEnum.Bishop));
                movesList.Add(new Move(square, sq, MoveTypeEnum.Promotion, PieceTypeEnum.Rook));
                movesList.Add(new Move(square, sq, MoveTypeEnum.Promotion, PieceTypeEnum.Queen));

                // clear least-significant set bit (faster than index lookup to powers array)
                mask &= mask - 1UL;
            }
        }

        private void GenerateEnPassant()
        {
            var enpOpt = _bitBoardContext.EnPassantSquare;
            if (!enpOpt.HasValue)
                return;

            var enp = enpOpt.Value;
            var player = _bitBoardContext.Player.Current;
            var attackers = _bitBoardContext.Attackers;
            var checkedSquares = _bitBoardContext.CheckedSquares;

            var pawnMask = _bitBoardContext.Pieces[player][(byte)PieceTypeEnum.Pawn];
            var shift = EnPassantShiftMasks[player] >> BoardConsts.FILE_FROM_SQUARE[enp];

            if (attackers != 0)
            {
                bool cantBlock = (Powers.powersOfTwo[enp] & checkedSquares) == 0;
                bool cantCapture = (shift & attackers) == 0;

                if (cantBlock && cantCapture)
                    return;
            }

            ulong mask = shift & pawnMask;

            mask &= player == Player.White
                ? BoardConsts.RANK_5_FULL_STATE
                : BoardConsts.RANK_4_FULL_STATE;

            if (mask == 0)
                return;

            var movesList = _movesList;

            while (mask != 0)
            {
                var sq = BitwiseHelper.FastBitScanForward(mask);
                mask &= mask - 1UL;

                if (CheckEnPassantMoveMakesDiscoveredCheck((byte)sq))
                    continue;

                movesList.Add(new Move((byte)sq, enp, MoveTypeEnum.EnPassante));
            }
        }

        private bool CheckEnPassantMoveMakesDiscoveredCheck(byte square)
        {
            if (_bitBoardContext.OpponentRooksAndQueens == 0 && _bitBoardContext.OpponentBishopsAndQueens == 0)
                return false;

            ulong occupiedSquares = _bitBoardContext.OccupiedSquares;
            occupiedSquares ^= _bitBoardContext.Player.Current == Player.White
                ? Powers.powersOfTwo[BoardConsts.SQUARES_BACKWARD[_bitBoardContext.EnPassantSquare.Value]]
                : Powers.powersOfTwo[BoardConsts.SQUARES_FORWARD[_bitBoardContext.EnPassantSquare.Value]];

            occupiedSquares ^= Powers.powersOfTwo[square];
            occupiedSquares ^= Powers.powersOfTwo[_bitBoardContext.EnPassantSquare.Value];

            var kingSquare = _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.King].GetAtIndex(0);
            ulong rookAttacked = Slider.GetAttacks(occupiedSquares, MovesContainer.RookMagics[kingSquare]);
            ulong bishopAttacked = Slider.GetAttacks(occupiedSquares, MovesContainer.BishopMagics[kingSquare]);

            return ((rookAttacked & _bitBoardContext.OpponentRooksAndQueens) != 0) 
                || ((bishopAttacked & _bitBoardContext.OpponentBishopsAndQueens) != 0);
        }
    }
}
