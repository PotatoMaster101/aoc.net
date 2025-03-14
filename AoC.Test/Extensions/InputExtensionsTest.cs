using AoC.Extensions;
using AoC.Geometry;

namespace AoC.Test.Extensions;

/// <summary>
/// Unit tests for <see cref="InputExtensions"/>.
/// </summary>
public class InputExtensionsTest
{
    [Theory]
    [ClassData(typeof(ReadLinesSkipEmptyTestData))]
    public async Task ReadLinesSkipEmpty_ReturnsCorrectValue(string[] lines, long lineCount, string[] expected)
    {
        // arrange
        var filename = $"{Guid.NewGuid()}.txt";
        await File.WriteAllLinesAsync(filename, lines, TestContext.Current.CancellationToken);
        using var reader = new StreamReader(filename);

        // act
        var result = new List<string>();
        await foreach (var line in reader.ReadLinesSkipEmpty(lineCount, TestContext.Current.CancellationToken))
            result.Add(line);

        // assert
        Assert.Equal(expected.Length, result.Count);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(ReadCharacterGridTestData))]
    public async Task ReadCharacterGrid_ReturnsCorrectValue(string[] lines, long lineCount, int expectedWidth, int expectedHeight, char[] expectedData)
    {
        // arrange
        var filename = $"{Guid.NewGuid()}.txt";
        await File.WriteAllLinesAsync(filename, lines, TestContext.Current.CancellationToken);
        using var reader = new StreamReader(filename);

        // act
        var result = await reader.ReadCharacterGrid(lineCount, TestContext.Current.CancellationToken);

        // assert
        Assert.Equal(expectedWidth, result.Width);
        Assert.Equal(expectedHeight, result.Height);
        Assert.Equal(expectedData.Length, result.Data.Count);
        for (var i = 0; i < expectedData.Length; i++)
            Assert.Equal(expectedData[i], result.Data[i]);
    }

    [Theory]
    [ClassData(typeof(ReadCharacterMapTestData))]
    public async Task ReadCharacterMap_ReturnsCorrectValue(
        string[] lines,
        char start,
        char end,
        long lineCount,
        int expectedWidth,
        int expectedHeight,
        char[] expectedData,
        Position<int> expectedStart,
        Position<int> expectedEnd)
    {
        // arrange
        var filename = $"{Guid.NewGuid()}.txt";
        await File.WriteAllLinesAsync(filename, lines, TestContext.Current.CancellationToken);
        using var reader = new StreamReader(filename);

        // act
        var result = await reader.ReadCharacterMap(start, end, lineCount, TestContext.Current.CancellationToken);

        // assert
        Assert.Equal(expectedWidth, result.Grid.Width);
        Assert.Equal(expectedHeight, result.Grid.Height);
        Assert.Equal(expectedData.Length, result.Grid.Data.Count);
        Assert.Equal(expectedStart, result.Start);
        Assert.Equal(expectedEnd, result.End);
        for (var i = 0; i < expectedData.Length; i++)
            Assert.Equal(expectedData[i], result.Grid.Data[i]);
    }

    /// <summary>
    /// Test data for <see cref="InputExtensionsTest.ReadLinesSkipEmpty_ReturnsCorrectValue"/>.
    /// </summary>
    private class ReadLinesSkipEmptyTestData : TheoryData<string[], long, string[]>
    {
        public ReadLinesSkipEmptyTestData()
        {
            Add(["a", "b", "c"], 3L, ["a", "b", "c"]);
            Add(["a", "b", "c"], 2L, ["a", "b"]);
            Add(["a", "", "b", "c"], 3L, ["a", "b"]);
            Add(["a", "", "b", "c"], 4L, ["a", "b", "c"]);
            Add(["", "a", "", "b", "c"], 4L, ["a", "b"]);
            Add(["", "a", "", "b", "c"], long.MaxValue, ["a", "b", "c"]);
        }
    }

    /// <summary>
    /// Test data for <see cref="InputExtensionsTest.ReadCharacterGrid_ReturnsCorrectValue"/>.
    /// </summary>
    private class ReadCharacterGridTestData : TheoryData<string[], long, int, int, char[]>
    {
        public ReadCharacterGridTestData()
        {
            Add(["abc", "def", "ghi"], 3L, 3, 3, ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i']);
            Add(["abc", "def", "ghi"], long.MaxValue, 3, 3, ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i']);
            Add(["abc", "def", "ghi"], 2L, 3, 2, ['a', 'b', 'c', 'd', 'e', 'f']);
        }
    }

    /// <summary>
    /// Test data for <see cref="InputExtensionsTest.ReadCharacterMap_ReturnsCorrectValue"/>.
    /// </summary>
    private class ReadCharacterMapTestData : TheoryData<string[], char, char, long, int, int, char[], Position<int>, Position<int>>
    {
        public ReadCharacterMapTestData()
        {
            Add(["S..", "...", "..E"], 'S', 'E', 3L, 3, 3, ['S', '.', '.', '.', '.', '.', '.', '.', 'E'], new Position<int>(0, 0), new Position<int>(2, 2));
            Add(["S..", "...", "..E"], 'S', 'E', long.MaxValue, 3, 3, ['S', '.', '.', '.', '.', '.', '.', '.', 'E'], new Position<int>(0, 0), new Position<int>(2, 2));
            Add(["S..", "...", "..E"], 'S', 'E', 2L, 3, 2, ['S', '.', '.', '.', '.', '.'], new Position<int>(0, 0), new Position<int>(0, 0));
        }
    }
}
