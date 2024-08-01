namespace RainWorldSaveAPI;

internal enum LogReportType
{
    /// <summary>
    /// Use for log printing
    /// </summary>
    Info,
    /// <summary>
    /// Use for operations that may result in incorrect behavior
    /// </summary>
    Warn,
    /// <summary>
    /// Use for operations that will result in incorrect behavior
    /// </summary>
    Error,

    Debug,
    Trace
}

public static class Logger
{
    /// <summary>
    /// Stream that all internal log operations will be written to
    /// </summary>
    public static StreamWriter LogStreamWriter = null!;

    internal static void Info(string message) => WriteLine(LogReportType.Info, message);
    internal static void Warn(string message) => WriteLine(LogReportType.Warn, message);
    internal static void Error(string message) => WriteLine(LogReportType.Error, message);
    internal static void Debug(string message) => WriteLine(LogReportType.Debug, message);
    internal static void Trace(string message) => WriteLine(LogReportType.Trace, message);

    internal static void ReadAttempt(string filepath) => Info($"Attempting to read file: \"{filepath}\"");
    internal static void WriteAttempt(string filepath) => Info($"Attempting to write file: \"{filepath}\"");
    internal static void Success() => Info($"Success!");
    internal static void Exception(Exception ex) => Error($"An exception has occured! Details are as follows:\n{ex.Message}");

    internal static void WriteLine(LogReportType reportType, string message)
    {
        string header = reportType switch
        {
            LogReportType.Info => "[INFO ] ",
            LogReportType.Warn => "[WARN ] ",
            LogReportType.Error => "[ERROR] ",
            LogReportType.Debug => "[DEBUG] ",
            LogReportType.Trace => "[TRACE] ",
            _ => "[????]"
        };

        LogStreamWriter?.Write(header + message);

        Console.ForegroundColor = reportType switch
        {
            LogReportType.Info => ConsoleColor.Gray,
            LogReportType.Warn => ConsoleColor.Yellow,
            LogReportType.Error => ConsoleColor.Red,
            LogReportType.Debug => ConsoleColor.Blue,
            LogReportType.Trace => ConsoleColor.White,
            _ => Console.ForegroundColor,
        };
        Console.Write(header);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(message);
    }
}
