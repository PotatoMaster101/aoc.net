using System.Collections;
using AoC.Extensions;
using AoC.Geometry;

namespace AoC.Test.Extensions;

/// <summary>
/// Unit tests for <see cref="InputExtensions"/>.
/// </summary>
public class InputExtensionsTest
{
    [Theory]
    [ClassData(typeof(ReadToEndSkipEmptyTestData))]
    public async Task ReadToEndSkipEmpty_ReturnsCorrectValue(string[] lines, int lineCount, string[] expected)
    {
        // arrange
        var filename = $"{Guid.NewGuid()}.txt";
        await File.WriteAllLinesAsync(filename, lines);
        using var reader = new StreamReader(filename);

        // act
        var result = new List<string>();
        await foreach (var line in reader.ReadToEndSkipEmpty(lineCount))
            result.Add(line);

        // assert
        Assert.Equal(expected.Length, result.Count);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [ClassData(typeof(ReadCharacterGridTestData))]
    public async Task ReadCharacterGrid_ReturnsCorrectValue(string[] lines, int lineCount, int expectedWidth, int expectedHeight, char[] expectedData)
    {
        // arrange
        var filename = $"{Guid.NewGuid()}.txt";
        await File.WriteAllLinesAsync(filename, lines);
        using var reader = new StreamReader(filename);

        // act
        var result = await reader.ReadCharacterGrid(lineCount);

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
        int lineCount,
        int expectedWidth,
        int expectedHeight,
        char[] expectedData,
        Position<int> expectedStart,
        Position<int> expectedEnd)
    {
        // arrange
        var filename = $"{Guid.NewGuid()}.txt";
        await File.WriteAllLinesAsync(filename, lines);
        using var reader = new StreamReader(filename);

        // act
        var result = await reader.ReadCharacterMap(start, end, lineCount);

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
    /// Test data for <see cref="InputExtensionsTest.ReadToEndSkipEmpty_ReturnsCorrectValue"/>.
    /// </summary>
    private class ReadToEndSkipEmptyTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [new[] { "a", "b", "c" }, 3, new[] { "a", "b", "c" }];
            yield return [new[] { "a", "b", "c" }, 2, new[] { "a", "b" }];
            yield return [new[] { "a", "", "b", "c" }, 3, new[] { "a", "b" }];
            yield return [new[] { "a", "", "b", "c" }, 4, new[] { "a", "b", "c" }];
            yield return [new[] { "", "a", "", "b", "c" }, 4, new[] { "a", "b" }];
            yield return [new[] { "", "a", "", "b", "c" }, int.MaxValue, new[] { "a", "b", "c" }];
        }
    }

    /// <summary>
    /// Test data for <see cref="InputExtensionsTest.ReadCharacterGrid_ReturnsCorrectValue"/>.
    /// </summary>
    private class ReadCharacterGridTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                new[] { "abc", "def", "ghi" }, 3, 3, 3, new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i'}
            ];
            yield return
            [
                new[] { "abc", "def", "ghi" }, int.MaxValue, 3, 3, new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i'}
            ];
            yield return
            [
                new[] { "abc", "def", "ghi" }, 2, 3, 2, new[] { 'a', 'b', 'c', 'd', 'e', 'f'}
            ];
        }
    }

    /// <summary>
    /// Test data for <see cref="InputExtensionsTest.ReadCharacterMap_ReturnsCorrectValue"/>.
    /// </summary>
    private class ReadCharacterMapTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                new[] { "S..", "...", "..E" }, 'S', 'E', 3, 3, 3, new[] { 'S', '.', '.', '.', '.', '.', '.', '.', 'E' },
                new Position<int>(0, 0), new Position<int>(2, 2)
            ];
            yield return
            [
                new[] { "S..", "...", "..E" }, 'S', 'E', int.MaxValue, 3, 3, new[] { 'S', '.', '.', '.', '.', '.', '.', '.', 'E' },
                new Position<int>(0, 0), new Position<int>(2, 2)
            ];
            yield return
            [
                new[] { "S..", "...", "..E" }, 'S', 'E', 2, 3, 2, new[] { 'S', '.', '.', '.', '.', '.' },
                new Position<int>(0, 0), new Position<int>(0, 0)
            ];
        }
    }
}
