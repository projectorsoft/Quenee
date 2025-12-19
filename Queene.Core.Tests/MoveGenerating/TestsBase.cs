using AutoFixture;
using Queene.Core.Converters;
using Queene.Core.Magics;
using Queene.Core.Models;
using Queene.Core.MovesGenerating;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.PiecesList;
using Queene.Core.Utils;
using QueeneEngine.Engine.Magics;

namespace Queene.Core.Tests.MoveGenerating
{
    public abstract class TestsBase
    {
        protected readonly IMagicsBinaryPersisterService _magicsBinaryPersister;
        protected readonly IBoardStateConverter _boardStateConverter;
        protected readonly IBitBoardContextConverter _bitBoardContextConverter;
        protected readonly IPiecesListService _piecesListService;
        protected readonly IList<Move> _movesList = new MovesList();
        protected readonly BitBoard _bitBoard;
        protected readonly IFixture _fixture;
        protected readonly ZorbistHash _zorbistHash;

        public TestsBase()
        {
            _fixture = new Fixture();
            _magicsBinaryPersister = new MagicsBinaryPersisterService();
            SquaresBetweenMasksGeneratorHelper.GenerateMasksBeetwenSquares();
            MovesContainer.InitMovesMasks(_magicsBinaryPersister);
            PerftTranspositionTable.Init();
            _boardStateConverter = new BoardStateToFenConverter();
            _bitBoardContextConverter = new BitBoardContextConverter(_boardStateConverter);

            _zorbistHash = new ZorbistHash();
            _zorbistHash.Init();

            _bitBoard = new BitBoard(_bitBoardContextConverter, _zorbistHash);
            _piecesListService = new PiecesListService(_bitBoard.Context);
        }
    }
}
