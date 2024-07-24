using RainWorldSaveEditor.Save;
using System.Text.Json;

namespace RainWorldSaveEditor;

public partial class MainForm : Form
{
    Settings settings = new();
    public MainForm()
    {

        InitializeComponent();
		
    }


    // TEMP
    public struct SlugcatInfo(string name, string saveID, bool requiresDLC, bool modded, byte pipCount, byte pipBarIndex)
    {
        public string Name { get; set; } = name;
        public string SaveID { get; set; } = saveID;
        public bool RequiresDLC { get; set; } = requiresDLC;
        public bool Modded { get; set; } = modded;
        public byte PipCount { get; set; } = pipCount;
        public byte PipBarIndex { get; set; } = pipBarIndex;
    }

    public List<SlugcatInfo> Slugcats = new();
    // TEND OF TEMP
    private void MainForm_Load(object sender, EventArgs e)
    {
        var slugcatFiles = Directory.GetFiles("Resources\\Slugcat Info", "*.json", SearchOption.AllDirectories);
        foreach (var slugcatFile in slugcatFiles)
            Slugcats.Add(JsonSerializer.Deserialize<SlugcatInfo>(File.ReadAllText(slugcatFile)));

        for (var i = 0; i < Slugcats.Count; i++)
            slugcatIconImageList.Images.Add(Slugcats[i].Name, Image.FromFile(Path.Combine("Resources\\Slugcat Icons\\", $"{Slugcats[i].Name}.png")));
        

        if (!Directory.Exists(settings.RainWorldDirectory))
        {
            Console.WriteLine($"RAIN WORLD DIRECTORY DOESNT EXIST WHAT \"{settings.RainWorldDirectory}\"");
        }

        SetDefaultState();

        mainTabControl.ImageList = slugcatIconImageList;
        for (var i = 0; i < Slugcats.Count; i++)
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

        control.FoodPipControl.PipCount = slugcatInfo.PipCount;
        control.FoodPipControl.PipBarIndex = slugcatInfo.PipBarIndex;
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

            Controls.SlugConfigControl control = (Controls.SlugConfigControl)(mainTabControl.TabPages[$"{slugcat.SaveID}_TabPage"]!.Controls[0]);

            if (id is null)
            {
                Logger.Log($"Save does not have information for slugcat: \"{slugcat.Name}\" ID: \"{slugcat.SaveID}\"");
                control.Enabled = false;
                continue;
            }

            // Setup Slugcat Info
            control.FoodPipControl.FilledPips = (byte)id.FoodCount;
            control.CycleNumber = (uint)id.CycleNumber;
            control.CurrentDenPosition = id.DenPosition;
            control.KarmaSelectorControl.KarmaLevel = id.DeathPersistentSaveData.Karma;
            control.KarmaSelectorControl.KarmaMax = id.DeathPersistentSaveData.KarmaCap;

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
