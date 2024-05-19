using System.Diagnostics;

namespace Aiirh.Basic.Utilities;

public static class DiagnosticsUtility
{
    public static long ElapsedAndReset(this Stopwatch sw)
    {
        var elapsed = sw.ElapsedMilliseconds;
        sw.Restart();
        return elapsed;
    }
}