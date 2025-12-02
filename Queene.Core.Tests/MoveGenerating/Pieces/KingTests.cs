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
    public class KingTests : TestsBase
    {
        public KingTests() : base()
        {
        }

        [Theory(DisplayName = "Should generate valid king moves")]
        [ClassData(typeof(KingMovesTestData))]
        public void ShouldGenerateValidKingMoves(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();

            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should generate valid king captures")]
        [ClassData(typeof(KingCapturesTestData))]
        public void ShouldGenerateValidKingCaptures(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();

            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should generate valid king side castle move")]
        [InlineData("rnbqk2r/pppp1ppp/5n2/2b1p3/2B1P3/5N2/PPPP1PPP/RNBQK2R w Kk - 4 4", 3, 1)]
        [InlineData("rnbqk2r/pppp1ppp/5n2/2b1p3/2B1P3/5N2/PPPPQPPP/RNB1K2R b Kk - 5 4", 59, 57)]
        public void ShouldGenerateValidKingSideCastleMove(string fen, byte expectedFromSquare, byte expectedToSquare)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Where(m => m.IsCastleMove()).ToList();

            moves.Count().Should().Be(1);
            moves[0].GetFromSquare().Should().Be(expectedFromSquare);
            moves[0].GetToSquare().Should().Be(expectedToSquare);
        }

        [Theory(DisplayName = "Should generate valid queen side castle move")]
        [InlineData("r3kbnr/ppp1pppp/2nqb3/3p4/3P4/2NQB3/PPP1PPPP/R3KBNR w Qq - 6 5", 3, 5)]
        [InlineData("r3kbnr/ppp1pppp/2nqb3/3p4/3P4/2NQBN2/PPP1PPPP/R3KB1R b Qq - 7 5", 59, 61)]
        [InlineData("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 b kq - 0 1", 59, 61)]
        [InlineData("r3k2r/Pp1p1ppp/1bpB2bN/nP5n/B1P1P3/q4N2/Pp1P2PP/R2Q1R1K b q - 3 3", 59, 61)]
        public void ShouldGenerateValidQueenSideCastleMove(string fen, byte expectedFromSquare, byte expectedToSquare)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Where(m => m.IsCastleMove()).ToList();

            moves.Count().Should().Be(1);
            moves[0].GetToSquare().Should().Be(expectedToSquare);
            moves[0].GetFromSquare().Should().Be(expectedFromSquare);
        }

        [Theory(DisplayName = "Should not generate king side castle move when castling squares are occupied")]
        [InlineData("rnbqkbnr/pppp1ppp/4p3/8/8/4P3/PPPP1PPP/RNBQKBNR w KQkq - 0 1")] //All columns occupied
        [InlineData("rnbqkbnr/pppp1ppp/4p3/8/8/4P3/PPPP1PPP/RNBQKBNR b KQkq - 0 1")]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/5N2/PPPPPPPP/RNBQKB1R w KQkq - 0 1")] //F column occupied
        [InlineData("rnbqkb1r/pppppppp/5n2/8/8/5N2/PPPPPPPP/RNBQKB1R b KQkq - 0 1")]
        [InlineData("rnbqk1nr/ppppbppp/4p3/8/8/3BP3/PPPP1PPP/RNBQK1NR w KQkq - 0 1")] //G column occupied
        [InlineData("rnbqk1nr/ppppbppp/4p3/8/8/3BP3/PPPP1PPP/RNBQK1NR b KQkq - 0 1")]
        public void ShouldNotGenerateKingSideCastleMoveWhenCastlingSquaresAreOccupied(string fen)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Where(m => m.IsCastleMove() && m.GetToSquare() < m.GetFromSquare()).ToList();

            moves.Count().Should().Be(0);
        }

        [Theory(DisplayName = "Should not generate queen side castle move when castling squares are occupied")]
        [InlineData("rnbqkb1r/pppp1ppp/4pn2/8/8/4PN2/PPPP1PPP/RNBQKB1R w KQkq - 0 1")] //All columns occupied
        [InlineData("rnbqkb1r/pppp1ppp/4pn2/8/8/4PN2/PPPP1PPP/RNBQKB1R b KQkq - 0 1")]
        [InlineData("rnb1kb1r/ppppqppp/4pn2/8/8/4PN2/PPPPQPPP/RNB1KB1R w KQkq - 0 1")] //D column not occupied
        [InlineData("rnb1kb1r/ppppqppp/4pn2/8/8/4PN2/PPPPQPPP/RNB1KB1R b KQkq - 0 1")]
        [InlineData("rn1qkb1r/pppp1ppp/2b1pn2/8/8/2B1PN2/PPPP1PPP/RN1QKB1R w KQkq - 0 1")] //C column not  occupied
        [InlineData("rn1qkb1r/pppp1ppp/2b1pn2/8/8/2B1PN2/PPPP1PPP/RN1QKB1R b KQkq - 0 1")]
        [InlineData("r1bqkb1r/pppp1ppp/2n1pn2/8/8/2N1PN2/PPPP1PPP/R1BQKB1R w KQkq - 0 1")] //B column not  occupied
        [InlineData("r1bqkb1r/pppp1ppp/2n1pn2/8/8/2N1PN2/PPPP1PPP/R1BQKB1R b KQkq - 0 1")]
        [InlineData("r2qkb1r/pppp1ppp/1nb1pn2/8/8/1NB1PN2/PPPP1PPP/R2QKB1R w KQkq - 0 1")] //B and C columns not occupied
        [InlineData("r2qkb1r/pppp1ppp/1nb1pn2/8/8/1NB1PN2/PPPP1PPP/R2QKB1R b KQkq - 0 1")]
        [InlineData("rn2kb1r/pppp1ppp/2bqpn2/8/8/2BQPN2/PPPP1PPP/RN2KB1R w KQkq - 0 1")] //C and D columns not occupied
        [InlineData("rn2kb1r/pppp1ppp/2bqpn2/8/8/2BQPN2/PPPP1PPP/RN2KB1R b KQkq - 0 1")]
        [InlineData("rn1qkb1r/pppp1ppp/2b1pn2/8/8/1N1QPN2/PPPP1PPP/R1B1KB1R w KQkq - 0 1")] //B and D columns not occupied
        [InlineData("rn1qkb1r/pppp1ppp/2b1pn2/8/8/1N1QPN2/PPPP1PPP/R1B1KB1R b KQkq - 0 1")]
        public void ShouldNotGenerateQueenSideCastleMoveWhenCastlingSquaresAreOccupied(string fen)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Where(m => m.IsCastleMove() && m.GetToSquare() > m.GetFromSquare()).ToList();

            moves.Count().Should().Be(0);
        }

        [Theory(DisplayName = "Should not generate king side castle move when castling squares are under attack")]
        [InlineData("r3kbnr/pp2pppp/nq4b1/2pp4/5BQ1/3P4/PPP1PPPP/RN2KBNR b KQkq - 0 1")] //All columns attacked
        [InlineData("r3kbnr/pp2pppp/nq4b1/2pp4/6Q1/3P4/PPPBPPPP/RN2KBNR b KQkq - 0 1")] //C column attacked
        [InlineData("r3kbnr/pp2pppp/n1q3b1/B1pp4/8/3P1Q2/PPP1PPPP/RN2KBNR b KQkq - 0 1")] //D column attacked
        [InlineData("r3k1nr/pp2pppp/n1q5/B1pp2bb/4P3/2NP3Q/PPP2PPP/R3KBNR w KQkq - 0 1")] //All columns attacked
        [InlineData("r3k1nr/pp2pppp/n1q3b1/B1pp2b1/8/2NP1Q2/PPP1PPPP/R3KBNR w KQkq - 0 1")] //C column attacked
        [InlineData("r3k1nr/pp2pppp/n1qb4/B1pp3b/4P3/2NP3Q/PPP2PPP/R3KBNR w KQkq - 0 1")] //D column attacked
        public void ShouldNotGenerateKingSideCastleMoveWhenCastlingSquaresAreUnderAttack(string fen)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Where(m => m.IsCastleMove() && m.GetToSquare() < m.GetFromSquare()).ToList();

            moves.Count().Should().Be(0);
        }

        [Theory(DisplayName = "Should not generate queen side castle move when castling squares are under attack")]
        [InlineData("rnbqk2r/pppp2pp/2b2p1n/4p3/1BB5/8/PPPPPPPP/RN1QK1NR b KQkq - 0 1")] //All columns attacked
        [InlineData("rnbqk2r/pppp2pp/2b2p1n/4p3/1B6/2B5/PPPPPPPP/RN1QK1NR b KQkq - 0 1")] //F column attacked
        [InlineData("rnbqk2r/pppp2pp/2b2p1n/4p3/8/1BB5/PPPPPPPP/RN1QK1NR b KQkq - 0 1")] //G column attacked
        [InlineData("rn1qk2r/pppp2pp/5p1n/1bb1P3/5P2/1BB2N2/PPPP2PP/RN1QK2R w KQkq - 0 1")] //All columns attacked
        [InlineData("rn1qk2r/pppp2pp/3b1p1n/1b2P3/5P2/1BB2N2/PPPP2PP/RN1QK2R w KQkq - 0 1")] //F column attacked
        [InlineData("rn1qk2r/pppp2pp/2b2p1n/2b1P3/5P2/1BB2N2/PPPP2PP/RN1QK2R w KQkq - 0 1")] //G column attacked
        public void ShouldNotGenerateQueenSideCastleMoveWhenCastlingSquaresAreUnderAttack(string fen)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Where(m => m.IsCastleMove() && m.GetToSquare() > m.GetFromSquare()).ToList();

            moves.Count().Should().Be(0);
        }

        [Theory(DisplayName = "Should not generate castling moves when castle is not available")]
        [InlineData("r6r/1pp1kppp/1bnbpn2/p2p1q2/P1B2N2/1PNBPQ2/2PPKPPP/R6R w - - 4 10")]
        [InlineData("r6r/1pp1kppp/1bnbpn2/p2p1q2/P1B2N2/1PNBPQ2/2PPKPPP/R6R b - - 4 10")]
        public void ShouldNotGenerateCastlingMovesWhenCastleIsNotAvailable(string fen)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Where(m => m.IsCastleMove()).ToList();

            moves.Count().Should().Be(0);
        }

        [Theory(DisplayName = "Should not generate king side castle move when king side castle is not available")]
        [InlineData("r3k2r/pppp1pp1/n1bbpq1p/7n/3N3P/NPBBPQ2/P1PP1PP1/R3K2R w Qq - 5 9")]
        [InlineData("r3k2r/pppp1pp1/n1bbp1qp/7n/3N3P/NPBBPQ2/P1PP1PP1/R3K2R b Qq - 12 12")]
        public void ShouldNotGenerateKingSideCastleMoveWhenKingSideCastleIsNotAvailable(string fen)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Where(m => m.IsCastleMove() && m.GetToSquare() < m.GetFromSquare()).ToList();

            moves.Count().Should().Be(0);
        }

        [Theory(DisplayName = "Should not generate queen side castle move when queen side castle is not available")]
        [InlineData("r3k2r/1pp1bppp/1bnppn2/p4q2/P4N2/1PNBPQ2/2PPBPPP/R3K2R w Kk - 2 7")]
        [InlineData("r3k2r/1pp1bppp/1bnppn2/p4q2/P4N2/1PNBPQ2/2PPBPPP/R3K2R b Kk - 2 7")]
        public void ShouldNotGenerateQueenSideCastleMoveWhenQueenSideCastleIsNotAvailable(string fen)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get().Where(m => m.IsCastleMove() && m.GetToSquare() > m.GetFromSquare()).ToList();

            moves.Count().Should().Be(0);
        }

        [Theory(DisplayName = "Should generate valid king moves with checked squares")]
        [ClassData(typeof(KingMovesCheckedSquaresTestData))]
        public void ShouldGenerateValidKingMovesWithCheckedSquares(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.All);

            // Assert
            var moves = _movesList.Get();

            moves.Should().BeEquivalentTo(expectedMoves);
        }

        [Theory(DisplayName = "Should generate valid king captures with checked squares")]
        [ClassData(typeof(KingCapturesCheckedSquaresTestData))]
        public void ShouldGenerateValidKingCapturesWithCheckedSquares(string fen, List<Move> expectedMoves)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            king.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            // Assert
            var moves = _movesList.Get();

            moves.Should().BeEquivalentTo(expectedMoves);
        }
    }
}
