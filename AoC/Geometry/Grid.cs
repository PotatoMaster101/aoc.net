using System.Diagnostics;
using System.Runtime.CompilerServices;
using AoC.DebugProxy;

namespace AoC.Geometry;

/// <summary>
/// Represents a 2D grid.
/// </summary>
/// <typeparam name="T">The grid data type.</typeparam>
[DebuggerDisplay("{Width} x {Height}")]
[DebuggerTypeProxy(typeof(GridDebugView<>))]
public class Grid<T>
    where T: struct
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
    public Area<int> Area { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Grid{T}"/>.
    /// </summary>
    /// <param name="grid"></param>
    public Grid(IReadOnlyList<IReadOnlyList<T>> grid)
    {
        ArgumentOutOfRangeException.ThrowIfZero(grid.Count);
        ArgumentOutOfRangeException.ThrowIfZero(grid[0].Count);

        _data = new List<T>(grid.Count * grid[0].Count);
        _data.AddRange(grid.SelectMany(x => x));
        Area = new Area<int>(Position<int>.Origin, new Position<int>(grid.Count - 1, grid[0].Count - 1));
        Width = grid[0].Count;
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
        var remainder = data.Count % width;
        if (remainder > 0)
            _data.AddRange(Enumerable.Repeat(defaultValue, width - remainder));

        Width = width;
        Area = new Area<int>(new Position<int>(), new Position<int>(Width - 1, Height - 1));
    }

    /// <summary>
    /// Gets the item at a specific position.
    /// </summary>
    /// <param name="position">The position of the item.</param>
    public GridEntry<T> this[Position<int> position] => new(position, _data[Width * position.Y + position.X]);

    /// <summary>
    /// Returns a row at a specific index.
    /// </summary>
    /// <param name="row">The row index.</param>
    /// <returns>The row at index.</returns>
    public IEnumerable<GridEntry<T>> GetRow(int row)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var position in Area.GetRow(row, 1))
            yield return this[position];
    }

    /// <summary>
    /// Returns a column at a specific index.
    /// </summary>
    /// <param name="column">The column index.</param>
    /// <returns>The column at index.</returns>
    public IEnumerable<GridEntry<T>> GetColumn(int column)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var position in Area.GetColumn(column, 1))
            yield return this[position];
    }

    /// <summary>
    /// Gets the first occurrence of an item.
    /// </summary>
    /// <param name="value">The value of the item.</param>
    /// <returns>The first occurrence of an item, or <see langword="null"/> if not found.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Position<int>? Find(T value)
    {
        var i = _data.IndexOf(value);
        return i is -1 ? null : new Position<int>(i % Width, i / Width);
    }

    /// <summary>
    /// Gets all the occurrences of an item.
    /// </summary>
    /// <param name="value">The value of the item.</param>
    /// <returns>All the occurrences of the item.</returns>
    public IEnumerable<Position<int>> FindAll(T value)
    {
        for (var i = 0; i < _data.Count; i++)
            if (_data[i].Equals(value))
                yield return new Position<int>(i % Width, i / Width);
    }

    /// <summary>
    /// Returns multiple items specified by a list of positions.
    /// </summary>
    /// <param name="positions">The positions of the items.</param>
    /// <returns>Multiple items specified by a list of positions.</returns>
    public IEnumerable<GridEntry<T>> Extract(params IEnumerable<Position<int>> positions)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var position in Area.FilterPositions(positions))
            yield return this[position];
    }

    /// <summary>
    /// Returns multiple items specified by a list of positions.
    /// Out of range positions will be wrapped.
    /// </summary>
    /// <param name="positions">The positions of the items.</param>
    /// <returns>Multiple items specified by a list of positions.</returns>
    public IEnumerable<GridEntry<T>> WrapExtract(params IEnumerable<Position<int>> positions)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var position in positions)
            yield return this[Area.Wrap(position)];
    }

    /// <summary>
    /// Returns a list of neighbouring <see cref="GridEntry{T}"/> that is inside this <see cref="Grid{T}"/>.
    /// </summary>
    /// <param name="position">The starting <see cref="Position{T}"/> to retrieve the neighbours.</param>
    /// <param name="distance">The distance to the neighbours.</param>
    /// <param name="directions">The directions to the neighbours.</param>
    /// <returns>The list of neighbouring <see cref="GridEntry{T}"/> that is inside this <see cref="Grid{T}"/>.</returns>
    public IEnumerable<GridEntry<T>> GetNeighbours(Position<int> position, int distance, params IEnumerable<Direction> directions)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var pos in Area.GetNeighbours(position, distance, directions))
            yield return this[pos];
    }

    /// <summary>
    /// Performs breadth first search.
    /// </summary>
    /// <param name="start">The start position.</param>
    /// <param name="end">The end position.</param>
    /// <param name="neighbourFunction">The function for returning neighbour positions.</param>
    /// <returns>The collection of positions visited.</returns>
    public IEnumerable<GridEntry<T>> BreadthFirstSearch(
        Position<int> start,
        Position<int> end,
        Func<Position<int>, IEnumerable<Position<int>>> neighbourFunction)
    {
        if (!Area.HasPosition(start))
            yield break;

        var visited = new HashSet<Position<int>>();
        var queue = new Queue<Position<int>>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!visited.Add(current))
                continue;

            yield return this[current];
            if (current == end)
                yield break;
            foreach (var neighbour in Area.FilterPositions(neighbourFunction(current)))
                queue.Enqueue(neighbour);
        }
    }

    /// <summary>
    /// Performs depth first search.
    /// </summary>
    /// <param name="start">The start position.</param>
    /// <param name="end">The end position.</param>
    /// <param name="neighbourFunction">The function for returning neighbour positions.</param>
    /// <returns>The collection of positions visited.</returns>
    public IEnumerable<GridEntry<T>> DepthFirstSearch(
        Position<int> start,
        Position<int> end,
        Func<Position<int>, IEnumerable<Position<int>>> neighbourFunction)
    {
        if (!Area.HasPosition(start))
            yield break;

        var visited = new HashSet<Position<int>>();
        var stack = new Stack<Position<int>>();
        stack.Push(start);
        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (!visited.Add(current))
                continue;

            yield return this[current];
            if (current == end)
                yield break;
            foreach (var neighbour in Area.FilterPositions(neighbourFunction(current)))
                stack.Push(neighbour);
        }
    }
}
