using System.Numerics;
using System.Runtime.CompilerServices;

namespace AoC.Extensions;

/// <summary>
/// Extension methods for math.
/// </summary>
public static class MathExtensions
{
    /// <summary>
    /// Checks whether a value is between two values (inclusive).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <returns>Whether <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/> (inclusive).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBetween<T>(this T value, T min, T max)
        where T : INumber<T>
    {
        return value >= min && value <= max;
    }

    /// <summary>
    /// Returns the mod of a value, ensuring the value is positive.
    /// </summary>
    /// <param name="value">The value to compute the mod.</param>
    /// <param name="mod">The mod value.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <returns>The mod of the given value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SafeMod<T>(this T value, T mod)
        where T : INumber<T>
    {
        return (value % mod + mod) % mod;
    }

    /// <summary>
    /// Returns the square of a value.
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <returns>The square of <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Square<T>(this T value)
        where T : INumber<T>
    {
        return value * value;
    }
}
