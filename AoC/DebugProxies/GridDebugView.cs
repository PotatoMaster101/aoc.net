using System.Diagnostics;
using AoC.Geometry;
using AoC.Grids;

namespace AoC.DebugProxies;

/// <summary>
/// Represents a debugger proxy for displaying <see cref="Grid{T}"/>.
/// </summary>
public class GridDebugView<T>(Grid<T> grid)
    where T : struct
{
    /// <summary>
    /// Gets the grid rows.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public T[][] Rows => GetRows();

    /// <summary>
    /// Gets the grid rows.
    /// </summary>
    private T[][] GetRows()
    {
        var result = new T[grid.Height][];
        for (var y = 0; y < grid.Height; y++)
        {
            var row = new T[grid.Width];
            for (var x = 0; x < grid.Width; x++)
                row[x] = grid[new Position<int>(x, y)].Value;
            result[y] = row;
        }
        return result;
    }
}
