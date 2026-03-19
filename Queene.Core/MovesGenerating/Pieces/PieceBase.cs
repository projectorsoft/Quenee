using Queene.Core.Consts;
using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.PiecesList;
using QueeneEngine.Helpers.Bitwise;

namespace Queene.Core.MovesGenerating.Pieces
{
    public abstract class PieceBase
    {
        public abstract PieceTypeEnum PieceType { get; }

        protected readonly BoardContext _bitBoardContext;
        protected readonly IList<Move> _movesList;
        protected readonly ZorbistHash _zorbistHash;
        private readonly IPiecesListService _piecesListService;

        protected ulong _moves;
        protected ulong _captures;
        protected byte _square;

        public PieceBase(BoardContext bitBoardContext,
            IList<Move> movesList,
            IPiecesListService piecesListService,
            ZorbistHash zorbistHash)
        {
            _bitBoardContext = bitBoardContext;
            _movesList = movesList;
            _piecesListService = piecesListService;
            _zorbistHash = zorbistHash;
            InitPiecesList();
        }

        public void InitPiecesList()
        {
            _bitBoardContext.InitPiecesList(PlayerEnum.White, PieceType);
            _bitBoardContext.InitPiecesList(PlayerEnum.Black, PieceType);
        }

        public virtual void MakeMove(ref ExtendedMove move)
        {
            byte player = _bitBoardContext.Player.Current;
            byte opponent = _bitBoardContext.Player.Oponnent;
            var piecesPlayer = _bitBoardContext.Pieces[player];
            var piecesOpponent = _bitBoardContext.Pieces[opponent];
            var zPieces = _zorbistHash.Pieces;

            byte pieceTypeByte = (byte)PieceType;
            ulong fromMask = Powers.powersOfTwo[move.From];
            ulong toMask = Powers.powersOfTwo[move.To];

            piecesPlayer[pieceTypeByte] ^= fromMask;
            _bitBoardContext.Hash ^= zPieces[player][pieceTypeByte][move.From];

            if (move.MoveType == MoveTypeEnum.Promotion)
            {
                byte promoted = (byte)move.PromotedTo.Value;
                piecesPlayer[promoted] |= toMask;
                _bitBoardContext.Hash ^= zPieces[player][promoted][move.To];
            }
            else
            {
                piecesPlayer[pieceTypeByte] |= toMask;
                _bitBoardContext.Hash ^= zPieces[player][pieceTypeByte][move.To];
            }

            if (move.IsPawnDoubleMove())
                _bitBoardContext.EnPassantSquare = _bitBoardContext.Player.Current == Player.White ? (byte)(move.To - 8) : (byte)(move.To + 8);
            else
                _bitBoardContext.EnPassantSquare = null;

            if (move.Captured.HasValue)
            {
                byte capType = (byte)move.Captured.Value;
                byte capSquare = move.CaptureSquare.Value;
                ulong capMask = Powers.powersOfTwo[capSquare];

                piecesOpponent[capType] ^= capMask;
                piecesOpponent[(byte)PieceTypeEnum.All] ^= capMask;

                _bitBoardContext.Hash ^= zPieces[opponent][capType][capSquare];
            }

            piecesPlayer[(byte)PieceTypeEnum.All] ^= fromMask;
            piecesPlayer[(byte)PieceTypeEnum.All] |= toMask;

            _bitBoardContext.OccupiedSquares = _bitBoardContext.Pieces[0][(byte)PieceTypeEnum.All] | _bitBoardContext.Pieces[1][(byte)PieceTypeEnum.All];
            _bitBoardContext.EmptySquares = ~_bitBoardContext.OccupiedSquares;

            _piecesListService.MakeMove(move);
        }

        public virtual void UnmakeMove(ref ExtendedMove move)
        {
            byte player = _bitBoardContext.Player.Current;
            byte opponent = _bitBoardContext.Player.Oponnent;
            var piecesPlayer = _bitBoardContext.Pieces[player];
            var piecesOpponent = _bitBoardContext.Pieces[opponent];
            var zPieces = _zorbistHash.Pieces;

            byte pieceTypeByte = (byte)PieceType;
            ulong fromMask = Powers.powersOfTwo[move.From];
            ulong toMask = Powers.powersOfTwo[move.To];

            _bitBoardContext.EnPassantSquare = move.EnPassanteSquare;

            piecesPlayer[pieceTypeByte] |= fromMask;
            _bitBoardContext.Hash ^= zPieces[player][pieceTypeByte][move.From];

            if (move.MoveType == MoveTypeEnum.Promotion)
            {
                byte promoted = (byte)move.PromotedTo.Value;
                piecesPlayer[promoted] ^= toMask;
                _bitBoardContext.Hash ^= zPieces[player][promoted][move.To];
            }
            else
            {
                piecesPlayer[pieceTypeByte] ^= toMask;
                _bitBoardContext.Hash ^= zPieces[player][pieceTypeByte][move.To];
            }

            if (move.Captured.HasValue)
            {
                byte capType = (byte)move.Captured.Value;
                byte capSquare = move.CaptureSquare.Value;
                ulong capMask = Powers.powersOfTwo[capSquare];

                piecesOpponent[capType] |= capMask;
                piecesOpponent[(byte)PieceTypeEnum.All] |= capMask;

                _bitBoardContext.Hash ^= zPieces[opponent][capType][capSquare];
            }

            piecesPlayer[(byte)PieceTypeEnum.All] |= fromMask;
            piecesPlayer[(byte)PieceTypeEnum.All] ^= toMask;

            _bitBoardContext.OccupiedSquares = _bitBoardContext.Pieces[0][(byte)PieceTypeEnum.All] | _bitBoardContext.Pieces[1][(byte)PieceTypeEnum.All];
            _bitBoardContext.EmptySquares = ~_bitBoardContext.OccupiedSquares;

            _piecesListService.UnmakeMove(move);
        }

        protected void AddMoves(ulong mask, byte fromSquare, MoveTypeEnum moveType, PieceTypeEnum promotionPieceType = PieceTypeEnum.Knight)
        {
            var movesList = _movesList;
            var localFrom = fromSquare;
            var localMoveType = moveType;
            var localPromotion = promotionPieceType;

            while (mask != 0)
            {
                var sq = BitwiseHelper.FastBitScanForward(mask);
                movesList.Add(new Move(localFrom, sq, localMoveType, localPromotion));
                mask &= mask - 1UL;
            }
        }
    }
}
