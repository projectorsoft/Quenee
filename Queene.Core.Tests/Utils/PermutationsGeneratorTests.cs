using FluentAssertions;
using QueeneEngine.Engine.Utils;
using System.Collections.Generic;
using Xunit;

namespace Queene.Core.Tests.Utils
{
    public class PermutationsGeneratorTests
    {
        [Fact(DisplayName = "Should return correct number of permutations")]
        public void ShouldReturnCorrectNumberOfPermutations()
        {
            // Arrange
            ulong mask = 12;

            // Act
            var result = PermutationsGenerator.GenerateMaskPermutations(mask);

            // Assert
            result.Count.Should().Be(4);
            result.Should().BeEquivalentTo(new List<ulong>() { 0, 4, 8, 12 });
        }
    }
}
