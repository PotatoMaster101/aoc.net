using System.Collections;
using AoC.Geometry;
using AoC.Grids;
using AoC.Maps;

namespace AoC.Test.Maps;

/// <summary>
/// Unit tests for <see cref="Map{T}"/>.
/// </summary>
public class MapTest
{
    [Theory]
    [ClassData(typeof(ConstructorTestData))]
    public void Constructor_SetsCorrectValues(Grid<int> grid, Position<int> start, Position<int> end, Position<int> expectedStart, Position<int> expectedEnd)
    {
        // act
        var result = new Map<int>(grid, start, end);

        // assert
        Assert.Equal(expectedStart, result.Start);
        Assert.Equal(expectedEnd, result.End);
        Assert.Equal(grid, result.Grid);
    }

    [Theory]
    [ClassData(typeof(AreaTestData))]
    public void Area_ReturnsCorrectValue(Grid<int> grid, Area<int> expected)
    {
        // arrange
        var sut = new Map<int>(grid);

        // act
        var result = sut.Area;

        // assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Test data for <see cref="MapTest.Constructor_SetsCorrectValues"/>.
    /// </summary>
    private class ConstructorTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var grid = new Grid<int>([1, 2, 3, 4, 5, 6, 7, 8, 9], 3);
            yield return [grid, new Position<int>(0, 0), new Position<int>(2, 2), new Position<int>(0, 0), new Position<int>(2, 2)];
            yield return [grid, new Position<int>(0, 0), new Position<int>(1, 2), new Position<int>(0, 0), new Position<int>(1, 2)];
            yield return [grid, new Position<int>(1, 1), new Position<int>(0, 0), new Position<int>(1, 1), new Position<int>(0, 0)];
            yield return [grid, new Position<int>(0, -1), new Position<int>(2, 2), new Position<int>(0, 0), new Position<int>(2, 2)];
            yield return [grid, new Position<int>(-3, 9), new Position<int>(2, 2), new Position<int>(0, 0), new Position<int>(2, 2)];
            yield return [grid, new Position<int>(0, 0), new Position<int>(2, 3), new Position<int>(0, 0), new Position<int>(0, 0)];
            yield return [grid, new Position<int>(0, 0), new Position<int>(6, -1), new Position<int>(0, 0), new Position<int>(0, 0)];
        }
    }

    /// <summary>
    /// Test data for <see cref="MapTest.Area_ReturnsCorrectValue"/>.
    /// </summary>
    private class AreaTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                new Grid<int>([1, 2, 3, 4, 5, 6, 7, 8, 9], 3),
                new Area<int>(new Position<int>(0, 0), new Position<int>(2, 2))
            ];
            yield return
            [
                new Grid<int>([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16], 4),
                new Area<int>(new Position<int>(0, 0), new Position<int>(3, 3))
            ];
        }
    }
}
