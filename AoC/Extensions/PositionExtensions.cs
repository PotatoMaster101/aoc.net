using System.Numerics;
using System.Runtime.CompilerServices;
using AoC.Geometry;

namespace AoC.Extensions;

/// <summary>
/// Extension methods for <see cref="Position{T}"/>.
/// </summary>
public static class PositionExtensions
{
    /// <summary>
    /// Returns an index from a <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="width">The container width.</param>
    /// <typeparam name="T">The index type.</typeparam>
    /// <returns>The index from <paramref name="position"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ToIndex<T>(this Position<T> position, T width)
        where T : struct, IBinaryInteger<T>
    {
        return position.X + position.Y * width;
    }

    /// <summary>
    /// Returns a <see cref="Position{T}"/> from an index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="width">The container width.</param>
    /// <typeparam name="T">The index type.</typeparam>
    /// <returns>The <see cref="Position{T}"/> from <paramref name="index"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position<T> ToPosition<T>(this T index, T width)
        where T : struct, IBinaryInteger<T>
    {
        return new Position<T>(index % width, index / width);
    }
}
