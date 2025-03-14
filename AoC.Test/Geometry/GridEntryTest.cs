using AoC.Geometry;

namespace AoC.Test.Geometry;

/// <summary>
/// Unit tests for <see cref="GridEntry{T}"/>.
/// </summary>
public class GridEntryTest
{
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
