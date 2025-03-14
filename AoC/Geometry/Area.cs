using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AoC.Geometry;

/// <summary>
/// Represents a 2D area.
/// </summary>
/// <typeparam name="T">The type of the axis.</typeparam>
[DebuggerDisplay("{Width} x {Height}")]
public readonly record struct Area<T> : IEnumerable<Position<T>>
    where T : INumber<T>
{
    /// <summary>
    /// Gets the top-left position.
    /// </summary>
    public Position<T> TopLeft { get; }

    /// <summary>
    /// Gets the top-right position.
    /// </summary>
    public Position<T> TopRight => new(BottomRight.X, TopLeft.Y);

    /// <summary>
    /// Gets the bottom-left position.
    /// </summary>
    public Position<T> BottomLeft => new(TopLeft.X, BottomRight.Y);

    /// <summary>
    /// Gets the bottom-right position.
    /// </summary>
    public Position<T> BottomRight { get; }

    /// <summary>
    /// Returns the maximum X position.
    /// </summary>
    public T MaxX => BottomRight.X;

    /// <summary>
    /// Returns the maximum Y position.
    /// </summary>
    public T MaxY => TopLeft.Y;

    /// <summary>
    /// Returns the minimum X position.
    /// </summary>
    public T MinX => TopLeft.X;

    /// <summary>
    /// Returns the minimum Y position.
    /// </summary>
    public T MinY => BottomRight.Y;

    /// <summary>
    /// Returns the area width.
    /// </summary>
    public T Width => MaxX - MinX + T.One;

    /// <summary>
    /// Returns the area height.
    /// </summary>
    public T Height => MaxY - MinY + T.One;

    /// <summary>
    /// Returns the area perimeter.
    /// </summary>
    public T Perimeter => Width + Width + Height + Height - (T.One + T.One + T.One + T.One);

    /// <summary>
    /// Constructs a new instance of <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="corner1">One of the corners in the area.</param>
    /// <param name="corner2">One of the corners in the area.</param>
    public Area(Position<T> corner1, Position<T> corner2)
    {
        TopLeft = new Position<T>(T.Min(corner1.X, corner2.X), T.Max(corner1.Y, corner2.Y));
        BottomRight = new Position<T>(T.Max(corner1.X, corner2.X), T.Min(corner1.Y, corner2.Y));
    }

    /// <inheritdoc />
    public IEnumerator<Position<T>> GetEnumerator()
    {
        for (var y = MinY; y <= MaxY; y++)
        for (var x = MinX; x <= MaxX; x++)
            yield return new Position<T>(x, y);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Returns all the positions in a row.
    /// </summary>
    /// <param name="row">The row position.</param>
    /// <param name="distance">The distance between each position.</param>
    /// <returns>All the positions in a row.</returns>
    public IEnumerable<Position<T>> GetRow(T row, T? distance = default)
    {
        if (!IsYInbound(row))
            yield break;

        var step = distance is null || distance <= T.Zero ? T.One : distance;
        for (var x = MinX; x <= MaxX; x += step)
            yield return new Position<T>(x, row);
    }

    /// <summary>
    /// Returns all the positions in a column.
    /// </summary>
    /// <param name="column">The column position.</param>
    /// <param name="distance">The distance between each position.</param>
    /// <returns>All the positions in a column.</returns>
    public IEnumerable<Position<T>> GetColumn(T column, T? distance = default)
    {
        if (!IsXInbound(column))
            yield break;

        var step = distance is null || distance <= T.Zero ? T.One : distance;
        for (var y = MinY; y <= MaxY; y += step)
            yield return new Position<T>(column, y);
    }

    /// <summary>
    /// Returns a list of neighbouring <see cref="Position{T}"/> that is inside this <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="position">The starting <see cref="Position{T}"/> to retrieve the neighbours.</param>
    /// <param name="distance">The distance to the neighbours.</param>
    /// <param name="directions">The directions to the neighbours. If <c>null</c>, use cross.</param>
    /// <param name="wrap">Whether to wrap out-of-range neighbours so they are included.</param>
    /// <returns>The list of neighbouring <see cref="Position{T}"/> that is inside this <see cref="Area{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Position<T>> GetNeighbours(Position<T> position, T? distance = default, IEnumerable<Direction>? directions = null, bool wrap = false)
    {
        var neighbours = position.GetNeighbours(distance, directions);
        return wrap ? Wrap(neighbours) : Filter(neighbours);
    }

    /// <summary>
    /// Returns an intersection area between this <see cref="Area{T}"/> and another.
    /// </summary>
    /// <param name="other">The other area.</param>
    /// <returns>The intersection area between this <see cref="Area{T}"/> and another, or <c>null</c> if areas don't intersect.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Area<T>? GetIntersection(Area<T> other)
    {
        var minX = T.Max(MinX, other.MinX);
        var maxX = T.Min(MaxX, other.MaxX);
        var minY = T.Max(MinY, other.MinY);
        var maxY = T.Min(MaxY, other.MaxY);
        return minX > maxX || minY > maxY ? null : new Area<T>(new Position<T>(minX, maxY), new Position<T>(maxX, minY));
    }

    /// <summary>
    /// Expands this <see cref="Area{T}"/> by the same amount on all four sides.
    /// </summary>
    /// <param name="amount">The amount to expand.</param>
    /// <returns>The expanded <see cref="Area{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Area<T> Expand(T amount)
    {
        return new Area<T>(TopLeft + new Position<T>(-amount, amount), BottomRight + new Position<T>(amount, -amount));
    }

    /// <summary>
    /// Determines whether this <see cref="Area{T}"/> includes a <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="position">The <see cref="Position{T}"/> to check.</param>
    /// <returns>Whether this <see cref="Area{T}"/> includes a <see cref="Position{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(Position<T> position)
    {
        return IsXInbound(position.X) && IsYInbound(position.Y);
    }

    /// <summary>
    /// Determines whether this <see cref="Area{T}"/> includes another <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="area">The <see cref="Area{T}"/> to check.</param>
    /// <returns>Whether this <see cref="Area{T}"/> includes another <see cref="Area{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(Area<T> area)
    {
        return Has(area.TopLeft) && Has(area.BottomRight);
    }

    /// <summary>
    /// Determines whether a <see cref="Position{T}"/> is on the X boundary of this <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="position">The <see cref="Position{T}"/> to check.</param>
    /// <returns>Whether the <see cref="Position{T}"/> is on the X boundary of this <see cref="Area{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool OnXBound(Position<T> position)
    {
        return IsYInbound(position.Y) && (position.X == MaxX || position.X == MinX);
    }

    /// <summary>
    /// Determines whether a <see cref="Position{T}"/> is on the Y boundary of this <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="position">The <see cref="Position{T}"/> to check.</param>
    /// <returns>Whether the <see cref="Position{T}"/> is on the Y boundary of this <see cref="Area{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool OnYBound(Position<T> position)
    {
        return IsXInbound(position.X) && (position.Y == MaxY || position.Y == MinY);
    }

    /// <summary>
    /// Determines whether a <see cref="Position{T}"/> is on the X or Y boundary of this <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="position">The <see cref="Position{T}"/> to check.</param>
    /// <returns>Whether the <see cref="Position{T}"/> is on the X or Y boundary of this <see cref="Area{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool OnBound(Position<T> position)
    {
        return OnXBound(position) || OnYBound(position);
    }

    /// <summary>
    /// Determines whether a <see cref="Position{T}"/> is on the corner of this <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="position">The <see cref="Position{T}"/> to check.</param>
    /// <returns>Whether the <see cref="Position{T}"/> is on the corner of this <see cref="Area{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool OnCorner(Position<T> position)
    {
        return OnXBound(position) && OnYBound(position);
    }

    /// <summary>
    /// Wraps a <see cref="Position{T}"/> so it is in this <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="position">The <see cref="Position{T}"/> to wrap.</param>
    /// <returns>The wrapped <see cref="Position{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Position<T> Wrap(Position<T> position)
    {
        return position % new Position<T>(Width, Height);
    }

    /// <summary>
    /// Wraps a list of <see cref="Position{T}"/> so they are in this <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="positions">The <see cref="Position{T}"/> to wrap.</param>
    /// <returns>The wrapped <see cref="Position{T}"/>.</returns>
    public IEnumerable<Position<T>> Wrap(IEnumerable<Position<T>> positions)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var position in positions)
            yield return position % new Position<T>(Width, Height);
    }

    /// <summary>
    /// Filters a list of <see cref="Position{T}"/> that is inside this <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="positions">The list of <see cref="Position{T}"/> to filter.</param>
    /// <returns>The list of <see cref="Position{T}"/> that is inside this <see cref="Area{T}"/>.</returns>
    public IEnumerable<Position<T>> Filter(IEnumerable<Position<T>> positions)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var position in positions)
            if (Has(position))
                yield return position;
    }

    /// <summary>
    /// Determines whether an X value is in bound.
    /// </summary>
    /// <param name="x">The X value to check.</param>
    /// <returns>Whether the X value is in bound.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsXInbound(T x)
    {
        return x >= MinX && x <= MaxX;
    }

    /// <summary>
    /// Determines whether a Y value is inbound.
    /// </summary>
    /// <param name="y">The Y value to check.</param>
    /// <returns>Whether the Y value is in bound.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsYInbound(T y)
    {
        return y >= MinY && y <= MaxY;
    }
}
