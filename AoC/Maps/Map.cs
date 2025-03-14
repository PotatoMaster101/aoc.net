using System.Diagnostics;
using AoC.Geometry;
using AoC.Grids;

namespace AoC.Maps;

/// <summary>
/// Represents a 2D map.
/// </summary>
[DebuggerDisplay("{Grid}")]
public class Map<T>
    where T : struct
{
    /// <summary>
    /// Gets the map data.
    /// </summary>
    public Grid<T> Grid { get; }

    /// <summary>
    /// Gets the start position.
    /// </summary>
    public Position<int> Start { get; }

    /// <summary>
    /// Gets the end position.
    /// </summary>
    public Position<int> End { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Map{T}"/>.
    /// </summary>
    /// <param name="grid">The map data.</param>
    /// <param name="start">The start position, origin if out-of-range.</param>
    /// <param name="end">The end position, origin if out-of-range.</param>
    // ReSharper disable once MemberCanBeProtected.Global
    public Map(Grid<T> grid, Position<int> start = default, Position<int> end = default)
    {
        Grid = grid;
        Start = grid.Area.Has(start) ? start : Position<int>.Origin;
        End = grid.Area.Has(end) ? end : Position<int>.Origin;
    }
}
