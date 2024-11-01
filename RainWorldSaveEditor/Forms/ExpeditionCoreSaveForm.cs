﻿using RainWorldSaveAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RainWorldSaveEditor.MainForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RainWorldSaveEditor.Forms;
public partial class ExpeditionCoreSaveForm : Form
{
    public ExpeditionCoreSaveForm()
    {
        InitializeComponent();
        UnloadSave();
    }

    public string ExternalSaveLocation { get; private set; } = string.Empty;

    public int SaveID
    {
        get
        {
            if (file1ToolStripMenuItem.Checked)
                return 1;
            if (file2ToolStripMenuItem.Checked)
                return 2;
            if (file3ToolStripMenuItem.Checked)
                return 3;

            return -1;
        }
    }

    public string SaveLocation
    {
        get
        {
            if (SaveID == -1 && openFileToolStripMenuItem.Checked)
                return ExternalSaveLocation;

            else return $"{settings.RainWorldSaveDirectory}\\expCore{SaveID}";
        }
    }

    ExpeditionCoreSave _save = null!;
    Settings settings = new();

    void UnloadSave()
    {
        _save = null!;
        saveToolStripMenuItem.Enabled = false;
        saveAsToolStripMenuItem.Enabled = false;
        expeditionSaveTabControl.Enabled = false;
    }

    void LoadSaveData(string filepath)
    {
        UnloadSave();
        Logger.Info($"Reading save file \"{filepath}\"");
        _save = new();

        using var fs = File.OpenRead(filepath);
        var table = HashtableSerializer.Read(fs);
        fs.Close();

        if (table["core"] is string saveData)
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
        expeditionSaveTabControl.Enabled = true;
        SetupFromSave(_save);
    }

    void WriteSaveData(string filepath)
    {
        var table = new System.Collections.Hashtable();
        table["core"] = _save.Write();
        using var fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
        HashtableSerializer.Write(fs, table);
        fs.Close();
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using AboutForm form = new AboutForm();
        form.ShowDialog();
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ClearLoadedFile()
    {
        file1ToolStripMenuItem.Checked = false;
        file2ToolStripMenuItem.Checked = false;
        file3ToolStripMenuItem.Checked = false;
        openFileToolStripMenuItem.Checked = false;
        ExternalSaveLocation = string.Empty;
    }

    private void file1ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClearLoadedFile();
        file1ToolStripMenuItem.Checked = true;
        LoadSaveData(SaveLocation);
    }

