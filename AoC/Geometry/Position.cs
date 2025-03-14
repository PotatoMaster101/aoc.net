using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using AoC.Extensions;
using AoC.Helpers;

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
      IAdditionOperators<Position<T>, T, Position<T>>,
      IComparable<Position<T>>,
      IComparisonOperators<Position<T>, Position<T>, bool>,
      IDivisionOperators<Position<T>, T, Position<T>>,
      IModulusOperators<Position<T>, Position<T>, Position<T>>,
      IModulusOperators<Position<T>, T, Position<T>>,
      IMultiplyOperators<Position<T>, T, Position<T>>,
      ISubtractionOperators<Position<T>, Position<T>, Position<T>>,
      ISubtractionOperators<Position<T>, T, Position<T>>,
      IUnaryNegationOperators<Position<T>, Position<T>>
    where T : INumber<T>
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
    public Position(T? xy = default)
        : this(xy ?? T.Zero, xy ?? T.Zero) { }

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
    /// <param name="directions">The neighbouring directions. If <c>null</c>, use cross.</param>
    /// <returns>The collection of neighboring <see cref="Position{T}"/>.</returns>
    public IEnumerable<Position<T>> GetNeighbours(T? distance = default, IEnumerable<Direction>? directions = null)
    {
        var offset = distance is null || distance <= T.Zero ? T.One : distance;
        var source = directions ?? DirectionHelper.GetCross();
        foreach (var direction in source)
            yield return this + (Position<T>)direction * offset;
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
    /// Returns the squared Euclidean distance to another <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="other">The other <see cref="Position{T}"/>.</param>
    /// <returns>The squared Euclidean distance to <paramref name="other"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetSquaredEuclideanDistance(Position<T> other)
    {
        return (X - other.X).Square() + (Y - other.Y).Square();
    }

    /// <summary>
    /// Returns a <see cref="Position{T}"/> rotated 90 degrees clockwise a specific number of times.
    /// </summary>
    /// <param name="turns">The number of 90 degrees rotations.</param>
    /// <returns>The <see cref="Position{T}"/> after rotating 90 degrees a specific number of times.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Position<T> Rotate(int turns)
    {
        return turns.SafeMod(4) switch
        {
            1 => new Position<T>(Y, -X),
            2 => new Position<T>(-X, -Y),
            3 => new Position<T>(-Y, X),
            _ => this,
        };
    }

    /// <summary>
    /// Returns a <see cref="Position{T}"/> rotated 90 degrees clockwise.
    /// </summary>
    /// <returns>The <see cref="Position{T}"/> rotated 90 degrees clockwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Position<T> Rotate90()
    {
        return Rotate(1);
    }

    /// <summary>
    /// Returns a <see cref="Position{T}"/> rotated 180 degrees clockwise.
    /// </summary>
    /// <returns>The <see cref="Position{T}"/> rotated 180 degrees clockwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Position<T> Rotate180()
    {
        return Rotate(2);
    }

    /// <summary>
    /// Returns a <see cref="Position{T}"/> rotated 270 degrees clockwise.
    /// </summary>
    /// <returns>The <see cref="Position{T}"/> rotated 270 degrees clockwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Position<T> Rotate270()
    {
        return Rotate(3);
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
    /// Returns an index from this <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="width">The container width.</param>
    /// <typeparam name="T">The index type.</typeparam>
    /// <returns>The index from this <see cref="Position{T}"/>.</returns>
    public T ToIndex(T width)
    {
        return X + Y * width;
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
    public static Position<T> operator +(Position<T> left, T right)
    {
        return new Position<T>(left.X + right, left.Y + right);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position<T> operator -(Position<T> left, Position<T> right)
    {
        return new Position<T>(left.X - right.X, left.Y - right.Y);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position<T> operator -(Position<T> left, T right)
    {
        return new Position<T>(left.X - right, left.Y - right);
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
        return new Position<T>(left.X.SafeMod(right.X), left.Y.SafeMod(right.Y));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position<T> operator %(Position<T> left, T right)
    {
        return new Position<T>(left.X.SafeMod(right), left.Y.SafeMod(right));
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
    /// Implicitly converts a <see cref="Vector2"/> to a <see cref="Position{T}"/>.
    /// </summary>
    /// <param name="vector">The <see cref="Vector2"/> to convert.</param>
    /// <returns>The converted <see cref="Position{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Position<T>(Vector2 vector)
    {
        return new Position<T>(T.CreateSaturating(vector.X), T.CreateSaturating(vector.Y));
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
            Direction.TopLeft => new Position<T>(-T.One, T.One),
            Direction.TopRight => new Position<T>(T.One, T.One),
            Direction.BottomLeft => new Position<T>(-T.One, -T.One),
            Direction.BottomRight => new Position<T>(T.One, -T.One),
            _ => new Position<T>(T.Zero, -T.One),
        };
    }
}
