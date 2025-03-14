using System.ComponentModel;

namespace AoC.Geometry;

/// <summary>
/// Represents a direction in a 2D space.
/// </summary>
public enum Direction
{
    /// <summary>
    /// North (up) direction.
    /// </summary>
    [Description("North")]
    Up = 0,

    /// <summary>
    /// South (down) direction.
    /// </summary>
    [Description("South")]
    Down,

    /// <summary>
    /// West (left) direction.
    /// </summary>
    [Description("West")]
    Left,

    /// <summary>
    /// East (right) direction.
    /// </summary>
    [Description("East")]
    Right,

    /// <summary>
    /// North West (up-left) direction.
    /// </summary>
    [Description("North West")]
    TopLeft,

    /// <summary>
    /// North East (up-right) direction.
    /// </summary>
    [Description("North East")]
    TopRight,

    /// <summary>
    /// South West (down-left) direction.
    /// </summary>
    [Description("South West")]
    BottomLeft,

    /// <summary>
    /// South Right (down-right) direction.
    /// </summary>
    [Description("North East")]
    BottomRight,
}
