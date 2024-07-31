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
    RainWorldSave save = new();

    public MainForm()
    {

        InitializeComponent();

    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        slugConfigControl.SetupFromState(null!);

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
            Logger.Warn($"RAIN WORLD DIRECTORY DOESNT EXIST WHAT \"{settings.RainWorldSaveDirectory}\"");
        }


        EditorCommon.ReadSlugcatInfo();

        CommunityInfo.ReadCommunities();

        for (var i = 0; i < EditorCommon.SlugcatInfo.Length; i++)
        {
            var slugcatInfo = EditorCommon.SlugcatInfo[i];

            Bitmap bmp = null!;

            var imgPath = $"Resources\\Slugcat\\Icons\\{slugcatInfo.Name}.png";
            ToolStripMenuItem menuItem = null!;

            if (!File.Exists(imgPath))
            {
                Logger.Warn($"Unable to find slugcat image: \"{imgPath}\"");
                bmp = Properties.Resources.Slugcat_Missing;
            }
            else
                bmp = new Bitmap(imgPath);

            if (slugcatInfo.Modded)
                menuItem = vanillaSlugcatsToolStripMenuItem;
            else if (slugcatInfo.RequiresDLC)
                menuItem = dlcSlugcatsToolStripMenuItem;
            else
                menuItem = vanillaSlugcatsToolStripMenuItem;

            menuItem.DropDownItems.Add(slugcatInfo.Name, bmp, SlugcatMenuItem_Click).Tag = slugcatInfo;
        }
    }

    private void SlugcatMenuItem_Click(object? sender, EventArgs e)
    { 
        ToolStripMenuItem menuItem = (ToolStripMenuItem)sender!;
        SlugcatInfo slugcatInfo = (SlugcatInfo)menuItem.Tag!;



        SaveState id = null!;

        foreach (var slugcat in EditorCommon.SlugcatInfo)
        {
            for (var i = 0; i < save.SaveStates.Count; i++)
            {
                if (save.SaveStates[i].SaveStateNumber == slugcat.SaveID)
                {
                    id = save.SaveStates[i];
                    break;
                }
            }
        }


        if (id is null)
        {
            Logger.Info($"Save does not have information for slugcat: \"{slugcatInfo.Name}\" ID: \"{slugcatInfo.SaveID}\"");
            slugConfigControl.SetupFromState(null!);
            return;
        }

        slugConfigControl.SetupFromState(id!);
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


    void ReadSaveData(string filepath)
    {
        var fs = File.OpenRead(filepath);
        var table = HashtableSerializer.Read(fs);
        fs.Close();

        // HashtableSerializer.Write(File.OpenWrite("TestFiles/savsaved.xml"), table);

        if (table["save"] is string saveData)
        {
            save.Read(saveData);
        }
        else
        {
            Logger.Warn("Save data not found.");
            return;
        }

    }

    void UpdateTitle()
    {
        Text = "Rainworld Save Editor";
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
        openFileToolStripMenuItem.Checked = false;
        ReadSaveData($"{settings.RainWorldSaveDirectory}\\sav");

    }

    private void openFile2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = true;
        openFile3ToolStripMenuItem.Checked = false;
        openFileToolStripMenuItem.Checked = false;
        ReadSaveData($"{settings.RainWorldSaveDirectory}\\sav2");
    }

    private void openFile3ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = false;
        openFile3ToolStripMenuItem.Checked = true;
        openFileToolStripMenuItem.Checked = false;
        ReadSaveData($"{settings.RainWorldSaveDirectory}\\sav3");
    }

    private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog();

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = false;
        openFile3ToolStripMenuItem.Checked = false;
        openFileToolStripMenuItem.Checked = true;

        Logger.Info($"Opening save file {dialog.FileName}");
        ReadSaveData(dialog.FileName);
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
