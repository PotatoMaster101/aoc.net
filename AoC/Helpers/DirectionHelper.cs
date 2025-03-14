using System.Runtime.CompilerServices;
using AoC.Geometry;

namespace AoC.Helpers;

/// <summary>
/// Helper methods for <see cref="Direction"/>.
/// </summary>
public static class DirectionHelper
{
    /// <summary>
    /// Returns the direction specified by a character.
    /// </summary>
    /// <param name="c">The direction character.</param>
    /// <returns>The direction specified by a character.</returns>
    public static Direction FromChar(char c)
    {
        return c switch
        {
            '<' => Direction.Left,
            '>' => Direction.Right,
            '^' => Direction.Up,
            _ => Direction.Down,
        };
    }

    /// <summary>
    /// Returns the directions in a cross shape (+).
    /// </summary>
    /// <returns>The directions in a cross shape.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction[] GetCross()
    {
        return [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
    }

    /// <summary>
    /// Returns the directions in a diagonal shape (X).
    /// </summary>
    /// <returns>The directions in a cross shape.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction[] GetDiagonal()
    {
        return [Direction.TopLeft, Direction.TopRight, Direction.BottomLeft, Direction.BottomRight];
    }
}
