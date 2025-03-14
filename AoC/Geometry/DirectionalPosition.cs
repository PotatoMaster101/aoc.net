using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AoC.Geometry;

/// <summary>
/// Represents a 2D position and a direction.
/// </summary>
/// <param name="Position">The 2D position.</param>
/// <param name="Direction">The direction.</param>
/// <typeparam name="T">The type of the axis.</typeparam>
[DebuggerDisplay("{Position}, {Direction}")]
public readonly record struct DirectionalPosition<T>(Position<T> Position, Position<T> Direction)
    where T: struct, IBinaryNumber<T>
{
    /// <summary>
    /// Returns a <see cref="DirectionalPosition{T}"/> after travelling some distance.
    /// </summary>
    /// <param name="distance">The distance to travel.</param>
    /// <returns>The <see cref="DirectionalPosition{T}"/> after travelling some distance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DirectionalPosition<T> Next(T distance)
    {
        return this with { Position = Position + Direction * distance };
    }

    /// <summary>
    /// Returns the manhattan distance to another <see cref="DirectionalPosition{T}"/>.
    /// </summary>
    /// <param name="other">The other <see cref="DirectionalPosition{T}"/>.</param>
    /// <returns>The manhattan distance to <paramref name="other"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetManhattanDistance(DirectionalPosition<T> other)
    {
        return Position.GetManhattanDistance(other.Position);
    }

    /// <summary>
    /// Returns a collection of <see cref="DirectionalPosition{T}"/> after travelling some distance.
    /// </summary>
    /// <param name="distance">The distance to travel.</param>
    /// <param name="count">The number of positions.</param>
    /// <returns>The collection of <see cref="DirectionalPosition{T}"/> after travelling some distance.</returns>
    public IEnumerable<DirectionalPosition<T>> Next(T distance, int count)
    {
        var current = this;
        for (var i = 0; i < count; i++)
            yield return current = current.Next(distance);
    }

    /// <summary>
    /// Returns a <see cref="DirectionalPosition{T}"/> after rotating 90 degrees clockwise.
    /// </summary>
    /// <returns>The <see cref="DirectionalPosition{T}"/> after rotating 90 degrees clockwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DirectionalPosition<T> Rotate90()
    {
        return this with { Direction = Direction.Rotate90() };
    }

    /// <summary>
    /// Returns a <see cref="DirectionalPosition{T}"/> after rotating 180 degrees clockwise.
    /// </summary>
    /// <returns>The <see cref="DirectionalPosition{T}"/> after rotating 180 degrees clockwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DirectionalPosition<T> Rotate180()
    {
        return this with { Direction = Direction.Rotate180() };
    }

    /// <summary>
    /// Returns a <see cref="DirectionalPosition{T}"/> after rotating 270 degrees clockwise.
    /// </summary>
    /// <returns>The <see cref="DirectionalPosition{T}"/> after rotating 270 degrees clockwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DirectionalPosition<T> Rotate270()
    {
        return this with { Direction = Direction.Rotate270() };
    }

    /// <summary>
    /// Implicitly converts a <see cref="DirectionalPosition{T}"/> to a <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="directional">The <see cref="DirectionalPosition{T}"/> to convert.</param>
    /// <returns>The converted <see cref="Position{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Position<T>(DirectionalPosition<T> directional)
    {
        return directional.Position;
    }
}
