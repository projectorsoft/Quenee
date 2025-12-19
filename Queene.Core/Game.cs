using Queene.Core.Converters;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.Magics;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.Utils;
using QueeneEngine.Engine.Magics;
using System;

namespace Queene.Core
{
    public class Game : IQueeneGame
    {
        private readonly Board _board;
        private readonly IBitBoardContextConverter _bitBoardContextConverter;
        private readonly ZorbistHash _zorbistHash;

        public Game(IBitBoardContextConverter bitBoardContextConverter,
            IMagicsBinaryPersisterService magicsBinaryPersister,
            ZorbistHash zorbistHash)
        {
            SquaresBetweenMasksGeneratorHelper.GenerateMasksBeetwenSquares();
            MovesContainer.InitMovesMasks(magicsBinaryPersister);
            PerftTranspositionTable.Init();

            if (zorbistHash == null)
                throw new ArgumentNullException("Missing Zorbis hash");

            _board = new Board(bitBoardContextConverter, zorbistHash);
            _bitBoardContextConverter = bitBoardContextConverter;
            _zorbistHash = zorbistHash;
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
            return _board.MakeMove(move);
        }

        public void UnmakeMove(ExtendedMove move)
        {
            _board.UnmakeMove(move);
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
