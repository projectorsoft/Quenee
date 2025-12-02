using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Queene.Core.Converters;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.Tests.Converters.TestData;
using Queene.Core.Tests.MoveGenerating;
using System.Collections.Generic;
using Xunit;

namespace Queene.Core.Tests.Converters
{
    public class BoardStateConverterTests : TestsBase
    {
        public BoardStateConverterTests()
        {
            _fixture.Customize(new AutoMoqCustomization());
            _fixture.Freeze<BoardStateToFenConverter>();
        }

        [Theory(DisplayName = "Should convert pieces")]
        [ClassData(typeof(BoardStateConverterTestData))]
        public void ShouldConvertPieces(Dictionary<PieceTypeEnum, List<byte>> whitePieces, Dictionary<PieceTypeEnum, List<byte>> blackPieces, string expectedPiecesPart)
        {
            // Arrange
            var boardState = _fixture.Build<BoardState>()
                .With(bs => bs.WhitePieces, whitePieces)
                .With(bs => bs.BlackPieces, blackPieces)
                .Create();

            // Act
            var fen = _boardStateConverter.Convert(boardState);

            // Assert
            _boardStateConverter.PiecesPart.Should().Be(expectedPiecesPart);
        }

        [Theory]
        [InlineData(PlayerEnum.White, "w")]
        [InlineData(PlayerEnum.Black, "b")]
        public void ShouldConvertPlayer(PlayerEnum player, string expectedPlayerPart)
        {
            // Arrange
            var boardState = _fixture.Build<BoardState>()
                .With(bs => bs.TurnToMove, player)
                .Without(bs => bs.WhitePieces)
                .Without(bs => bs.BlackPieces)
                .Create();

            // Act
            var fen = _boardStateConverter.Convert(boardState);

            // Assert
            _boardStateConverter.PlayerPart.Should().Be(expectedPlayerPart);
        }

        [Fact]
        public void ShouldConvertEnPassante()
        {
            // Arrange
            var boardState = _fixture.Build<BoardState>()
                .With(bs => bs.EnPassant, "h6")
                .Without(bs => bs.WhitePieces)
                .Without(bs => bs.BlackPieces)
                .Create();

            // Act
            var fen = _boardStateConverter.Convert(boardState);

            // Assert
            _boardStateConverter.EnPassantePart.Should().Be("h6");
        }

        [Fact]
        public void ShouldConvertHalfMove()
        {
            // Arrange
            var boardState = _fixture.Build<BoardState>()
                .Without(bs => bs.WhitePieces)
                .Without(bs => bs.BlackPieces)
                .Create();

            // Act
            var fen = _boardStateConverter.Convert(boardState);

            // Assert
            _boardStateConverter.HalfMovePart.Should().Be(boardState.HalfMoveNumber.ToString());
        }

        [Fact]
        public void ShouldConvertFullMove()
        {
            // Arrange
            var boardState = _fixture.Build<BoardState>()
                .Without(bs => bs.WhitePieces)
                .Without(bs => bs.BlackPieces)
                .Create();

            // Act
            var fen = _boardStateConverter.Convert(boardState);

            // Assert
            _boardStateConverter.FullMovePart.Should().Be(boardState.FullMoveNumber.ToString());
        }
    }
}
