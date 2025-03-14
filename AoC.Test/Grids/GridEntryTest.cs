using AoC.Geometry;
using AoC.Grids;

namespace AoC.Test.Grids;

/// <summary>
/// Unit tests for <see cref="GridEntry{T}"/>.
/// </summary>
public class GridEntryTest
{
    [Theory]
    [InlineData(0, 0, 0, "(0, 0): 0")]
    [InlineData(3, 5, 0, "(3, 5): 0")]
    [InlineData(-4, 9, 999, "(-4, 9): 999")]
    public void ToString_ReturnsCorrectValue(int x, int y, int value, string expected)
    {
        // arrange
        var sut = new GridEntry<int>(new Position<int>(x, y), value);

        // act
        var result = sut.ToString();

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 0, 5)]
    [InlineData(-1, 1, -2)]
    public void ToValueConversion_ReturnsCorrectValue(int x, int y, int expected)
    {
        // arrange
        var sut = new GridEntry<int>(new Position<int>(x, y), expected);

        // act
        var result = (int)sut;

        // assert
        Assert.Equal(expected, result);
    }
}
