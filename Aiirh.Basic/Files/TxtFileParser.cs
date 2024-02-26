using Aiirh.Basic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiirh.Basic.Files;

public class TxtFileParser
{
    [Obsolete("This method internally tries to decompress GZip. Logic will be removed in future")]
    public static async Task<IEnumerable<T>> Parse<T>(byte[] data, FileParseOptions<T> options)
    {
            var unzipped = await data.DecompressIfGZipAsync();
            var fileAsString = Encoding.UTF8.GetString(unzipped);
            var fileLines = fileAsString.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var minRowsCount = options.SkipRows;
            if (fileLines.Length <= minRowsCount)
            {
                return Enumerable.Empty<T>();
            }

            return fileLines.Skip(minRowsCount).Select(x => x.Split(options.Separator).ToArray()).Select(options.Mapper);
        }
}