    private void file2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClearLoadedFile();
        file2ToolStripMenuItem.Checked = true;
        LoadSaveData(SaveLocation);
    }

    private void file3ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClearLoadedFile();
        file3ToolStripMenuItem.Checked = true;
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

    private void ExpeditionCoreSaveForm_Load(object sender, EventArgs e)
    {
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
    }

    void UpdateTitle()
    {
        var targetName = $"Rain World Save Editor (Beta) - {Assembly.GetExecutingAssembly().GetName().Version}";

        if (Text != targetName)
            Text = targetName;
    }

    private void UpdateRainbowAuraState(List<int> integers)
    {
        var canActivateRainbow = integers.All(x => x >= (int)RainbowAuraState.SpotReached);

        survivorEggDoneCheckBox.Checked = integers[0] >= (int)RainbowAuraState.SpotReached;
        monkEggDoneCheckBox.Checked = integers[1] >= (int)RainbowAuraState.SpotReached;
        hunterEggDoneCheckBox.Checked = integers[2] >= (int)RainbowAuraState.SpotReached;
        artificerEggDoneCheckBox.Checked = integers[3] >= (int)RainbowAuraState.SpotReached;
        gourmandEggDoneCheckBox.Checked = integers[4] >= (int)RainbowAuraState.SpotReached;
        spearmasterEggDoneCheckBox.Checked = integers[5] >= (int)RainbowAuraState.SpotReached;
        rivuletEggDoneCheckBox.Checked = integers[6] >= (int)RainbowAuraState.SpotReached;
        saintEggDoneCheckBox.Checked = integers[7] >= (int)RainbowAuraState.SpotReached;

        survivorEggActiveCheckBox.Checked = canActivateRainbow && integers[0] >= (int)RainbowAuraState.AuraActive;
        monkEggActiveCheckBox.Checked = canActivateRainbow && integers[1] >= (int)RainbowAuraState.AuraActive;
        hunterEggActiveCheckBox.Checked = canActivateRainbow && integers[2] >= (int)RainbowAuraState.AuraActive;
        artificerEggActiveCheckBox.Checked = canActivateRainbow && integers[3] >= (int)RainbowAuraState.AuraActive;
        gourmandEggActiveCheckBox.Checked = canActivateRainbow && integers[4] >= (int)RainbowAuraState.AuraActive;
        spearmasterEggActiveCheckBox.Checked = canActivateRainbow && integers[5] >= (int)RainbowAuraState.AuraActive;
        rivuletEggActiveCheckBox.Checked = canActivateRainbow && integers[6] >= (int)RainbowAuraState.AuraActive;
        saintEggActiveCheckBox.Checked = canActivateRainbow && integers[7] >= (int)RainbowAuraState.AuraActive;

        survivorEggActiveCheckBox.Enabled = canActivateRainbow;
        monkEggActiveCheckBox.Enabled = canActivateRainbow;
        hunterEggActiveCheckBox.Enabled = canActivateRainbow;
        artificerEggActiveCheckBox.Enabled = canActivateRainbow;
        gourmandEggActiveCheckBox.Enabled = canActivateRainbow;
        spearmasterEggActiveCheckBox.Enabled = canActivateRainbow;
        rivuletEggActiveCheckBox.Enabled = canActivateRainbow;
        saintEggActiveCheckBox.Enabled = canActivateRainbow;
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

    private void SetupFromSave(ExpeditionCoreSave? save)
    {
        saveSlotNumericUpDown.Value = save?.SaveSlot ?? 0;
        levelNumericUpDown.Value = save?.Level ?? 0;
        perkLimitNumericUpDown.Value = save?.PerkLimit ?? 0;
        pointsNumericUpDown.Value = save?.Points ?? 0;
        totalPointsNumericUpDown.Value = save?.TotalPoints ?? 0;
        totalChallengesNumericUpDown.Value = save?.TotalChallenges ?? 0;
        totalHiddenChallengesNumericUpDown.Value = save?.TotalHiddenChallenges ?? 0;
        winsNumericUpDown.Value = save?.Wins ?? 0;
        selectedSlugcatTextBox.Text = save?.Slugcat ?? "White";
        selectedMenuSongTextBox.Text = save?.MenuSong ?? "";
        viewedManualCheckBox.Checked = save?.HasViewedManual ?? false;

        unlocksDataGridView.Rows.Clear();
        foreach (var item in save?.Unlockables ?? [])
        {
            unlocksDataGridView.Rows.Add([item]);
        }

        songsDataGridView.Rows.Clear();
        foreach (var item in save?.NewSongs ?? [])
        {
            songsDataGridView.Rows.Add([item]);
        }

        completedQuestsDataGridView.Rows.Clear();
        foreach (var item in save?.Quests ?? [])
        {
            completedQuestsDataGridView.Rows.Add([item]);
        }

        completedMissionsDataGridView.Rows.Clear();
        foreach (var item in save?.Missions ?? [])
        {
            completedMissionsDataGridView.Rows.Add([item]);
        }

        missionBestTimesDataGridView.Rows.Clear();
        foreach (var item in save?.MissionBestTimes ?? [])
        {
            missionBestTimesDataGridView.Rows.Add([item.Mission, item.Time.ToString()]);
        }

        challengesDataGridView.Rows.Clear();
        foreach (var item in save?.ChallengeTypes ?? [])
        {
            challengesDataGridView.Rows.Add([item.Type, item.Count.ToString()]);
        }

        // Sanitize egg state
        var integers = save?.Integers ?? new int[8].ToList();
        var canActivateRainbow = integers.All(x => x >= (int)RainbowAuraState.SpotReached);
        for (int i = 0; i < integers.Count; i++)
        {
            if (integers[i] >= (int)RainbowAuraState.AuraActive && !canActivateRainbow)
                integers[i] = (int)RainbowAuraState.SpotReached;
        }

        UpdateRainbowAuraState(integers);
    }

    private void saveSlotNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        _save.SaveSlot = (int)saveSlotNumericUpDown.Value;
    }

    private void levelNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        _save.Level = (int)levelNumericUpDown.Value;
    }

    private void perkLimitNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        _save.PerkLimit = (int)perkLimitNumericUpDown.Value;
    }

    private void pointsNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        _save.Points = (int)pointsNumericUpDown.Value;
    }

    private void totalPointsNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        _save.TotalPoints = (int)totalPointsNumericUpDown.Value;
    }

    private void totalChallengesNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        _save.TotalChallenges = (int)totalChallengesNumericUpDown.Value;
    }

    private void totalHiddenChallengesNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        _save.TotalHiddenChallenges = (int)totalHiddenChallengesNumericUpDown.Value;
    }

    private void winsNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        _save.Wins = (int)winsNumericUpDown.Value;
    }

    private void selectedSlugcatTextBox_TextChanged(object sender, EventArgs e)
    {
        _save.Slugcat = selectedSlugcatTextBox.Text;
    }

    private void selectedMenuSongTextBox_TextChanged(object sender, EventArgs e)
    {
        _save.MenuSong = selectedMenuSongTextBox.Text;
    }

    private void viewedManualCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        _save.HasViewedManual = viewedManualCheckBox.Checked;
    }

    private void UpdateReachedRainbowAuraIndex(object sender, int index)
    {
        if (((CheckBox)sender).Checked)
            _save.Integers[index] = (int)RainbowAuraState.SpotReached;
        else _save.Integers[index] = (int)RainbowAuraState.SpotNotReached;
        UpdateRainbowAuraState(_save.Integers);
    }

    private void UpdateActiveRainbowAuraIndex(object sender, int index)
    {
        if (((CheckBox)sender).Checked)
            _save.Integers[index] = (int)RainbowAuraState.AuraActive;
        else _save.Integers[index] = _save.Integers[index] == (int)RainbowAuraState.AuraActive ? (int)RainbowAuraState.SpotReached : _save.Integers[index];
        UpdateRainbowAuraState(_save.Integers);
    }

    private void survivorEggDoneCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateReachedRainbowAuraIndex(sender, 0);
    }

    private void survivorEggActiveCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateActiveRainbowAuraIndex(sender, 0);
    }

    private void monkEggDoneCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateReachedRainbowAuraIndex(sender, 1);
    }

    private void monkEggActiveCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateActiveRainbowAuraIndex(sender, 1);
    }

    private void hunterEggDoneCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateReachedRainbowAuraIndex(sender, 2);
    }

    private void hunterEggActiveCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateActiveRainbowAuraIndex(sender, 2);
    }

    private void artificerEggDoneCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateReachedRainbowAuraIndex(sender, 3);
    }

    private void artificerEggActiveCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateActiveRainbowAuraIndex(sender, 3);
    }

    private void gourmandEggDoneCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateReachedRainbowAuraIndex(sender, 4);
    }

    private void gourmandEggActiveCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateActiveRainbowAuraIndex(sender, 4);
    }

    private void spearmasterEggDoneCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateReachedRainbowAuraIndex(sender, 5);
    }

    private void spearmasterEggActiveCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateActiveRainbowAuraIndex(sender, 5);
    }

    private void rivuletEggDoneCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateReachedRainbowAuraIndex(sender, 6);
    }

    private void rivuletEggActiveCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateActiveRainbowAuraIndex(sender, 6);
    }

    private void saintEggDoneCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateReachedRainbowAuraIndex(sender, 7);
    }

    private void saintEggActiveCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateActiveRainbowAuraIndex(sender, 7);
    }
}
