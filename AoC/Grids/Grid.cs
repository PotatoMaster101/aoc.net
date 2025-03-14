using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using AoC.DebugProxies;
using AoC.Geometry;

namespace AoC.Grids;

/// <summary>
/// Represents a 2D grid.
/// </summary>
/// <typeparam name="T">The grid data type.</typeparam>
[DebuggerDisplay("{Width} x {Height}")]
[DebuggerTypeProxy(typeof(GridDebugView<>))]
public class Grid<T> : IEnumerable<GridEntry<T>>
    where T : struct
{
    private readonly List<T> _data;

    /// <summary>
    /// Gets the grid width.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the grid height.
    /// </summary>
    public int Height => _data.Count / Width;

    /// <summary>
    /// Gets the grid data.
    /// </summary>
    public IReadOnlyList<T> Data => _data;

    /// <summary>
    /// Gets the grid area.
    /// </summary>
    public Area<int> Area { get; }      // cache for performance

    /// <summary>
    /// Constructs a new instance of <see cref="Grid{T}"/>.
    /// </summary>
    /// <param name="grid">The grid data.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="grid"/> is empty.</exception>
    public Grid(IReadOnlyList<IReadOnlyList<T>> grid)
    {
        ArgumentOutOfRangeException.ThrowIfZero(grid.Count);
        ArgumentOutOfRangeException.ThrowIfZero(grid[0].Count);

        _data = new List<T>(grid.Count * grid[0].Count);
        Width = grid[0].Count;
        Area = new Area<int>(Position<int>.Origin, new Position<int>(Width - 1, Height - 1));
        foreach (var row in grid)
            _data.AddRange(row);
    }

    /// <summary>
    /// Constructs a new instance of <see cref="Grid{T}"/>.
    /// </summary>
    /// <param name="data">The grid data.</param>
    /// <param name="width">The grid width.</param>
    /// <param name="defaultValue">The data default value.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="width"/> is 0 or negative.</exception>
    public Grid(IReadOnlyList<T> data, int width, T defaultValue = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);

        _data = new List<T>(data.Count > 0 ? data : Enumerable.Repeat(defaultValue, width));
        Width = width;
        Area = new Area<int>(Position<int>.Origin, new Position<int>(Width - 1, Height - 1));
        var remainder = data.Count % width;
        if (remainder > 0)
            _data.AddRange(Enumerable.Repeat(defaultValue, width - remainder));
    }

    /// <summary>
    /// Gets the item at a specific index.
    /// </summary>
    /// <param name="i">The index of the item.</param>
    public GridEntry<T> this[int i] => new(new Position<int>(i % Width, i / Width), _data[i]);

    /// <summary>
    /// Gets the item at a specific position.
    /// </summary>
    /// <param name="position">The position of the item.</param>
    public GridEntry<T> this[Position<int> position] => new(position, _data[position.ToIndex(Width)]);

    /// <inheritdoc />
    public IEnumerator<GridEntry<T>> GetEnumerator()
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        for (var i = 0; i < _data.Count; i++)
            yield return new GridEntry<T>(new Position<int>(i % Width, i / Width), _data[i]);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Returns a row at a specific index.
    /// </summary>
    /// <param name="row">The row index.</param>
    /// <param name="distance">The distance between each position.</param>
    /// <returns>The row at index.</returns>
    public IEnumerable<GridEntry<T>> GetRow(int row, int distance = 1)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var position in Area.GetRow(row, distance))
            yield return this[position];
    }

    /// <summary>
    /// Returns a column at a specific index.
    /// </summary>
    /// <param name="column">The column index.</param>
    /// <param name="distance">The distance between each position.</param>
    /// <returns>The column at index.</returns>
    public IEnumerable<GridEntry<T>> GetColumn(int column, int distance = 1)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var position in Area.GetColumn(column, distance))
            yield return this[position];
    }

    /// <summary>
    /// Returns a list of neighbouring <see cref="GridEntry{T}"/> that is inside this <see cref="Grid{T}"/>.
    /// </summary>
    /// <param name="position">The starting <see cref="Position{T}"/> to retrieve the neighbours.</param>
    /// <param name="distance">The distance to the neighbours.</param>
    /// <param name="directions">The directions to the neighbours. If <c>null</c>, use cross.</param>
    /// <param name="wrap">Whether to wrap out-of-range neighbours so they are included.</param>
    /// <returns>The list of neighbouring <see cref="GridEntry{T}"/> that is inside this <see cref="Grid{T}"/>.</returns>
    public IEnumerable<GridEntry<T>> GetNeighbours(Position<int> position, int distance = 1, IEnumerable<Direction>? directions = null, bool wrap = false)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var pos in Area.GetNeighbours(position, distance, directions, wrap))
            yield return this[pos];
    }

    /// <summary>
    /// Gets the first occurrence of an item.
    /// </summary>
    /// <param name="value">The value of the item.</param>
    /// <returns>The first occurrence of an item, or <see langword="null"/> if not found.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GridEntry<T>? Find(T value)
    {
        var i = _data.IndexOf(value);
        return i < 0 ? null : new GridEntry<T>(new Position<int>(i % Width, i / Width), value);
    }

    /// <summary>
    /// Gets all the occurrences of an item.
    /// </summary>
    /// <param name="value">The value of the item.</param>
    /// <returns>All the occurrences of the item.</returns>
    public IEnumerable<GridEntry<T>> FindAll(T value)
    {
        for (var i = 0; i < _data.Count; i++)
            if (_data[i].Equals(value))
                yield return new GridEntry<T>(new Position<int>(i % Width, i / Width), value);
    }

    /// <summary>
    /// Returns multiple items specified by a list of positions. Out-of-range positions are ignored.
    /// </summary>
    /// <param name="positions">The positions of the items.</param>
    /// <param name="wrap">Whether to wrap out-of-range positions so they are included.</param>
    /// <returns>Multiple items specified by a list of positions.</returns>
    public IEnumerable<GridEntry<T>> Extract(IEnumerable<Position<int>> positions, bool wrap = false)
    {
        var source = wrap ? Area.Wrap(positions) : Area.Filter(positions);
        foreach (var position in source)
            yield return this[position];
    }

    /// <inheritdoc />
    public override string ToString()
    {
        var sb = new StringBuilder(_data.Count + Height);
        for (var i = 0; i < _data.Count; i++)
        {
            sb.Append(_data[i]);
            if ((i + 1) % Width == 0 && i < _data.Count - 1)
                sb.AppendLine();
        }
        return sb.ToString();
    }
}
