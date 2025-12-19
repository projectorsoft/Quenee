using FluentAssertions;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Pieces;
using Queene.Core.MovesGenerating.PiecesList;
using Queene.Core.Tests.MoveGenerating.Pieces.TestData;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Queene.Core.Tests.MoveGenerating.Pieces
{
    public class BishopTests : TestsBase
    {
        public BishopTests() : base()
        {
        }

        [Theory(DisplayName = "Should generate valid bishop moves when king is in check")]
        [ClassData(typeof(BishopMovesTestData))]
        public void ShouldGenerateValidBishopMovesWhenKingIsInCheck(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var piecesListService = new PiecesListService(_bitBoard.Context);
            var bishop = new Bishop(_bitBoard.Context, _movesList, piecesListService, _zorbistHash);

            // Act
            bishop.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Select(m => new ExtendedMove(m, _bitBoard.Context)).Where(m => !m.Captured.HasValue).Select(m => m.Move);
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should generate valid bishop captures when king is in check")]
        [ClassData(typeof(BishopCapturesTestData))]
        public void ShouldGenerateValidBishopCapturesWhenKingIsInCheck(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var piecesListService = new PiecesListService(_bitBoard.Context);
            var bishop = new Bishop(_bitBoard.Context, _movesList, piecesListService, _zorbistHash);

            // Act
            bishop.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should generate valid bishop moves when bishop is pinned")]
        [ClassData(typeof(BishopMovesPinTestData))]
        public void ShouldGenerateValidBishopMovesWhenBishopIsPinned(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var piecesListService = new PiecesListService(_bitBoard.Context);
            var bishop = new Bishop(_bitBoard.Context, _movesList, piecesListService, _zorbistHash);

            // Act
            bishop.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Select(m => new ExtendedMove(m, _bitBoard.Context)).Where(m => !m.Captured.HasValue).Select(m => m.Move);
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should generate valid Bishop captures when Bishop is pinned")]
        [ClassData(typeof(BishopCapturesPinTestData))]
        public void ShouldGenerateValidBishopCapturesWhenBishopIsPinned(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var piecesListService = new PiecesListService(_bitBoard.Context);
            var bishop = new Bishop(_bitBoard.Context, _movesList, piecesListService, _zorbistHash);

            // Act
            bishop.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEquivalentTo(expectedMoves);
        }
    }
}
