using AoC.Extensions;

namespace AoC.Test.Extensions;

/// <summary>
/// Unit tests for <see cref="MathExtensions"/>.
/// </summary>
public class MathExtensionsTest
{
    [Theory]
    [InlineData(0, 0, 10, true)]
    [InlineData(5, 0, 10, true)]
    [InlineData(10, 0, 10, true)]
    [InlineData(0, 10, 0, false)]
    [InlineData(5, 10, 0, false)]
    [InlineData(10, 10, 0, false)]
    [InlineData(11, 0, 10, false)]
    [InlineData(-1, 0, 10, false)]
    public void IsBetween_ReturnsCorrectValue(int value, int min, int max, bool expected)
    {
        // act
        var result = value.IsBetween(min, max);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10, 10, 0)]
    [InlineData(0, 10, 0)]
    [InlineData(11, 10, 1)]
    [InlineData(-1, 10, 9)]
    public void SafeMod_ReturnsCorrectValue(long value, long mod, long expected)
    {
        // act
        var result = value.SafeMod(mod);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(5, 25)]
    [InlineData(10, 100)]
    [InlineData(-1, 1)]
    [InlineData(-5, 25)]
    public void Square_ReturnsCorrectValue(int value, int expected)
    {
        // act
        var result = value.Square();

        // assert
        Assert.Equal(expected, result);
    }
}
