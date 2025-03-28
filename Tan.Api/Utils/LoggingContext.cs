namespace Tan.Api.Utils;

public static class LoggingContext
{
    private static readonly AsyncLocal<string?> _traceId = new();

    public static string? TraceId
    {
        get => _traceId.Value;
        set
        {
            _traceId.Value = value;
            NLog.MappedDiagnosticsContext.Set("TraceId", value);
        }
    }

    public static void Clear()
    {
        _traceId.Value = null;
        NLog.MappedDiagnosticsContext.Remove("TraceId");
    }
}
