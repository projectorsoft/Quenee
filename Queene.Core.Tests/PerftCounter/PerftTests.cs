using FluentAssertions;
using Queene.Core.Engine.PerftCounter;
using Queene.Core.Tests.MoveGenerating;
using Queene.Core.Tests.PerftCounter.TestData;
using Xunit;

namespace Queene.Core.Tests.PerftCounter
{
    public class PerftTests: TestsBase
    {
        [Theory]
        [ClassData(typeof(PerftTestData))]
        public void ShouldReturnCorrectPerftResults(string fen, int ply, long expectedResult)
        {
            // Arrange
            var board = new Board(_bitBoardContextConverter, _zorbistHash);
            board.NewGame(fen);
            var hash = _bitBoard.Context.Hash;

            var perft = new Perft(board);

            // Act
            var result = perft.Run(ply);

            // Assert
            result.Should().Be(expectedResult);
            _bitBoard.Context.Hash.Should().Be(hash);
        }
    }
}
