using AoC.Geometry;

namespace AoC.Helpers;

/// <summary>
/// Helper methods for puzzle input.
/// </summary>
public static class InputHelper
{
    /// <summary>
    /// Reads a file and returns the <see cref="Grid{T}"/>
    /// </summary>
    /// <param name="file">The file path.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The <see cref="Grid{T}"/> read.</returns>
    public static async Task<Grid<char>> ReadGrid(string file, CancellationToken cancellationToken = default)
    {
        var lines = await File.ReadAllLinesAsync(file, cancellationToken).ConfigureAwait(false);
        var notEmpty = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        var chars = notEmpty.SelectMany(x => x).ToArray();
        return new Grid<char>(chars, notEmpty[0].Length);
    }

    /// <summary>
    /// Reads a file and returns the <see cref="Grid{T}"/>
    /// </summary>
    /// <param name="reader">The file stream reader.</param>
    /// <param name="lineCount">The max number of lines to read.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The <see cref="Grid{T}"/> read.</returns>
    public static async Task<Grid<char>> ReadGrid(StreamReader reader, int lineCount = int.MaxValue, CancellationToken cancellationToken = default)
    {
        var lines = new List<string>();
        var count = 0;
        while (!reader.EndOfStream && count < lineCount)
        {
            var line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);
            if (string.IsNullOrEmpty(line))
                continue;

            lines.Add(line);
            count++;
        }

        var chars = lines.SelectMany(x => x).ToArray();
        return new Grid<char>(chars, lines[0].Length);
    }
}
