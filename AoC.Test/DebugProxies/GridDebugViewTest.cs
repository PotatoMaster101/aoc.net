using AoC.DebugProxies;
using AoC.Grids;

namespace AoC.Test.DebugProxies;

/// <summary>
/// Unit tests for <see cref="GridDebugView{T}"/>
/// </summary>
public class GridDebugViewTest
{
    [Theory]
    [ClassData(typeof(RowsTestData))]
    public void Rows_ReturnsCorrectValue(int[] data, int width, int[][] expected)
    {
        // arrange
        var grid = new Grid<int>(data, width);
        var sut = new GridDebugView<int>(grid);

        // act
        var result = sut.Rows;

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i].Length, result[i].Length);
            for (var j = 0; j < expected.Length; j++)
                Assert.Equal(expected[j], result[j]);
        }
    }

    /// <summary>
    /// Test data for <see cref="GridDebugViewTest.Rows_ReturnsCorrectValue"/>.
    /// </summary>
    private class RowsTestData : TheoryData<int[], int, int[][]>
    {
        public RowsTestData()
        {
            Add([1, 2, 3, 4, 5, 6, 7, 8, 9], 3, [[1, 2, 3], [4, 5, 6], [7, 8, 9]]);
            Add([1, 2, 3, 4, 5, 6], 3, [[1, 2, 3], [4, 5, 6]]);
            Add([1, 2, 3, 4, 5, 6], 2, [[1, 2], [3, 4], [5, 6]]);
        }
    }
}
