using System.Runtime.CompilerServices;
using AoC.Geometry;

namespace AoC.Helpers;

/// <summary>
/// Helper methods for <see cref="Direction"/>.
/// </summary>
public static class DirectionHelper
{
    /// <summary>
    /// Returns all the directions.
    /// </summary>
    /// <returns>All the directions.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction[] GetAll()
    {
        return
        [
            Direction.Up, Direction.Down, Direction.Left, Direction.Right, Direction.TopLeft, Direction.TopRight,
            Direction.BottomLeft, Direction.BottomRight
        ];
    }

    /// <summary>
    /// Returns the directions in + shape.
    /// </summary>
    /// <returns>The directions in + shape.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction[] GetCross()
    {
        return [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
    }

    /// <summary>
    /// Returns the directions in X shape.
    /// </summary>
    /// <returns>The directions in X shape.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction[] GetDiagonal()
    {
        return [Direction.TopLeft, Direction.TopRight, Direction.BottomLeft, Direction.BottomRight];
    }
}
