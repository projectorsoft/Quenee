using FluentAssertions;
using Queene.Core.Enums;
using Queene.Core.Models;
using Xunit;

namespace Queene.Core.Tests.Models
{
    public class MoveTests
    {
        [Theory(DisplayName = "Should create valid move")]
        [InlineData(1, 8, MoveTypeEnum.Move)]
        [InlineData(63, 8, MoveTypeEnum.Castle)]
        [InlineData(0, 8, MoveTypeEnum.EnPassante)]
        [InlineData(0, 8, MoveTypeEnum.Promotion)]
        public void ShouldCreateValidMove(byte from, byte to, MoveTypeEnum moveType)
        {
            // Arrange && act
            var move = new Move(from, to, moveType);

            // Assert
            move.GetFromSquare().Should().Be(from);
            move.GetToSquare().Should().Be(to);
            move.GetMoveType().Should().Be(moveType);
        }

        [Fact(DisplayName = "Should be EnPassante")]
        public void ShouldBeEnPassante()
        {
            // Arrange && act
            var move = new Move(1, 8, MoveTypeEnum.EnPassante);

            // Assert
            move.IsEnPassantMove().Should().BeTrue();
        }

        [Theory(DisplayName = "Should be Promotion")]
        [InlineData(PieceTypeEnum.Knight)]
        [InlineData(PieceTypeEnum.Bishop)]
        [InlineData(PieceTypeEnum.Rook)]
        [InlineData(PieceTypeEnum.Queen)]
        public void ShouldBePromotion(PieceTypeEnum pieceType)
        {
            // Arrange && act
            var move = new Move(1, 8, MoveTypeEnum.Promotion, pieceType);

            // Assert
            move.IsPromotionMove().Should().BeTrue();
            move.GetPromotionPieceType().Should().Be(pieceType);
        }

        [Fact(DisplayName = "Should be CastleKingSide")]
        public void ShouldBeCastleKingSide()
        {
            // Arrange && act
            var move = new Move(1, 8, MoveTypeEnum.Castle);

            // Assert
            move.IsCastleMove().Should().BeTrue();
        }

        [Fact(DisplayName = "Should be CastleQueenSide")]
        public void ShouldBeCastleQueenSide()
        {
            // Arrange && act
            var move = new Move(1, 8, MoveTypeEnum.Castle);

            // Assert
            move.IsCastleMove().Should().BeTrue();
        }

        [Fact(DisplayName = "Should be invalid move")]
        public void ShouldBeInvalidMove()
        {
            // Arrange && act
            var move = new Move(1, 1, MoveTypeEnum.Castle);

            // Assert
            move.IsValidMove().Should().BeFalse();
        }

        [Fact(DisplayName = "Should check two moves are equal")]
        public void ShouldCheckTwoMovesAreEqual()
        {
            // Arrange && act
            var move = new Move(1, 10, MoveTypeEnum.Promotion);
            var otherMove = new Move(1, 10, MoveTypeEnum.Promotion);

            // Assert
            move.Equals(otherMove).Should().BeTrue();
        }
    }
}
