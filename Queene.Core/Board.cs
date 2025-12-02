using Queene.Core.Converters;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.Magics;
using Queene.Core.Models;
using Queene.Core.MovesGenerating;
using Queene.Core.MovesGenerating.Pieces;
using Queene.Core.MovesGenerating.PiecesList;
using QueeneEngine.Engine.Magics;
using QueeneEngine.Helpers.Bitwise;
using System.Runtime.CompilerServices;

namespace Queene.Core
{
    public class Board
    {
        private readonly IMagicsBinaryPersisterService _magicsBinaryPersister;
        private readonly IList<Move> _movesList = new MovesList();
        private readonly MovesContainer _movesContainer;
        private readonly BitBoard _bitBoard;
        private readonly IPiece[] _pieces;
        private readonly IPiecesListService _piecesListService;

        public BitBoardContext BitBoardContext => _bitBoard.Context;

        public Board(IBitBoardContextConverter bitBoardContextConverter)
        {
            _magicsBinaryPersister = new MagicsBinaryPersisterService();
            _movesContainer = new MovesContainer(_magicsBinaryPersister);
            _bitBoard = new BitBoard(_movesContainer, bitBoardContextConverter);
            _piecesListService = new PiecesListService(_bitBoard.Context);

            _pieces = new IPiece[6];
            _pieces[(byte)PieceTypeEnum.Pawn] = new Pawn(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);
            _pieces[(byte)PieceTypeEnum.King] = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);
            _pieces[(byte)PieceTypeEnum.Rook] = new Rook(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);
            _pieces[(byte)PieceTypeEnum.Knight] = new Knight(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);
            _pieces[(byte)PieceTypeEnum.Bishop] = new Bishop(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);
            _pieces[(byte)PieceTypeEnum.Queen] = new Queen(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);
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

            return extMove;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UnmakeMove(ExtendedMove extMove)
        {
            _bitBoard.Context.Player.Change();
            _pieces[(byte)extMove.PieceType].UnmakeMove(extMove);
        }

        public string GetFen()
        {
            return _bitBoard.Context.ToString();
        }
    }
}
