using AoC.Geometry;
using AoC.Grids;

namespace AoC.Test.Grids;

/// <summary>
/// Unit tests for <see cref="Grid{T}"/>.
/// </summary>
public class GridTest
{
    [Theory]
    [ClassData(typeof(DataConstructorTestData))]
    public void DataConstructor_ReturnsCorrectValue(int[][] data, int[] expected, int expectedWidth)
    {
        // act
        var result = new Grid<int>(data);

        // assert
        Assert.Equal(expectedWidth, result.Width);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result.Data[i]);
    }

    [Fact]
    public void DataConstructor_ThrowsOnZeroLength()
    {
        // assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Grid<int>([]));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Grid<int>([[]]));
    }

    [Theory]
    [ClassData(typeof(WidthConstructorTestData))]
    public void WidthConstructor_ReturnsCorrectValue(int[] data, int width, int defaultValue, int[] expected)
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
    [ClassData(typeof(IndexIndexerTestData))]
    public void IndexIndexer_ReturnsCorrectValue(int[] data, int width, int i, GridEntry<int> expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut[i];

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(PositionIndexerTestData))]
    public void PositionIndexer_ReturnsCorrectValue(int[] data, int width, Position<int> position, GridEntry<int> expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut[position];

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(GetEnumeratorTestData))]
    public void GetEnumerator_ReturnsCorrectValue(int[] data, int width, GridEntry<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.ToList();

        // assert
        Assert.Equal(expected.Length, result.Count);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(GetRowTestData))]
    public void GetRow_ReturnsCorrectValue(int[] data, int width, int row, int distance, GridEntry<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.GetRow(row, distance).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(GetColumnTestData))]
    public void GetColumn_ReturnsCorrectValue(int[] data, int width, int column, int distance, GridEntry<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.GetColumn(column, distance).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(GetNeighboursTestData))]
    public void GetNeighbours_ReturnsCorrectResult(int[] data, int width, Position<int> position, int distance, Direction[] directions, bool wrap, GridEntry<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.GetNeighbours(position, distance, directions, wrap).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(FindTestData))]
    public void Find_ReturnsCorrectValue(int[] data, int width, int value, GridEntry<int>? expected)
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
    public void FindAll_ReturnsCorrectValue(int[] data, int width, int value, GridEntry<int>[] expected)
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
    public void Extract_ReturnsCorrectValue(int[] data, int width, Position<int>[] positions, bool wrap, GridEntry<int>[] expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.Extract(positions, wrap).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(ToStringTestData))]
    public void ToString_ReturnsCorrectValue(int[] data, int width, string expected)
    {
        // arrange
        var sut = new Grid<int>(data, width);

        // act
        var result = sut.ToString();

        // assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Unit tests for <see cref="GridTest.DataConstructor_ReturnsCorrectValue"/>.
    /// </summary>
    private class DataConstructorTestData : TheoryData<int[][], int[], int>
    {
        public DataConstructorTestData()
        {
            Add([[1, 2, 3], [4, 5, 6], [7, 8, 9]], [1, 2, 3, 4, 5, 6, 7, 8, 9], 3);
            Add([[1, 2, 3], [4, 5, 6]], [1, 2, 3, 4, 5, 6], 3);
            Add([[1, 2, 3, 4], [5, 6, 7, 8]], [1, 2, 3, 4, 5, 6, 7, 8], 4);
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.WidthConstructor_ReturnsCorrectValue"/>.
    /// </summary>
    private class WidthConstructorTestData : TheoryData<int[], int, int, int[]>
    {
        public WidthConstructorTestData()
        {
            Add([1, 2, 3], 3, 0, [1, 2, 3]);
            Add([1, 2, 3, 4, 5], 2, 5, [1, 2, 3, 4, 5, 5]);
            Add([], 5, 4, [4, 4, 4, 4, 4]);
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.Height_ReturnsCorrectValue"/>.
    /// </summary>
    private class HeightTestData : TheoryData<int[], int, int>
    {
        public HeightTestData()
        {
            Add([1, 2, 3], 3, 1);
            Add([1, 2, 3, 4, 5, 6], 3, 2);
            Add([1, 2, 3, 4, 5, 6], 2, 3);
            Add([1, 2, 3, 4, 5, 6], 1, 6);
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.Area_ReturnsCorrectValue"/>.
    /// </summary>
    private class AreaTestData : TheoryData<int[], int, Area<int>>
    {
        public AreaTestData()
        {
            Add([1, 2, 3], 3, new Area<int>(new Position<int>(), new Position<int>(2, 0)));
            Add([1, 2, 3, 4, 5, 6], 3, new Area<int>(new Position<int>(), new Position<int>(2, 1)));
            Add([1, 2, 3, 4, 5, 6], 2, new Area<int>(new Position<int>(), new Position<int>(1, 2)));
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.IndexIndexer_ReturnsCorrectValue"/>.
    /// </summary>
    private class IndexIndexerTestData : TheoryData<int[], int, int, GridEntry<int>>
    {
        public IndexIndexerTestData()
        {
            Add([1, 2, 3], 3, 0, new GridEntry<int>(new Position<int>(), 1));
            Add([1, 2, 3], 3, 1, new GridEntry<int>(new Position<int>(1, 0), 2));
            Add([1, 2, 3, 4, 5, 6, 7, 8, 9], 3, 4, new GridEntry<int>(new Position<int>(1, 1), 5));
            Add([1, 2, 3, 4, 5, 6, 7, 8, 9], 3, 8, new GridEntry<int>(new Position<int>(2, 2), 9));
            Add([1, 2, 3, 4, 5, 6, 7, 8, 9], 3, 6, new GridEntry<int>(new Position<int>(0, 2), 7));
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.PositionIndexer_ReturnsCorrectValue"/>.
    /// </summary>
    private class PositionIndexerTestData : TheoryData<int[], int, Position<int>, GridEntry<int>>
    {
        public PositionIndexerTestData()
        {
            Add([1, 2, 3], 3, new Position<int>(), new GridEntry<int>(new Position<int>(), 1));
            Add([1, 2, 3], 3, new Position<int>(1, 0), new GridEntry<int>(new Position<int>(1, 0), 2));
            Add([1, 2, 3, 4, 5, 6, 7, 8, 9], 3, new Position<int>(1, 1), new GridEntry<int>(new Position<int>(1, 1), 5));
            Add([1, 2, 3, 4, 5, 6, 7, 8, 9], 3, new Position<int>(2, 2), new GridEntry<int>(new Position<int>(2, 2), 9));
            Add([1, 2, 3, 4, 5, 6, 7, 8, 9], 3, new Position<int>(0, 2), new GridEntry<int>(new Position<int>(0, 2), 7));
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.GetEnumerator_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetEnumeratorTestData : TheoryData<int[], int, GridEntry<int>[]>
    {
        public GetEnumeratorTestData()
        {
            Add([1, 2, 3, 4, 5, 6], 3, [
                new GridEntry<int>(new Position<int>(), 1), new GridEntry<int>(new Position<int>(1, 0), 2),
                new GridEntry<int>(new Position<int>(2, 0), 3), new GridEntry<int>(new Position<int>(0, 1), 4),
                new GridEntry<int>(new Position<int>(1, 1), 5), new GridEntry<int>(new Position<int>(2, 1), 6),
            ]);
            Add([1, 2, 3, 4, 5, 6], 2, [
                new GridEntry<int>(new Position<int>(), 1), new GridEntry<int>(new Position<int>(1, 0), 2),
                new GridEntry<int>(new Position<int>(0, 1), 3), new GridEntry<int>(new Position<int>(1, 1), 4),
                new GridEntry<int>(new Position<int>(0, 2), 5), new GridEntry<int>(new Position<int>(1, 2), 6),
            ]);
            Add([1, 2, 3, 4, 5, 6, 7, 8, 9], 3, [
                new GridEntry<int>(new Position<int>(), 1), new GridEntry<int>(new Position<int>(1, 0), 2),
                new GridEntry<int>(new Position<int>(2, 0), 3), new GridEntry<int>(new Position<int>(0, 1), 4),
                new GridEntry<int>(new Position<int>(1, 1), 5), new GridEntry<int>(new Position<int>(2, 1), 6),
                new GridEntry<int>(new Position<int>(0, 2), 7), new GridEntry<int>(new Position<int>(1, 2), 8),
                new GridEntry<int>(new Position<int>(2, 2), 9),
            ]);
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.GetRow_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetRowTestData : TheoryData<int[], int, int, int, GridEntry<int>[]>
    {
        public GetRowTestData()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Add(array, 3, 0, 1, [
                new GridEntry<int>(new Position<int>(), 1), new GridEntry<int>(new Position<int>(1, 0), 2),
                new GridEntry<int>(new Position<int>(2, 0), 3),
            ]);
            Add(array, 3, 1, 1, [
                new GridEntry<int>(new Position<int>(0, 1), 4), new GridEntry<int>(new Position<int>(1, 1), 5),
                new GridEntry<int>(new Position<int>(2, 1), 6),
            ]);
            Add(array, 3, 2, 1, [
                new GridEntry<int>(new Position<int>(0, 2), 7), new GridEntry<int>(new Position<int>(1, 2), 8),
                new GridEntry<int>(new Position<int>(2, 2), 9),
            ]);
            Add(array, 3, 0, 2, [new GridEntry<int>(new Position<int>(), 1), new GridEntry<int>(new Position<int>(2, 0), 3)]);
            Add(array, 3, 1, 3, [new GridEntry<int>(new Position<int>(0, 1), 4)]);
            Add(array, 3, -1, 1, []);
            Add(array, 3, 3, 1, []);
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.GetColumn_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetColumnTestData : TheoryData<int[], int, int, int, GridEntry<int>[]>
    {
        public GetColumnTestData()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Add(array, 3, 0, 1, [
                new GridEntry<int>(new Position<int>(), 1), new GridEntry<int>(new Position<int>(0, 1), 4),
                new GridEntry<int>(new Position<int>(0, 2), 7),
            ]);
            Add(array, 3, 1, 1, [
                new GridEntry<int>(new Position<int>(1, 0), 2), new GridEntry<int>(new Position<int>(1, 1), 5),
                new GridEntry<int>(new Position<int>(1, 2), 8),
            ]);
            Add(array, 3, 2, 1, [
                new GridEntry<int>(new Position<int>(2, 0), 3), new GridEntry<int>(new Position<int>(2, 1), 6),
                new GridEntry<int>(new Position<int>(2, 2), 9),
            ]);
            Add(array, 3, 0, 2, [new GridEntry<int>(new Position<int>(), 1), new GridEntry<int>(new Position<int>(0, 2), 7)]);
            Add(array, 3, 1, 3, [new GridEntry<int>(new Position<int>(1, 0), 2)]);
            Add(array, 3, -1, 1, []);
            Add(array, 3, 3, 1, []);

        }
    }

    /// <summary>
    /// Unit tests for <see cref="GridTest.GetNeighbours_ReturnsCorrectResult"/>.
    /// </summary>
    private class GetNeighboursTestData : TheoryData<int[], int, Position<int>, int, Direction[], bool, GridEntry<int>[]>
    {
        public GetNeighboursTestData()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Add(array, 3, new Position<int>(), 1, [Direction.Up, Direction.Down, Direction.Left, Direction.Right], false,
                [new GridEntry<int>(new Position<int>(0, 1), 4), new GridEntry<int>(new Position<int>(1, 0), 2)]);
            Add(array, 3, new Position<int>(), 1, [Direction.Up, Direction.Down, Direction.Up, Direction.Down], false,
                [new GridEntry<int>(new Position<int>(0, 1), 4), new GridEntry<int>(new Position<int>(0, 1), 4)]);
            Add(array, 3, new Position<int>(), 2, [Direction.Up, Direction.Down, Direction.Left, Direction.Right], false,
                [new GridEntry<int>(new Position<int>(0, 2), 7), new GridEntry<int>(new Position<int>(2, 0), 3)]);
            Add(array, 3, new Position<int>(-10, -10), 3, [Direction.Up, Direction.Down, Direction.Left, Direction.Right], false, []);
            Add(array, 3, new Position<int>(-10, -10), 12, [Direction.TopRight, Direction.TopLeft], false, [new GridEntry<int>(new Position<int>(2, 2), 9)]);
            Add(array, 3, new Position<int>(), 1,
                [Direction.Up, Direction.Down, Direction.Left, Direction.Right], true,
                [
                    new GridEntry<int>(new Position<int>(0, 2), 7), new GridEntry<int>(new Position<int>(0, 1), 4),
                    new GridEntry<int>(new Position<int>(2, 0), 3),
                    new GridEntry<int>(new Position<int>(1, 0), 2)
                ]);
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.Find_ReturnsCorrectValue"/>.
    /// </summary>
    private class FindTestData : TheoryData<int[], int, int, GridEntry<int>?>
    {
        public FindTestData()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Add(array, 3, 1, new GridEntry<int>(new Position<int>(), 1));
            Add(array, 3, 2, new GridEntry<int>(new Position<int>(1, 0), 2));
            Add(array, 3, 5, new GridEntry<int>(new Position<int>(1, 1), 5));
            Add(array, 3, 9, new GridEntry<int>(new Position<int>(2, 2), 9));
            Add(array, 3, 0, null!);
            Add(array, 3, -1, null!);
            Add(array, 3, 10, null!);
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.FindAll_ReturnsCorrectValue"/>.
    /// </summary>
    private class FindAllTestData : TheoryData<int[], int, int, GridEntry<int>[]>
    {
        public FindAllTestData()
        {
            var array = new[] { 1, 2, 1, 3, 2, 3, 4, 0, 0 };
            Add(array, 3, 0, [new GridEntry<int>(new Position<int>(1, 2), 0), new GridEntry<int>(new Position<int>(2, 2), 0)]);
            Add(array, 3, 1, [new GridEntry<int>(new Position<int>(), 1), new GridEntry<int>(new Position<int>(2, 0), 1)]);
            Add(array, 3, 2, [new GridEntry<int>(new Position<int>(1, 0), 2), new GridEntry<int>(new Position<int>(1, 1), 2)]);
            Add(array, 3, 3, [new GridEntry<int>(new Position<int>(0, 1), 3), new GridEntry<int>(new Position<int>(2, 1), 3)]);
            Add(array, 3, 4, [new GridEntry<int>(new Position<int>(0, 2), 4)]);
            Add(array, 3, 5, []);
            Add(array, 3, -1, []);
        }
    }

    /// <summary>
    /// Test data for <see cref="GridTest.Extract_ReturnsCorrectValue"/>.
    /// </summary>
    private class ExtractTestData : TheoryData<int[], int, Position<int>[], bool, GridEntry<int>[]>
    {
        public ExtractTestData()
        {
            var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Add(array, 3, [new Position<int>(1, 0)], false,
                [new GridEntry<int>(new Position<int>(1, 0), 2)]);
            Add(array, 3, [new Position<int>(), new Position<int>(1, 1), new Position<int>(2, 2)], false,
            [
                new GridEntry<int>(new Position<int>(), 1), new GridEntry<int>(new Position<int>(1, 1), 5),
                new GridEntry<int>(new Position<int>(2, 2), 9)
            ]);
            Add(array, 3, [new Position<int>(), new Position<int>(1, 1), new Position<int>(2, 2), new Position<int>(2, 2)], false,
            [
                new GridEntry<int>(new Position<int>(), 1), new GridEntry<int>(new Position<int>(1, 1), 5),
                new GridEntry<int>(new Position<int>(2, 2), 9), new GridEntry<int>(new Position<int>(2, 2), 9)
            ]);
            Add(array, 3, [new Position<int>(), new Position<int>(1, 1), new Position<int>(3, 3), new Position<int>(2, 2)], false,
            [
                new GridEntry<int>(new Position<int>(), 1), new GridEntry<int>(new Position<int>(1, 1), 5),
                new GridEntry<int>(new Position<int>(2, 2), 9)
            ]);
            Add(array, 3, [], false, []);
            Add(array, 3, [new Position<int>(1, 0)], true, [new GridEntry<int>(new Position<int>(1, 0), 2)]);
            Add(array, 3, [new Position<int>(-1, 0)], true, [new GridEntry<int>(new Position<int>(2, 0), 3)]);
            Add(array, 3, [new Position<int>(), new Position<int>(1, 1), new Position<int>(2, 2)], true,
            [
                new GridEntry<int>(new Position<int>(), 1), new GridEntry<int>(new Position<int>(1, 1), 5),
                new GridEntry<int>(new Position<int>(2, 2), 9)
            ]);
            Add(array, 3, [new Position<int>(-1, -1), new Position<int>(3, 3)], true,
            [
                new GridEntry<int>(new Position<int>(2, 2), 9), new GridEntry<int>(new Position<int>(0, 0), 1)
            ]);
        }
    }

    /// <summary>
    /// Test data for <see cref="ToString_ReturnsCorrectValue"/>.
    /// </summary>
    private class ToStringTestData : TheoryData<int[], int, string>
    {
        public ToStringTestData()
        {
            Add([1, 2, 3, 4, 5, 6], 3, $"123{Environment.NewLine}456");
            Add([1, 2, 3, 4, 5, 6], 2, $"12{Environment.NewLine}34{Environment.NewLine}56");
            Add([1, 2, 3, 4, 5, 6, 7, 8, 9], 3, $"123{Environment.NewLine}456{Environment.NewLine}789");
        }
    }
}
