using System.Runtime.InteropServices;

namespace RainWorldSaveEditor;

public enum LogReportType
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
    [DllImport("Kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AttachConsole(int processId);

    [DllImport("Kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AllocConsole();

    [DllImport("Kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool FreeConsole();

    private const int ATTACH_PARENT_PROCESS = -1;
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    const int SW_HIDE = 0;
    const int SW_SHOW = 5;

    private static bool _consoleShown = false;
    public static bool ConsoleShown
    {
        get => _consoleShown;
        set
        {
            if (!_consoleAllocated)
                AllocateCMD();

            _consoleShown = value;

            ShowWindow(GetConsoleWindow(), value ? SW_SHOW : SW_HIDE);
        }
    }

    private static bool _consoleAllocated = false;
    /// <summary>
    /// Attached the console, or allocated a new one if it can't
    /// </summary>
    /// <returns></returns>
    public static bool AllocateCMD()
    {
        if (!AttachConsole(ATTACH_PARENT_PROCESS))
            AllocConsole();

        _consoleShown = true;
        _consoleAllocated = true;
        return true;
    }

    /// <summary>
    /// Frees the currently allocated console
    /// </summary>
    /// <returns></returns>
    public static bool FreeCMD()
    {
        _consoleShown = false;
        _consoleAllocated = false;
        return FreeConsole();
    }
    public static bool ConsoleAllocated => _consoleAllocated;

    private static bool _logFileOpen = false;
    private static string _logName = string.Empty;
    private static FileStream _logStream = null!;
    public static StreamWriter LogStreamWriter { get; private set; } = null!;

    public static void OpenLogFile()
    {
        if (_logFileOpen)
            return;

        if (!Directory.Exists("logs"))
            Directory.CreateDirectory("logs");
        _logName = $"logs\\{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.log";

        _logStream = File.Create(_logName);
        LogStreamWriter = new StreamWriter(_logStream);

        _logFileOpen = true;

    }

    public static void CloseLogFile()
    {
        if (!_logFileOpen)
            return;

        LogStreamWriter?.Close();
        LogStreamWriter?.Dispose();

        _logStream?.Close();
        _logStream?.Dispose();

        LogStreamWriter = null!;
        _logStream = null!;

        _logFileOpen = false;
    }



    public static void Info(string message) => WriteLine(LogReportType.Info, message);
    public static void Warn(string message) => WriteLine(LogReportType.Warn, message);
    public static void Error(string message) => WriteLine(LogReportType.Error, message);
    public static void Debug(string message) => WriteLine(LogReportType.Debug, message);
    public static void Trace(string message) => WriteLine(LogReportType.Trace, message);

    public static void ReadAttempt(string filepath) => Info($"Attempting to read file: \"{filepath}\"");
    public static void WriteAttempt(string filepath) => Info($"Attempting to write file: \"{filepath}\"");
    public static void Success() => Info($"Success!");
    public static void Exception(Exception ex) => Error($"An exception has occured! Details are as follows:\n{ex.Message}");
    public static void DeserializationError(string path, string objectName, Exception ex) => Error($"Unable to deserialize \"{path}\" into type \"{objectName}\"! Exception details are as follows:\n{ex.Message}");

    public static void WriteLine(LogReportType reportType, string message)
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

        LogStreamWriter.WriteLine(header + message);


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
