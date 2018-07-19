using AutoFixture;
using FluentAssertions;
using Jobus.Common.Extensions;
using Xunit;

namespace Jobus.Tests.Common.Extensions
{
    public class ExtensionsTests
    {
        [Theory(DisplayName = "Shorten")]
        [InlineData(2, 3, 2)]
        [InlineData(3, 3, 3)]
        [InlineData(9, 3, 3)]
        public void Shorten(int stringLength, int maxLength, int expectedLength)
        {
            // Arrange
            string str = new Fixture().Create<string>().Substring(0, stringLength);

            // Act
            string shortString = str.Shorten(maxLength);

            // Assert
            shortString.Length.Should().Be(expectedLength);
        }
    }
}
