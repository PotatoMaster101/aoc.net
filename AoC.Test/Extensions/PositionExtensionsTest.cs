using AoC.Extensions;
using AoC.Geometry;

namespace AoC.Test.Extensions;

/// <summary>
/// Unit tests for <see cref="PositionExtensions"/>.
/// </summary>
public class PositionExtensionsTest
{
    [Theory]
    [InlineData(0, 0, 10, 0)]
    [InlineData(3, 0, 10, 3)]
    [InlineData(0, 3, 10, 30)]
    [InlineData(2, 4, 5, 22)]
    public void ToIndex_ReturnsCorrectValue(int x, int y, int width, int expected)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.ToIndex(width);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 10, 0, 0)]
    [InlineData(1, 10, 1, 0)]
    [InlineData(21, 10, 1, 2)]
    [InlineData(19, 5, 4, 3)]
    public void ToPosition_ReturnsCorrectValue(int index, int width, int expectedX, int expectedY)
    {
        // act
        var result = index.ToPosition(width);

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }
}
