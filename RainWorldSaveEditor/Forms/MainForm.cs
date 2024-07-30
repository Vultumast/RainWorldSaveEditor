using RainWorldSaveEditor.Editor_Classes;
using RainWorldSaveEditor.Forms;
using System.Diagnostics;
using System.Text.Json;
using RainWorldSaveAPI;
using RainWorldSaveAPI.SaveElements;
using System.Reflection;
using System.Globalization;
using System.Resources;

namespace RainWorldSaveEditor;

public partial class MainForm : Form
{
    Settings settings = new();
    public MainForm()
    {

        InitializeComponent();

    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        if (!File.Exists(Settings.Filepath))
        {
            Logger.Info("Unable to find settings file, creating a new one.");
            settings.Save();
        }
        settings = Settings.Read();

        if (settings.ShowDisclaimer)
        {
            if (MessageBox.Show(
                "Marioalexsan, Vultumast, and all other contributors are NOT RESPONSIBLE for any damage to computer, software, save information, etc. that may arise from usage of this software.\nDo you accept the terms of using this software?",
                "Disclaimer",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation
                ) == DialogResult.No)
            {
                Close();
                return;
            }
            settings.ShowDisclaimer = false;
            settings.Save();
        }


        if (!Directory.Exists(settings.RainWorldSaveDirectory))
        {
            Console.WriteLine($"RAIN WORLD DIRECTORY DOESNT EXIST WHAT \"{settings.RainWorldSaveDirectory}\"");
        }


        EditorCommon.ReadSlugcatInfo();

        CommunityInfo.ReadCommunities();

        for (var i = 0; i < EditorCommon.SlugcatInfo.Length; i++)
            slugcatIconImageList.Images.Add(EditorCommon.SlugcatInfo[i].Name, Image.FromFile(Path.Combine("Resources\\Slugcat Icons\\", $"{EditorCommon.SlugcatInfo[i].Name}.png")));

        mainTabControl.ImageList = slugcatIconImageList;
        for (var i = 0; i < EditorCommon.SlugcatInfo.Length; i++)
            SetupSlugcatPage(EditorCommon.SlugcatInfo[i]);
    }



    void SetDefaultState()
    {
        openFile1ToolStripMenuItem.Enabled = File.Exists(Path.Combine(settings.RainWorldSaveDirectory, "sav"));
        openFile2ToolStripMenuItem.Enabled = File.Exists(Path.Combine(settings.RainWorldSaveDirectory, "sav2"));
        openFile3ToolStripMenuItem.Enabled = File.Exists(Path.Combine(settings.RainWorldSaveDirectory, "sav3"));

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
        var fs = File.OpenRead(filepath);
        var table = HashtableSerializer.Read(fs);
        fs.Close();

        // HashtableSerializer.Write(File.OpenWrite("TestFiles/savsaved.xml"), table);
        RainWorldSave save = new();

        if (table["save"] is string saveData)
        {
            save.Read(saveData);
        }
        else
        {
            Logger.Warn("Save data not found.");
            return;
        }

        foreach (var slugcat in EditorCommon.SlugcatInfo)
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
                Logger.Info($"Save does not have information for slugcat: \"{slugcat.Name}\" ID: \"{slugcat.SaveID}\"");
                control.Enabled = false;
                continue;
            }

            control.SetupFromState(id);

        }
    }


    #region Menustrip

    private void openRainWorldSaveDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Directory.Exists(settings.RainWorldSaveDirectory))
            Process.Start("explorer.exe", settings.RainWorldSaveDirectory);
        else
            Logger.Error($"Unable to open directory: \"{settings.RainWorldSaveDirectory}\"");
    }
    private void rainworldExecutableDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void openFile1ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFile1ToolStripMenuItem.Checked = true;
        openFile2ToolStripMenuItem.Checked = false;
        openFile3ToolStripMenuItem.Checked = false;
        ReadSaveData($"{settings.RainWorldSaveDirectory}\\sav");

    }

    private void openFile2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = true;
        openFile3ToolStripMenuItem.Checked = false;
        ReadSaveData($"{settings.RainWorldSaveDirectory}\\sav2");
    }

    private void openFile3ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = false;
        openFile3ToolStripMenuItem.Checked = true;
        ReadSaveData($"{settings.RainWorldSaveDirectory}\\sav3");
    }


    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Vultu: Put needed close code in here
        this.Close();
    }
    #endregion


    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using AboutForm form = new AboutForm();
        form.ShowDialog();
    }

    private void toggleConsoleToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Logger.ConsoleShown = !Logger.ConsoleShown;
    }

}
