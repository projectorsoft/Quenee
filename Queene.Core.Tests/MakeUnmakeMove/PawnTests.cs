using FluentAssertions;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Pieces;
using Queene.Core.Tests.MakeUnmakeMove.TestData;
using Queene.Core.Tests.MoveGenerating;
using QueeneEngine.Helpers.Bitwise;
using System.Linq;
using Xunit;

namespace Queene.Core.Tests.MakeUnmakeMove
{
    public class PawnTests : TestsBase
    {
        [Theory]
        [ClassData(typeof(PawnSingleMoveTestData))]
        public void MakeSingleMove(string fen, byte square)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);
            var move = _movesList.Get().First(m => m.GetFromSquare() == square && m.GetToSquare() == square + 8);
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            pawn.MakeMove(extMove);

            // Assert
            extMove.Move.Should().BeEquivalentTo(move);
            extMove.MoveType.Should().Be(MoveTypeEnum.Move);
            extMove.PieceType.Should().Be(PieceTypeEnum.Pawn);
            extMove.Captured.Should().BeNull();
            _bitBoard.Context.EnPassantSquare.Should().BeNull();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.To).Should().BeTrue();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.From).Should().BeFalse();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.To).Should().BeTrue();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.From).Should().BeFalse();
        }

        [Theory]
        [ClassData(typeof(PawnSingleMoveTestData))]
        public void UnmakeSingleMove(string fen, byte square)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);
            var move = _movesList.Get().First(m => m.GetFromSquare() == square && m.GetToSquare() == square + 8);
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            pawn.MakeMove(extMove);
            pawn.UnmakeMove(extMove);

            // Assert
            extMove.Move.Should().BeEquivalentTo(move);
            extMove.MoveType.Should().Be(MoveTypeEnum.Move);
            extMove.PieceType.Should().Be(PieceTypeEnum.Pawn);
            extMove.Captured.Should().BeNull();
            _bitBoard.Context.EnPassantSquare.Should().BeNull();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.To).Should().BeFalse();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.From).Should().BeTrue();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.To).Should().BeFalse();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.From).Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(PawnDoubleMoveTestData))]
        public void MakeDoubleMove(string fen, byte square)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);
            var move = _movesList.Get().First(m => m.GetFromSquare() == square && m.GetToSquare() == square + 16);
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            pawn.MakeMove(extMove);

            // Assert
            extMove.Move.Should().BeEquivalentTo(move);
            extMove.MoveType.Should().Be(MoveTypeEnum.Move);
            extMove.PieceType.Should().Be(PieceTypeEnum.Pawn);
            extMove.Captured.Should().BeNull();
            extMove.IsPawnDoubleMove().Should().BeTrue();
            _bitBoard.Context.EnPassantSquare.Should().NotBeNull();
            _bitBoard.Context.EnPassantSquare.Should().Be((byte)(square + 8));

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.To).Should().BeTrue();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.From).Should().BeFalse();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.To).Should().BeTrue();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.From).Should().BeFalse();
        }

        [Theory]
        [ClassData(typeof(PawnDoubleMoveTestData))]
        public void UnmakeDoubleMove(string fen, byte square)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);
            var move = _movesList.Get().First(m => m.GetFromSquare() == square && m.GetToSquare() == square + 16);
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            pawn.MakeMove(extMove);
            pawn.UnmakeMove(extMove);

            // Assert
            extMove.Move.Should().BeEquivalentTo(move);
            extMove.MoveType.Should().Be(MoveTypeEnum.Move);
            extMove.PieceType.Should().Be(PieceTypeEnum.Pawn);
            extMove.Captured.Should().BeNull();
            extMove.IsPawnDoubleMove().Should().BeTrue();
            _bitBoard.Context.EnPassantSquare.Should().BeNull();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.To).Should().BeFalse();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.From).Should().BeTrue();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.To).Should().BeFalse();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.From).Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(PawnPromotionMoveTestData))]
        public void MakePromotionMove(string fen, byte square, PieceTypeEnum promotTo)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);
            var move = _movesList.Get().First(m => m.GetFromSquare() == square && m.GetPromotionPieceType() == promotTo);
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            pawn.MakeMove(extMove);

            // Assert
            extMove.Move.Should().BeEquivalentTo(move);
            extMove.MoveType.Should().Be(MoveTypeEnum.Promotion);
            extMove.PieceType.Should().Be(PieceTypeEnum.Pawn);
            extMove.Captured.Should().BeNull();
            _bitBoard.Context.EnPassantSquare.Should().BeNull();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.To).Should().BeFalse();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.From).Should().BeFalse();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)promotTo], extMove.To).Should().BeTrue();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.From).Should().BeFalse();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)promotTo], extMove.To).Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(PawnPromotionMoveTestData))]
        public void UnmakePromotionMove(string fen, byte square, PieceTypeEnum promotTo)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.All);
            var move = _movesList.Get().First(m => m.GetFromSquare() == square && m.GetPromotionPieceType() == promotTo);
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            pawn.MakeMove(extMove);
            pawn.UnmakeMove(extMove);

            // Assert
            extMove.Move.Should().BeEquivalentTo(move);
            extMove.MoveType.Should().Be(MoveTypeEnum.Promotion);
            extMove.PieceType.Should().Be(PieceTypeEnum.Pawn);
            extMove.Captured.Should().BeNull();
            _bitBoard.Context.EnPassantSquare.Should().BeNull();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.To).Should().BeFalse();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.From).Should().BeTrue();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)promotTo], extMove.To).Should().BeFalse();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.From).Should().BeTrue();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)promotTo], extMove.To).Should().BeFalse();
        }

        [Theory]
        [ClassData(typeof(PawnPromotionCaptureTestData))]
        public void MakePromotionCaptureMove(string fen, byte square, PieceTypeEnum promotTo)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);
            var moves = _movesList.Get().Where(m => m.GetFromSquare() == square && m.GetPromotionPieceType() == promotTo);

            foreach (var move in moves)
            {
                var extMove = new ExtendedMove(move, _bitBoard.Context);

                pawn.MakeMove(extMove);

                // Assert
                extMove.Move.Should().BeEquivalentTo(move);
                extMove.MoveType.Should().Be(MoveTypeEnum.Promotion);
                extMove.PieceType.Should().Be(PieceTypeEnum.Pawn);
                extMove.Captured.Should().Be(PieceTypeEnum.Rook);
                _bitBoard.Context.EnPassantSquare.Should().BeNull();

                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.From).Should().BeFalse();
                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.To).Should().BeFalse();
                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)promotTo], extMove.To).Should().BeTrue();
                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.Black][(byte)extMove.Captured], extMove.To).Should().BeFalse();

                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.From).Should().BeFalse();
                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)promotTo], extMove.To).Should().BeTrue();
                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.Black][(byte)PieceTypeEnum.All], extMove.To).Should().BeFalse();

                pawn.UnmakeMove(extMove);
            }
        }

        [Theory]
        [ClassData(typeof(PawnPromotionCaptureTestData))]
        public void UnmakePromotionCaptureMove(string fen, byte square, PieceTypeEnum promotTo)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);
            var moves = _movesList.Get().Where(m => m.GetFromSquare() == square && m.GetPromotionPieceType() == promotTo);

            foreach (var move in moves)
            {
                var extMove = new ExtendedMove(move, _bitBoard.Context);

                pawn.MakeMove(extMove);
                pawn.UnmakeMove(extMove);

                // Assert
                extMove.Move.Should().BeEquivalentTo(move);
                extMove.MoveType.Should().Be(MoveTypeEnum.Promotion);
                extMove.PieceType.Should().Be(PieceTypeEnum.Pawn);
                extMove.Captured.Should().Be(PieceTypeEnum.Rook);
                _bitBoard.Context.EnPassantSquare.Should().BeNull();

                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.From).Should().BeTrue();
                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.To).Should().BeFalse();
                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)promotTo], extMove.To).Should().BeFalse();
                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.Black][(byte)extMove.Captured], extMove.To).Should().BeTrue();

                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.From).Should().BeTrue();
                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)promotTo], extMove.To).Should().BeFalse();
                BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.Black][(byte)PieceTypeEnum.All], extMove.To).Should().BeTrue();
            }
        }

        [Theory]
        [ClassData(typeof(PawnEnPassanteTestData))]
        public void MakeEnPassanteMove(string fen, byte square)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);
            var move = _movesList.Get().First(m => m.GetFromSquare() == square && m.IsEnPassantMove() == true);
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            pawn.MakeMove(extMove);

            // Assert
            extMove.Move.Should().BeEquivalentTo(move);
            extMove.MoveType.Should().Be(MoveTypeEnum.EnPassante);
            extMove.PieceType.Should().Be(PieceTypeEnum.Pawn);
            extMove.Captured.Should().NotBeNull();
            extMove.Captured.Should().Be(PieceTypeEnum.Pawn);
            _bitBoard.Context.EnPassantSquare.Should().Be(null);

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.To).Should().BeTrue();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.From).Should().BeFalse();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.Black][(byte)extMove.Captured], extMove.To - 8).Should().BeFalse();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.To).Should().BeTrue();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.From).Should().BeFalse();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.Black][(byte)PieceTypeEnum.All], extMove.To - 8).Should().BeFalse();
        }

        [Theory]
        [ClassData(typeof(PawnEnPassanteTestData))]
        public void UnmakeEnPassanteMove(string fen, byte square)
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState(fen);

            _bitBoard.SetupBoard(boardState);

            var pawn = new Pawn(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            pawn.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);
            var move = _movesList.Get().First(m => m.GetFromSquare() == square && m.IsEnPassantMove() == true);
            var extMove = new ExtendedMove(move, _bitBoard.Context);

            pawn.MakeMove(extMove);
            pawn.UnmakeMove(extMove);

            // Assert
            extMove.Move.Should().BeEquivalentTo(move);
            extMove.MoveType.Should().Be(MoveTypeEnum.EnPassante);
            extMove.PieceType.Should().Be(PieceTypeEnum.Pawn);
            extMove.Captured.Should().NotBeNull();
            extMove.Captured.Should().Be(PieceTypeEnum.Pawn);
            _bitBoard.Context.EnPassantSquare.Should().Be(extMove.To);

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.To).Should().BeFalse();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.Pawn], extMove.From).Should().BeTrue();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.Black][(byte)extMove.Captured], extMove.To - 8).Should().BeTrue();

            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.To).Should().BeFalse();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.White][(byte)PieceTypeEnum.All], extMove.From).Should().BeTrue();
            BitwiseHelper.IsSet(_bitBoard.Context.Pieces[(byte)PlayerEnum.Black][(byte)PieceTypeEnum.All], extMove.To - 8).Should().BeTrue();
        }
    }
}
