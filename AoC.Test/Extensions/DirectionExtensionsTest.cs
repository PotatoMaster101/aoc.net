using AoC.Extensions;
using AoC.Geometry;

namespace AoC.Test.Extensions;

/// <summary>
/// Unit tests for <see cref="DirectionExtensions"/>.
/// </summary>
public class DirectionExtensionsTest
{
    [Theory]
    [InlineData('^', Direction.Up)]
    [InlineData('v', Direction.Down)]
    [InlineData('<', Direction.Left)]
    [InlineData('>', Direction.Right)]
    public void ToDirection_ReturnsCorrectValue(char c, Direction expected)
    {
        // act
        var result = c.ToDirection();

        // assert
        Assert.Equal(expected, result);
    }
}
