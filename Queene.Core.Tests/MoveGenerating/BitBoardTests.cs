using FluentAssertions;
using Queene.Core.Fen;
using Queene.Core.MovesGenerating.Pieces;
using Queene.Core.Utils;
using Xunit;

namespace Queene.Core.Tests.MoveGenerating
{
    public class BitBoardTests : TestsBase
    {
        public BitBoardTests() : base()
        {
        }

        [Theory(DisplayName = "Should setup correct checked squares and attackers")]
        [InlineData("1k6/4r3/8/8/8/4K3/8/6N1 w - - 0 1", 0x80808000000, 0x8000000000000)] //rook file single attack
        [InlineData("1k6/8/8/8/8/5K1r/8/8 w - - 0 1", 0x20000, 0x10000)] //rook rank single attack
        [InlineData("bk6/8/8/8/8/5K2/8/8 w - - 0 1", 0x40201008000000, 0x8000000000000000)] //bishop single attack
        [InlineData("1k6/8/8/8/5b2/8/3K4/8 w - - 0 1", 0x80000, 0x4000000)] //bishop single attack
        [InlineData("1k6/8/8/3q4/8/8/3K4/8 w - - 0 1", 0x10100000, 0x1000000000)] //queen file single attack 
        [InlineData("1k3q2/8/8/8/8/K7/8/8 w - - 0 1", 0x8102040000000, 0x400000000000000)] //queen single diagonal attack
        [InlineData("1k6/8/3K4/8/2n5/8/8/8 w - - 0 1", 0x20000000, 0x20000000)] //knight single attack
        [InlineData("1k6/1n6/3K4/8/2n5/8/8/8 w - - 0 1", 0x40000020000000, 0x40000020000000)] //two knights attack
        [InlineData("1k6/8/8/4p3/3K4/8/8/8 w - - 0 1", 0x800000000, 0x800000000)] //pawn attack
        [InlineData("1k6/8/7p/6K1/8/8/8/8 w - - 0 1", 0x10000000000, 0x10000000000)] //pawn attack
        [InlineData("1k6/6p1/7K/8/8/8/8/8 w - - 0 1", 0x2000000000000, 0x2000000000000)] //pawn attack
        [InlineData("8/8/7K/8/8/1k6/P7/8 b - - 0 1", 0x8000, 0x8000)] //pawn attack
        public void ShouldSetupCorrectCheckedSquaresAndAttackers(string fen, ulong expectedCheckedSquares, ulong expectedAttackers)
        {
            // Arrange
            SquaresBetweenMasksGeneratorHelper.GenerateMasksBeetwenSquares();
            var boardState = FenHelper.CreateBoardState(fen);
            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            _bitBoard.SetupCheckingSquaresAndAttackers(_bitBoard.Context.Player, king.Square);

            //Assert
            _bitBoard.Context.CheckedSquares.Should().Be(expectedCheckedSquares);
            _bitBoard.Context.Attackers.Should().Be(expectedAttackers);
        }

        [Theory(DisplayName = "Should setup correct pinned squares and pinners")]
        [InlineData("8/5k2/8/8/1b5q/8/3P1P2/4K3 w - - 0 1", 0x1400, 0x41000000)] //bishop and queen pinning pawns
        [InlineData("8/1b3k1q/8/3P1P2/4K3/3P1P2/2q3b1/8 w - - 0 1", 0x1400140000, 0x41000000002200)] //bishop and queen pinning pawns all sides
        [InlineData("4r3/8/4Q3/8/q1R1KQ1r/8/4R3/4q2k w - - 0 1", 0x80024000800, 0x800000081000008)] //rooks and queen all sides
        public void ShouldSetupCorrectPinnedSquaresAndPinners(string fen, ulong expectedPinnedSquares, ulong expectedPinners)
        {
            // Arrange
            SquaresBetweenMasksGeneratorHelper.GenerateMasksBeetwenSquares();
            var boardState = FenHelper.CreateBoardState(fen);
            _bitBoard.SetupBoard(boardState);

            var king = new King(_bitBoard.Context, _movesContainer, _movesList, _piecesListService);

            // Act
            _bitBoard.SetupCheckingSquaresAndAttackers(_bitBoard.Context.Player, king.Square);

            //Assert
            _bitBoard.Context.PinnedSquares.Should().Be(expectedPinnedSquares);
            _bitBoard.Context.Pinners.Should().Be(expectedPinners);
        }
    }
}
