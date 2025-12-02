using FluentAssertions;
using Queene.Core.Converters;
using Queene.Core.Tests.MoveGenerating;
using Queene.Core.Tests.Perft.TestData;
using Xunit;

namespace Queene.Core.Tests.Perft
{
    public class PerftTests: TestsBase
    {
        [Theory]
        [ClassData(typeof(PerftTestData))]
        [InlineData("1nb2b2/r1B5/1P4k1/pP2p3/2P1P1p1/1N2r1P1/1R4BP/5K1R w - - 0 1", 6, 722745522)]
        [InlineData("r1qk1bn1/pp5r/B4p1p/1Pp1p2Q/P1P4P/2RPBb2/8/1N2K1NR b - -", 6, 1814335108)]
        [InlineData("rnk2br1/1b1pQ3/1P6/p3p1p1/1PPpPP2/7p/3N2PP/R1BK1B1R w - - 0 1", 6, 605210848)]
        public void ShouldReturnCorrectPerftResults(string fen, int ply, ulong expectedResult)
        {
            // Arrange
            var board = new Board(new BitBoardContextConverter(new BoardStateToFenConverter()));
            board.NewGame(fen);

            var perft = new Engine.Perft(board);

            // Act
            var result = perft.Run(ply);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
