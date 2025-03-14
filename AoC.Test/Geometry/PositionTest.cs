using AoC.Geometry;

namespace AoC.Test.Geometry;

/// <summary>
/// Unit tests for <see cref="Position{T}"/>.
/// </summary>
public class PositionTest
{
    [Fact]
    public void Origin_GetsCorrectValue()
    {
        // act
        var result = Position<int>.Origin;

        // assert
        Assert.Equal(new Position<int>(), result);
    }

    [Fact]
    public void UnitX_GetsCorrectValue()
    {
        // act
        var result = Position<int>.UnitX;

        // assert
        Assert.Equal(new Position<int>(1, 0), result);
    }

    [Fact]
    public void UnitY_GetsCorrectValue()
    {
        // act
        var result = Position<int>.UnitY;

        // assert
        Assert.Equal(new Position<int>(0, 1), result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(-9)]
    public void XyConstructor_ReturnsCorrectValue(int xy)
    {
        // act
        var result = new Position<int>(xy);

        // arrange
        Assert.Equal(xy, result.X);
        Assert.Equal(xy, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, true)]
    [InlineData(0, 0, 0, 5, true)]
    [InlineData(3, 4, 7, 4, true)]
    [InlineData(1, 1, 4, 4, true)]
    [InlineData(-1, -1, -4, -4, true)]
    [InlineData(2, 3, 5, 7, false)]
    [InlineData(0, 0, 1, 2, false)]
    public void AlignedWith_ReturnsCorrectValue(int x1, int y1, int x2, int y2, bool expected)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut.AlignedWith(new Position<int>(x2, y2));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 1, 1, -1)]
    [InlineData(1, 1, 0, 0, 1)]
    [InlineData(-1, -1, 1, 1, -1)]
    [InlineData(-1, 3, 4, -21, -1)]
    [InlineData(1, 1, 1, 1, 0)]
    public void CompareTo_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut.CompareTo(new Position<int>(x2, y2));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(5, 5, 0, 0, 10, 10, 5, 5)]
    [InlineData(-1, 5, 0, 0, 10, 10, 0, 5)]
    [InlineData(15, 5, 0, 0, 10, 10, 10, 5)]
    [InlineData(5, -2, 0, 0, 10, 10, 5, 0)]
    [InlineData(5, 20, 0, 0, 10, 10, 5, 10)]
    [InlineData(-5, -5, 0, 0, 10, 10, 0, 0)]
    [InlineData(20, 20, 0, 0, 10, 10, 10, 10)]
    [InlineData(0, 10, 0, 0, 10, 10, 0, 10)]
    public void Clamp_ReturnsCorrectValue(int x1, int y1, int minX, int minY, int maxX, int maxY, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut.Clamp(new Position<int>(minX, minY), new Position<int>(maxX, maxY));

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(1, 0, 0, 1, 1)]
    [InlineData(0, 1, 1, 0, -1)]
    [InlineData(1, 0, 1, 0, 0)]
    [InlineData(0, 0, 1, 1, 0)]
    [InlineData(1, 1, 0, 0, 0)]
    [InlineData(2, 3, 4, 5, -2)]
    public void Cross_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut.Cross(new Position<int>(x2, y2));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(0, 1, 1)]
    [InlineData(1, 1, 2)]
    [InlineData(3, 4, 25)]
    [InlineData(-1, -1, 2)]
    [InlineData(-3, -4, 25)]
    [InlineData(5, 12, 169)]
    [InlineData(7, -24, 625)]
    public void GetDistanceSquared_ReturnsCorrectValue(int x, int y, int expected)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.GetDistanceSquared();

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 1, 1, 2)]
    [InlineData(-1, 1, 1, -1, 8)]
    [InlineData(-1, 1, -1, 1, 0)]
    [InlineData(-3, 5, -7, 9, 32)]
    public void GetEuclideanDistanceSquared_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut.GetEuclideanDistanceSquared(new Position<int>(x2, y2));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 1, 1, 2)]
    [InlineData(-1, 1, 1, -1, 4)]
    [InlineData(-1, 1, -1, 1, 0)]
    [InlineData(-3, 5, -7, 9, 8)]
    public void GetManhattanDistance_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut.GetManhattanDistance(new Position<int>(x2, y2));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(GetNeighboursTestData))]
    public void GetNeighbours_ReturnsCorrectValue(int x, int y, int distance, Direction[] directions, Position<int>[] expected)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.GetNeighbours(distance, directions).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [InlineData(0, 0, 0, 1, 1, true)]
    [InlineData(0, 0, 1, 0, 1, true)]
    [InlineData(0, 0, 1, 1, 1, true)]
    [InlineData(0, 0, -1, -1, 1, true)]
    [InlineData(0, 0, -1, -1, -1, true)]
    [InlineData(0, 0, 0, 0, 1, true)]
    [InlineData(0, 0, 2, 2, 1, false)]
    [InlineData(0, 0, 2, 2, 2, true)]
    [InlineData(0, 0, 0, 3, 2, false)]
    [InlineData(0, 0, 2, 1, 2, true)]
    [InlineData(0, 0, 3, 3, 3, true)]
    [InlineData(0, 0, -3, -3, 3, true)]
    [InlineData(0, 0, 4, 0, 3, false)]
    [InlineData(0, 0, 0, 4, 3, false)]
    [InlineData(0, 0, 1, 0, 0, false)]
    public void InRange_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int range, bool expected)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut.InRange(new Position<int>(x2, y2), range);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(-1, -1)]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    public void MirrorX_ReturnsCorrectValue(int x, int y)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.MirrorX();

        // assert
        Assert.Equal(x, result.X);
        Assert.Equal(-y, result.Y);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(-1, -1)]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    public void MirrorY_ReturnsCorrectValue(int x, int y)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.MirrorY();

        // assert
        Assert.Equal(-x, result.X);
        Assert.Equal(y, result.Y);
    }

    [Theory]
    [InlineData(1, 1, 0, 1, 1)]
    [InlineData(1, 1, 1, 1, -1)]
    [InlineData(1, 1, 2, -1, -1)]
    [InlineData(1, 1, 3, -1, 1)]
    [InlineData(1, 1, 4, 1, 1)]
    [InlineData(1, 1, 5, 1, -1)]
    [InlineData(1, 1, -1, -1, 1)]
    [InlineData(1, 1, -2, -1, -1)]
    [InlineData(1, 1, -3, 1, -1)]
    [InlineData(1, 1, -4, 1, 1)]
    public void Rotate_ReturnsCorrectValue(int x, int y, int turns, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.Rotate(turns);

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(1, 3, 3, -1)]
    [InlineData(5, -9, -9, -5)]
    [InlineData(-5, 9, 9, 5)]
    [InlineData(-5, -9, -9, 5)]
    public void Rotate90_ReturnsCorrectValue(int x, int y, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.Rotate90();

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(1, 3, -1, -3)]
    [InlineData(5, -9, -5, 9)]
    [InlineData(-5, 9, 5, -9)]
    [InlineData(-5, -9, 5, 9)]
    public void Rotate180_ReturnsCorrectValue(int x, int y, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.Rotate180();

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(1, 3, -3, 1)]
    [InlineData(5, -9, 9, 5)]
    [InlineData(-5, 9, -9, -5)]
    [InlineData(-5, -9, 9, -5)]
    public void Rotate270_ReturnsCorrectValue(int x, int y, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.Rotate270();

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(-1, 3, 3, -1)]
    [InlineData(9, -4, -4, 9)]
    public void Swap_ReturnsCorrectValue(int x, int y, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.Swap();

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 10, 0)]
    [InlineData(3, 0, 10, 3)]
    [InlineData(0, 3, 10, 30)]
    [InlineData(2, 4, 5, 22)]
    public void ToIndex_ReturnsCorrectValue(int x, int y, int width, int expected)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.ToIndex(width);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, "(0, 0)")]
    [InlineData(-4, 9, "(-4, 9)")]
    public void ToString_ReturnsCorrectValue(int x, int y, string expected)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.ToString();

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 1, 1, 1, 1)]
    [InlineData(0, 0, -1, -1, -1, -1)]
    [InlineData(-3, 9, 8, -4, 5, 5)]
    public void Add_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut + new Position<int>(x2, y2);

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 1, 1, 1)]
    [InlineData(0, 0, -1, -1, -1)]
    [InlineData(-3, 9, 8, 5, 17)]
    public void AddNumber_ReturnsCorrectValue(int x1, int y1, int right, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut + right;

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 1, 1, -1, -1)]
    [InlineData(0, 0, -1, -1, 1, 1)]
    [InlineData(-3, 9, 8, -4, -11, 13)]
    public void Subtract_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut - new Position<int>(x2, y2);

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 1, -1, -1)]
    [InlineData(0, 0, -1, 1, 1)]
    [InlineData(-3, 9, 8, -11, 1)]
    public void SubtractNumber_ReturnsCorrectValue(int x1, int y1, int right, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut - right;

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 1, 1, 0)]
    [InlineData(1, 0, 1, 0, 1)]
    [InlineData(0, 1, 0, 1, 1)]
    [InlineData(1, 0, 0, 1, 0)]
    [InlineData(2, 3, 4, 5, 23)]
    [InlineData(1, 2, -1, -2, -5)]
    public void Multiply_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut * new Position<int>(x2, y2);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, 1, 3, 3, 3)]
    [InlineData(-5, 4, -3, 15, -12)]
    public void MultiplyNumber_ReturnsCorrectValue(int x, int y, int value, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut * value;

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(3, 3, 1, 3, 3)]
    [InlineData(-10, 5, 5, -2, 1)]
    public void DivideNumber_ReturnsCorrectValue(int x, int y, int value, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut / value;

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(10, 10, 10, 10, 0, 0)]
    [InlineData(10, 10, 3, 5, 1, 0)]
    [InlineData(3, 5, 3, 5, 0, 0)]
    [InlineData(-1, -1, 10, 15, 9, 14)]
    public void Mod_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut % new Position<int>(x2, y2);

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(10, 10, 10, 0, 0)]
    [InlineData(10, 10, 11, 10, 10)]
    [InlineData(10, 10, 15, 10, 10)]
    public void ModNumber_ReturnsCorrectValue(int x1, int y1, int right, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut % right;

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(1, -1, -1, 1)]
    public void Negate_ReturnsCorrectValue(int x, int y, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = -sut;

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, false)]
    [InlineData(0, 0, 1, 0, false)]
    [InlineData(0, 0, 0, 1, false)]
    [InlineData(1, 0, 0, 0, true)]
    [InlineData(0, 1, 0, 0, true)]
    public void GreaterThan_ReturnsCorrectValue(int x1, int y1, int x2, int y2, bool expected)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut > new Position<int>(x2, y2);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, true)]
    [InlineData(0, 0, 1, 0, false)]
    [InlineData(0, 0, 0, 1, false)]
    [InlineData(1, 0, 0, 0, true)]
    [InlineData(0, 1, 0, 0, true)]
    public void GreaterThanOrEqualTo_ReturnsCorrectValue(int x1, int y1, int x2, int y2, bool expected)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut >= new Position<int>(x2, y2);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, false)]
    [InlineData(0, 0, 1, 0, true)]
    [InlineData(0, 0, 0, 1, true)]
    [InlineData(1, 0, 0, 0, false)]
    [InlineData(0, 1, 0, 0, false)]
    public void LessThan_ReturnsCorrectValue(int x1, int y1, int x2, int y2, bool expected)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut < new Position<int>(x2, y2);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, true)]
    [InlineData(0, 0, 1, 0, true)]
    [InlineData(0, 0, 0, 1, true)]
    [InlineData(1, 0, 0, 0, false)]
    [InlineData(0, 1, 0, 0, false)]
    public void LessThanOrEqualTo_ReturnsCorrectValue(int x1, int y1, int x2, int y2, bool expected)
    {
        // arrange
        var sut = new Position<int>(x1, y1);

        // act
        var result = sut <= new Position<int>(x2, y2);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-3, 9)]
    public void FromTupleConversion_ReturnsCorrectValue(int x, int y)
    {
        // arrange
        var sut = (x, y);

        // act
        var result = (Position<int>)sut;

        // assert
        Assert.Equal(x, result.X);
        Assert.Equal(y, result.Y);
    }

    [Theory]
    [InlineData(Direction.Up, 0, -1)]
    [InlineData(Direction.Down, 0, 1)]
    [InlineData(Direction.Left, -1, 0)]
    [InlineData(Direction.Right, 1, 0)]
    [InlineData(Direction.TopLeft, -1, 1)]
    [InlineData(Direction.TopRight, 1, 1)]
    [InlineData(Direction.BottomLeft, -1, -1)]
    [InlineData(Direction.BottomRight, 1, -1)]
    [InlineData((Direction)999, 0, -1)]
    public void FromDirectionConversion_ReturnsCorrectValue(Direction direction, int expectedX, int expectedY)
    {
        // act
        var result = (Position<int>)direction;

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    /// <summary>
    /// Test data for <see cref="PositionTest.GetNeighbours_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetNeighboursTestData : TheoryData<int, int, int, Direction[], Position<int>[]>
    {
        public GetNeighboursTestData()
        {
            Add(0, 0, 1, [Direction.Up, Direction.Down, Direction.Left, Direction.Right],
                [new Position<int>(0, -1), new Position<int>(0, 1), new Position<int>(-1, 0), new Position<int>(1, 0)]);
            Add(0, 0, 3, [Direction.Up, Direction.Down, Direction.Left, Direction.Right],
                [new Position<int>(0, -3), new Position<int>(0, 3), new Position<int>(-3, 0), new Position<int>(3, 0)]);
            Add(2, 2, 5, [Direction.TopLeft, Direction.TopRight, Direction.BottomLeft, Direction.BottomRight],
                [new Position<int>(-3, 7), new Position<int>(7, 7), new Position<int>(-3, -3), new Position<int>(7, -3)]);
            Add(-1, 3, 2, [Direction.Up, Direction.TopRight, Direction.Up, Direction.Down],
                [new Position<int>(-1, 1), new Position<int>(1, 5), new Position<int>(-1, 1), new Position<int>(-1, 5)]);
            Add(0, 0, 3, [Direction.Up, Direction.Up, Direction.Down], [new Position<int>(0, -3), new Position<int>(0, -3), new Position<int>(0, 3)]);
            Add(0, 0, 1, [], []);
            Add(0, 0, 1, null!, [new Position<int>(0, -1), new Position<int>(0, 1), new Position<int>(-1, 0), new Position<int>(1, 0)]);
            Add(0, 0, 0, null!, [new Position<int>(0, -1), new Position<int>(0, 1), new Position<int>(-1, 0), new Position<int>(1, 0)]);
            Add(0, 0, -1, null!, [new Position<int>(0, -1), new Position<int>(0, 1), new Position<int>(-1, 0), new Position<int>(1, 0)]);
        }
    }
}
