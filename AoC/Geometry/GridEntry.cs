using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AoC.Geometry;

/// <summary>
/// Represents a position in a 2D grid.
/// </summary>
/// <param name="Position">The grid position.</param>
/// <param name="Value">The grid value.</param>
/// <typeparam name="T">The value data type.</typeparam>
[DebuggerDisplay("{Position}: {Value}")]
public readonly record struct GridEntry<T>(Position<int> Position, T Value)
{
    /// <summary>
    /// Returns the manhattan distance to another <see cref="GridEntry{T}"/>.
    /// </summary>
    /// <param name="other">The other <see cref="GridEntry{T}"/>.</param>
    /// <returns>The manhattan distance to <paramref name="other"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetManhattanDistance(GridEntry<T> other)
    {
        return Position.GetManhattanDistance(other.Position);
    }

    /// <summary>
    /// Implicitly converts a <see cref="GridEntry{T}"/> to a <see cref="T"/>.
    /// </summary>
    /// <param name="entry">The <see cref="GridEntry{T}"/> to convert.</param>
    /// <returns>The converted <see cref="T"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T(GridEntry<T> entry)
    {
        return entry.Value;
    }
}
