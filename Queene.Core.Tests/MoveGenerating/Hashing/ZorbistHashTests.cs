using FluentAssertions;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Pieces;
using System.Linq;
using Xunit;

namespace Queene.Core.Tests.MoveGenerating.Hashing
{
    public class ZorbistHashTests : TestsBase
    {
        [Fact]
        public void ShouldInitializeZorbistHashes()
        {
            //Arrange
            //Act

            //Assert
            for (PlayerEnum player = PlayerEnum.Black; player <= PlayerEnum.White; player++)
                for (PieceTypeEnum piece = PieceTypeEnum.Knight; piece <= PieceTypeEnum.King; piece++)
                {
                    for (int square = 0; square < 64; square++)
                        _zorbistHash.Pieces[(byte)player][(byte)piece][square].Should().BeGreaterThan(0);
                }

            for (PlayerEnum player = PlayerEnum.Black; player <= PlayerEnum.White; player++)
            {
                for (int i = 0; i < 8; i++)
                    _zorbistHash.EnPassante[(byte)player][i].Should().BeGreaterThan(0);
            }

            for (PlayerEnum player = PlayerEnum.Black; player <= PlayerEnum.White; player++)
                _zorbistHash.Player[(byte)player].Should().BeGreaterThan(0);

            for (PlayerEnum player = PlayerEnum.Black; player <= PlayerEnum.White; player++)
            {
                _zorbistHash.KingSideCastle[(byte)player].Should().BeGreaterThan(0);
                _zorbistHash.QueenSideCastle[(byte)player].Should().BeGreaterThan(0);
            }
        }

        [Fact]
        public void ShouldCreateHashBasedOnBitBoard()
        {
            //Arrange
            var board = new Board(_bitBoardContextConverter, _zorbistHash);
            board.NewGame(FenHelper.FEN_INITIAL_START_POSITION);

            //Act
            //Assert
            board.BoardContext.Hash.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("r3k2r/Pppp1p1p/1b3nbN/nP6/BBP1P1pP/q4N2/Pp1P2P1/R2Q1RK1 w kq - 0 1", 1, 0)]
        [InlineData("r3k2r/pppppppp/nbq5/8/8/5QBN/PPPPPPPP/R3K2R w KQkq - 0 1", 3, 2)]
        public void ShouldHashBeTheSameAfterUnmakeStandardKingMove(string fen, byte square, byte toSquare)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);
            _bitBoard.SetupBoard(boardState);
            var hash = _bitBoard.Context.Hash;

            var piece = new King(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            piece.GenerateMoves(MoveGenerationTypeEnum.All);
            var move = _movesList.Get().First(m => m.GetFromSquare() == square && m.GetToSquare() == toSquare);
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            piece.MakeMove(extMove);
            piece.UnmakeMove(extMove);

            // Assert
            _bitBoard.Context.Hash.Should().Be(hash);
        }

        [Theory]
        [InlineData("r3k2r/pppppppp/nbq5/8/8/5QBN/PPPPPPPP/R3K2R w KQkq - 0 1", 3, 1)]
        [InlineData("r3k2r/pppppppp/nbq5/8/8/5QBN/PPPPPPPP/R3K2R w KQkq - 0 1", 3, 5)]
        [InlineData("r3k2r/pppppppp/nbq5/8/8/5QBN/PPPPPPPP/R3K2R b KQkq - 0 1", 59, 57)]
        [InlineData("r3k2r/pppppppp/nbq5/8/8/5QBN/PPPPPPPP/R3K2R b KQkq - 0 1", 59, 61)]
        public void ShouldHashBeTheSameAfterUnmakeCastleMove(string fen, byte square, byte toSquare)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);
            _bitBoard.SetupBoard(boardState);
            var hash = _bitBoard.Context.Hash;

            var piece = new King(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            piece.GenerateMoves(MoveGenerationTypeEnum.All);
            var move = _movesList.Get().First(m => m.GetFromSquare() == square && m.GetToSquare() == toSquare);
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            piece.MakeMove(extMove);
            piece.UnmakeMove(extMove);

            // Assert
            _bitBoard.Context.Hash.Should().Be(hash);
        }

        [Theory]
        [InlineData("rnbqkbnr/p1pppppp/8/8/Pp6/8/1PPPPPPP/RNBQKBNR b KQkq a3 0 1", 30, 23)]
        [InlineData("rnbqkbnr/p1pppppp/8/Pp6/8/8/1PPPPPPP/RNBQKBNR w KQkq b6 0 1", 39, 46)]
        public void ShouldHashBeTheSameAfterUnmakeEnPassanteMove(string fen, byte square, byte toSquare)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);
            _bitBoard.SetupBoard(boardState);
            var hash = _bitBoard.Context.Hash;

            var piece = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            piece.GenerateMoves(MoveGenerationTypeEnum.All);
            var move = _movesList.Get().First(m => m.GetFromSquare() == square && m.GetToSquare() == toSquare);
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            piece.MakeMove(extMove);
            piece.UnmakeMove(extMove);

            // Assert
            _bitBoard.Context.Hash.Should().Be(hash);
        }

        [Theory]
        //[InlineData("4kr1q/6P1/8/8/8/8/6p1/3K1N1B w - - 0 1", 49, 57)]
        //[InlineData("4kr1q/6P1/8/8/8/8/6p1/3K1N1B w - - 0 1", 49, 56)]
        //[InlineData("4kr1q/6P1/8/8/8/8/6p1/3K1N1B w - - 0 1", 49, 58)]
        //[InlineData("4kr1q/6P1/8/8/8/8/6p1/3K1N1B b - - 0 1", 9, 1)]
        //[InlineData("4kr1q/6P1/8/8/8/8/6p1/3K1N1B b - - 0 1", 9, 0)]
        [InlineData("4kr1q/6P1/8/8/8/8/6p1/3K1N1B b - - 0 1", 9, 2)]
        public void ShouldHashBeTheSameAfterUnmakePromotionMove(string fen, byte square, byte toSquare)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);
            _bitBoard.SetupBoard(boardState);
            var hash = _bitBoard.Context.Hash;

            var piece = new Pawn(_bitBoard.Context, _movesList, _piecesListService, _zorbistHash);

            // Act
            piece.GenerateMoves(MoveGenerationTypeEnum.All);
            var move = _movesList.Get().First(m => m.GetFromSquare() == square && m.GetToSquare() == toSquare);
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            piece.MakeMove(extMove);
            piece.UnmakeMove(extMove);

            // Assert
            _bitBoard.Context.Hash.Should().Be(hash);
        }
    }
}
