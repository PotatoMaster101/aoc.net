using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AoC.Geometry;

/// <summary>
/// Represents a 2D position.
/// </summary>
/// <param name="X">The X position.</param>
/// <param name="Y">The Y position.</param>
/// <typeparam name="T">The type of the axis.</typeparam>
[DebuggerDisplay("({X}, {Y})")]
public readonly record struct Position<T>(T X, T Y)
    : IAdditionOperators<Position<T>, Position<T>, Position<T>>,
      IComparable<Position<T>>,
      IComparisonOperators<Position<T>, Position<T>, bool>,
      IDivisionOperators<Position<T>, T, Position<T>>,
      IModulusOperators<Position<T>, Position<T>, Position<T>>,
      IMultiplyOperators<Position<T>, T, Position<T>>,
      ISubtractionOperators<Position<T>, Position<T>, Position<T>>,
      IUnaryNegationOperators<Position<T>, Position<T>>
    where T: struct, IBinaryNumber<T>
{
    /// <summary>
    /// Gets the origin position (0, 0).
    /// </summary>
    public static readonly Position<T> Origin = new(T.Zero, T.Zero);

    /// <summary>
    /// Gets the unit X position (1, 0).
    /// </summary>
    public static readonly Position<T> UnitX = new(T.One, T.Zero);

    /// <summary>
    /// Gets the unit Y position (0, 1).
    /// </summary>
    public static readonly Position<T> UnitY = new(T.Zero, T.One);

    /// <summary>
    /// Constructs a new instance of <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="xy">The position's X and Y.</param>
    public Position(T xy = default)
        : this(xy, xy) { }

    /// <summary>
    /// Constructs a new instance of <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="vector">The <see cref="Vector2"/> containing the X and Y.</param>
    public Position(Vector2 vector)
        : this(T.CreateSaturating(vector.X), T.CreateSaturating(vector.Y)) { }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Position<T> other)
    {
        var xComparison = X.CompareTo(other.X);
        return xComparison is 0 ? Y.CompareTo(other.Y) : xComparison;
    }

    /// <summary>
    /// Returns a collection of neighbouring <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="distance">The distance to neighbours.</param>
    /// <param name="directions">The neighbouring directions</param>
    /// <returns>The collection of neighboring <see cref="Position{T}"/>.</returns>
    public IEnumerable<Position<T>> GetNeighbours(T distance, params IEnumerable<Direction> directions)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var direction in directions)
            yield return this + (Position<T>)direction * distance;
    }

     /// <summary>
     /// Returns the manhattan distance to another <see cref="Position{T}"/>.
     /// </summary>
     /// <param name="other">The other <see cref="Position{T}"/>.</param>
     /// <returns>The manhattan distance to <paramref name="other"/>.</returns>
     [MethodImpl(MethodImplOptions.AggressiveInlining)]
     public T GetManhattanDistance(Position<T> other)
     {
         return T.Abs(X - other.X) + T.Abs(Y - other.Y);
     }

    /// <summary>
    /// Returns a <see cref="Position{T}"/> rotated 90 degrees clockwise.
    /// </summary>
    /// <returns>The <see cref="Position{T}"/> rotated 90 degrees clockwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Position<T> Rotate90()
    {
        return new Position<T>(Y, -X);
    }

    /// <summary>
    /// Returns a <see cref="Position{T}"/> rotated 180 degrees clockwise.
    /// </summary>
    /// <returns>The <see cref="Position{T}"/> rotated 180 degrees clockwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Position<T> Rotate180()
    {
        return new Position<T>(-X, -Y);
    }

    /// <summary>
    /// Returns a <see cref="Position{T}"/> rotated 270 degrees clockwise.
    /// </summary>
    /// <returns>The <see cref="Position{T}"/> rotated 270 degrees clockwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Position<T> Rotate270()
    {
        return new Position<T>(-Y, X);
    }

    /// <summary>
    /// Returns a <see cref="Position{T}"/> where X and Y are swapped.
    /// </summary>
    /// <returns>The <see cref="Position{T}"/> where X and Y are swapped.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Position<T> Swap()
    {
        return new Position<T>(Y, X);
    }

    /// <summary>
    /// Returns a <see cref="DirectionalPosition{T}"/> created using this <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="direction">The direction.</param>
    /// <returns>The <see cref="DirectionalPosition{T}"/> created using this <see cref="Position{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DirectionalPosition<T> ToDirectional(Position<T> direction)
    {
        return new DirectionalPosition<T>(this, direction);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position<T> operator +(Position<T> left, Position<T> right)
    {
        return new Position<T>(left.X + right.X, left.Y + right.Y);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position<T> operator -(Position<T> left, Position<T> right)
    {
        return new Position<T>(left.X - right.X, left.Y - right.Y);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position<T> operator *(Position<T> left, T right)
    {
        return new Position<T>(left.X * right, left.Y * right);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position<T> operator /(Position<T> left, T right)
    {
        return new Position<T>(left.X / right, left.Y / right);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position<T> operator %(Position<T> left, Position<T> right)
    {
        return new Position<T>((left.X % right.X + right.X) % right.X, (left.Y % right.Y + right.Y) % right.Y);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position<T> operator -(Position<T> value)
    {
        return new Position<T>(-value.X, -value.Y);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Position<T> left, Position<T> right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Position<T> left, Position<T> right)
    {
        return left.CompareTo(right) >= 0;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Position<T> left, Position<T> right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Position<T> left, Position<T> right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Implicitly converts a <see cref="Position{T}"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="position">The <see cref="Position{T}"/> to convert.</param>
    /// <returns>The <see cref="Vector2"/> converted.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector2(Position<T> position)
    {
        return new Vector2(float.CreateSaturating(position.X), float.CreateSaturating(position.Y));
    }

    /// <summary>
    /// Implicitly converts a <see cref="Tuple"/> to a <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="tuple">The <see cref="Tuple"/> to convert.</param>
    /// <returns>The converted <see cref="Position{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Position<T>((T, T) tuple)
    {
        return new Position<T>(tuple.Item1, tuple.Item2);
    }

    /// <summary>
    /// Implicitly converts a <see cref="Direction"/> to a <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="direction">The <see cref="Direction"/> to convert.</param>
    /// <returns>The converted <see cref="Position{T}"/>.</returns>
    public static implicit operator Position<T>(Direction direction)
    {
        return direction switch
        {
            Direction.Down => new Position<T>(T.Zero, T.One),
            Direction.Left => new Position<T>(-T.One, T.Zero),
            Direction.Right => new Position<T>(T.One, T.Zero),
            Direction.TopLeft => new Position<T>(X: -T.One, Y: T.One),
            Direction.TopRight => new Position<T>(X: T.One, Y: T.One),
            Direction.BottomLeft => new Position<T>(X: -T.One, Y: -T.One),
            Direction.BottomRight => new Position<T>(X: T.One, Y: -T.One),
            _ => new Position<T>(T.Zero, -T.One),
        };
    }
}
