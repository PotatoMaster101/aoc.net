using AoC.Helpers;

namespace AoC.Test.Helpers;

/// <summary>
/// Unit tests for <see cref="MathHelper"/>.
/// </summary>
public class MathHelperTest
{
    [Theory]
    [InlineData(8, 16, 8)]
    [InlineData(7, 16, 1)]
    public void Gcd_ReturnsCorrectValue(int a, int b, int expected)
    {
        // act
        var result = MathHelper.Gcd(a, b);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(8, 16, 16)]
    [InlineData(12, 15, 60)]
    public void Lcm_ReturnsCorrectValue(long a, long b, long expected)
    {
        // act
        var result = MathHelper.Lcm(a, b);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0, -1, -1, 0)]
    [InlineData(-1, 0, -1, 0)]
    [InlineData(0, 1, 0, 1)]
    [InlineData(1, 0, 0, 1)]
    public void MinMax_ReturnsCorrectValue(long a, long b, long expectedMin, long expectedMax)
    {
        // act
        var result = MathHelper.MinMax(a, b);

        // assert
        Assert.Equal(expectedMin, result.Min);
        Assert.Equal(expectedMax, result.Max);
    }
}
