using RainWorldSaveEditor.Editor_Classes;
using System.Collections;
using System.Diagnostics;

namespace RainWorldSaveEditor;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.ThreadException += Application_ThreadException;
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        Application.ApplicationExit += Application_ApplicationExit;

#if DEBUG
        Logger.AllocateCMD();
#endif
        Logger.OpenLogFile();

        RainWorldSaveAPI.Logger.LogStreamWriter = Logger.LogStreamWriter;

        Translation.Read();

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }


    private static void Application_ApplicationExit(object? sender, EventArgs e)
    {
        Logger.CloseLogFile();
    }

    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Logger.Error($"**** UNHANDLED UI EXCEPTION! *****\n{(e.ExceptionObject as Exception)!.Message}");
    }

    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        Logger.Error($"**** UNHANDLED THREAD EXCEPTION! *****\n{e.Exception.Message}");
    }
}