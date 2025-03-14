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
    [InlineData(0, 0, 2, 2, 8)]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(1, 1, 1, 3, 4)]
    [InlineData(2, 2, 4, 2, 4)]
    [InlineData(1, 3, 3, 1, 8)]
    [InlineData(-1, -1, 1, 1, 8)]
    [InlineData(-3, -3, -1, -1, 8)]
    [InlineData(100, 100, 100, 100, 0)]
    [InlineData(0, 0, 0, 2, 4)]
    [InlineData(0, 0, 2, 0, 4)]
    public void Perimeter_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.Perimeter;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(GetEnumeratorTestData))]
    public void GetEnumerator_ReturnsCorrectValue(int x1, int y1, int x2, int y2, Position<int>[] expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.ToList();

        // assert
        Assert.Equal(expected.Length, result.Count);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
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
    [ClassData(typeof(GetNeighboursTestData))]
    public void GetNeighbours_ReturnsCorrectValue(Position<int> corner1, Position<int> corner2, Position<int> position, int distance, Direction[] directions, bool wrap, Position<int>[] expected)
    {
        // arrange
        var sut = new Area<int>(corner1, corner2);

        // act
        var result = sut.GetNeighbours(position, distance, directions, wrap).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [InlineData(0, 0, 4, 4, 2, 2, 6, 6, 2, 2, 4, 4, false)]
    [InlineData(0, 0, 4, 4, 1, 1, 3, 3, 1, 1, 3, 3, false)]
    [InlineData(0, 0, 4, 4, 0, 0, 4, 4, 0, 0, 4, 4, false)]
    [InlineData(0, 0, 4, 4, 5, 5, 6, 6, 0, 0, 0, 0, true)]
    [InlineData(0, 0, 4, 4, 4, 4, 6, 6, 4, 4, 4, 4, false)]
    [InlineData(0, 0, 2, 2, 2, 0, 4, 2, 2, 0, 2, 2, false)]
    [InlineData(0, 0, 2, 2, -2, 0, 0, 2, 0, 0, 0, 2, false)]
    [InlineData(0, 0, 2, 2, 0, 2, 2, 4, 0, 2, 2, 2, false)]
    [InlineData(0, 0, 2, 2, 0, -2, 2, 0, 0, 0, 2, 0, false)]
    public void GetIntersection_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int expectedX1, int expectedY1, int expectedX2, int expectedY2, bool isNull)
    {
        // arrange
        Area<int>? expected = isNull ? null : new Area<int>(new Position<int>(expectedX1, expectedY1), new Position<int>(expectedX2, expectedY2));
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.GetIntersection(new Area<int>(new Position<int>(x3, y3), new Position<int>(x4, y4)));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 1, -1, -1, 1, 1)]
    [InlineData(0, 0, 0, 0, 2, -2, -2, 2, 2)]
    [InlineData(0, 0, 3, 3, 1, -1, -1, 4, 4)]
    [InlineData(-1, 0, 3, 4, 1, -2, -1, 4, 5)]
    public void Expand_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int amount, int expectedX1, int expectedY1, int expectedX2, int expectedY2)
    {
        // arrange
        var expected = new Area<int>(new Position<int>(expectedX1, expectedY1), new Position<int>(expectedX2, expectedY2));
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.Expand(amount);

        // assert
        Assert.Equal(expected, result);
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
    public void Has_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int x3, int y3, bool expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.Has(new Position<int>(x3, y3));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 1, 1, 4, 4, true)]
    [InlineData(0, 0, 5, 5, 2, 3, 4, 5, true)]
    [InlineData(0, 0, 5, 5, 0, 0, 5, 5, true)]
    [InlineData(0, 0, 5, 5, -1, -1, 5, 5, false)]
    [InlineData(0, 0, 5, 5, 0, 0, 6, 6, false)]
    [InlineData(0, 0, 5, 5, -1, 0, 5, 5, false)]
    [InlineData(0, 0, 5, 5, 0, -1, 5, 5, false)]
    [InlineData(0, 0, 5, 5, 0, 0, 6, 5, false)]
    [InlineData(0, 0, 5, 5, 0, 0, 0, 6, false)]
    public void HasArea_ReturnsCorrectValue(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, bool expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.Has(new Area<int>(new Position<int>(x3, y3), new Position<int>(x4, y4)));

        // assert
        Assert.Equal(expected, result);
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
    [ClassData(typeof(WrapListTestData))]
    public void WrapList_ReturnsCorrectValue(int x1, int y1, int x2, int y2, Position<int>[] positions, Position<int>[] expected)
    {
        // arrange
        var sut = new Area<int>(new Position<int>(x1, y1), new Position<int>(x2, y2));

        // act
        var result = sut.Wrap(positions).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(FilterTestData))]
    public void Filter_ReturnsCorrectValue(Position<int> corner1, Position<int> corner2, Position<int>[] positions, Position<int>[] expected)
    {
        // arrange
        var sut = new Area<int>(corner1, corner2);

        // act
        var result = sut.Filter(positions).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    /// <summary>
    /// Test data for <see cref="AreaTest.Constructor_ReturnsCorrectValue"/>.
    /// </summary>
    private class ConstructorTestData : TheoryData<Position<int>, Position<int>, Position<int>, Position<int>>
    {
        public ConstructorTestData()
        {
            Add(new Position<int>(), new Position<int>(5, 5), new Position<int>(0, 5), new Position<int>(5, 0));
            Add(new Position<int>(-3, -4), new Position<int>(-5, 7), new Position<int>(-5, 7), new Position<int>(-3, -4));
            Add(new Position<int>(-3, 7), new Position<int>(-5, -4), new Position<int>(-5, 7), new Position<int>(-3, -4));
        }
    }

    /// <summary>
    /// Test data for <see cref="AreaTest.GetEnumerator_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetEnumeratorTestData : TheoryData<int, int, int, int, Position<int>[]>
    {
        public GetEnumeratorTestData()
        {
            Add(0, 0, 1, 1, [new Position<int>(), new Position<int>(1, 0), new Position<int>(0, 1), new Position<int>(1, 1)]);
            Add(0, 0, 2, 2,
            [
                new Position<int>(), new Position<int>(1, 0), new Position<int>(2, 0), new Position<int>(0, 1), new Position<int>(1, 1),
                new Position<int>(2, 1), new Position<int>(0, 2), new Position<int>(1, 2), new Position<int>(2, 2)
            ]);
            Add(-2, -2, 0, 0,
            [
                new Position<int>(-2, -2), new Position<int>(-1, -2), new Position<int>(0, -2), new Position<int>(-2, -1), new Position<int>(-1, -1),
                new Position<int>(0, -1), new Position<int>(-2, 0), new Position<int>(-1, 0), new Position<int>(0, 0)
            ]);
        }
    }

    /// <summary>
    /// Test data for <see cref="AreaTest.GetRow_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetRowTestData : TheoryData<int, int, int, int, int, int, Position<int>[]>
    {
        public GetRowTestData()
        {
            Add(0, 0, 2, 2, 0, 1, [new Position<int>(), new Position<int>(1, 0), new Position<int>(2, 0)]);
            Add(0, 0, 2, 2, 0, 2, [new Position<int>(), new Position<int>(2, 0)]);
            Add(0, 0, 2, 2, 0, 3, [new Position<int>()]);
            Add(0, 0, 2, 2, 1, 1, [new Position<int>(0, 1), new Position<int>(1, 1), new Position<int>(2, 1)]);
            Add(0, 0, 2, 2, 1, 10, [new Position<int>(0, 1)]);
            Add(0, 0, 2, 2, -1, 1, []);
            Add(0, 0, 2, 2, 3, 1, []);
            Add(0, 0, 2, 2, 0, 0, [new Position<int>(), new Position<int>(1, 0), new Position<int>(2, 0)]);
            Add(0, 0, 2, 2, 0, -1, [new Position<int>(), new Position<int>(1, 0), new Position<int>(2, 0)]);
        }
    }

    /// <summary>
    /// Test data for <see cref="AreaTest.GetColumn_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetColumnTestData : TheoryData<int, int, int, int, int, int, Position<int>[]>
    {
        public GetColumnTestData()
        {
            Add(0, 0, 2, 2, 0, 1, [new Position<int>(), new Position<int>(0, 1), new Position<int>(0, 2)]);
            Add(0, 0, 2, 2, 0, 2, [new Position<int>(), new Position<int>(0, 2)]);
            Add(0, 0, 2, 2, 0, 3, [new Position<int>()]);
            Add(0, 0, 2, 2, 1, 1, [new Position<int>(1, 0), new Position<int>(1, 1), new Position<int>(1, 2)]);
            Add(0, 0, 2, 2, 1, 10, [new Position<int>(1, 0)]);
            Add(0, 0, 2, 2, -1, 1, []);
            Add(0, 0, 2, 2, 3, 1, []);
            Add(0, 0, 2, 2, 0, 0, [new Position<int>(), new Position<int>(0, 1), new Position<int>(0, 2)]);
            Add(0, 0, 2, 2, 0, -1, [new Position<int>(), new Position<int>(0, 1), new Position<int>(0, 2)]);
        }
    }

    /// <summary>
    /// Test data for <see cref="AreaTest.WrapList_ReturnsCorrectValue"/>.
    /// </summary>
    private class WrapListTestData : TheoryData<int, int, int, int, Position<int>[], Position<int>[]>
    {
        public WrapListTestData()
        {
            Add(0, 0, 6, 6, [new Position<int>(0, 0), new Position<int>(1, 1), new Position<int>(6, 6)],
                [new Position<int>(0, 0), new Position<int>(1, 1), new Position<int>(6, 6)]);
            Add(0, 0, 6, 6, [new Position<int>(-1, 0), new Position<int>(0, -1), new Position<int>(-1, -1)],
                [new Position<int>(6, 0), new Position<int>(0, 6), new Position<int>(6, 6)]);
            Add(0, 0, 6, 6, [new Position<int>(7, 0), new Position<int>(0, 7), new Position<int>(7, 7)],
                [new Position<int>(0, 0), new Position<int>(0, 0), new Position<int>(0, 0)]);
            Add(0, 0, 6, 6, [new Position<int>(-2, 8), new Position<int>(9, -3)],
                [new Position<int>(5, 1), new Position<int>(2, 4)]);
        }
    }

    /// <summary>
    /// Test data for <see cref="AreaTest.Filter_ReturnsCorrectValue"/>.
    /// </summary>
    private class FilterTestData : TheoryData<Position<int>, Position<int>, Position<int>[], Position<int>[]>
    {
        public FilterTestData()
        {
            Add(new Position<int>(), new Position<int>(5, 5), [
                    new Position<int>(), new Position<int>(6, 6), new Position<int>(1, 3), new Position<int>(5, 5), new Position<int>(-1, -1)
                ],
                [
                    new Position<int>(), new Position<int>(1, 3), new Position<int>(5, 5)
                ]);
            Add(new Position<int>(), new Position<int>(5, 5), [
                    new Position<int>(6, 6), new Position<int>(6, 5), new Position<int>(5, 6), new Position<int>(-1, -1), new Position<int>(-1, 0),
                    new Position<int>(0, -1)
                ], []);
            Add(new Position<int>(), new Position<int>(5, 5), [], []);
        }
    }

    /// <summary>
    /// Test data for <see cref="AreaTest.GetNeighbours_ReturnsCorrectValue"/>.
    /// </summary>
    private class GetNeighboursTestData : TheoryData<Position<int>, Position<int>, Position<int>, int, Direction[], bool, Position<int>[]>
    {
        public GetNeighboursTestData()
        {
            Add(new Position<int>(), new Position<int>(5, 5), new Position<int>(0, 0), 1,
                [Direction.Up, Direction.Down, Direction.Left, Direction.Right],
                false, [new Position<int>(0, 1), new Position<int>(1, 0)]);
            Add(new Position<int>(), new Position<int>(5, 5), new Position<int>(2, 2), 1,
                [Direction.Up, Direction.Down, Direction.Left, Direction.Right],
                false, [new Position<int>(2, 1), new Position<int>(2, 3), new Position<int>(1, 2), new Position<int>(3, 2)]);
            Add(new Position<int>(), new Position<int>(5, 5), new Position<int>(0, 0), 3,
                [Direction.Up, Direction.Down, Direction.Left, Direction.Right],
                false, [new Position<int>(0, 3), new Position<int>(3, 0)]);
            Add(new Position<int>(), new Position<int>(5, 5), new Position<int>(2, 2), 2,
                [Direction.Up, Direction.Up, Direction.TopLeft],
                false, [new Position<int>(2, 0), new Position<int>(2, 0), new Position<int>(0, 4)]);
            Add(new Position<int>(), new Position<int>(5, 5), new Position<int>(6, 6), 2,
                [Direction.Up, Direction.TopLeft, Direction.BottomLeft, Direction.BottomRight, Direction.BottomLeft],
                false, [new Position<int>(4, 4), new Position<int>(4, 4)]);
            Add(new Position<int>(), new Position<int>(5, 5), new Position<int>(-1, -2), 3,
                [Direction.Up, Direction.TopRight],
                false, [new Position<int>(2, 1)]);
            Add(new Position<int>(), new Position<int>(5, 5), new Position<int>(-10, -9), 3,
                [Direction.Up, Direction.TopRight], false, []);
            Add(new Position<int>(), new Position<int>(5, 5), new Position<int>(2, 2), 1,
                [], false, []);
            Add(new Position<int>(), new Position<int>(5, 5), new Position<int>(2, 2), 1,
                null!, false, [new Position<int>(2, 1), new Position<int>(2, 3), new Position<int>(1, 2), new Position<int>(3, 2)]);
            Add(new Position<int>(), new Position<int>(5, 5), new Position<int>(0, 0), 1,
                [Direction.Up, Direction.Down, Direction.Left, Direction.Right],
                true, [new Position<int>(0, 5), new Position<int>(0, 1), new Position<int>(5, 0), new Position<int>(1, 0)]);
        }
    }
}
