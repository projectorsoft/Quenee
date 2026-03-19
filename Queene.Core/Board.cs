using Queene.Core.Consts;
using Queene.Core.Converters;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.Models;
using Queene.Core.MovesGenerating;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.Pieces;
using Queene.Core.MovesGenerating.PiecesList;
using System.Numerics;

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

        public Move[] GenerateMoves(MoveGenerationTypeEnum generationType = MoveGenerationTypeEnum.All)
        {
            var ctx = _bitBoard.Context;
            var movesList = _movesList;

            movesList.Clear();
            ctx.SetOpponnentSliders();

            var king = (King)_pieces[(byte)PieceTypeEnum.King];

            _bitBoard.SetupCheckingSquaresAndAttackers(ctx.Player, king.Square);

            king.GenerateMoves(generationType);

            var attackers = ctx.Attackers;
            bool doubleCheck = attackers != 0 && BitOperations.PopCount(attackers) > 1;

            if (!doubleCheck)
            {
                _bitBoard.SetupPinnedPiecesAndPinners(ctx.Player, king.Square);

                _pieces[(byte)PieceTypeEnum.Pawn].GenerateMoves(generationType);
                _pieces[(byte)PieceTypeEnum.Rook].GenerateMoves(generationType);
                _pieces[(byte)PieceTypeEnum.Knight].GenerateMoves(generationType);
                _pieces[(byte)PieceTypeEnum.Bishop].GenerateMoves(generationType);
                _pieces[(byte)PieceTypeEnum.Queen].GenerateMoves(generationType);
            }

            return movesList.Get();
        }

        public ExtendedMove MakeMove(Move move)
        {
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            var oldPlayer = _bitBoard.Context.Player.Current;
            var oldEnPassant = _bitBoard.Context.EnPassantSquare;

            _pieces[(byte)extMove.PieceType].MakeMove(ref extMove);

            if (oldEnPassant.HasValue)
            {
                var idx = BoardConsts.FILE_FROM_SQUARE[oldEnPassant.Value];
                _bitBoard.Context.Hash ^= _zorbistHash.EnPassante[oldPlayer][idx];
            }

            var newEnPassant = _bitBoard.Context.EnPassantSquare;
            if (newEnPassant.HasValue)
            {
                var idx = BoardConsts.FILE_FROM_SQUARE[newEnPassant.Value];
                _bitBoard.Context.Hash ^= _zorbistHash.EnPassante[oldPlayer][idx];
            }

            _bitBoard.Context.Hash ^= _zorbistHash.Player[oldPlayer];
            _bitBoard.Context.Player.Change();
            _bitBoard.Context.Hash ^= _zorbistHash.Player[_bitBoard.Context.Player.Current];

            return extMove;
        }

        public void UnmakeMove(ExtendedMove extMove)
        {
            var currentPlayer = _bitBoard.Context.Player.Current;

            if (_bitBoard.Context.EnPassantSquare.HasValue)
            {
                var idx = BoardConsts.FILE_FROM_SQUARE[_bitBoard.Context.EnPassantSquare.Value];
                _bitBoard.Context.Hash ^= _zorbistHash.EnPassante[currentPlayer][idx];
            }

            _bitBoard.Context.Hash ^= _zorbistHash.Player[currentPlayer];
            _bitBoard.Context.Player.Change();
            _bitBoard.Context.Hash ^= _zorbistHash.Player[_bitBoard.Context.Player.Current];

            if (extMove.EnPassanteSquare.HasValue)
            {
                var idx = BoardConsts.FILE_FROM_SQUARE[extMove.EnPassanteSquare.Value];
                _bitBoard.Context.Hash ^= _zorbistHash.EnPassante[_bitBoard.Context.Player.Current][idx];
            }

            _pieces[(byte)extMove.PieceType].UnmakeMove(ref extMove);
        }

        public string GetFen()
        {
            return _bitBoard.Context.ToString();
        }
    }
}
