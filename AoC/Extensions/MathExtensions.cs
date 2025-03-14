using System.Numerics;
using System.Runtime.CompilerServices;

namespace AoC.Extensions;

/// <summary>
/// Extension methods for math.
/// </summary>
public static class MathExtensions
{
    extension<T>(T value) where T : INumber<T>
    {
        /// <summary>
        /// Clamps the specified value within the inclusive range defined by <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Clamp(T min, T max)
        {
            return value < min ? min : value > max ? max : value;
        }

        /// <summary>
        /// Checks whether a value is between two values (inclusive).
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>Whether <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/> (inclusive).</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsBetween(T min, T max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// Returns the mod of a value, ensuring the value is positive.
        /// </summary>
        /// <param name="mod">The mod value.</param>
        /// <returns>The mod of the given value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SafeMod(T mod)
        {
            return (value % mod + mod) % mod;
        }
    }
}
