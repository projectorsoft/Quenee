using Microsoft.Extensions.Caching.Memory;
using Queene.Core.Converters;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.Magics;
using Queene.Core.Models;
using Queene.Core.MovesGenerating;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.Utils;
using QueeneEngine.Engine.Magics;
using System;
using System.Collections.Generic;

namespace Queene.Core
{
    public class Game : IQueeneGame
    {
        private readonly Board _board;
        private readonly IBitBoardContextConverter _bitBoardContextConverter;
        private readonly IMemoryCache _movesCache;
        private readonly ZorbistHash _zorbistHash;
        private readonly Stack<ExtendedMove> _movesHistory;

        public BoardContext Context => _board.BoardContext;

        public Game(IBitBoardContextConverter bitBoardContextConverter,
            IMagicsBinaryPersisterService magicsBinaryPersister,
            IMemoryCache movesCache,
            ZorbistHash zorbistHash)
        {
            SquaresBetweenMasksGeneratorHelper.GenerateMasksBeetwenSquares();
            MovesContainer.InitMovesMasks(magicsBinaryPersister);
            PerftTranspositionTable.Init();

            if (zorbistHash == null)
                throw new ArgumentNullException("Missing Zorbis hash");

            _movesCache = movesCache;
            _board = new Board(bitBoardContextConverter, zorbistHash);
            _bitBoardContextConverter = bitBoardContextConverter;
            _zorbistHash = zorbistHash;
            _movesHistory = new Stack<ExtendedMove>();
        }

        public void NewGame()
        {
            _board.NewGame(FenHelper.FEN_INITIAL_START_POSITION);
        }

        public void SetupPosition(string fen)
        {
            _board.NewGame(fen);
        }

        public Move[] GenerateMoves(MoveGenerationTypeEnum generationType = MoveGenerationTypeEnum.All)
        {
            return _board.GenerateMoves(generationType);
        }

        public ExtendedMove MakeMove(Move move)
        {
            var madeMove = _board.MakeMove(move);

            _movesHistory.Push(madeMove);

            return madeMove;
        }

        public bool UnmakeMove()
        {
            if (_movesHistory.Count == 0)
                return false;

            var move = _movesHistory.Pop();
            _board.UnmakeMove(move);

            return true;
        }

        public string GetFen()
        {
            return _board.GetFen();
        }

        public Board CloneBoard()
        {
            var board = new Board(_bitBoardContextConverter, _zorbistHash);
            board.NewGame(_board.GetFen());

            return board;
        }
    }
}
