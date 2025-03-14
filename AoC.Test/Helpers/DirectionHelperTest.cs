using AoC.Geometry;
using AoC.Helpers;

namespace AoC.Test.Helpers;

/// <summary>
/// Unit tests for <see cref="DirectionHelper"/>.
/// </summary>
public class DirectionHelperTest
{
    [Theory]
    [InlineData('^', Direction.Up)]
    [InlineData('v', Direction.Down)]
    [InlineData('<', Direction.Left)]
    [InlineData('>', Direction.Right)]
    public void FromChar_ReturnsCorrectValue(char c, Direction expected)
    {
        // act
        var result = DirectionHelper.FromChar(c);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(Direction.Up, Direction.Down, Direction.Left, Direction.Right)]
    public void GetCross_ReturnsCorrectValue(params Direction[] expected)
    {
        // act
        var result = DirectionHelper.GetCross();

        // assert
        Assert.Equal(result.Length, expected.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [InlineData(Direction.TopLeft, Direction.TopRight, Direction.BottomLeft, Direction.BottomRight)]
    public void GetDiagonal_ReturnsCorrectValue(params Direction[] expected)
    {
        // act
        var result = DirectionHelper.GetDiagonal();

        // assert
        Assert.Equal(result.Length, expected.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }
}
