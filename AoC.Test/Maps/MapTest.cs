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

    /// <summary>
    /// Test data for <see cref="MapTest.Constructor_SetsCorrectValues"/>.
    /// </summary>
    private class ConstructorTestData : TheoryData<Grid<int>, Position<int>, Position<int>, Position<int>, Position<int>>
    {
        public ConstructorTestData()
        {
            var grid = new Grid<int>([1, 2, 3, 4, 5, 6, 7, 8, 9], 3);
            Add(grid, new Position<int>(0, 0), new Position<int>(2, 2), new Position<int>(0, 0), new Position<int>(2, 2));
            Add(grid, new Position<int>(0, 0), new Position<int>(1, 2), new Position<int>(0, 0), new Position<int>(1, 2));
            Add(grid, new Position<int>(1, 1), new Position<int>(0, 0), new Position<int>(1, 1), new Position<int>(0, 0));
            Add(grid, new Position<int>(0, -1), new Position<int>(2, 2), new Position<int>(0, 0), new Position<int>(2, 2));
            Add(grid, new Position<int>(-3, 9), new Position<int>(2, 2), new Position<int>(0, 0), new Position<int>(2, 2));
            Add(grid, new Position<int>(0, 0), new Position<int>(2, 3), new Position<int>(0, 0), new Position<int>(0, 0));
            Add(grid, new Position<int>(0, 0), new Position<int>(6, -1), new Position<int>(0, 0), new Position<int>(0, 0));
        }
    }
}
