using System.Collections;
using AoC.Geometry;

namespace AoC.Test.Geometry;

/// <summary>
/// Unit tests for <see cref="Grid{T}"/>.
/// </summary>
public class GridTest
{
    [Theory]
    [ClassData(typeof(ConstructorTestData))]
    public void Constructor_ReturnsCorrectValue(int[] data, int width, int defaultValue, int[] expected)
    {
        // act
        var result = new Grid<int>(data, width, defaultValue);

        // assert
        Assert.Equal(width, result.Width);
        Assert.Equal(expected.Length, result.Data.Count);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result.Data[i]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-4)]
    public void Constructor_ThrowsOnOutOfRange(int width)
    {
        // arrange
        var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        // assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Grid<int>(array, width));
    }

    [Theory]
    [ClassData(typeof(HeightTestData))]
    public void Height_ReturnsCorrectValue(int[] data, int width, int expectedHeight)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.Height;

        // assert
        Assert.Equal(expectedHeight, result);
    }

    [Theory]
    [ClassData(typeof(AreaTestData))]
    public void Area_ReturnsCorrectValue(int[] data, int width, Area<int> expectedArea)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.Area;

        // assert
        Assert.Equal(expectedArea, result);
    }

    [Theory]
    [ClassData(typeof(IndexerTestData))]
    public void Indexer_ReturnsCorrectValue(int[] data, int width, Position<int> position, GridEntry<int> expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut[position];

        // assert
        Assert.Equal(expected.Position, result.Position);
        Assert.Equal(expected.Value, result.Value);
    }

    [Theory]
    [ClassData(typeof(GetRowTestData))]
    public void GetRow_ReturnsCorrectValue(int[] data, int width, int row, GridEntry<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.GetRow(row).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(GetColumnTestData))]
    public void GetColumn_ReturnsCorrectValue(int[] data, int width, int column, GridEntry<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.GetColumn(column).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(FindTestData))]
    public void Find_ReturnsCorrectValue(int[] data, int width, int value, Position<int>? expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.Find(value);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(FindAllTestData))]
    public void FindAll_ReturnsCorrectValue(int[] data, int width, int value, Position<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.FindAll(value).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(ExtractTestData))]
    public void Extract_ReturnsCorrectValue(int[] data, int width, Position<int>[] positions, GridEntry<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.Extract(positions).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(WrapExtractTestData))]
    public void WrapExtract_ReturnsCorrectValue(int[] data, int width, Position<int>[] positions, GridEntry<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.WrapExtract(positions).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(GetNeighboursTestData))]
    public void GetNeighbours_ReturnsCorrectResult(int[] data, int width, Position<int> position, int distance, Direction[] directions, GridEntry<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.GetNeighbours(position, distance, directions).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(BreadthFirstSearchTestData))]
    public void BreadthFirstSearch_ReturnsCorrectValue(
        int[] data,
        int width,
        Position<int> start,
        Position<int> end,
        Func<Position<int>, IEnumerable<Position<int>>> neighbourFunction,
        GridEntry<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.BreadthFirstSearch(start, end, neighbourFunction).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(DepthFirstSearchTestData))]
    public void DepthFirstSearch_ReturnsCorrectValue(
        int[] data,
        int width,
        Position<int> start,
        Position<int> end,
        Func<Position<int>, IEnumerable<Position<int>>> neighbourFunction,
        GridEntry<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.DepthFirstSearch(start, end, neighbourFunction).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    /// <summary>
    /// Test data for <see cref="GridTest.Constructor_ReturnsCorrectValue"/>.
    /// </summary>
    private class ConstructorTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [new[] { 1, 2, 3 }, 3, 0, new[] { 1, 2, 3 }];
            yield return [new[] { 1, 2, 3, 4, 5 }, 2, 5, new[] { 1, 2, 3, 4, 5, 5 }];
            yield return [Array.Empty<int>(), 5, 4, new[] { 4, 4, 4, 4, 4 }];
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.Height_ReturnsCorrectValue"/>.
    /// </summary>
    private class HeightTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [new[] { 1, 2, 3 }, 3, 1];
            yield return [new[] { 1, 2, 3, 4, 5, 6 }, 3, 2];
            yield return [new[] { 1, 2, 3, 4, 5, 6 }, 2, 3];
            yield return [new[] { 1, 2, 3, 4, 5, 6 }, 1, 6];
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.Area_ReturnsCorrectValue"/>.
    /// </summary>
    private class AreaTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [new[] { 1, 2, 3 }, 3, new Area<int>(new Position<int>(), new Position<int>(2, 0))];
            yield return [new[] { 1, 2, 3, 4, 5, 6 }, 3, new Area<int>(new Position<int>(), new Position<int>(2, 1))];
            yield return [new[] { 1, 2, 3, 4, 5, 6 }, 2, new Area<int>(new Position<int>(), new Position<int>(1, 2))];
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.Indexer_ReturnsCorrectValue"/>.
    /// </summary>
    private class IndexerTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            yield return [new[] { 1, 2, 3 }, 3, new Position<int>(), new GridEntry<int>(new Position<int>(), 1)];
            yield return [new[] { 1, 2, 3 }, 3, new Position<int>(1, 0), new GridEntry<int>(new Position<int>(1, 0), 2)];
            yield return [array, 3, new Position<int>(1, 1), new GridEntry<int>(new Position<int>(1, 1), 5)];
            yield return [array, 3, new Position<int>(2, 2), new GridEntry<int>(new Position<int>(2, 2), 9)];
            yield return [array, 3, new Position<int>(0, 2), new GridEntry<int>(new Position<int>(0, 2), 7)];
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.GetRow_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetRowTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            yield return
            [
                array, 3, 0, new GridEntry<int>[]
                {
                    new(new Position<int>(), 1), new(new Position<int>(1, 0), 2), new(new Position<int>(2, 0), 3)
                }
            ];
            yield return
            [
                array, 3, 1, new GridEntry<int>[]
                {
                    new(new Position<int>(0, 1), 4), new(new Position<int>(1, 1), 5), new(new Position<int>(2, 1), 6)
                }
            ];
            yield return
            [
                array, 3, 2, new GridEntry<int>[]
                {
                    new(new Position<int>(0, 2), 7), new(new Position<int>(1, 2), 8), new(new Position<int>(2, 2), 9)
                }
            ];
            yield return
            [
                array, 3, -1, Array.Empty<GridEntry<int>>()
            ];
            yield return
            [
                array, 3, 3, Array.Empty<GridEntry<int>>()
            ];
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.GetColumn_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetColumnTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            yield return
            [
                array, 3, 0, new GridEntry<int>[]
                {
                    new(new Position<int>(), 1), new(new Position<int>(0, 1), 4), new(new Position<int>(0, 2), 7)
                }
            ];
            yield return
            [
                array, 3, 1, new GridEntry<int>[]
                {
                    new(new Position<int>(1, 0), 2), new(new Position<int>(1, 1), 5), new(new Position<int>(1, 2), 8)
                }
            ];
            yield return
            [
                array, 3, 2, new GridEntry<int>[]
                {
                    new(new Position<int>(2, 0), 3), new(new Position<int>(2, 1), 6), new(new Position<int>(2, 2), 9)
                }
            ];
            yield return
            [
                array, 3, -1, Array.Empty<GridEntry<int>>()
            ];
            yield return
            [
                array, 3, 3, Array.Empty<GridEntry<int>>()
            ];
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.Find_ReturnsCorrectValue"/>.
    /// </summary>
    private class FindTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            yield return [array, 3, 1, new Position<int>()];
            yield return [array, 3, 2, new Position<int>(1, 0)];
            yield return [array, 3, 5, new Position<int>(1, 1)];
            yield return [array, 3, 9, new Position<int>(2, 2)];
            yield return [array, 3, 0, null!];
            yield return [array, 3, -1, null!];
            yield return [array, 3, 10, null!];
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.FindAll_ReturnsCorrectValue"/>.
    /// </summary>
    private class FindAllTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var array = new[] { 1, 2, 1, 3, 2, 3, 4, 0, 0 };
            yield return [array, 3, -1, Array.Empty<Position<int>>()];
            yield return [array, 3, 0, new Position<int>[] { new(1, 2), new(2, 2) }];
            yield return [array, 3, 1, new Position<int>[] { new(), new(2, 0) }];
            yield return [array, 3, 2, new Position<int>[] { new(1, 0), new(1, 1) }];
            yield return [array, 3, 3, new Position<int>[] { new(0, 1), new(2, 1) }];
            yield return [array, 3, 4, new Position<int>[] { new(0, 2) }];
            yield return [array, 3, 5, Array.Empty<Position<int>>()];
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.Extract_ReturnsCorrectValue"/>.
    /// </summary>
    private class ExtractTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            yield return
            [
                array, 3, new Position<int>[] { new(1, 0) },
                new GridEntry<int>[] { new(new Position<int>(1, 0), 2) }
            ];
            yield return
            [
                array, 3, new Position<int>[] { new(), new(1, 1), new(2, 2) },
                new GridEntry<int>[] { new(new Position<int>(), 1), new(new Position<int>(1, 1), 5), new(new Position<int>(2, 2), 9) }
            ];
            yield return
            [
                array, 3, new Position<int>[] { new(), new(1, 1), new(2, 2), new(2, 2) },
                new GridEntry<int>[] { new(new Position<int>(), 1), new(new Position<int>(1, 1), 5), new(new Position<int>(2, 2), 9), new(new Position<int>(2, 2), 9) }
            ];
            yield return
            [
                array, 3, new Position<int>[] { new(), new(1, 1), new(3, 3), new(2, 2) },
                new GridEntry<int>[] { new(new Position<int>(), 1), new(new Position<int>(1, 1), 5), new(new Position<int>(2, 2), 9) }
            ];
            yield return
            [
                array, 3, Array.Empty<Position<int>>(), Array.Empty<GridEntry<int>>()
            ];
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.WrapExtract_ReturnsCorrectValue"/>.
    /// </summary>
    private class WrapExtractTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            yield return
            [
                array, 3, new Position<int>[] { new(1, 0) },
                new GridEntry<int>[] { new(new Position<int>(1, 0), 2) }
            ];
            yield return
            [
                array, 3, new Position<int>[] { new(), new(1, 1), new(2, 2) },
                new GridEntry<int>[] { new(new Position<int>(), 1), new(new Position<int>(1, 1), 5), new(new Position<int>(2, 2), 9) }
            ];
            yield return
            [
                array, 3, new Position<int>[] { new(-1, -1) },
                new GridEntry<int>[] { new(new Position<int>(2, 2), 9) }
            ];
        }
    }

    /// <summary>
    /// Unit tests for <see cref="GridTest.GetNeighbours_ReturnsCorrectResult"/>.
    /// </summary>
    private class GetNeighboursTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            yield return
            [
                array, 3, new Position<int>(), 1,
                new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
                new GridEntry<int>[] { new(new Position<int>(0, 1), 4), new(new Position<int>(1, 0), 2) }
            ];
            yield return
            [
                array, 3, new Position<int>(), 1,
                new[] { Direction.Up, Direction.Down, Direction.Up, Direction.Down },
                new GridEntry<int>[] { new(new Position<int>(0, 1), 4), new(new Position<int>(0, 1), 4) }
            ];
            yield return
            [
                array, 3, new Position<int>(), 2,
                new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
                new GridEntry<int>[] { new(new Position<int>(0, 2), 7), new(new Position<int>(2, 0), 3) }
            ];
            yield return
            [
                array, 3, new Position<int>(-10, -10), 3,
                new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
                Array.Empty<GridEntry<int>>()
            ];
            yield return
            [
                array, 3, new Position<int>(-10, -10), 12,
                new[] { Direction.TopRight, Direction.TopLeft },
                new GridEntry<int>[] { new(new Position<int>(2, 2), 9) }
            ];
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.BreadthFirstSearch_ReturnsCorrectValue"/>.
    /// </summary>
    private class BreadthFirstSearchTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            yield return
            [
                array, 3, new Position<int>(), new Position<int>(1, 1),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.Up, Direction.Down, Direction.Left, Direction.Right),
                new GridEntry<int>[]
                {
                    new(new Position<int>(), 1),
                    new(new Position<int>(0, 1), 4),
                    new(new Position<int>(1, 0), 2),
                    new(new Position<int>(0, 2), 7),
                    new(new Position<int>(1, 1), 5),
                }
            ];
            yield return
            [
                array, 3, new Position<int>(), new Position<int>(10, 10),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.Up, Direction.Down, Direction.Left, Direction.Right),
                new GridEntry<int>[]
                {
                    new(new Position<int>(), 1),
                    new(new Position<int>(0, 1), 4),
                    new(new Position<int>(1, 0), 2),
                    new(new Position<int>(0, 2), 7),
                    new(new Position<int>(1, 1), 5),
                    new(new Position<int>(2, 0), 3),
                    new(new Position<int>(1, 2), 8),
                    new(new Position<int>(2, 1), 6),
                    new(new Position<int>(2, 2), 9),
                }
            ];
            yield return
            [
                array, 3, new Position<int>(), new Position<int>(1, 1),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.TopRight),
                new GridEntry<int>[] { new(new Position<int>(), 1), new(new Position<int>(1, 1), 5) }
            ];
            yield return
            [
                array, 3, new Position<int>(), new Position<int>(1, 1),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.Left, Direction.Right),
                new GridEntry<int>[] { new(new Position<int>(), 1), new(new Position<int>(1, 0), 2), new(new Position<int>(2, 0), 3) }
            ];
            yield return
            [
                array, 3, new Position<int>(1, 1), new Position<int>(-1, -1),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.Up, Direction.Down),
                new GridEntry<int>[] { new(new Position<int>(1, 1), 5), new(new Position<int>(1, 0), 2), new(new Position<int>(1, 2), 8) }
            ];
            yield return
            [
                array, 3, new Position<int>(), new Position<int>(),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.Up, Direction.Down, Direction.Left, Direction.Right),
                new GridEntry<int>[] { new(new Position<int>(), 1) }
            ];
            yield return
            [
                array, 3, new Position<int>(6, 7), new Position<int>(8, 9),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.Up, Direction.Down, Direction.Left, Direction.Right),
                Array.Empty<GridEntry<int>>()
            ];
            yield return
            [
                array, 3, new Position<int>(), new Position<int>(1, 1),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.BottomRight),
                new GridEntry<int>[] { new(new Position<int>(), 1) }
            ];
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.DepthFirstSearch_ReturnsCorrectValue"/>.
    /// </summary>
    private class DepthFirstSearchTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            yield return
            [
                array, 3, new Position<int>(), new Position<int>(1, 1),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.Up, Direction.Down, Direction.Left, Direction.Right),
                new GridEntry<int>[]
                {
                    new(new Position<int>(), 1),
                    new(new Position<int>(1, 0), 2),
                    new(new Position<int>(2, 0), 3),
                    new(new Position<int>(2, 1), 6),
                    new(new Position<int>(1, 1), 5),
                }
            ];
            yield return
            [
                array, 3, new Position<int>(), new Position<int>(10, 10),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.Up, Direction.Down, Direction.Left, Direction.Right),
                new GridEntry<int>[]
                {
                    new(new Position<int>(), 1),
                    new(new Position<int>(1, 0), 2),
                    new(new Position<int>(2, 0), 3),
                    new(new Position<int>(2, 1), 6),
                    new(new Position<int>(1, 1), 5),
                    new(new Position<int>(0, 1), 4),
                    new(new Position<int>(0, 2), 7),
                    new(new Position<int>(1, 2), 8),
                    new(new Position<int>(2, 2), 9),
                }
            ];
            yield return
            [
                array, 3, new Position<int>(), new Position<int>(1, 1),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.TopRight),
                new GridEntry<int>[] { new(new Position<int>(), 1), new(new Position<int>(1, 1), 5) }
            ];
            yield return
            [
                array, 3, new Position<int>(), new Position<int>(1, 1),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.Left, Direction.Right),
                new GridEntry<int>[] { new(new Position<int>(), 1), new(new Position<int>(1, 0), 2), new(new Position<int>(2, 0), 3) }
            ];
            yield return
            [
                array, 3, new Position<int>(1, 1), new Position<int>(-1, -1),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.Up, Direction.Down),
                new GridEntry<int>[] { new(new Position<int>(1, 1), 5), new(new Position<int>(1, 2), 8), new(new Position<int>(1, 0), 2) }
            ];
            yield return
            [
                array, 3, new Position<int>(), new Position<int>(),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.Up, Direction.Down, Direction.Left, Direction.Right),
                new GridEntry<int>[] { new(new Position<int>(), 1) }
            ];
            yield return
            [
                array, 3, new Position<int>(6, 7), new Position<int>(8, 9),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.Up, Direction.Down, Direction.Left, Direction.Right),
                Array.Empty<GridEntry<int>>()
            ];
            yield return
            [
                array, 3, new Position<int>(), new Position<int>(1, 1),
                (Position<int> pos) => pos.GetNeighbours(1, Direction.BottomRight),
                new GridEntry<int>[] { new(new Position<int>(), 1) }
            ];
        }
    }
}
