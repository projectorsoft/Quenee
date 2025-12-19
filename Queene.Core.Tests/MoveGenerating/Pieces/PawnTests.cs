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
    public class PawnTests : TestsBase
    {
        public PawnTests() : base()
        {
        }

        [Theory(DisplayName = "Should create valid moves for white from second rank")]
        [ClassData(typeof(PawnWhiteSecondRankMovesTestData))]
        public void ShouldCreateValidEnPassantMovesForWhiteFromSecondRank(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();
            moves.All(m => m.GetMoveType() == MoveTypeEnum.Move).Should().BeTrue();
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should create valid moves for black from second rank")]
        [ClassData(typeof(PawnBlackSecondRankMovesTestData))]
        public void ShouldCreateValidEnPassantMovesForBlackFromSecondRank(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();
            moves.All(m => m.GetMoveType() == MoveTypeEnum.Move).Should().BeTrue();
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should create valid one square move for white")]
        [InlineData("8/p7/8/8/8/P7/8/K6k w KQkq - 0 1", 31)]
        [InlineData("8/p7/8/8/8/1P6/8/K6k w KQkq - 0 1", 30)]
        [InlineData("8/p7/8/8/8/2P5/8/K6k w KQkq - 0 1", 29)]
        [InlineData("8/p7/8/8/8/3P4/8/K6k w KQkq - 0 1", 28)]
        [InlineData("8/p7/8/8/8/4P3/8/K6k w KQkq - 0 1", 27)]
        [InlineData("8/p7/8/8/8/5P2/8/K6k w KQkq - 0 1", 26)]
        [InlineData("8/p7/8/8/8/6P1/8/K6k w KQkq - 0 1", 25)]
        [InlineData("8/p7/8/8/8/7P/8/K6k w KQkq - 0 1", 24)]
        public void ShouldCreateValidOneSquareMoveForWhite(string fen, byte expectedToSquare)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();
            moves.Count().Should().Be(1);
            moves.All(m => m.GetMoveType() == MoveTypeEnum.Move).Should().BeTrue();
            moves.Select(m => m.GetToSquare()).All(m => m == expectedToSquare).Should().BeTrue();
        }

        [Theory(DisplayName = "Should create valid one square move for black")]
        [InlineData("8/8/p7/8/8/7P/8/K6k b KQkq - 0 1", 39)]
        [InlineData("8/8/1p6/8/8/7P/8/K6k b KQkq - 0 1", 38)]
        [InlineData("8/8/2p5/8/8/7P/8/K6k b KQkq - 0 1", 37)]
        [InlineData("8/8/3p4/8/8/7P/8/K6k b KQkq - 0 1", 36)]
        [InlineData("8/8/4p3/8/8/7P/8/K6k b KQkq - 0 1", 35)]
        [InlineData("8/8/5p2/8/8/7P/8/K6k b KQkq - 0 1", 34)]
        [InlineData("8/8/6p1/8/8/7P/8/K6k b KQkq - 0 1", 33)]
        [InlineData("8/8/7p/8/8/7P/8/K6k b KQkq - 0 1", 32)]
        public void ShouldCreateValidOneSquareMoveForBlack(string fen, byte expectedToSquare)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();
            moves.Count().Should().Be(1);
            moves.All(m => m.GetMoveType() == MoveTypeEnum.Move).Should().BeTrue();
            moves.Select(m => m.GetToSquare()).All(m => m == expectedToSquare).Should().BeTrue();
        }

        [Theory(DisplayName = "Should create valid captures for white")]
        [ClassData(typeof(PawnWhiteCapturesTestData))]
        public void ShouldCreateValidCapturesForWhite(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.All(m => m.GetMoveType() == MoveTypeEnum.Move).Should().BeTrue();
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should create valid captures for black")]
        [ClassData(typeof(PawnBlackCapturesTestData))]
        public void ShouldCreateValidCapturesForBlack(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.All(m => m.GetMoveType() == MoveTypeEnum.Move).Should().BeTrue();
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should create valid promotion moves for white")]
        [InlineData("8/P7/8/7k/8/7K/8/8 w KQkq - 0 1", 63)]
        [InlineData("8/1P6/8/7k/8/7K/8/8 w KQkq - 0 1", 62)]
        [InlineData("8/2P5/8/7k/8/7K/8/8 w KQkq - 0 1", 61)]
        [InlineData("8/3P4/8/7k/8/7K/8/8 w KQkq - 0 1", 60)]
        [InlineData("8/4P3/8/7k/8/7K/8/8 w KQkq - 0 1", 59)]
        [InlineData("8/5P2/8/7k/8/7K/8/8 w KQkq - 0 1", 58)]
        [InlineData("8/6P1/8/7k/8/7K/8/8 w KQkq - 0 1", 57)]
        [InlineData("8/7P/8/7k/8/7K/8/8 w KQkq - 0 1", 56)]
        public void ShouldCreateValidPromotionMovesForWhite(string fen, byte expectedToSquare)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();
            moves.Count().Should().Be(4);
            moves.All(m => m.IsPromotionMove()).Should().BeTrue();
            moves.Select(m => m.GetToSquare()).All(m => m == expectedToSquare).Should().BeTrue();
        }

        [Theory(DisplayName = "Should create valid promotion moves for black")]
        [InlineData("8/8/8/7k/8/7K/p7/8 b KQkq - 0 1", 7)]
        [InlineData("8/8/8/7k/8/7K/1p6/8 b KQkq - 0 1", 6)]
        [InlineData("8/8/8/7k/8/7K/2p5/8 b KQkq - 0 1", 5)]
        [InlineData("8/8/8/7k/8/7K/3p4/8 b KQkq - 0 1", 4)]
        [InlineData("8/8/8/7k/8/7K/4p3/8 b KQkq - 0 1", 3)]
        [InlineData("8/8/8/7k/8/7K/5p2/8 b KQkq - 0 1", 2)]
        [InlineData("8/8/8/7k/8/7K/6p1/8 b KQkq - 0 1", 1)]
        [InlineData("8/8/8/7k/8/7K/7p/8 b KQkq - 0 1", 0)]
        public void ShouldCreateValidPromotionMovesForBlack(string fen, byte expectedToSquare)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();
            moves.Count().Should().Be(4);
            moves.All(m => m.IsPromotionMove()).Should().BeTrue();
            moves.Select(m => m.GetToSquare()).All(m => m == expectedToSquare).Should().BeTrue();
        }

        [Theory(DisplayName = "Should create valid promotion captures for white")]
        [ClassData(typeof(PawnWhitePromotionCapturesTestData))]
        public void ShouldCreateValidPromotionCapturesForWhite(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.All(m => m.IsPromotionMove()).Should().BeTrue();
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should create valid promotion captures for black")]
        [ClassData(typeof(PawnBlackPromotionCapturesTestData))]
        public void ShouldCreateValidPromotionCapturesForBlack(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.All(m => m.IsPromotionMove()).Should().BeTrue();
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should create valid enPassant moves for white")]
        [ClassData(typeof(PawnWhiteEnPassantTestData))]
        public void ShouldCreateValidEnPassantMovesForWhite(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.Where(m => m.IsEnPassantMove()).Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should create valid enPassant moves for black")]
        [ClassData(typeof(PawnBlackEnPassantTestData))]
        public void ShouldCreateValidEnPassantMovesForBlack(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.Where(m => m.IsEnPassantMove()).Should().BeEquivalentTo(expectedMoves);
        }

        [Fact(DisplayName = "Should create only valid moves when king is in check")]
        public void ShouldCreateOnlyValidMovesWhenKingIsInCheck()
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState("rn1qkbnr/pp2pppp/b4p2/2pP4/8/3K4/PPP1PPPP/RNBQ1BNR w - - 0 1");

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEquivalentTo(new List<Move> { new Move(13, 29, MoveTypeEnum.Move) });
        }

        [Fact(DisplayName = "Should create only valid captures when king is in check")]
        public void ShouldCreateOnlyValidCapturesWhenKingIsInCheck()
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState("8/8/8/k3p3/3p4/2Q5/8/3K4 b - - 0 1");

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEquivalentTo(new List<Move> { new Move(28, 21, MoveTypeEnum.Move) });
        }

        [Theory(DisplayName = "Should create only valid enPassant moves when king is in check")]
        [ClassData(typeof(PawnEnPassantValidMovesTestData))]
        public void ShouldCreateOnlyValidEnPassantMovesWhenKingIsInCheck(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should create only valid moves when pawn is pinned")]
        [ClassData(typeof(PawnMovesPinTestData))]
        public void ShouldCreateOnlyValidMovesWhenPawnIsPinned(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should create only valid captures when pawn is pinned")]
        [ClassData(typeof(PawnCapturesPinTestData))]
        public void ShouldCreateOnlyValidCapturesWhenPawnIsPinned(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();
            moves.Should().BeEquivalentTo(expectedMoves);
        }
    }
}
