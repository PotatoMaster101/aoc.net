using System.Diagnostics;
using System.Runtime.CompilerServices;
using AoC.Geometry;

namespace AoC.Grids;

/// <summary>
/// Represents a position in a 2D grid.
/// </summary>
/// <param name="Position">The grid position.</param>
/// <param name="Value">The grid value.</param>
/// <typeparam name="T">The value data type.</typeparam>
[DebuggerDisplay("{Position}: {Value}")]
public readonly record struct GridEntry<T>(Position<int> Position, T Value)
{
    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"{Position}: {Value}";
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
