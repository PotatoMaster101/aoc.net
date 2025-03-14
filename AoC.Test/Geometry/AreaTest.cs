﻿using System.Collections;
using AoC.Geometry;

namespace AoC.Test.Geometry;

/// <summary>
/// Unit tests for <see cref="Area{T}"/>.
/// </summary>
public class AreaTest
{
    [Theory]
    [ClassData(typeof(ConstructorTestData))]
    public void Constructor_ReturnsCorrectValue(Position<int> corner1, Position<int> corner2, Position<int> expectedTopLeft, Position<int> expectedBottomRight)
    {
        // act
        var result = new Area<int>(corner1, corner2);

        // assert
        Assert.Equal(expectedTopLeft, result.TopLeft);
        Assert.Equal(expectedBottomRight, result.BottomRight);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 5, 5)]
    [InlineData(-3, -4, -5, 7, -3, 7)]
    [InlineData(-3, 7, -5, -4, -3, 7)]
    public void TopRight_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.TopRight;

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 0, 0)]
    [InlineData(-3, -4, -5, 7, -5, -4)]
    [InlineData(-3, 7, -5, -4, -5, -4)]
    public void BottomLeft_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.BottomLeft;

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 5)]
    [InlineData(-3, -4, -5, 7, -3)]
    [InlineData(3, 7, -5, -4, 3)]
    public void MaxX_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.MaxX;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 5)]
    [InlineData(-3, -4, -5, 7, 7)]
    [InlineData(3, -7, -5, -4, -4)]
    public void MaxY_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.MaxY;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 0)]
    [InlineData(-3, -4, -5, 7, -5)]
    [InlineData(3, -7, 5, -4, 3)]
    public void MinX_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.MinX;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 0)]
    [InlineData(-3, -4, -5, 7, -4)]
    [InlineData(3, -7, 5, -4, -7)]
    public void MinY_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.MinY;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 6)]
    [InlineData(0, 0, 5, 10, 6)]
    [InlineData(0, 0, 10, 5, 11)]
    public void Width_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.Width;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 6)]
    [InlineData(0, 0, 5, 10, 11)]
    [InlineData(0, 0, 10, 5, 6)]
    public void Height_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.Height;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(GetRowTestData))]
    public void GetRow_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int row, int distance, Position<int>[] expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.GetRow(row, distance).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(GetColumnTestData))]
    public void GetColumn_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int column, int distance, Position<int>[] expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.GetColumn(column, distance).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 0, 0, true)]
    [InlineData(0, 0, 5, 5, 0, 1, true)]
    [InlineData(0, 0, 5, 5, -1, 1, false)]
    [InlineData(0, 0, 5, 5, 5, 5, true)]
    [InlineData(0, 0, 5, 5, 5, 4, true)]
    [InlineData(0, 0, 5, 5, 5, 6, false)]
    [InlineData(0, 0, 5, 5, 2, 3, false)]
    public void OnXBound_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int x3, int y3, bool expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.OnXBound(new Position<int>(x3, y3));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 0, 0, true)]
    [InlineData(0, 0, 5, 5, 1, 0, true)]
    [InlineData(0, 0, 5, 5, -1, 0, false)]
    [InlineData(0, 0, 5, 5, 5, 5, true)]
    [InlineData(0, 0, 5, 5, 4, 5, true)]
    [InlineData(0, 0, 5, 5, -1, 5, false)]
    [InlineData(0, 0, 5, 5, 2, 3, false)]
    public void OnYBound_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int x3, int y3, bool expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.OnYBound(new Position<int>(x3, y3));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 0, 0, true)]
    [InlineData(0, 0, 5, 5, 1, 0, true)]
    [InlineData(0, 0, 5, 5, 0, 1, true)]
    [InlineData(0, 0, 5, 5, 1, 1, false)]
    [InlineData(0, 0, 5, 5, -1, 0, false)]
    [InlineData(0, 0, 5, 5, 5, 5, true)]
    [InlineData(0, 0, 5, 5, 5, 4, true)]
    [InlineData(0, 0, 5, 5, 4, 5, true)]
    [InlineData(0, 0, 5, 5, -1, 5, false)]
    [InlineData(0, 0, 5, 5, 2, 3, false)]
    public void OnBound_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int x3, int y3, bool expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.OnBound(new Position<int>(x3, y3));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 0, 0, true)]
    [InlineData(0, 0, 5, 5, 0, 5, true)]
    [InlineData(0, 0, 5, 5, 5, 0, true)]
    [InlineData(0, 0, 5, 5, 5, 5, true)]
    [InlineData(0, 0, 5, 5, 1, 5, false)]
    [InlineData(0, 0, 5, 5, -1, 5, false)]
    [InlineData(0, 0, 5, 5, 5, 1, false)]
    [InlineData(0, 0, 5, 5, 5, -1, false)]
    [InlineData(0, 0, 5, 5, 2, 3, false)]
    public void OnCorner_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int x3, int y3, bool expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.OnCorner(new Position<int>(x3, y3));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 6, 6, 0, 0, 0, 0)]
    [InlineData(0, 0, 6, 6, 1, 1, 1, 1)]
    [InlineData(0, 0, 6, 6, 2, 2, 2, 2)]
    [InlineData(0, 0, 6, 6, 3, 3, 3, 3)]
    [InlineData(0, 0, 6, 6, 6, 6, 6, 6)]
    [InlineData(0, 0, 6, 6, 3, 7, 3, 0)]
    [InlineData(0, 0, 6, 6, 7, 3, 0, 3)]
    [InlineData(0, 0, 6, 7, 8, 10, 1, 2)]
    [InlineData(0, 0, 6, 6, 2, -3, 2, 4)]
    [InlineData(0, 0, 6, 6, -3, 2, 4, 2)]
    [InlineData(0, 0, 6, 7, -3, -8, 4, 0)]
    [InlineData(0, 0, 6, 6, -1, -1, 6, 6)]
    [InlineData(0, 0, 6, 6, -1, -2, 6, 5)]
    public void Wrap_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int x3, int y3, int expectedX, int expectedY)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.Wrap(new Position<int>(x3, y3));

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 0, 0, true)]
    [InlineData(0, 0, 5, 5, 3, 4, true)]
    [InlineData(0, 0, 5, 5, 5, 5, true)]
    [InlineData(0, 0, 5, 5, -1, -1, false)]
    [InlineData(0, 0, 5, 5, 0, -1, false)]
    [InlineData(0, 0, 5, 5, -1, 0, false)]
    [InlineData(0, 0, 5, 5, 5, 6, false)]
    [InlineData(0, 0, 5, 5, 6, 5, false)]
    [InlineData(0, 0, 5, 5, 6, 6, false)]
    [InlineData(0, 0, 5, 10, 6, 6, false)]
    [InlineData(0, 0, 5, 10, 5, 6, true)]
    public void HasPosition_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int x3, int y3, bool expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.HasPosition(new Position<int>(x3, y3));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(FilterPositionsTestData))]
    public void FilterPositions_ReturnsCorrectValue(Position<int> corner1, Position<int> corner2, Position<int>[] positions, Position<int>[] expected)
    {
        // arrange
        var sut = new Area<int>(corner1, corner2);

        // act
        var result = sut.FilterPositions(positions).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(GetNeighboursTestData))]
    public void GetNeighbours_ReturnsCorrectValue(Position<int> corner1, Position<int> corner2, Position<int> position, int distance, Direction[] directions, Position<int>[] expected)
    {
        // arrange
        var sut = new Area<int>(corner1, corner2);

        // act
        var result = sut.GetNeighbours(position, distance, directions).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    /// <summary>
    /// Test data for <see cref="AreaTest.Constructor_ReturnsCorrectValue"/>.
    /// </summary>
    private class ConstructorTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [new Position<int>(), new Position<int>(5, 5), new Position<int>(0, 5), new Position<int>(5, 0)];
            yield return [new Position<int>(-3, -4), new Position<int>(-5, 7), new Position<int>(-5, 7), new Position<int>(-3, -4)];
            yield return [new Position<int>(-3, 7), new Position<int>(-5, -4), new Position<int>(-5, 7), new Position<int>(-3, -4)];
        }
    }

    /// <summary>
    /// Test data for <see cref="AreaTest.GetRow_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetRowTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [0, 0, 2, 2, 0, 1, new Position<int>[] { new(), new(1, 0), new(2, 0) }];
            yield return [0, 0, 2, 2, 0, 2, new Position<int>[] { new(), new(2, 0) }];
            yield return [0, 0, 2, 2, 0, 3, new Position<int>[] { new() }];
            yield return [0, 0, 2, 2, 1, 1, new Position<int>[] { new(0, 1), new(1, 1), new(2, 1) }];
            yield return [0, 0, 2, 2, 1, 10, new Position<int>[] { new(0, 1) }];
            yield return [0, 0, 2, 2, -1, 1, Array.Empty<Position<int>>()];
            yield return [0, 0, 2, 2, 3, 1, Array.Empty<Position<int>>()];
        }
    }

    /// <summary>
    /// Test data for <see cref="AreaTest.GetColumn_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetColumnTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [0, 0, 2, 2, 0, 1, new Position<int>[] { new(), new(0, 1), new(0, 2) }];
            yield return [0, 0, 2, 2, 0, 2, new Position<int>[] { new(), new(0, 2) }];
            yield return [0, 0, 2, 2, 0, 3, new Position<int>[] { new() }];
            yield return [0, 0, 2, 2, 1, 1, new Position<int>[] { new(1, 0), new(1, 1), new(1, 2) }];
            yield return [0, 0, 2, 2, 1, 10, new Position<int>[] { new(1, 0) }];
            yield return [0, 0, 2, 2, -1, 1, Array.Empty<Position<int>>()];
            yield return [0, 0, 2, 2, 3, 1, Array.Empty<Position<int>>()];
        }
    }

    /// <summary>
    /// Test data for <see cref="AreaTest.FilterPositions_ReturnsCorrectValue"/>.
    /// </summary>
    private class FilterPositionsTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                new Position<int>(), new Position<int>(5, 5), new Position<int>[]
                {
                    new(), new(6, 6), new(1, 3), new(5, 5), new(-1, -1)
                },
                new Position<int>[]
                {
                    new(), new(1, 3), new(5, 5)
                }
            ];
            yield return
            [
                new Position<int>(), new Position<int>(5, 5), new Position<int>[]
                {
                    new(6, 6), new(6, 5), new(5, 6), new(-1, -1), new(-1, 0), new(0, -1)
                },
                Array.Empty<Position<int>>()
            ];
            yield return
            [
                new Position<int>(), new Position<int>(5, 5), Array.Empty<Position<int>>(), Array.Empty<Position<int>>()
            ];
        }
    }

    /// <summary>
    /// Test data for <see cref="AreaTest.GetNeighbours_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetNeighboursTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                new Position<int>(), new Position<int>(5, 5), new Position<int>(0, 0), 1,
                new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
                new Position<int>[] { new(0, 1), new(1, 0) }
            ];
            yield return
            [
                new Position<int>(), new Position<int>(5, 5), new Position<int>(0, 0), 3,
                new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
                new Position<int>[] { new(0, 3), new(3, 0) }
            ];
            yield return
            [
                new Position<int>(), new Position<int>(5, 5), new Position<int>(2, 2), 2,
                new[] { Direction.Up, Direction.Up, Direction.TopLeft },
                new Position<int>[] { new(2, 0), new(2, 0), new(0, 4) }
            ];
            yield return
            [
                new Position<int>(), new Position<int>(5, 5), new Position<int>(6, 6), 2,
                new[] { Direction.Up, Direction.TopLeft, Direction.BottomLeft, Direction.BottomRight, Direction.BottomLeft },
                new Position<int>[] { new(4, 4), new(4, 4) }
            ];
            yield return
            [
                new Position<int>(), new Position<int>(5, 5), new Position<int>(-1, -2), 3,
                new[] { Direction.Up, Direction.TopRight },
                new Position<int>[] { new(2, 1) }
            ];
            yield return
            [
                new Position<int>(), new Position<int>(5, 5), new Position<int>(-10, -9), 3,
                new[] { Direction.Up, Direction.TopRight },
                Array.Empty<Position<int>>()
            ];
        }
    }
}
