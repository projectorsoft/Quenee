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
    public class King : PieceBase, IPiece
    {
        public byte Square => _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.King].GetAtIndex(0);

        public static readonly byte[] InitialSquare = [59, 3];

        public override PieceTypeEnum PieceType => PieceTypeEnum.King;

        //Contains valid square number for castling as a mask
        private readonly ulong[] _castleKingSideDestinationSquareMasks = [0x200000000000000, 0x2];
        private readonly ulong[] _castleQueenSideDestinationSquareMasks = [0x2000000000000000, 0x20];

        private readonly ulong[] _castleKingSideSquaresLine = [0x600000000000000, 0x6];
        private readonly ulong[] _castleQueenSideOccupancySquaresLine = [0x7000000000000000, 0x70];

        private readonly byte[][] _castleKingSideSquares = [[57, 58], [1, 2]];
        private readonly byte[][] _castleQueenSideSquares = [[60, 61], [4, 5]];

        private readonly byte[] _movesCounter = new byte[2];

        public King(BitBoardContext bitBoardContext,
            MovesContainer movesContainer,
            IList<Move> movesList,
            IPiecesListService piecesListService)
            : base(bitBoardContext, movesContainer, movesList, piecesListService)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GenerateMoves(MoveGenerationTypeEnum generationType)
        {
            var square = Square;

            if (generationType == MoveGenerationTypeEnum.All)
            {
                GenerateCastlings(_bitBoardContext.Player.Current, square);

                _moves = _movesContainer.KingMoves[square] & _bitBoardContext.EmptySquares;
            }

            _captures = _movesContainer.KingMoves[square] & _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)PieceTypeEnum.All];

            if (_bitBoardContext.Attackers != 0)
                _moves &= ~_bitBoardContext.CheckedSquares;

            AddMoves(_moves | _captures, square);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void MakeMove(ExtendedMove move)
        {
            if (move.MoveType == MoveTypeEnum.Castle)
            {
                MakeUnmakeCastleMove(move);

                if (move.IsCastleKingSideMove)
                    _bitBoardContext.SetKingSideCastleAllowance(_bitBoardContext.Player.Current, false);
                else if (move.IsCastleQueenSideMove)
                    _bitBoardContext.SetQueenSideCastleAllowance(_bitBoardContext.Player.Current, false);

                _bitBoardContext.SetCastleAllowance(_bitBoardContext.Player.Current, false);
            }
            //first king move - castling is impossible
            else if (_movesCounter[_bitBoardContext.Player.Current] == 0)
            {
                _bitBoardContext.SetCastleAllowance(_bitBoardContext.Player.Current, false);
                _bitBoardContext.Hash ^= ZorbistHash.KingSideCastle[_bitBoardContext.Player.Current];
                _bitBoardContext.Hash ^= ZorbistHash.QueenSideCastle[_bitBoardContext.Player.Current];
            }

            //standard not castling move breaks castling ability
            if (_bitBoardContext.CanCastle[_bitBoardContext.Player.Current])
            {
                move.CastleBreak = true;
                _bitBoardContext.CanCastle[_bitBoardContext.Player.Current] = false;
            }

            _movesCounter[_bitBoardContext.Player.Current]++;

            base.MakeMove(move);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void UnmakeMove(ExtendedMove move)
        {
            _movesCounter[_bitBoardContext.Player.Current]--;

            if (move.MoveType == MoveTypeEnum.Castle)
            {
                MakeUnmakeCastleMove(move);

                if (move.IsCastleKingSideMove)
                    _bitBoardContext.SetKingSideCastleAllowance(_bitBoardContext.Player.Current, true);
                else if (move.IsCastleQueenSideMove)
                    _bitBoardContext.SetQueenSideCastleAllowance(_bitBoardContext.Player.Current, true);

                _bitBoardContext.SetCastleAllowance(_bitBoardContext.Player.Current, true);
            }
            //first king move - castling is again possible
            else if (_movesCounter[_bitBoardContext.Player.Current] == 0) //TODO: case when position read from FEN and king is already somewhere on the board
            {
                _bitBoardContext.SetCastleAllowance(_bitBoardContext.Player.Current, true);
                _bitBoardContext.Hash ^= ZorbistHash.KingSideCastle[_bitBoardContext.Player.Current];
                _bitBoardContext.Hash ^= ZorbistHash.QueenSideCastle[_bitBoardContext.Player.Current];
            }

            if (move.CastleBreak)
                _bitBoardContext.CanCastle[_bitBoardContext.Player.Current] = true;

            base.UnmakeMove(move);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MakeUnmakeCastleMove(ExtendedMove move)
        {
            if (move.IsCastleKingSideMove)
            {
                _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.Rook] ^= Powers.powersOfTwo[Rook.KingSideSourceSquare[_bitBoardContext.Player.Current]];
                _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.Rook] ^= Powers.powersOfTwo[Rook.KingSideDestinationSquare[_bitBoardContext.Player.Current]];
                _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.All] ^= Powers.powersOfTwo[Rook.KingSideSourceSquare[_bitBoardContext.Player.Current]];
                _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.All] ^= Powers.powersOfTwo[Rook.KingSideDestinationSquare[_bitBoardContext.Player.Current]];

                _bitBoardContext.Hash ^= ZorbistHash.KingSideCastle[_bitBoardContext.Player.Current];
            }
            else if (move.IsCastleQueenSideMove)
            {
                _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.Rook] ^= Powers.powersOfTwo[Rook.QueenSideSourceSquare[_bitBoardContext.Player.Current]];
                _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.Rook] ^= Powers.powersOfTwo[Rook.QueenSideDestinationSquare[_bitBoardContext.Player.Current]];
                _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.All] ^= Powers.powersOfTwo[Rook.QueenSideSourceSquare[_bitBoardContext.Player.Current]];
                _bitBoardContext.Pieces[_bitBoardContext.Player.Current][(byte)PieceTypeEnum.All] ^= Powers.powersOfTwo[Rook.QueenSideDestinationSquare[_bitBoardContext.Player.Current]];

                _bitBoardContext.Hash ^= ZorbistHash.QueenSideCastle[_bitBoardContext.Player.Current];
            }
        }

        /// <summary>
        /// Generates castling moves if available
        /// </summary>
        /// <param name="movesList">List of moves</param>
        /// <param name="square">King square</param>
        /// <param name="player">Player</param>
        private void GenerateCastlings(byte player, byte square)
        {
            if (_bitBoardContext.CanCastle[player])
            {
                if (_bitBoardContext.CanCastleKingSide[player] &&
                    Rook.IsRookOnKingSideInitialPosition(_bitBoardContext) &&
                    !AreCastlingSquaresOccupied(_castleKingSideSquaresLine[player]) &&
                    !AreCastlingSquaresUnderAttack(_castleKingSideSquares[player]))
                    AddMoves(_castleKingSideDestinationSquareMasks[player], square, MoveTypeEnum.Castle);

                if (_bitBoardContext.CanCastleQueenSide[player] &&
                    Rook.IsRookOnQueenSideInitialPosition(_bitBoardContext) &&
                    !AreCastlingSquaresOccupied(_castleQueenSideOccupancySquaresLine[player]) &&
                    !AreCastlingSquaresUnderAttack(_castleQueenSideSquares[player]))
                    AddMoves(_castleQueenSideDestinationSquareMasks[player], square, MoveTypeEnum.Castle);
            }
        }

        private void AddMoves(ulong moves, byte fromSquare)
        {
            if (moves == 0)
                return;

            _bitBoardContext.OccupiedSquares ^= Powers.powersOfTwo[Square];

            while (moves > 0)
            {
                _square = BitwiseHelper.FastBitScanForward(moves);

                if (!BitBoard.IsSquareAttacked(_bitBoardContext, _movesContainer, _bitBoardContext.Player, _square))
                    _movesList.Add(new Move(fromSquare, _square, MoveTypeEnum.Move, PieceTypeEnum.Knight));

                moves ^= Powers.powersOfTwo[_square];
            }

            _bitBoardContext.OccupiedSquares ^= Powers.powersOfTwo[Square];
        }

        /// <summary>
        /// Checks if castling squares are under attack
        /// </summary>
        /// <param name="castlingSquaresMask">Mask with squares available in castling type</param>
        /// <returns>True if one of squares is under attack</returns>
        private bool AreCastlingSquaresUnderAttack(byte[] castlingSquares)
        {
            //if (_bitBoardContext.Attackers > 0)
            //    return true;

            if (BitBoard.IsSquareAttacked(_bitBoardContext, _movesContainer, _bitBoardContext.Player, Square))
                    return true;

            for (byte i = 0; i < castlingSquares.Length; i++)
            {
                if (BitBoard.IsSquareAttacked(_bitBoardContext, _movesContainer, _bitBoardContext.Player, castlingSquares[i]))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if castling squares are occupied by other pieces
        /// </summary>
        /// <param name="castlingSquaresMask">Mask with squares available in castling type</param>
        /// <returns>True if one of squares is occupied by other pieces</returns>
        private bool AreCastlingSquaresOccupied(ulong castlingSquaresMask)
        {
            return (castlingSquaresMask & _bitBoardContext.OccupiedSquares) != 0;
        }
    }
}
