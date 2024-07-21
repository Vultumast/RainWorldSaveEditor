using RainWorldSaveEditor.Save;

namespace RainWorldSaveEditor;

public partial class MainForm : Form
{
    Settings settings = new();
    public MainForm()
    {

        InitializeComponent();

        /*
        var table = HashtableSerializer.Read(File.OpenRead("TestFiles/sav.xml"));
        HashtableSerializer.Write(File.OpenWrite("TestFiles/savsaved.xml"), table);

        if (table["save"] is string saveData)
        {
            RainWorldSave save = new();
            save.Read(saveData);
        }
        else
        {
            Logger.Log("Save data not found.");
        }

        if (table["save__Backup"] is string saveBackupData)
        {
            RainWorldSave saveBackup = new();
            saveBackup.Read(saveBackupData);
        }
        else
        {
            Logger.Log("Save data not found.");
        }*/
    }


    // TEMP
    public struct SlugcatInfo(string name, string saveID, bool requiresDLC, bool modded)
    {
        public string Name { get; set; } = name;
        public string SaveID { get; set; } = saveID;
        public bool RequiresDLC { get; set; } = requiresDLC;
        public bool Modded { get; set; } = modded;
    }

    public SlugcatInfo[] Slugcats =
    {
        new SlugcatInfo("Monk", "Yellow", false, false),
        new SlugcatInfo("Survivor", "White", false, false),
        new SlugcatInfo("Hunter", "Red", false, false),

        new SlugcatInfo("Gourmand", "Gourmand", true, false),
        new SlugcatInfo("Artificer", "Artificer", true, false),
        new SlugcatInfo("Rivulet", "Rivulet", true, false),
        new SlugcatInfo("Spearmaster", "Spearmaster", true, false),
        new SlugcatInfo("Saint", "Saint", true, false),
        new SlugcatInfo("Inv", "Inv", true, false),
        // new SlugcatInfo("Watcher", true, false),
    };
    // TEND OF TEMP
    private void MainForm_Load(object sender, EventArgs e)
    {
        for (var i = 0; i < Slugcats.Length; i++)
            slugcatIconImageList.Images.Add(Slugcats[i].Name, Image.FromFile(Path.Combine("Resources\\Slugcat Icons\\", $"{Slugcats[i].Name}.png")));
        

        if (!Directory.Exists(settings.RainWorldDirectory))
        {
            Console.WriteLine($"RAIN WORLD DIRECTORY DOESNT EXIST WHAT \"{settings.RainWorldDirectory}\"");
        }

        SetDefaultState();

        mainTabControl.ImageList = slugcatIconImageList;
        for (var i = 0; i < Slugcats.Length; i++)
            SetupSlugcatPage(Slugcats[i]);
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

    void SetupSlugcatPage(SlugcatInfo slugcatInfo)
    {
        mainTabControl.TabPages.Add($"{slugcatInfo.SaveID}_TabPage", slugcatInfo.Name, mainTabControl.ImageList!.Images.IndexOfKey(slugcatInfo.Name));

        TabPage page = mainTabControl.TabPages[mainTabControl.TabPages.Count - 1];

        page.Controls.Add(new Controls.SlugConfigControl());
        Controls.SlugConfigControl control = (Controls.SlugConfigControl)page.Controls[page.Controls.Count - 1];

        control.Dock = DockStyle.Fill;

    }

    void ReadSaveData(string filepath)
    {
        var table = HashtableSerializer.Read(File.OpenRead(filepath));
        // HashtableSerializer.Write(File.OpenWrite("TestFiles/savsaved.xml"), table);
        RainWorldSave save = new();

        if (table["save"] is string saveData)
        {

            save.Read(saveData);
        }
        else
        {
            Logger.Log("Save data not found.");
            return;
        }

        foreach (var slugcat in Slugcats)
        {
            SaveState id = null!;

            for (var i = 0; i < save.SaveStates.Count; i++)
            {
                if (save.SaveStates[i].SaveStateNumber == slugcat.SaveID)
                {
                    id = save.SaveStates[i];
                    break;
                }
            }

            if (id is null)
            {
                Logger.Log($"Save does not have information for slugcat: \"{slugcat.Name}\" ID: \"{slugcat.SaveID}\"");
                continue;
            }

            Controls.SlugConfigControl control = (Controls.SlugConfigControl)(mainTabControl.TabPages[$"{slugcat.SaveID}_TabPage"]!.Controls[0]);

            control.foodPipControl.FilledPips = (byte)id.FoodCount;
            control.CycleNumber = (uint)id.CycleNumber;

        }
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
        ReadSaveData($"{settings.RainWorldDirectory}\\sav");

    }

    private void openFile2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = true;
        openFile3ToolStripMenuItem.Checked = false;
        ReadSaveData($"{settings.RainWorldDirectory}\\sav2");
    }

    private void openFile3ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = false;
        openFile3ToolStripMenuItem.Checked = true;
        ReadSaveData($"{settings.RainWorldDirectory}\\sav3");
    }


    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Vultu: Put needed close code in here
        this.Close();
    }
    #endregion


}
