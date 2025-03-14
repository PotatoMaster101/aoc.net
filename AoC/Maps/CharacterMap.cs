using System.Diagnostics;
using System.Runtime.CompilerServices;
using AoC.Geometry;
using AoC.Grids;

namespace AoC.Maps;

/// <summary>
/// Represents a map of <see cref="char"/>.
/// </summary>
[DebuggerDisplay("{Grid}")]
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class CharacterMap : Map<char>
{
    /// <summary>
    /// Constructs a new instance of <see cref="CharacterMap"/>.
    /// </summary>
    /// <param name="grid">The map data.</param>
    /// <param name="start">The start character.</param>
    /// <param name="end">The end character.</param>
    public CharacterMap(Grid<char> grid, char start = 'S', char end = 'E')
        : base(grid, grid.Find(start)?.Position ?? Position<int>.Origin, grid.Find(end)?.Position ?? Position<int>.Origin) { }

    /// <summary>
    /// Constructs a new instance of <see cref="CharacterMap"/>.
    /// </summary>
    /// <param name="data">The map data.</param>
    /// <param name="start">The start character.</param>
    /// <param name="end">The end character.</param>
    public CharacterMap(IReadOnlyList<string> data, char start = 'S', char end = 'E')
        : this(new Grid<char>(data.SelectMany(x => x).ToList(), data[0].Length), start, end) { }

    /// <summary>
    /// Determines whether a position is a wall tile.
    /// </summary>
    /// <param name="position">The position to check.</param>
    /// <param name="walls">The possible wall tiles.</param>
    /// <returns>Whether <paramref name="position"/> is a wall tile.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual bool IsWall(Position<int> position, IReadOnlySet<char>? walls = null)
    {
        walls ??= new HashSet<char> { '#', '|', '-' };
        return walls.Contains(Grid[position].Value);
    }
}
