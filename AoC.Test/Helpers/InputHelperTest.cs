using System.Collections;
using AoC.Helpers;

namespace AoC.Test.Helpers;

/// <summary>
/// Unit tests for <see cref="InputHelper"/>.
/// </summary>
public class InputHelperTest
{
    [Theory]
    [ClassData(typeof(ReadGridFileTestData))]
    public async Task ReadGridFile_ReturnsCorrectValue(int expectedWidth, int expectedHeight, string[] lines, char[] expectedData)
    {
        // arrange
        var filename = $"{Guid.NewGuid()}.txt";
        await File.WriteAllLinesAsync(filename, lines);

        // act
        var result = await InputHelper.ReadGrid(filename);

        // assert
        Assert.Equal(expectedWidth, result.Width);
        Assert.Equal(expectedHeight, result.Height);
        Assert.Equal(expectedWidth * expectedHeight, result.Data.Count);
        foreach (var item in expectedData)
            Assert.NotNull(result.Find(item));
    }

    [Theory]
    [ClassData(typeof(ReadGridStreamTestData))]
    public async Task ReadGridStream_ReturnsCorrectValue(int lineCount, int expectedWidth, int expectedHeight, string[] lines, char[] expectedData)
    {
        // arrange
        var filename = $"{Guid.NewGuid()}.txt";
        await File.WriteAllLinesAsync(filename, lines);
        using var reader = new StreamReader(filename);

        // act
        var result = await InputHelper.ReadGrid(reader, lineCount);

        // assert
        Assert.Equal(expectedWidth, result.Width);
        Assert.Equal(expectedHeight, result.Height);
        Assert.Equal(expectedWidth * expectedHeight, result.Data.Count);
        foreach (var item in expectedData)
            Assert.NotNull(result.Find(item));
    }

    /// <summary>
    /// Test data for <see cref="InputHelperTest.ReadGridFile_ReturnsCorrectValue"/>.
    /// </summary>
    private class ReadGridFileTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [3, 3, new[] { "123", "456", "789" }, new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' }];
            yield return [3, 3, new[] { "123", "456", "7" }, new[] { '1', '2', '3', '4', '5', '6', '7' }];
            yield return [3, 3, new[] { "", "123", "456", "", "7", "" }, new[] { '1', '2', '3', '4', '5', '6', '7' }];
        }
    }

    /// <summary>
    /// Test data for <see cref="InputHelperTest.ReadGridStream_ReturnsCorrectValue"/>.
    /// </summary>
    private class ReadGridStreamTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [int.MaxValue, 3, 3, new[] { "123", "456", "789" }, new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' }];
            yield return [3, 3, 3, new[] { "123", "456", "789" }, new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' }];
            yield return [2, 3, 2, new[] { "123", "456", "789" }, new[] { '1', '2', '3', '4', '5', '6' }];
            yield return [1, 3, 1, new[] { "123", "456", "789" }, new[] { '1', '2', '3' }];
            yield return [1, 3, 1, new[] { "", "123", "456", "", "7", "" }, new[] { '1', '2', '3' }];
        }
    }
}
