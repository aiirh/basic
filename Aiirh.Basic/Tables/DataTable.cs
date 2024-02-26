using System.Collections.Generic;

namespace Aiirh.Basic.Tables;

public class DataTable
{
    public IList<string> Headers { get; set; }

    public IList<object[]> Rows { get; set; }
}