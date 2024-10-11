using RainWorldSaveEditor.Forms;
using System.Diagnostics;
using System.Text.Json;
using RainWorldSaveAPI;
using System.Reflection;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace RainWorldSaveEditor;

public partial class MainForm : Form
{
    Settings settings = new();

    SlugcatInfo _slugcatInfo = null!;
    RainWorldSave _save = null!;
    SaveState _saveState = null!;



    #region Properties
    public string ExternalSaveLocation { get; private set; } = string.Empty;
    public bool UsingExternalSave => ExternalSaveLocation != string.Empty;

    public int SaveID
    {
        get
        {
            if (openFile1ToolStripMenuItem.Checked)
                return 1;
            if (openFile2ToolStripMenuItem.Checked)
                return 2;
            if (openFile3ToolStripMenuItem.Checked)
                return 3;

            return -1;
        }
    }
    #endregion

    #region Private Methods
    void ClearSlugcatCheckStates()
    {
        foreach (ToolStripMenuItem item in vanillaSlugcatsToolStripMenuItem.DropDownItems)
            item.Checked = false;

        foreach (ToolStripMenuItem item in dlcSlugcatsToolStripMenuItem.DropDownItems)
            item.Checked = false;

        foreach (ToolStripMenuItem item in moddedSlugcatsToolStripMenuItem.DropDownItems)
            item.Checked = false;
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


    void LoadSaveData(string filepath)
    {
        UnloadSave();
        _save = new();
        slugcatsToolStripMenuItem.Enabled = true;

        using var fs = File.OpenRead(filepath);
        var table = HashtableSerializer.Read(fs);
        fs.Close();

        if (table["save"] is string saveData)
        {
            _save.Read(saveData);
        }
        else
        {
            Logger.Warn("Save data not found.");
            return;
        }

        saveToolStripMenuItem.Enabled = true;
        saveAsToolStripMenuItem.Enabled = true;

        UpdateTitle();
    }

    void WriteSaveData(string filepath)
    {
        var table = new System.Collections.Hashtable();
        table["save"] = _save.Write();
        table["save__Backup"] = table["save"];
        using var fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
        HashtableSerializer.Write(fs, table);
        fs.Close();
    }

    void UnloadSave()
    {
        _save = null!;
        _slugcatInfo = null!;
        _saveState = null!;
        slugcatsToolStripMenuItem.Enabled = false;
        slugConfigControl.SetupFromState(null!);
        slugConfigControl.FoodPipControl.FilledPips = 0;
        slugConfigControl.FoodPipControl.PipBarIndex = 4;
        slugConfigControl.FoodPipControl.PipCount = 7;
        saveToolStripMenuItem.Enabled = false;
        saveAsToolStripMenuItem.Enabled = false;
        ClearSlugcatCheckStates();
        UpdateTitle();
    }

    void UpdateTitle()
    {
        var targetName = $"Rain World Save Editor (Beta) - {Assembly.GetExecutingAssembly().GetName().Version!.ToString()}"; ;

        if (_save is not null)
        {
            if (UsingExternalSave)
                targetName += " - External Save";
            else
                targetName += " - Save " + SaveID;
        }

        if (_slugcatInfo is not null)
            targetName += $" : {_slugcatInfo.Name}";

        if (Text != targetName)
            Text = targetName;
    }
    #endregion

    public MainForm()
    {
        InitializeComponent();

        SlugcatInfo.ReadSlugcatInfo();

        CommunityInfo.ReadCommunities();
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

        List<string> userDirs = [];
        var dirs = Directory.GetDirectories("C:\\Users\\");

        foreach (var dir in dirs)
        {
            if (Directory.Exists(Path.Combine(dir, Utils.RainworldSaveDirectoryPostFix)))
            {
                userProfileToolStripMenuItem.DropDownItems.Add(Path.GetFileNameWithoutExtension(dir));
                var item = userProfileToolStripMenuItem.DropDownItems[userProfileToolStripMenuItem.DropDownItems.Count - 1];
                item.Click += Item_Click;
                item.Tag = dir;
            }
        }

        Text = $"Rain World Save Editor (Beta) - {Assembly.GetExecutingAssembly().GetName().Version!.ToString()}";

        if (userProfileToolStripMenuItem.DropDownItems.Count != 0)
        {
            var item = ((ToolStripMenuItem)userProfileToolStripMenuItem.DropDownItems[0]);
            item.Checked = true;
            settings.RainWorldSaveDirectory = $"{item.Tag!.ToString()!}\\{Utils.RainworldSaveDirectoryPostFix}";
        }

        if (userProfileToolStripMenuItem.DropDownItems.Count == 0)
        {
            if (MessageBox.Show("todo: this lol", "Unable to find Rain World Save Files", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
            {

            }
        }

        for (var i = 0; i < SlugcatInfo.SlugcatInfos.Length; i++)
        {
            var slugcatInfo = SlugcatInfo.SlugcatInfos[i];

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
                menuItem = moddedSlugcatsToolStripMenuItem;
            else if (slugcatInfo.RequiresDLC)
                menuItem = dlcSlugcatsToolStripMenuItem;
            else
                menuItem = vanillaSlugcatsToolStripMenuItem;

            ToolStripMenuItem item = (ToolStripMenuItem)menuItem.DropDownItems.Add(slugcatInfo.Name, bmp, SlugcatMenuItem_Click);
            item.Tag = slugcatInfo;
            item.CheckOnClick = true;

        }
    }

    private void Item_Click(object? sender, EventArgs e)
    {
        foreach (var item in userProfileToolStripMenuItem.DropDownItems)
        {
            var tItem = ((ToolStripMenuItem)item);
            if (sender == item)
            {
                tItem.Checked = true;
                settings.RainWorldSaveDirectory = $"{tItem.Tag!.ToString()!}\\{Utils.RainworldSaveDirectoryPostFix}";
            }
            else
                tItem.Checked = false;
        }
    }

    private void SlugcatMenuItem_Click(object? sender, EventArgs e)
    {
        ToolStripMenuItem menuItem = (ToolStripMenuItem)sender!;
        SlugcatInfo slugcatInfo = (SlugcatInfo)menuItem.Tag!;

        if (_saveState is not null)
            Logger.Info($"Clearing current info from: \"{_saveState.SaveStateNumber}\"");

        for (var i = 0; i < _save.SaveStates.Count; i++)
        {
            if (_save.SaveStates[i].SaveStateNumber == slugcatInfo.SaveID)
            {
                _slugcatInfo = slugcatInfo;
                _saveState = _save.SaveStates[i];
                break;
            }
        }


        ClearSlugcatCheckStates();
        menuItem.Checked = true;


        // Clear Current State info
        slugConfigControl.SetupFromState(null!);
        slugConfigControl.FoodPipControl.FilledPips = 0;
        slugConfigControl.FoodPipControl.PipBarIndex = 4;
        slugConfigControl.FoodPipControl.PipCount = 7;

        if (_saveState is null)
        {
            Logger.Info($"Save does not have information for slugcat: \"{slugcatInfo.Name}\" ID: \"{slugcatInfo.SaveID}\"");
            return;
        }

        Logger.Info($"Loading state info from: \"{_saveState.SaveStateNumber}\"");

        // Load new state info
        slugConfigControl.FoodPipControl.PipBarIndex = slugcatInfo.PipBarIndex;
        slugConfigControl.FoodPipControl.PipCount = slugcatInfo.PipCount;

        slugConfigControl.SetupFromState(_saveState!);

        UpdateTitle();
        Logger.Info($"Finished updating state info for \"{_saveState.SaveStateNumber}\"");
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
        LoadSaveData($"{settings.RainWorldSaveDirectory}\\sav");

    }

    private void openFile2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = true;
        openFile3ToolStripMenuItem.Checked = false;
        openFileToolStripMenuItem.Checked = false;

        ExternalSaveLocation = string.Empty;

        LoadSaveData($"{settings.RainWorldSaveDirectory}\\sav2");
    }

    private void openFile3ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = false;
        openFile3ToolStripMenuItem.Checked = true;
        openFileToolStripMenuItem.Checked = false;

        ExternalSaveLocation = string.Empty;

        LoadSaveData($"{settings.RainWorldSaveDirectory}\\sav3");
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


        ExternalSaveLocation = dialog.FileName;
        Logger.Info($"Opening save file {dialog.FileName}");
        LoadSaveData(dialog.FileName);
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Vultu: Put needed close code in here
        this.Close();
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        string filepath = settings.RainWorldSaveDirectory + "\\sav" + (SaveID == 1 ? string.Empty : SaveID.ToString());

        Logger.Info($"Writing save file {filepath}");


        if (UsingExternalSave)
        {
            throw new NotImplementedException();
        }
        else
            WriteSaveData(filepath);
    }
    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog();

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        Logger.Info($"Writing save file {dialog.FileName}");
        WriteSaveData(dialog.FileName);
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using AboutForm form = new AboutForm();
        form.ShowDialog();
    }

    private void toggleConsoleToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Logger.ConsoleShown = !Logger.ConsoleShown;
    }

    private void exportToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using SaveFileDialog dlg = new SaveFileDialog();

        dlg.Filter = "*.json (JSON File)|*.json";

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            // Vultu: this don't work
            File.WriteAllText(dlg.FileName, JsonSerializer.Serialize(_saveState, new JsonSerializerOptions() { WriteIndented = true }));
        }

    }

    private void importToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }
    #endregion

    private void explorerToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using SaveExplorer form = new();

        form.Save = _save;

        form.ShowDialog();
    }
}
