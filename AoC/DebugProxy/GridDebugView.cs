using System.Diagnostics;
using System.Numerics;
using AoC.Geometry;

namespace AoC.DebugProxy;

/// <summary>
/// Represents a debugger proxy for displaying <see cref="Grid{T}"/>.
/// </summary>
public class GridDebugView<T>(Grid<T> grid)
    where T : struct, IBinaryNumber<T>
{
    /// <summary>
    /// Gets the grid data represented in a 2D collection.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public IReadOnlyList<IReadOnlyList<T>> Data => GetData();

    /// <summary>
    /// Gets the grid data.
    /// </summary>
    private List<List<T>> GetData()
    {
        var result = new List<List<T>>(grid.Height);
        for (var y = 0; y < grid.Height; y++)
        {
            var row = new List<T>(grid.Width);
            for (var x = 0; x < grid.Width; x++)
                row.Add(grid[new Position<int>(x, y)].Value);

            result.Add(row);
        }
        return result;
    }
}
