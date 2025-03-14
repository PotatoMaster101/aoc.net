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
    /// <typeparam name="T">Integer type.</typeparam>
    /// <returns>The GCD of 2 numbers.</returns>
    public static T Gcd<T>(T a, T b)
        where T : IBinaryInteger<T>
    {
        (a, b) = (T.Abs(a), T.Abs(b));
        while (b != T.Zero)
            (a, b) = (b, a % b);
        return a;
    }

    /// <summary>
    /// Computes the least common multiple (LCM) of 2 numbers.
    /// </summary>
    /// <param name="a">The first number.</param>
    /// <param name="b">The second number.</param>
    /// <typeparam name="T">Integer type.</typeparam>
    /// <returns>The LCM of 2 numbers.</returns>
    public static T Lcm<T>(T a, T b)
        where T : IBinaryInteger<T>
    {
        (a, b) = (T.Abs(a), T.Abs(b));
        return a / Gcd(a, b) * b;
    }

    /// <summary>
    /// Gets the minimum and maximum values.
    /// </summary>
    /// <param name="a">The first number.</param>
    /// <param name="b">The second number.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <returns>The minimum and maximum values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T Min, T Max) MinMax<T>(T a, T b)
        where T : INumber<T>
    {
        return a < b ? (a, b) : (b, a);
    }
}
