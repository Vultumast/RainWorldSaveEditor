using RainWorldSaveEditor.Save;

namespace RainWorldSaveEditor;

public partial class MainForm : Form
{
    Settings settings = new();
    public MainForm()
    {

        InitializeComponent();

        var table = HashtableSerializer.Read(File.OpenRead("TestFiles/sav.xml"));
        HashtableSerializer.Write(File.OpenWrite("TestFiles/savsaved.xml"), table);

        if (table["save"] is string saveData)
        {
            try
            {
                RainWorldSave save = new();
                save.Read(saveData);
            }
            catch (Exception e)
            {
                Logger.Log($"Crashed while reading save data: {e}");
            }
        }
        else
        {
            Logger.Log("Save data not found.");
        }

        if (table["save__Backup"] is string saveBackupData)
        {
            try
            {
                RainWorldSave saveBackup = new();
                saveBackup.Read(saveBackupData);
            }
            catch (Exception e)
            {
                Logger.Log($"Crashed while reading save data: {e}");
            }
        }
        else
        {
            Logger.Log("Save data not found.");
        }
    }


    // TEMP
    public struct SlugcatInfo(string name, bool requiresDLC, bool modded)
    {
        public string Name { get; set; } = name;
        public bool RequiresDLC { get; set; } = requiresDLC;
        public bool Modded { get; set; } = modded;
    }

    public SlugcatInfo[] Slugcats =
    {
        new SlugcatInfo("Monk", false, false),
        new SlugcatInfo("Survivor", false, false),
        new SlugcatInfo("Hunter", false, false),

        new SlugcatInfo("Gourmand", true, false),
        new SlugcatInfo("Artificer", true, false),
        new SlugcatInfo("Rivulet", true, false),
        new SlugcatInfo("Spearmaster", true, false),
        new SlugcatInfo("Saint", true, false),
        new SlugcatInfo("Inv", true, false),
        // new SlugcatInfo("Watcher", true, false),
    };
    // TEND OF TEMP
    private void MainForm_Load(object sender, EventArgs e)
    {
        for (var i = 0; i < Slugcats.Length; i++)
            slugcatIconImageList.Images.Add(Image.FromFile(Path.Combine("Resources\\Slugcat Icons\\", $"{Slugcats[i].Name}.png")));
        

        if (!Directory.Exists(settings.RainWorldDirectory))
        {
            Console.WriteLine($"RAIN WORLD DIRECTORY DOESNT EXIST WHAT \"{settings.RainWorldDirectory}\"");
        }

        SetDefaultState();

        tabControl1.ImageList = slugcatIconImageList;
        for (var i = 0; i < Slugcats.Length; i++)
            tabControl1.TabPages.Add($"{Slugcats[i].Name}_TabPage", Slugcats[i].Name, i);
    }

    void SetDefaultState()
    {
        openFile1ToolStripMenuItem.Enabled = File.Exists(Path.Combine(settings.RainWorldDirectory, "sav"));
        openFile2ToolStripMenuItem.Enabled = File.Exists(Path.Combine(settings.RainWorldDirectory, "sav2"));
        openFile3ToolStripMenuItem.Enabled = File.Exists(Path.Combine(settings.RainWorldDirectory, "sav3"));

        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = false;
        openFile3ToolStripMenuItem.Checked = false;
    }

    #region Menustrip

    private void openRainWorldSaveDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void openFile1ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFile1ToolStripMenuItem.Checked = true;
        openFile2ToolStripMenuItem.Checked = false;
        openFile3ToolStripMenuItem.Checked = false;
    }

    private void openFile2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = true;
        openFile3ToolStripMenuItem.Checked = false;
    }

    private void openFile3ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = false;
        openFile3ToolStripMenuItem.Checked = true;
    }


    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Vultu: Put needed close code in here
        this.Close();
    }
    #endregion


}
