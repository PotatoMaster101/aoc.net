using AoC.Geometry;

namespace AoC.Test.Geometry;

/// <summary>
/// Unit tests for <see cref="DirectionalPosition{T}"/>
/// </summary>
public class DirectionalPositionTest
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(1, -1)]
    public void Constructor_SetsCorrectValues(int dirX, int dirY)
    {
        // act
        var sut = new DirectionalPosition<int>(new Position<int>(dirX, dirY));

        // assert
        Assert.Equal(dirX, sut.Direction.X);
        Assert.Equal(dirY, sut.Direction.Y);
        Assert.Equal(0, sut.Position.X);
        Assert.Equal(0, sut.Position.Y);
    }

    [Theory]
    [InlineData(0, 0, 1, 0, 1, 1, 0)]
    [InlineData(0, 0, 1, 0, 2, 2, 0)]
    [InlineData(0, 0, 2, 2, 2, 4, 4)]
    [InlineData(3, 4, 1, -1, 3, 6, 1)]
    [InlineData(3, 4, 1, -1, -5, -2, 9)]
    [InlineData(0, 0, 1, 0, 0, 1, 0)]
    public void Next_ReturnsCorrectValue(int posX, int posY, int dirX, int dirY, int distance, int expectedPosX, int expectedPosY)
    {
        // arrange
        var sut = new DirectionalPosition<int>(new Position<int>(posX, posY), new Position<int>(dirX, dirY));

        // act
        var result = sut.Next(distance);

        // assert
        Assert.Equal(expectedPosX, result.Position.X);
        Assert.Equal(expectedPosY, result.Position.Y);
        Assert.Equal(sut.Direction.X, result.Direction.X);
        Assert.Equal(sut.Direction.Y, result.Direction.Y);
    }

    [Theory]
    [ClassData(typeof(NextRangeTestData))]
    public void NextRange_ReturnsCorrectValue(int posX, int posY, int dirX, int dirY, long count, int distance, DirectionalPosition<int>[] expected)
    {
        // arrange
        var sut = new DirectionalPosition<int>(new Position<int>(posX, posY), new Position<int>(dirX, dirY));

        // act
        var result = sut.NextRange(count, distance).ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Theory]
    [InlineData(1, 1, 0, 1, 1)]
    [InlineData(1, 1, 1, 1, -1)]
    [InlineData(1, 1, 2, -1, -1)]
    [InlineData(1, 1, 3, -1, 1)]
    [InlineData(1, 1, 4, 1, 1)]
    [InlineData(1, 1, 5, 1, -1)]
    [InlineData(1, 1, -1, -1, 1)]
    [InlineData(1, 1, -4, 1, 1)]
    public void Rotate_ReturnsCorrectValue(int dirX, int dirY, int turns, int expectedDirX, int expectedDirY)
    {
        // arrange
        var sut = new DirectionalPosition<int>(new Position<int>(0, 0), new Position<int>(dirX, dirY));

        // act
        var result = sut.Rotate(turns);

        // assert
        Assert.Equal(0, result.Position.X);
        Assert.Equal(0, result.Position.Y);
        Assert.Equal(expectedDirX, result.Direction.X);
        Assert.Equal(expectedDirY, result.Direction.Y);
    }

    [Theory]
    [InlineData(1, 3, 3, -1)]
    [InlineData(5, -9, -9, -5)]
    [InlineData(-5, 9, 9, 5)]
    [InlineData(-5, -9, -9, 5)]
    public void Rotate90_ReturnsCorrectValue(int dirX, int dirY, int expectedDirX, int expectedDirY)
    {
        // arrange
        var sut = new DirectionalPosition<int>(new Position<int>(0, 0), new Position<int>(dirX, dirY));

        // act
        var result = sut.Rotate90();

        // assert
        Assert.Equal(0, result.Position.X);
        Assert.Equal(0, result.Position.Y);
        Assert.Equal(expectedDirX, result.Direction.X);
        Assert.Equal(expectedDirY, result.Direction.Y);
    }

    [Theory]
    [InlineData(1, 3, -1, -3)]
    [InlineData(5, -9, -5, 9)]
    [InlineData(-5, 9, 5, -9)]
    [InlineData(-5, -9, 5, 9)]
    public void Rotate180_ReturnsCorrectValue(int dirX, int dirY, int expectedDirX, int expectedDirY)
    {
        // arrange
        var sut = new DirectionalPosition<int>(new Position<int>(0, 0), new Position<int>(dirX, dirY));

        // act
        var result = sut.Rotate180();

        // assert
        Assert.Equal(0, result.Position.X);
        Assert.Equal(0, result.Position.Y);
        Assert.Equal(expectedDirX, result.Direction.X);
        Assert.Equal(expectedDirY, result.Direction.Y);
    }

    [Theory]
    [InlineData(1, 3, -3, 1)]
    [InlineData(5, -9, 9, 5)]
    [InlineData(-5, 9, -9, -5)]
    [InlineData(-5, -9, 9, -5)]
    public void Rotate270_ReturnsCorrectValue(int dirX, int dirY, int expectedDirX, int expectedDirY)
    {
        // arrange
        var sut = new DirectionalPosition<int>(new Position<int>(0, 0), new Position<int>(dirX, dirY));

        // act
        var result = sut.Rotate270();

        // assert
        Assert.Equal(0, result.Position.X);
        Assert.Equal(0, result.Position.Y);
        Assert.Equal(expectedDirX, result.Direction.X);
        Assert.Equal(expectedDirY, result.Direction.Y);
    }

    [Theory]
    [InlineData(0, 0, 1, 0)]
    [InlineData(3, -4, 2, 1)]
    public void ToPositionConversion_ReturnsCorrectValue(int posX, int posY, int dirX, int dirY)
    {
        // arrange
        var sut = new DirectionalPosition<int>(new Position<int>(posX, posY), new Position<int>(dirX, dirY));

        // act
        var result = (Position<int>)sut;

        // assert
        Assert.Equal(posX, result.X);
        Assert.Equal(posY, result.Y);
    }

    /// <summary>
    /// Test data for <see cref="DirectionalPositionTest.NextRange_ReturnsCorrectValue"/>.
    /// </summary>
    private class NextRangeTestData : TheoryData<int, int, int, int, long, int, DirectionalPosition<int>[]>
    {
        public NextRangeTestData()
        {
            Add(0, 0, 0, 3, 0, 1, []);
            Add(0, 0, 0, 3, -1, 1, []);
            Add(0, 0, 1, 0, 3, 1, [
                new DirectionalPosition<int>(new Position<int>(1, 0), new Position<int>(1, 0)),
                new DirectionalPosition<int>(new Position<int>(2, 0), new Position<int>(1, 0)),
                new DirectionalPosition<int>(new Position<int>(3, 0), new Position<int>(1, 0)),
            ]);
            Add(0, 0, 1, 0, 5, 2, [
                new DirectionalPosition<int>(new Position<int>(2, 0), new Position<int>(1, 0)),
                new DirectionalPosition<int>(new Position<int>(4, 0), new Position<int>(1, 0)),
                new DirectionalPosition<int>(new Position<int>(6, 0), new Position<int>(1, 0)),
                new DirectionalPosition<int>(new Position<int>(8, 0), new Position<int>(1, 0)),
                new DirectionalPosition<int>(new Position<int>(10, 0), new Position<int>(1, 0)),
            ]);
            Add(0, 0, 0, 3, 5, 1, [
                new DirectionalPosition<int>(new Position<int>(0, 3), new Position<int>(0, 3)),
                new DirectionalPosition<int>(new Position<int>(0, 6), new Position<int>(0, 3)),
                new DirectionalPosition<int>(new Position<int>(0, 9), new Position<int>(0, 3)),
                new DirectionalPosition<int>(new Position<int>(0, 12), new Position<int>(0, 3)),
                new DirectionalPosition<int>(new Position<int>(0, 15), new Position<int>(0, 3)),
            ]);
            Add(-5, 5, 1, 1, 3, 2, [
                new DirectionalPosition<int>(new Position<int>(-3, 7), new Position<int>(1, 1)),
                new DirectionalPosition<int>(new Position<int>(-1, 9), new Position<int>(1, 1)),
                new DirectionalPosition<int>(new Position<int>(1, 11), new Position<int>(1, 1)),
            ]);
        }
    }
}
