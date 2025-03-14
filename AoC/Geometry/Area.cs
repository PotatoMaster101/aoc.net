﻿using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AoC.Geometry;

/// <summary>
/// Represents a 2D area.
/// </summary>
/// <typeparam name="T">The type of the axis.</typeparam>
[DebuggerDisplay("{Width} x {Height}")]
public readonly record struct Area<T>
    where T: struct, IBinaryNumber<T>
{
    /// <summary>
    /// Gets the top left position.
    /// </summary>
    public Position<T> TopLeft { get; }

    /// <summary>
    /// Gets the top right position.
    /// </summary>
    public Position<T> TopRight => new(BottomRight.X, TopLeft.Y);

    /// <summary>
    /// Gets the bottom left position.
    /// </summary>
    public Position<T> BottomLeft => new(TopLeft.X, BottomRight.Y);

    /// <summary>
    /// Gets the bottom right position.
    /// </summary>
    public Position<T> BottomRight { get; }

    /// <summary>
    /// Returns the maximum X position.
    /// </summary>
    public T MaxX => TopRight.X;

    /// <summary>
    /// Returns the maximum Y position.
    /// </summary>
    public T MaxY => TopRight.Y;

    /// <summary>
    /// Returns the minimum X position.
    /// </summary>
    public T MinX => BottomLeft.X;

    /// <summary>
    /// Returns the minimum Y position.
    /// </summary>
    public T MinY => BottomLeft.Y;

    /// <summary>
    /// Returns the area width.
    /// </summary>
    public T Width => MaxX - MinX + T.One;

    /// <summary>
    /// Returns the area height.
    /// </summary>
    public T Height => MaxY - MinY + T.One;

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

    /// <summary>
    /// Returns all the positions in a row.
    /// </summary>
    /// <param name="row">The row position.</param>
    /// <param name="distance">The distance between each position.</param>
    /// <returns>All the positions in a row.</returns>
    public IEnumerable<Position<T>> GetRow(T row, T distance)
    {
        if (!IsYInbound(row))
            yield break;
        for (var x = MinX; x <= MaxX; x += distance)
            yield return new Position<T>(x, row);
    }

    /// <summary>
    /// Returns all the positions in a column.
    /// </summary>
    /// <param name="column">The column position.</param>
    /// <param name="distance">The distance between each position.</param>
    /// <returns>All the positions in a column.</returns>
    public IEnumerable<Position<T>> GetColumn(T column, T distance)
    {
        if (!IsXInbound(column))
            yield break;
        for (var y = MinY; y <= MaxY; y += distance)
            yield return new Position<T>(column, y);
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
    /// Determines whether a <see cref="Position{T}"/> is on the X and Y boundary of this <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="position">The <see cref="Position{T}"/> to check.</param>
    /// <returns>Whether the <see cref="Position{T}"/> is on the X and Y boundary of this <see cref="Area{T}"/>.</returns>
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
    /// Determines whether this <see cref="Area{T}"/> includes a <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="position">The <see cref="Position{T}"/> to check.</param>
    /// <returns>Whether this <see cref="Area{T}"/> includes a <see cref="Position{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasPosition(Position<T> position)
    {
        return IsXInbound(position.X) && IsYInbound(position.Y);
    }

    /// <summary>
    /// Filters a list of <see cref="Position{T}"/> that is inside this <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="positions">The list of <see cref="Position{T}"/> to filter.</param>
    /// <returns>The list of <see cref="Position{T}"/> that is inside this <see cref="Area{T}"/>.</returns>
    public IEnumerable<Position<T>> FilterPositions(params IEnumerable<Position<T>> positions)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var position in positions)
            if (HasPosition(position))
                yield return position;
    }

    /// <summary>
    /// Returns a list of neighbouring <see cref="Position{T}"/> that is inside this <see cref="Area{T}"/>.
    /// </summary>
    /// <param name="position">The starting <see cref="Position{T}"/> to retrieve the neighbours.</param>
    /// <param name="distance">The distance to the neighbours.</param>
    /// <param name="directions">The directions to the neighbours.</param>
    /// <returns>The list of neighbouring <see cref="Position{T}"/> that is inside this <see cref="Area{T}"/>.</returns>
    public IEnumerable<Position<T>> GetNeighbours(Position<T> position, T distance, params IEnumerable<Direction> directions)
    {
        return FilterPositions(position.GetNeighbours(distance, directions));
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
    /// Determines whether an Y value is in bound.
    /// </summary>
    /// <param name="y">The Y value to check.</param>
    /// <returns>Whether the Y value is in bound.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsYInbound(T y)
    {
        return y >= MinY && y <= MaxY;
    }
}
