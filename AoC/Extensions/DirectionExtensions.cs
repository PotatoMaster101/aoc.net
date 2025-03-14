using System.Runtime.CompilerServices;
using AoC.Geometry;

namespace AoC.Extensions;

/// <summary>
/// Extension methods for <see cref="Direction"/>.
/// </summary>
public static class DirectionExtensions
{
    /// <summary>
    /// Returns the <see cref="Direction"/> specified by a character.
    /// </summary>
    /// <param name="c">The direction character.</param>
    /// <returns>The <see cref="Direction"/> specified by <paramref name="c"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction ToDirection(this char c)
    {
        return c switch
        {
            '<' => Direction.Left,
            '>' => Direction.Right,
            '^' => Direction.Up,
            _ => Direction.Down,
        };
    }
}
