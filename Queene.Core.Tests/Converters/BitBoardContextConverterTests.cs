using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Queene.Core.Converters;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.MovesGenerating;
using Queene.Core.Tests.MoveGenerating;
using System.Collections.Generic;
using Xunit;

namespace Queene.Core.Tests.Converters
{
    public class BitBoardContextConverterTests : TestsBase 
    {
        public BitBoardContextConverterTests() 
        {
            _fixture.Customize(new AutoMoqCustomization());
            _fixture.Freeze<BitBoardContextConverter>();
        }

        [Theory(DisplayName = "Should set player")]
        [InlineData(PlayerEnum.White)]
        [InlineData(PlayerEnum.Black)]
        public void ShouldSetPlayer(PlayerEnum playerEnum) 
        {
            // Arrange
            var bitBoardContext = _fixture.Build<BitBoardContext>()
                .Without(b => b.EnPassantSquare)
                .Create();
            bitBoardContext.Player.Set(playerEnum);

            // Act
            var boardState = _bitBoardContextConverter.Convert(bitBoardContext);

            // Assert
            boardState.TurnToMove.Should().Be(playerEnum);
        }

        [Fact(DisplayName = "Should set empty enPassant square")]
        public void ShouldSetEmptyEnPassantSquare() 
        {
            // Arrange
            var bitBoardContext = _fixture.Build<BitBoardContext>()
                .With(b => b.EnPassantSquare, (byte?)null)
                .Create();

            // Act
            var boardState = _bitBoardContextConverter.Convert(bitBoardContext);

            // Assert
            boardState.EnPassant.Should().Be(FenHelper.EMPTY_SECTION_CHARACTER);
        }

        [Theory(DisplayName = "Should set enPassant square for white")]
        [InlineData(16, "H3")]
        [InlineData(17, "G3")]
        [InlineData(18, "F3")]
        [InlineData(19, "E3")]
        [InlineData(20, "D3")]
        [InlineData(21, "C3")]
        [InlineData(22, "B3")]
        [InlineData(23, "A3")]
        public void ShouldSetEnPassantSquareForWhite(byte square, string squareName) 
        {
            // Arrange
            _fixture.Freeze<BitBoardContextConverter>();
            var bitBoardContext = _fixture.Build<BitBoardContext>()
                .With(b => b.EnPassantSquare, square)
                .Create();

            // Act
            var boardState = _bitBoardContextConverter.Convert(bitBoardContext);

            // Assert
            boardState.EnPassant.Should().Be(squareName);
        }

        [Theory(DisplayName = "Should set enPassant square for black")]
        [InlineData(40, "H6")]
        [InlineData(41, "G6")]
        [InlineData(42, "F6")]
        [InlineData(43, "E6")]
        [InlineData(44, "D6")]
        [InlineData(45, "C6")]
        [InlineData(46, "B6")]
        [InlineData(47, "A6")]
        public void ShouldSetEnPassantSquareForBlack(byte square, string squareName) 
        {
            // Arrange
            var bitBoardContext = _fixture.Build<BitBoardContext>()
                .With(b => b.EnPassantSquare, square)
                .Create();

            // Act
            var boardState = _bitBoardContextConverter.Convert(bitBoardContext);

            // Assert
            boardState.EnPassant.Should().Be(squareName);
        }

        [Fact(DisplayName = "Should set WhiteCastlingRights to None")]
        public void ShouldSetWhiteCastlingRightsToNone() 
        {
            // Arrange
            var bitBoardContext = _fixture.Build<BitBoardContext>()
                .Without(b => b.EnPassantSquare)
                .Create();
            bitBoardContext.SetCastleAllowance((byte)PlayerEnum.White, false);

            // Act
            var boardState = _bitBoardContextConverter.Convert(bitBoardContext);

            // Assert
            boardState.WhiteCastlingRights.Should().Be(CastleTypeEnum.None);
        }

        [Fact(DisplayName = "Should set WhiteCastlingRights to CastleKingSide")]
        public void ShouldSetWhiteCastlingRightsToCastleKingSide() 
        {
            // Arrange
            var bitBoardContext = _fixture.Build<BitBoardContext>()
                .Without(b => b.EnPassantSquare)
                .Create();
            bitBoardContext.SetCastleAllowance((byte)PlayerEnum.White, true);
            bitBoardContext.SetKingSideCastleAllowance((byte)PlayerEnum.White, true);

            // Act
            var boardState = _bitBoardContextConverter.Convert(bitBoardContext);

            // Assert
            boardState.WhiteCastlingRights.Should().Be(CastleTypeEnum.CastleKingSide);
        }

        [Fact(DisplayName = "Should set WhiteCastlingRights to CastleQueenSide")]
        public void ShouldSetWhiteCastlingRightsToCastleQueenSide()
        {
            // Arrange
            var bitBoardContext = _fixture.Build<BitBoardContext>()
                .Without(b => b.EnPassantSquare)
                .Create();
            bitBoardContext.SetCastleAllowance((byte)PlayerEnum.White, true);
            bitBoardContext.SetQueenSideCastleAllowance((byte)PlayerEnum.White, true);

            // Act
            var boardState = _bitBoardContextConverter.Convert(bitBoardContext);

            // Assert
            boardState.WhiteCastlingRights.Should().Be(CastleTypeEnum.CastleQueenSide);
        }

