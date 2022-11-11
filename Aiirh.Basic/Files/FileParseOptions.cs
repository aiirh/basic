using System;

namespace Aiirh.Basic.Files
{
    public class FileParseOptions<T>
    {
        public FileParseOptions(Func<string[], T> mapper, char separator, bool hasHeader)
        {
            SkipRows = hasHeader ? 1 : 0;
            Separator = separator;
            Mapper = mapper;
        }

        public FileParseOptions(Func<string[], T> mapper, char separator, int skipRows)
        {
            SkipRows = skipRows;
            Separator = separator;
            Mapper = mapper;
        }

        public char Separator { get; }

        public Func<string[], T> Mapper { get; }

        public int SkipRows { get; }
    }
}
