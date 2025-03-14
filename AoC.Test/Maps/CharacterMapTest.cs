using AoC.Geometry;
using AoC.Grids;
using AoC.Maps;

namespace AoC.Test.Maps;

/// <summary>
/// Unit tests for <see cref="CharacterMap"/>
/// </summary>
public class CharacterMapTest
{
    [Theory]
    [ClassData(typeof(ConstructorTestData))]
    public void Constructor_SetsCorrectValues(Grid<char> grid, char start, char end, Position<int> expectedStart, Position<int> expectedEnd)
    {
        // act
        var result = new CharacterMap(grid, start, end);

        // assert
        Assert.Equal(expectedStart, result.Start);
        Assert.Equal(expectedEnd, result.End);
        Assert.Equal(grid, result.Grid);
    }

    [Theory]
    [ClassData(typeof(ArrayConstructorTestData))]
    public void ArrayConstructor_SetsCorrectValues(string[] grid, char start, char end, Position<int> expectedStart, Position<int> expectedEnd)
    {
        // act
        var result = new CharacterMap(grid, start, end);

        // assert
        Assert.Equal(expectedStart, result.Start);
        Assert.Equal(expectedEnd, result.End);

        var gridExpand = grid.SelectMany(x => x).ToList();
        Assert.Equal(gridExpand.Count, result.Grid.Data.Count);
        for (var i = 0; i < gridExpand.Count; i++)
            Assert.Equal(gridExpand[i], result.Grid.Data[i]);
    }

    [Theory]
    [ClassData(typeof(IsWallTestData))]
    public void IsWall_ReturnsCorrectValue(Grid<char> grid, Position<int> position, IReadOnlySet<char> walls, bool expected)
    {
        // arrange
        var sut = new CharacterMap(grid);

        // act
        var result = sut.IsWall(position, walls);

        // assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Test data for <see cref="CharacterMapTest.Constructor_SetsCorrectValues"/>.
    /// </summary>
    private class ConstructorTestData : TheoryData<Grid<char>, char, char, Position<int>, Position<int>>
    {
        public ConstructorTestData()
        {
            Add(new Grid<char>(['S', '.', '.', '.', '.', '.', '.', '.', 'E'], 3), 'S', 'E', new Position<int>(0, 0), new Position<int>(2, 2));
            Add(new Grid<char>(['S', '.', '.', '.', '.', '.', '.', '.', 'E'], 3), '.', '.', new Position<int>(1, 0), new Position<int>(1, 0));
            Add(new Grid<char>(['S', '.', 'S', '.', '.', '.', 'E', '.', 'E'], 3), 'S', 'E', new Position<int>(0, 0), new Position<int>(0, 2));
            Add(new Grid<char>(['S', '.', 'S', '.', '.', '.', 'E', '.', 'E'], 3), 'X', 'Y', new Position<int>(0, 0), new Position<int>(0, 0));
        }
    }

    /// <summary>
    /// Test data for <see cref="CharacterMapTest.ArrayConstructor_SetsCorrectValues"/>.
    /// </summary>
    private class ArrayConstructorTestData : TheoryData<string[], char, char, Position<int>, Position<int>>
    {
        public ArrayConstructorTestData()
        {
            Add(["S..", "...", "..E"], 'S', 'E', new Position<int>(0, 0), new Position<int>(2, 2));
            Add(["S..", "...", "..E"], '.', '.', new Position<int>(1, 0), new Position<int>(1, 0));
            Add(["S.S", "...", "E.E"], 'S', 'E', new Position<int>(0, 0), new Position<int>(0, 2));
            Add(["S..", "...", "..E"], 'X', 'Y', new Position<int>(0, 0), new Position<int>(0, 0));
        }
    }

    /// <summary>
    /// Test data for <see cref="CharacterMapTest.IsWall_ReturnsCorrectValue"/>.
    /// </summary>
    private class IsWallTestData : TheoryData<Grid<char>, Position<int>, IReadOnlySet<char>, bool>
    {
        public IsWallTestData()
        {
            Add(new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(0, 0), new HashSet<char> { '#' }, false);
            Add(new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(1, 0), new HashSet<char> { '#' }, false);
            Add(new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(2, 0), new HashSet<char> { '#' }, true);
            Add(new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(0, 1), new HashSet<char> { '#' }, false);
            Add(new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(1, 1), new HashSet<char> { '#' }, true);
            Add(new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(2, 1), new HashSet<char> { '#' }, false);
            Add(new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(0, 2), new HashSet<char> { '#' }, true);
            Add(new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(1, 2), new HashSet<char> { '#' }, false);
            Add(new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(2, 2), new HashSet<char> { '#' }, false);
            Add(new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(1, 0), new HashSet<char> { '.' }, true);
        }
    }
}
