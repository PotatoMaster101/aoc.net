using System.Numerics;
using System.Runtime.CompilerServices;

namespace AoC.Helpers;

/// <summary>
/// Helper methods for math.
/// </summary>
public static class MathHelper
{
    /// <summary>
    /// Computes the greatest common divisor (GCD) of 2 numbers.
    /// </summary>
    /// <param name="a">The first number.</param>
    /// <param name="b">The second number.</param>
    /// <returns>The GCD of 2 numbers.</returns>
    public static long Gcd(long a, long b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    /// <summary>
    /// Computes the least common multiple (LCM) of 2 numbers.
    /// </summary>
    /// <param name="a">The first number.</param>
    /// <param name="b">The second number.</param>
    /// <returns>The LCM of 2 numbers.</returns>
    public static long Lcm(long a, long b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        return a / Gcd(a, b) * b;
    }

    /// <summary>
    /// Gets the minimum and maximum values.
    /// </summary>
    /// <param name="a">The first number.</param>
    /// <param name="b">The second number.</param>
    /// <returns>The minimum and maximum values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T Min, T Max) MinMax<T>(T a, T b) where T : struct, IBinaryInteger<T>
    {
        return a < b ? (a, b) : (b, a);
    }
}
