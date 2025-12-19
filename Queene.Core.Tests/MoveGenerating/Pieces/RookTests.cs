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
    public class RookTests : TestsBase
    {
        public RookTests() : base()
        {
        }

        [Theory(DisplayName = "Should generate valid rook moves for files with stop piece")]
        [ClassData(typeof(RookFileWithStopPieceTestData))]
        public void ShouldGenerateValidRookMovesForFilesWithStopPiece(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var rook = new Rook(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            rook.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Select(m => new ExtendedMove(m, _bitBoard.Context)).Where(m => !m.Captured.HasValue).Select(m => m.Move);

            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should generate valid rook moves")]
        [ClassData(typeof(RookMovesTestData))]
        public void ShouldGenerateValidRookMoves(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var rook = new Rook(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            rook.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();

            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should generate valid rook captures")]
        [ClassData(typeof(RookCapturesTestData))]
        public void ShouldGenerateValidRookCaptures(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var rook = new Rook(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            rook.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();

            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Fact(DisplayName = "Should generate valid rook moves when king is in check")]
        public void ShouldGenerateValidRookMovesWhenKingIsInCheck()
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState("8/8/2r5/8/k3Q3/8/8/3K4 b - - 0 1");

            _bitBoard.SetupBoard(boardState);

            var rook = new Rook(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            rook.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEquivalentTo(new List<Move> { new Move(45, 29, MoveTypeEnum.Move) });
        }

        [Fact(DisplayName = "Should generate valid rook captures when king is in check")]
        public void ShouldGenerateValidRookCapturesWhenKingIsInCheck()
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState("8/4k3/8/N7/8/r3Q3/8/3K4 b - - 0 1");

            _bitBoard.SetupBoard(boardState);

            var rook = new Rook(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            rook.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEquivalentTo(new List<Move> { new Move(23, 19, MoveTypeEnum.Move) });
        }

        [Theory(DisplayName = "Should generate valid rook moves when rook is pinned")]
        [ClassData(typeof(RookMovesPinTestData))]
        public void ShouldGenerateValidRookMovesWhenRookIsPinned(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var rook = new Rook(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            rook.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Select(m => new ExtendedMove(m, _bitBoard.Context)).Where(m => !m.Captured.HasValue).Select(m => m.Move);
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should generate valid rook captures when rook is pinned")]
        [ClassData(typeof(RookCapturesPinTestData))]
        public void ShouldGenerateValidRookCapturesWhenRookIsPinned(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var rook = new Rook(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            rook.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEquivalentTo(expectedMoves);
        }
    }
}
