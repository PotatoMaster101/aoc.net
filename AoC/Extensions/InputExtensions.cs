using System.Runtime.CompilerServices;
using AoC.Grids;

namespace AoC.Extensions;

/// <summary>
/// Extension methods for input.
/// </summary>
public static class InputExtensions
{
    extension(StreamReader reader)
    {
        /// <summary>
        /// Reads the non-empty lines from a stream.
        /// </summary>
        /// <param name="lineCount">The number of lines to read.</param>
        /// <param name="cancellationToken">The cancellation token to cancel this operation.</param>
        /// <returns>The non-empty lines from <paramref name="reader"/>.</returns>
        public async IAsyncEnumerable<string> ReadLinesSkipEmpty(long lineCount = long.MaxValue, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            for (var i = 0L; i < lineCount; i++)
            {
                var line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);
                if (line is null)
                    yield break;
                if (!string.IsNullOrEmpty(line))
                    yield return line;
            }
        }

        /// <summary>
        /// Reads a <see cref="Grid{T}"/> of characters.
        /// </summary>
        /// <param name="lineCount">The number of lines to read.</param>
        /// <param name="cancellationToken">The cancellation token to cancel this operation.</param>
        /// <returns>The read <see cref="Grid{T}"/> of characters.</returns>
        public async Task<Grid<char>> ReadCharacterGrid(long lineCount = long.MaxValue, CancellationToken cancellationToken = default)
        {
            var lines = new List<string>();
            await foreach (var line in reader.ReadLinesSkipEmpty(lineCount, cancellationToken).ConfigureAwait(false))
                lines.Add(line);

            var chars = lines.SelectMany(x => x).ToArray();
            return new Grid<char>(chars, lines[0].Length);
        }
    }
}
