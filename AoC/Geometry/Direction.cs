using System.ComponentModel;

namespace AoC.Geometry;

/// <summary>
/// Represents a direction in a 2D space.
/// </summary>
public enum Direction
{
    /// <summary>
    /// North direction.
    /// </summary>
    [Description("North")]
    Up = 0,

    /// <summary>
    /// South direction.
    /// </summary>
    [Description("South")]
    Down,

    /// <summary>
    /// West direction.
    /// </summary>
    [Description("West")]
    Left,

    /// <summary>
    /// East direction.
    /// </summary>
    [Description("East")]
    Right,

    /// <summary>
    /// North West direction.
    /// </summary>
    [Description("North West")]
    TopLeft,

    /// <summary>
    /// North East direction.
    /// </summary>
    [Description("North East")]
    TopRight,

    /// <summary>
    /// South West direction.
    /// </summary>
    [Description("South West")]
    BottomLeft,

    /// <summary>
    /// South East direction.
    /// </summary>
    [Description("North East")]
    BottomRight,
}