        [Fact(DisplayName = "Should set WhiteCastlingRights to Both")]
        public void ShouldSetWhiteCastlingRightsToBoth() 
        {
            // Arrange
            var bitBoardContext = _fixture.Build<BitBoardContext>()
                .Without(b => b.EnPassantSquare)
                .Create();
            bitBoardContext.SetAllCastlesAllowance((byte)PlayerEnum.White, true);

            // Act
            var boardState = _bitBoardContextConverter.Convert(bitBoardContext);

            // Assert
            boardState.WhiteCastlingRights.Should().Be (CastleTypeEnum.Both);
        }

        [Fact(DisplayName = "Should set BlackCastlingRights to None")]
        public void ShouldSetBlackCastlingRightsToNone () 
        {
            // Arrange
            var bitBoardContext = _fixture.Build<BitBoardContext>()
                .Without(b => b.EnPassantSquare)
                .Create();
            bitBoardContext.SetCastleAllowance((byte) PlayerEnum.Black, false);

            // Act
            var boardState = _bitBoardContextConverter.Convert(bitBoardContext);

            // Assert
            boardState.BlackCastlingRights.Should().Be(CastleTypeEnum.None);
        }

        [Fact(DisplayName = "Should set BlackCastlingRights to CastleKingSide")]
        public void ShouldSetBlackCastlingRightsToCastleKingSide() 
        {
            // Arrange
            var bitBoardContext = _fixture.Build<BitBoardContext>()
                .Without(b => b.EnPassantSquare)
                .Create();
            bitBoardContext.SetCastleAllowance((byte)PlayerEnum.Black, true);
            bitBoardContext.SetKingSideCastleAllowance((byte)PlayerEnum.Black, true);

            // Act
            var boardState = _bitBoardContextConverter.Convert (bitBoardContext);

            // Assert
            boardState.BlackCastlingRights.Should().Be (CastleTypeEnum.CastleKingSide);
        }

        [Fact(DisplayName = "Should set BlackCastlingRights to CastleQueenSide")]
        public void ShouldSetBlackCastlingRightsToCastleQueenSide() 
        {
            // Arrange
            var bitBoardContext = _fixture.Build<BitBoardContext>()
                .Without(b => b.EnPassantSquare)
                .Create();
            bitBoardContext.SetCastleAllowance ((byte)PlayerEnum.Black, true);
            bitBoardContext.SetQueenSideCastleAllowance((byte)PlayerEnum.Black, true);

            // Act
            var boardState = _bitBoardContextConverter.Convert(bitBoardContext);

            // Assert
            boardState.BlackCastlingRights.Should().Be(CastleTypeEnum.CastleQueenSide);
        }

        [Fact(DisplayName = "Should set BlackCastlingRights to Both")]
        public void ShouldSetBlackCastlingRightsToBoth()
        {
            // Arrange
            var bitBoardContext = _fixture.Build<BitBoardContext>()
                .Without(b => b.EnPassantSquare)
                .Create();
            bitBoardContext.SetAllCastlesAllowance((byte)PlayerEnum.Black, true);

            // Act
            var boardState = _bitBoardContextConverter.Convert(bitBoardContext);

            // Assert
            boardState.BlackCastlingRights.Should().Be(CastleTypeEnum.Both);
        }

        [Fact(DisplayName = "Should set pieces")]
        public void ShouldSetPieces() 
        {
            // Arrange
            var boardState = FenHelper.CreateBoardState("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KkQq - 0 1");

            _bitBoard.SetupBoard(boardState);

            // Act
            var newBoardState = _bitBoardContextConverter.Convert (_bitBoard.Context);

            // Assert
            newBoardState.WhitePieces[PieceTypeEnum.Rook].Should().BeEquivalentTo (new List<byte> { 0, 7 });
            newBoardState.WhitePieces[PieceTypeEnum.Knight].Should().BeEquivalentTo (new List<byte> { 1, 6 });
            newBoardState.WhitePieces[PieceTypeEnum.Bishop].Should().BeEquivalentTo (new List<byte> { 2, 5 });
            newBoardState.WhitePieces[PieceTypeEnum.Queen].Should().BeEquivalentTo (new List<byte> { 4 });
            newBoardState.WhitePieces[PieceTypeEnum.King].Should().BeEquivalentTo (new List<byte> { 3 });
            newBoardState.WhitePieces[PieceTypeEnum.Pawn].Should().BeEquivalentTo (new List<byte> { 8, 9, 10, 11, 12, 13, 14, 15 });

            newBoardState.BlackPieces[PieceTypeEnum.Rook].Should().BeEquivalentTo (new List<byte> { 56, 63 });
            newBoardState.BlackPieces[PieceTypeEnum.Knight].Should().BeEquivalentTo (new List<byte> { 57, 62 });
            newBoardState.BlackPieces[PieceTypeEnum.Bishop].Should().BeEquivalentTo (new List<byte> { 58, 61 });
            newBoardState.BlackPieces[PieceTypeEnum.Queen].Should().BeEquivalentTo (new List<byte> { 60 });
            newBoardState.BlackPieces[PieceTypeEnum.King].Should().BeEquivalentTo (new List<byte> { 59 });
            newBoardState.BlackPieces[PieceTypeEnum.Pawn].Should().BeEquivalentTo (new List<byte> { 48, 49, 50, 51, 52, 53, 54, 55 });
        }
    }
}