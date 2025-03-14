using System.Collections;
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
    public void IsWall_ReturnsCorrectValue(Grid<char> grid, Position<int> position, bool expected)
    {
        // arrange
        var sut = new CharacterMap(grid);

        // act
        var result = sut.IsWall(position);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(IsPathTestData))]
    public void IsPath_ReturnsCorrectValue(Grid<char> grid, Position<int> position, bool expected)
    {
        // arrange
        var sut = new CharacterMap(grid);

        // act
        var result = sut.IsPath(position);

        // assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Test data for <see cref="CharacterMapTest.Constructor_SetsCorrectValues"/>.
    /// </summary>
    private class ConstructorTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                new Grid<char>(['S', '.', '.', '.', '.', '.', '.', '.', 'E'], 3), 'S', 'E',
                new Position<int>(0, 0), new Position<int>(2, 2)
            ];
            yield return
            [
                new Grid<char>(['S', '.', '.', '.', '.', '.', '.', '.', 'E'], 3), '.', '.',
                new Position<int>(1, 0), new Position<int>(1, 0)
            ];
            yield return
            [
                new Grid<char>(['S', '.', 'S', '.', '.', '.', 'E', '.', 'E'], 3), 'S', 'E',
                new Position<int>(0, 0), new Position<int>(0, 2)
            ];
            yield return
            [
                new Grid<char>(['S', '.', 'S', '.', '.', '.', 'E', '.', 'E'], 3), 'X', 'Y',
                new Position<int>(0, 0), new Position<int>(0, 0)
            ];
        }
    }

    /// <summary>
    /// Test data for <see cref="CharacterMapTest.ArrayConstructor_SetsCorrectValues"/>.
    /// </summary>
    private class ArrayConstructorTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [new[]{"S..", "...", "..E"}, 'S', 'E', new Position<int>(0, 0), new Position<int>(2, 2)];
            yield return [new[]{"S..", "...", "..E"}, '.', '.', new Position<int>(1, 0), new Position<int>(1, 0)];
            yield return [new[]{"S.S", "...", "E.E"}, 'S', 'E', new Position<int>(0, 0), new Position<int>(0, 2)];
            yield return [new[]{"S..", "...", "..E"}, 'X', 'Y', new Position<int>(0, 0), new Position<int>(0, 0)];
        }
    }

    /// <summary>
    /// Test data for <see cref="CharacterMapTest.IsWall_ReturnsCorrectValue"/>.
    /// </summary>
    private class IsWallTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(0, 0), false];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(1, 0), false];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(2, 0), true];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(0, 1), false];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(1, 1), true];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(2, 1), false];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(0, 2), true];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(1, 2), false];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(2, 2), false];
        }
    }

    /// <summary>
    /// Test data for <see cref="CharacterMapTest.IsPath_ReturnsCorrectValue"/>.
    /// </summary>
    private class IsPathTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(0, 0), true];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(1, 0), true];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(2, 0), false];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(0, 1), true];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(1, 1), false];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(2, 1), true];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(0, 2), false];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(1, 2), true];
            yield return [new Grid<char>(['S', '.', '#', '.', '#', '.', '#', '.', 'E'], 3), new Position<int>(2, 2), true];
        }
    }
}
