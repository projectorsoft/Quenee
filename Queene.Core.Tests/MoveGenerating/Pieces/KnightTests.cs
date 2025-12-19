using FluentAssertions;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Pieces;
using Queene.Core.Tests.MoveGenerating.Pieces.TestData;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Queene.Core.Tests.MoveGenerating.Pieces
{
    public class KnightTests : TestsBase
    {
        public KnightTests() : base()
        {
        }

        [Theory(DisplayName = "Should generate valid knight moves")]
        [ClassData(typeof(KnightMovesTestData))]
        public void ShouldGenerateValidKnightMoves(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var knight = new Knight(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            knight.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();

            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should generate valid Knight captures")]
        [ClassData(typeof(KnightCapturesTestData))]
        public void ShouldGenerateValidKnightCaptures(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var knight = new Knight(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            knight.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();

            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Fact(DisplayName = "Should generate valid knight moves when king is in check")]
        public void ShouldGenerateValidKnightMovesWhenKingIsInCheck()
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState("1n6/3Q4/8/8/k7/8/8/3K4 b - - 0 1");

            _bitBoard.SetupBoard(boardState);

            var knight = new Knight(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            knight.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Select(m => new ExtendedMove(m, _bitBoard.Context)).Where(m => !m.Captured.HasValue).Select(m => m.Move);
            moves.Should().BeEquivalentTo(new List<Move> { new Move(62, 45, MoveTypeEnum.Move ) });
        }

        [Fact(DisplayName = "Should generate valid knight captures when king is in check")]
        public void ShouldGenerateValidKnightCapturesWhenKingIsInCheck()
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState("8/n7/2Q5/8/k7/8/8/3K4 b - - 0 1");

            _bitBoard.SetupBoard(boardState);

            var knight = new Knight(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            knight.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEquivalentTo(new List<Move> { new Move(55, 45, MoveTypeEnum.Move) });
        }

        [Theory(DisplayName = "Should generate valid knight moves when knight is pinned")]
        [ClassData(typeof(KnightMovesPinTestData))]
        public void ShouldGenerateValidKnightMovesWhenKnightIsPinned(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var knight = new Knight(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            knight.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Fact(DisplayName = "Should generate valid knight captures when knight is pinned")]
        public void ShouldGenerateValidKnightCapturesWhenKnightIsPinned()
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState("8/4Q3/2N5/8/1n6/k7/8/3K4 b - - 0 1");

            _bitBoard.SetupBoard(boardState);

            var knight = new Knight(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            knight.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEmpty();
        }
    }
}
