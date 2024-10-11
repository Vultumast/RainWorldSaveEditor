using RainWorldSaveEditor.Editor_Classes;

namespace RainWorldSaveEditor;
struct LockInfo
{
    public LockInfo(ulong id64, ulong id3, string name)
    {
        ID64 = id64;
        ID3 = id3;
        Name = name;
    }
    public string Name;
    public ulong ID64;
    public ulong ID3;
}

internal static class Program
{
    static LockInfo[] lockList =
    {
            new LockInfo(76561198452960140, 492694412, "Ray"), // Ray
			new LockInfo(76561199524109180, 1563843452, "Cato"), // Cato
			new LockInfo(76561198359604077, 399338349, "Jas"), // Jas
			new LockInfo(76561198324455426, 364189698, "Lux"), // Lux
			new LockInfo(76561198251907980, 291642252, "Mae"), // Mae
			new LockInfo(76561198142589722, 182323994, "Nei"), // Nei
			new LockInfo(76561198861589814, 901324086, "Catle"), // Catle
			new LockInfo(76561199097603288, 1137337560, "Batcatz"), // Batcatz
		};

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Vultu: Lockout those I don't like/who have hurt me

        // As per the license, this code may not be removed and MUST come first in main
        // you are not allowed to alter this code, or bypass this lockout.
        var pgramFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        var pgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);


        bool exit = false;
        foreach (var lockInfo in lockList)
        {
            var pathX86ID3 = Path.Combine(pgramFilesX86, "Steam", "userdata", lockInfo.ID3.ToString());
            var pathX86ID64 = Path.Combine(pgramFilesX86, "Steam", "userdata", lockInfo.ID64.ToString());

            var pathID3 = Path.Combine(pgramFiles, "Steam", "userdata", lockInfo.ID3.ToString());
            var pathID64 = Path.Combine(pgramFiles, "Steam", "userdata", lockInfo.ID64.ToString());

            exit |= Directory.Exists(pathX86ID3);
            exit |= Directory.Exists(pathX86ID64);

            exit |= Directory.Exists(pathID3);
            exit |= Directory.Exists(pathID64);

            if (exit)
            {
                MessageBox.Show(
                    $"{lockInfo.Name}, you are not authorized to use my works. In any shape or form.\n" +
                    $"The program will now exit.", $"Hello {lockInfo.Name}");
                return;
            }    
        }

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