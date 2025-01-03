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
    public enum SaveTypes
    {
        MainSave,
        ExpeditionSave,
        Arbitrary
    }

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
            if (openFile1ToolStripMenuItem.Checked || openExpeditionFile1ToolStripMenuItem.Checked)
                return 1;
            if (openFile2ToolStripMenuItem.Checked || openExpeditionFile2ToolStripMenuItem.Checked)
                return 2;
            if (openFile3ToolStripMenuItem.Checked || openExpeditionFile3ToolStripMenuItem.Checked)
                return 3;

            return -1;
        }
    }

    public SaveTypes SaveType
    {
        get
        {
            if (openFile1ToolStripMenuItem.Checked || openFile2ToolStripMenuItem.Checked || openFile3ToolStripMenuItem.Checked)
                return SaveTypes.MainSave;

            if (openExpeditionFile1ToolStripMenuItem.Checked || openExpeditionFile2ToolStripMenuItem.Checked || openExpeditionFile3ToolStripMenuItem.Checked)
                return SaveTypes.ExpeditionSave;

            return SaveTypes.Arbitrary;
        }
    }

    public string SaveLocation
    {
        get
        {
            if (SaveType == SaveTypes.Arbitrary)
                return ExternalSaveLocation;

            else if (SaveType == SaveTypes.MainSave)
                return SaveID switch
                {
                    1 => $"{settings.RainWorldSaveDirectory}\\sav",
                    _ => $"{settings.RainWorldSaveDirectory}\\sav{SaveID}"
                };

            else return $"{settings.RainWorldSaveDirectory}\\exp{SaveID}";
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

        openExpeditionFile1ToolStripMenuItem.Enabled = File.Exists(Path.Combine(settings.RainWorldSaveDirectory, "exp1"));
        openExpeditionFile2ToolStripMenuItem.Enabled = File.Exists(Path.Combine(settings.RainWorldSaveDirectory, "exp2"));
        openExpeditionFile3ToolStripMenuItem.Enabled = File.Exists(Path.Combine(settings.RainWorldSaveDirectory, "exp3"));

        openExpeditionFile1ToolStripMenuItem.Checked = false;
        openExpeditionFile2ToolStripMenuItem.Checked = false;
        openExpeditionFile3ToolStripMenuItem.Checked = false;
    }


    void LoadSaveData(string filepath)
    {
        UnloadSave();
        Logger.Info($"Reading save file \"{filepath}\"");

        slugcatsToolStripMenuItem.Enabled = true;

        var save = RWFileSerializer.ReadSavExpFile(filepath);

        if (save is not null)
        {
            _save = save;
        }
        else
        {
            _save = new();
            Logger.Warn("Save data not found.");
            return;
        }

        saveToolStripMenuItem.Enabled = true;
        saveAsToolStripMenuItem.Enabled = true;

        UpdateTitle();
    }

    void WriteSaveData(string filepath)
    {
        RWFileSerializer.WriteSavExpFile(filepath, _save);
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
        var targetName = $"Rain World Save Editor (Beta) - {Assembly.GetExecutingAssembly().GetName().Version}";

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

        UpdateTitle();

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

        Utils.PopulateSlugcatMenu(
            moddedSlugcatsToolStripMenuItem,
            dlcSlugcatsToolStripMenuItem,
            vanillaSlugcatsToolStripMenuItem,
            SlugcatMenuItem_Click
            );
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

    private void ClearLoadedFile()
    {
        openFile1ToolStripMenuItem.Checked = false;
        openFile2ToolStripMenuItem.Checked = false;
        openFile3ToolStripMenuItem.Checked = false;
        openExpeditionFile1ToolStripMenuItem.Checked = false;
        openExpeditionFile2ToolStripMenuItem.Checked = false;
        openExpeditionFile3ToolStripMenuItem.Checked = false;
        openFileToolStripMenuItem.Checked = false;
        ExternalSaveLocation = string.Empty;
    }

    private void openFile1ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClearLoadedFile();
        openFile1ToolStripMenuItem.Checked = true;
        LoadSaveData(SaveLocation);
    }

    private void openFile2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClearLoadedFile();
        openFile2ToolStripMenuItem.Checked = true;
        LoadSaveData(SaveLocation);
    }

    private void openFile3ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClearLoadedFile();
        openFile3ToolStripMenuItem.Checked = true;
        LoadSaveData(SaveLocation);
    }

    private void openExpeditionFile1ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClearLoadedFile();
        openExpeditionFile1ToolStripMenuItem.Checked = true;
        LoadSaveData(SaveLocation);
    }

    private void openExpeditionFile2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClearLoadedFile();
        openExpeditionFile2ToolStripMenuItem.Checked = true;
        LoadSaveData(SaveLocation);
    }

    private void openExpeditionFile3ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClearLoadedFile();
        openExpeditionFile3ToolStripMenuItem.Checked = true;
        LoadSaveData(SaveLocation);
    }

    private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog();

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        ClearLoadedFile();
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
        string filepath = SaveLocation;

        Logger.Info($"Writing save file {filepath}");

        WriteSaveData(SaveLocation);
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog();

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        Logger.Info($"Writing save file {dialog.FileName}");
        WriteSaveData(dialog.FileName);

        ClearLoadedFile();
        openFileToolStripMenuItem.Checked = true;
        ExternalSaveLocation = dialog.FileName;
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
