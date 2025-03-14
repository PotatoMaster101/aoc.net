using System.Collections;
using System.Numerics;
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
    [InlineData(0, 0)]
    [InlineData(-3, 9)]
    [InlineData(3, -9)]
    [InlineData(-3, -9)]
    public void VectorConstructor_ReturnsCorrectValue(float x, float y)
    {
        // arrange
        var sut = new Vector2(x, y);

        // act
        var result = new Position<float>(sut);

        // assert
        Assert.Equal(x, result.X);
        Assert.Equal(y, result.Y);
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
    [InlineData(0, 0, 1, 0)]
    [InlineData(0, 0, -1, 1)]
    [InlineData(4, -7, 1, -1)]
    [InlineData(0, 0, 0, 0)]
    public void ToDirectional_ReturnsCorrectValue(int x, int y, int dirX, int dirY)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = sut.ToDirectional(new Position<int>(dirX, dirY));

        // assert
        Assert.Equal(x, result.Position.X);
        Assert.Equal(y, result.Position.Y);
        Assert.Equal(dirX, result.Direction.X);
        Assert.Equal(dirY, result.Direction.Y);
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
    [InlineData(1, 1, 3, 3, 3)]
    [InlineData(-5, 4, -3, 15, -12)]
    public void Multiply_ReturnsCorrectValue(int x, int y, int value, int expectedX, int expectedY)
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
    public void Divide_ReturnsCorrectValue(int x, int y, int value, int expectedX, int expectedY)
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
    public void Modulus_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expectedX, int expectedY)
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
    public void FromVectorConversion_ReturnsCorrectValue(int x, int y)
    {
        // arrange
        var sut = new Position<int>(x, y);

        // act
        var result = (Vector2)sut;

        // assert
        Assert.Equal(x, result.X);
        Assert.Equal(y, result.Y);
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
    private class GetNeighboursTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                0, 0, 1, new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
                new Position<int>[] { new(0, -1), new(0, 1), new(-1, 0), new(1, 0) }
            ];
            yield return
            [
                0, 0, 3, new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
                new Position<int>[] { new(0, -3), new(0, 3), new(-3, 0), new(3, 0) }
            ];
            yield return
            [
                0, 0, 3, new[] { Direction.Up, Direction.Up, Direction.Down },
                new Position<int>[] { new(0, -3), new(0, -3), new(0, 3) }
            ];
            yield return
            [
                2, 2, 5, new[] { Direction.TopLeft, Direction.TopRight, Direction.BottomLeft, Direction.BottomRight },
                new Position<int>[] { new(-3, 7), new(7, 7), new(-3, -3), new(7, -3) }
            ];
            yield return
            [
                -1, 3, 2, new[] { Direction.Up, Direction.TopRight, Direction.Up, Direction.Down },
                new Position<int>[] { new(-1, 1), new(1, 5), new(-1, 1), new(-1, 5) }
            ];
            yield return
            [
                2, 2, 5, Array.Empty<Direction>(), Array.Empty<Position<int>>()
            ];
        }
    }
}
