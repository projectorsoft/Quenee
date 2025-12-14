using AutoFixture;
using Queene.Core.Converters;
using Queene.Core.Magics;
using Queene.Core.Models;
using Queene.Core.MovesGenerating;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.PiecesList;
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
        protected readonly MovesContainer _movesContainer;
        protected readonly BitBoard _bitBoard;
        protected readonly IFixture _fixture;

        public TestsBase()
        {
            ZorbistHash.Init();
            _fixture = new Fixture();
            _magicsBinaryPersister = new MagicsBinaryPersisterService();
            _movesContainer = new MovesContainer(_magicsBinaryPersister);
            _boardStateConverter = new BoardStateToFenConverter();
            _bitBoardContextConverter = new BitBoardContextConverter(_boardStateConverter);

            _bitBoard = new BitBoard(_movesContainer, _bitBoardContextConverter);
            _piecesListService = new PiecesListService(_bitBoard.Context);
        }
    }
}
