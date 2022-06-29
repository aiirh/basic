using System;

namespace Aiirh.Basic.Files
{
    public class FileParseOptions<T>
    {
        public FileParseOptions(Func<string[], T> mapper, char separator, bool hasHeader)
        {
            HasHeader = hasHeader;
            Separator = separator;
            Mapper = mapper;
        }

        public char Separator { get; }

        public Func<string[], T> Mapper { get; }

        public bool HasHeader { get; }
    }
}
