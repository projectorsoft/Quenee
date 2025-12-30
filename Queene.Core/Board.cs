using Queene.Core.Converters;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.Models;
using Queene.Core.MovesGenerating;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.Pieces;
using Queene.Core.MovesGenerating.PiecesList;
using QueeneEngine.Helpers.Bitwise;
using System.Runtime.CompilerServices;

namespace Queene.Core
{
    public class Board
    {
        private readonly IList<Move> _movesList = new MovesList();
        private readonly BitBoard _bitBoard;
        private readonly IPiece[] _pieces;
        private readonly IPiecesListService _piecesListService;
        private readonly ZorbistHash _zorbistHash;

        public BoardContext BoardContext => _bitBoard.Context;

        public Board(IBitBoardContextConverter bitBoardContextConverter,
            ZorbistHash zorbistHash)
        {
            _zorbistHash = zorbistHash;
            _bitBoard = new BitBoard(bitBoardContextConverter, zorbistHash);
            _piecesListService = new PiecesListService(_bitBoard.Context);

            _pieces = new IPiece[6];
            _pieces[(byte)PieceTypeEnum.Pawn] = new Pawn(_bitBoard.Context, _movesList, _piecesListService, zorbistHash);
            _pieces[(byte)PieceTypeEnum.King] = new King(_bitBoard.Context, _movesList, _piecesListService, zorbistHash);
            _pieces[(byte)PieceTypeEnum.Rook] = new Rook(_bitBoard.Context, _movesList, _piecesListService, zorbistHash);
            _pieces[(byte)PieceTypeEnum.Knight] = new Knight(_bitBoard.Context, _movesList, _piecesListService, zorbistHash);
            _pieces[(byte)PieceTypeEnum.Bishop] = new Bishop(_bitBoard.Context, _movesList, _piecesListService, zorbistHash);
            _pieces[(byte)PieceTypeEnum.Queen] = new Queen(_bitBoard.Context, _movesList, _piecesListService, zorbistHash);
        }

        public void NewGame(string fen)
        {
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);
            _movesList.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Move[] GenerateMoves(MoveGenerationTypeEnum generationType = MoveGenerationTypeEnum.All)
        {
            _movesList.Clear();
            _bitBoard.Context.SetOpponnentSliders();

            var king = (King)_pieces[(byte)PieceTypeEnum.King];

            _bitBoard.SetupCheckingSquaresAndAttackers(_bitBoard.Context.Player, king.Square);

            king.GenerateMoves(generationType);

            bool doubleCheck = _bitBoard.Context.Attackers > 0 
                && BitwiseHelper.IsMoreThanOneSetBits(_bitBoard.Context.Attackers);

            if (!doubleCheck)
            {
                _bitBoard.SetupPinnedPiecesAndPinners(_bitBoard.Context.Player, king.Square);
                _pieces[(byte)PieceTypeEnum.Pawn].GenerateMoves(generationType);
                _pieces[(byte)PieceTypeEnum.Rook].GenerateMoves(generationType);
                _pieces[(byte)PieceTypeEnum.Knight].GenerateMoves(generationType);
                _pieces[(byte)PieceTypeEnum.Bishop].GenerateMoves(generationType);
                _pieces[(byte)PieceTypeEnum.Queen].GenerateMoves(generationType);
            }

            return _movesList.Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ExtendedMove MakeMove(Move move)
        {
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            _pieces[(byte)extMove.PieceType].MakeMove(extMove);
            _bitBoard.Context.Player.Change();
            _bitBoard.Context.Hash ^= _zorbistHash.Player[_bitBoard.Context.Player.Current];

            return extMove;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UnmakeMove(ExtendedMove extMove)
        {
            _bitBoard.Context.Player.Change();
            _pieces[(byte)extMove.PieceType].UnmakeMove(extMove);
            _bitBoard.Context.Hash ^= _zorbistHash.Player[_bitBoard.Context.Player.Current];
        }

        public string GetFen()
        {
            return _bitBoard.Context.ToString();
        }
    }
}
