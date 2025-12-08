using Queene.Core.Consts;
using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.PiecesList;
using QueeneEngine.Engine.Magics;
using QueeneEngine.Helpers.Bitwise;
using System.Runtime.CompilerServices;

namespace Queene.Core.MovesGenerating.Pieces
{
    public abstract class PieceBase
    {
        public abstract PieceTypeEnum PieceType { get; }

        protected readonly BitBoardContext _bitBoardContext;
        protected readonly MovesContainer _movesContainer;
        protected readonly IList<Move> _movesList;
        private readonly IPiecesListService _piecesListService;

        protected ulong _moves;
        protected ulong _captures;
        protected byte _square;

        public PieceBase(BitBoardContext bitBoardContext,
            MovesContainer movesContainer,
            IList<Move> movesList,
            IPiecesListService piecesListService)
        {
            _bitBoardContext = bitBoardContext;
            _movesContainer = movesContainer;
            _movesList = movesList;
            _piecesListService = piecesListService;

            InitPiecesList();
        }

        public void InitPiecesList()
        {
            _bitBoardContext.InitPiecesList(PlayerEnum.White, PieceType);
            _bitBoardContext.InitPiecesList(PlayerEnum.Black, PieceType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void MakeMove(ExtendedMove move)
        {
            //remove piece from source square
            _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceType] ^= Powers.powersOfTwo[move.From];
            _bitBoardContext.Hash ^= ZorbistHash.Pieces[(byte)PieceType][move.From];

            //set piece at destination square
            if (move.MoveType == MoveTypeEnum.Promotion) //set promoted piece
            {
                _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)move.PromotedTo] |= Powers.powersOfTwo[move.To];
                _bitBoardContext.Hash ^= ZorbistHash.Pieces[(byte)move.PromotedTo][move.To];
            }
            else
            {
                _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceType] |= Powers.powersOfTwo[move.To];
                _bitBoardContext.Hash ^= ZorbistHash.Pieces[(byte)PieceType][move.To];
            }

            if (move.IsPawnDoubleMove())
                _bitBoardContext.EnPassantSquare = _bitBoardContext.Player.Current == Player.White ? (byte)(move.To - 8) : (byte)(move.To + 8);
            else
                _bitBoardContext.EnPassantSquare = null;

            //remove captured piece
            if (move.Captured.HasValue && move.MoveType != MoveTypeEnum.EnPassante)
            {
                _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)move.Captured.Value] ^= Powers.powersOfTwo[move.To];
                _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)PieceTypeEnum.All] ^= Powers.powersOfTwo[move.To];

                _bitBoardContext.Hash ^= ZorbistHash.Pieces[(byte)move.Captured.Value][move.To];
            }

            //update all pieces mask
            _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.All] ^= Powers.powersOfTwo[move.From];
            _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.All] |= Powers.powersOfTwo[move.To];

            _bitBoardContext.OccupiedSquares = _bitBoardContext.Pieces[0][(byte)PieceTypeEnum.All] | _bitBoardContext.Pieces[1][(byte)PieceTypeEnum.All];
            _bitBoardContext.EmptySquares = ~_bitBoardContext.OccupiedSquares;

            _piecesListService.MakeMove(move);
            //_bitBoardContext.ComputeHash(move);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void UnmakeMove(ExtendedMove move)
        {
            _bitBoardContext.EnPassantSquare = move.EnPassanteSquare;

            //restore piece at source square
            _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceType] |= Powers.powersOfTwo[move.From];
            _bitBoardContext.Hash ^= ZorbistHash.Pieces[(byte)PieceType][move.From];

            if (move.MoveType == MoveTypeEnum.Promotion) //remove promoted piece
            {
                _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)move.PromotedTo] ^= Powers.powersOfTwo[move.To];
                _bitBoardContext.Hash ^= ZorbistHash.Pieces[(byte)move.PromotedTo][move.To];
            }
            //remove piece at destination square
            else
            {
                _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceType] ^= Powers.powersOfTwo[move.To];
                _bitBoardContext.Hash ^= ZorbistHash.Pieces[(byte)PieceType][move.To];
            }

            //restore captured piece
            if (move.Captured.HasValue && move.MoveType != MoveTypeEnum.EnPassante)
            {
                _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)move.Captured.Value] |= Powers.powersOfTwo[move.To];
                _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)PieceTypeEnum.All] |= Powers.powersOfTwo[move.To];

                _bitBoardContext.Hash ^= ZorbistHash.Pieces[(byte)move.Captured.Value][move.To];
            }

            //update all pieces mask
            _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.All] |= Powers.powersOfTwo[move.From];
            _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.All] ^= Powers.powersOfTwo[move.To];

            _bitBoardContext.OccupiedSquares = _bitBoardContext.Pieces[0][(byte)PieceTypeEnum.All] | _bitBoardContext.Pieces[1][(byte)PieceTypeEnum.All];
            _bitBoardContext.EmptySquares = ~_bitBoardContext.OccupiedSquares;

            _piecesListService.UnmakeMove(move);
            //_bitBoardContext.ComputeHash(move);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void AddMoves(ulong mask, byte fromSquare, MoveTypeEnum moveType, PieceTypeEnum promotionPieceType = PieceTypeEnum.Knight)
        {
            while (mask > 0)
            {
                _square = BitwiseHelper.FastBitScanForward(mask);

                _movesList.Add(new Move(fromSquare, _square, moveType, promotionPieceType));

                mask ^= Powers.powersOfTwo[_square];
            }
        }
    }
}
