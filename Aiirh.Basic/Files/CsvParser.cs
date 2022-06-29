using Aiirh.Basic.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiirh.Basic.Files
{
    public static class CsvParser
    {
        /// <summary>
        /// Parses CSV file to rows and columns. Also take into account quoted columns.
        /// </summary>
        /// <param name="data">Input file data as byte array.</param>
        /// <param name="options">Options.</param>
        /// <returns>Collection of parsed T objects.</returns>
        public static async Task<IEnumerable<T>> Parse<T>(byte[] data, FileParseOptions<T> options)
        {
            var unzipped = await data.DecompressIfGZipAsync();
            var separator = options.Separator;
            var s = Encoding.UTF8.GetString(unzipped);
            var quoteStarted = false;
            var row = new List<string>();
            var columns = new List<string[]>();
            var buf = new StringBuilder();

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];
                var nextChar = i < s.Length - 1 ? s[i + 1] : '\0';
                if (c == '"')
                {
                    quoteStarted = !quoteStarted;
                }
                else if (!quoteStarted && c == separator)
                {
                    row.Add(buf.ToString());
                    buf.Clear();
                }
                else if (!quoteStarted && c == '\n')
                {
                    row.Add(buf.ToString());
                    buf.Clear();
                    columns.Add(row.ToArray());
                    row = new List<string>();
                }
                else if (quoteStarted && (nextChar == separator || nextChar == '\n'))
                {
                    // When we have extra quote in the value field then ignore that
                    quoteStarted = false;
                }
                else if (!quoteStarted && c == '\r')
                {
                    // Ignore
                }
                else if (i == s.Length - 1)
                {
                    buf.Append(c);
                    row.Add(buf.ToString());
                    buf.Clear();
                    columns.Add(row.ToArray());
                    row = new List<string>();
                }
                else
                {
                    buf.Append(c);
                }
            }

            columns.Add(row.ToArray());

            if (columns.Last().Length == 0)
            {
                columns.RemoveAt(columns.Count - 1);
            }

            var skipRowsCount = options.HasHeader ? 1 : 0;
            return columns.Skip(skipRowsCount).Select(options.Mapper);
        }
    }
}
