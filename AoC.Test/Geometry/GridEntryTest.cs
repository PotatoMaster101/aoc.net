using AoC.Geometry;

namespace AoC.Test.Geometry;

/// <summary>
/// Unit tests for <see cref="GridEntry{T}"/>.
/// </summary>
public class GridEntryTest
{
    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 1, 1, 2)]
    [InlineData(-1, 1, 1, -1, 4)]
    [InlineData(-1, 1, -1, 1, 0)]
    [InlineData(-3, 5, -7, 9, 8)]
    public void GetManhattanDistance_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new GridEntry<int>(new Position<int>(x1, y1), 0);

        // act
        var result = sut.GetManhattanDistance(new GridEntry<int>(new Position<int>(x2, y2), 0));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 0, 5)]
    [InlineData(-1, 1, -2)]
    public void ToValueConversion_ReturnsCorrectValue(int x, int y, int value)
    {
        // arrange
        var sut = new GridEntry<int>(new Position<int>(x, y), value);

        // act
        var result = (int)sut;

        // assert
        Assert.Equal(value, result);
    }
}
