using System.Runtime.CompilerServices;
using AoC.Grids;
using AoC.Maps;

namespace AoC.Extensions;

/// <summary>
/// Extension methods for input.
/// </summary>
public static class InputExtensions
{
    /// <summary>
    /// Reads the non-empty lines from a stream.
    /// </summary>
    /// <param name="reader">The reader to use.</param>
    /// <param name="lineCount">The number of lines to read.</param>
    /// <param name="cancellationToken">The cancellation token to cancel this operation.</param>
    /// <returns>The non-empty lines from <paramref name="reader"/>.</returns>
    public static async IAsyncEnumerable<string> ReadLinesSkipEmpty(
        this StreamReader reader,
        long lineCount = long.MaxValue,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        for (var i = 0L; i < lineCount && !reader.EndOfStream; i++)
        {
            var line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(line))
                yield return line;
        }
    }

    /// <summary>
    /// Reads a <see cref="Grid{T}"/> of characters.
    /// </summary>
    /// <param name="reader">The reader to use.</param>
    /// <param name="lineCount">The number of lines to read.</param>
    /// <param name="cancellationToken">The cancellation token to cancel this operation.</param>
    /// <returns>The read <see cref="Grid{T}"/> of characters.</returns>
    public static async Task<Grid<char>> ReadCharacterGrid(
        this StreamReader reader,
        long lineCount = long.MaxValue,
        CancellationToken cancellationToken = default)
    {
        var lines = new List<string>();
        await foreach (var line in reader.ReadLinesSkipEmpty(lineCount, cancellationToken).ConfigureAwait(false))
            lines.Add(line);

        var chars = lines.SelectMany(x => x).ToArray();
        return new Grid<char>(chars, lines[0].Length);
    }

    /// <summary>
    /// Reads a <see cref="CharacterMap"/>.
    /// </summary>
    /// <param name="reader">The reader to use.</param>
    /// <param name="start">The start tile character.</param>
    /// <param name="end">The end tile character.</param>
    /// <param name="lineCount">The number of lines to read.</param>
    /// <param name="cancellationToken">The cancellation token to cancel this operation.</param>
    /// <returns>The read <see cref="CharacterMap"/>.</returns>
    public static async Task<CharacterMap> ReadCharacterMap(
        this StreamReader reader,
        char start = 'S',
        char end = 'E',
        long lineCount = long.MaxValue,
        CancellationToken cancellationToken = default)
    {
        var lines = new List<string>();
        await foreach (var line in reader.ReadLinesSkipEmpty(lineCount, cancellationToken).ConfigureAwait(false))
            lines.Add(line);
        return new CharacterMap(lines, start, end);
    }
}
