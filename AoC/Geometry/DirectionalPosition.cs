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
    where T : INumber<T>
{
    /// <summary>
    /// Constructs a new instance of <see cref="DirectionalPosition{T}"/>.
    /// </summary>
    /// <param name="direction">The direction.</param>
    public DirectionalPosition(Position<T> direction)
        : this(Position<T>.Origin, direction) { }

    /// <summary>
    /// Returns a <see cref="DirectionalPosition{T}"/> after travelling some distance.
    /// </summary>
    /// <param name="distance">The distance to travel.</param>
    /// <returns>The <see cref="DirectionalPosition{T}"/> after travelling some distance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DirectionalPosition<T> Next(T? distance = default)
    {
        return distance is null || distance == T.Zero
            ? this with { Position = Position + Direction }
            : this with { Position = Position + Direction * distance };
    }

    /// <summary>
    /// Returns a collection of <see cref="DirectionalPosition{T}"/> after travelling some distance.
    /// </summary>
    /// <param name="count">The number of positions.</param>
    /// <param name="distance">The distance to travel.</param>
    /// <returns>The collection of <see cref="DirectionalPosition{T}"/> after travelling some distance.</returns>
    public IEnumerable<DirectionalPosition<T>> NextRange(long count, T? distance = default)
    {
        var current = this;
        for (var i = 0L; i < count; i++)
            yield return current = current.Next(distance);
    }

    /// <summary>
    /// Returns a <see cref="DirectionalPosition{T}"/> after turning 90 degrees a specific number of times.
    /// </summary>
    /// <param name="turns">The number of 90 degrees turns.</param>
    /// <returns>The <see cref="DirectionalPosition{T}"/> after turning 90 degrees a specific number of times.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DirectionalPosition<T> Rotate(int turns)
    {
        return this with { Direction = Direction.Rotate(turns) };
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
