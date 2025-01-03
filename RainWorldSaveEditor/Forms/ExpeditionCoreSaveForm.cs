﻿using RainWorldSaveAPI;
using RainWorldSaveEditor.Controls;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RainWorldSaveEditor.Forms;
public partial class ExpeditionCoreSaveForm : Form
{
    public ExpeditionCoreSaveForm()
    {
        InitializeComponent();
        UnloadSave();

        slugcatActiveMissionMappedLabel.Text = "";
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

    string? selectedSlugcat = null;

    void UnloadSave()
    {
        _save = null!;
        saveToolStripMenuItem.Enabled = false;
        saveAsToolStripMenuItem.Enabled = false;
        expeditionSaveTabControl.Enabled = false;
        slugcatsToolStripMenuItem.Enabled = false;
        expedtionSlugcatTabPage.Enabled = false;
        ClearSlugcatCheckStates();
        LoadSlugcatData(null);
    }

    void LoadSaveData(string filepath)
    {
        UnloadSave();
        Logger.Info($"Reading save file \"{filepath}\"");

        slugcatsToolStripMenuItem.Enabled = true;

        var save = RWFileSerializer.ReadExpcoreFile(filepath);

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
        expeditionSaveTabControl.Enabled = true;
        SetupFromSave(_save);
    }

    void WriteSaveData(string filepath)
    {
        RWFileSerializer.WriteExpcoreFile(filepath, _save);
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

        Utils.PopulateSlugcatMenu(
            moddedSlugcatsToolStripMenuItem,
            dlcSlugcatsToolStripMenuItem,
            vanillaSlugcatsToolStripMenuItem,
            SlugcatMenuItem_Click
            );
    }

    void ClearSlugcatCheckStates()
    {
        foreach (ToolStripMenuItem item in vanillaSlugcatsToolStripMenuItem.DropDownItems)
            item.Checked = false;

        foreach (ToolStripMenuItem item in dlcSlugcatsToolStripMenuItem.DropDownItems)
            item.Checked = false;

        foreach (ToolStripMenuItem item in moddedSlugcatsToolStripMenuItem.DropDownItems)
            item.Checked = false;
    }

    private void SlugcatMenuItem_Click(object? sender, EventArgs e)
    {
        ToolStripMenuItem menuItem = (ToolStripMenuItem)sender!;
        SlugcatInfo slugcatInfo = (SlugcatInfo)menuItem.Tag!;

        ClearSlugcatCheckStates();
        menuItem.Checked = true;
        expedtionSlugcatTabPage.Enabled = true;
        LoadSlugcatData(slugcatInfo.SaveID);
    }

    void LoadSlugcatData(string? slugcat)
    {
        selectedSlugcat = slugcat;
        if (slugcat is null)
        {
            slugcatWinsNumericUpDown.Value = 0;
            slugcatPassagesNumericUpDown.Value = 0;
            slugcatActiveMissionComboBox.Text = "";
            slugcatActiveMissionMappedLabel.Text = "[no mission]";

            slugcatUnlocksListBox.Items.Clear();
            slugcatRequiredModsListBox.Items.Clear();
        }
        else
        {
            // Fill with slugcat data
            slugcatWinsNumericUpDown.Value = _save.WinEntries.FirstOrDefault(x => x.Slugcat == slugcat).Wins;
            slugcatPassagesNumericUpDown.Value = _save.Passages.FirstOrDefault(x => x.Slugcat == slugcat).Count;

            var activeMission = _save.ActiveMissionEntries.FirstOrDefault(x => x.Slugcat == slugcat).Data;
            var missionName = ExpeditionMissionInfo.Missions.Values.FirstOrDefault(x => x.Key == activeMission)?.Name;

            if (missionName == null)
            {
                missionName = activeMission;
            }
            else
            {
                missionName = $"{missionName} ({activeMission})";
            }

            slugcatActiveMissionComboBox.Text = missionName;
            UpdateSlugcatMission(missionName);

            slugcatUnlocksListBox.Items.Clear();
            foreach (var item in _save.Unlocks.Where(x => x.Slugcat == slugcat))
            {
                var realName = ExpeditionUnlockInfo.Unlocks.ContainsKey(item.Data) ? ExpeditionUnlockInfo.Unlocks[item.Data].Name : item.Data;

                slugcatUnlocksListBox.Items.Add(realName);
            }

            slugcatRequiredModsListBox.Items.Clear();
            foreach (var item in _save.RequiredModsEntries.Where(x => x.Slugcat == slugcat))
            {
                foreach (var mod in item.RequiredMods)
                {
                    slugcatRequiredModsListBox.Items.Add(mod);
                }
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

        unlocksListBox.Items.Clear();
        foreach (var item in save?.Unlockables ?? [])
        {
            var realName = ExpeditionUnlockInfo.Unlocks.ContainsKey(item) ? ExpeditionUnlockInfo.Unlocks[item].Name : item;

            unlocksListBox.Items.Add(realName);
        }

        songsListBox.Items.Clear();
        foreach (var item in save?.NewSongs ?? [])
        {
            var realName = ExpeditionUnlockInfo.Unlocks.ContainsKey(item) ? ExpeditionUnlockInfo.Unlocks[item].Name : item;

            if (!realName.StartsWith("Music"))
                realName = item;

            songsListBox.Items.Add(realName);
        }

        completedQuestsListBox.Items.Clear();
        foreach (var item in save?.Quests ?? [])
        {
            completedQuestsListBox.Items.Add($"Quest #{item[3..]}");
        }

        completedMissionsListBox.Items.Clear();
        foreach (var item in save?.Missions ?? [])
        {
            var realName = ExpeditionMissionInfo.Missions.ContainsKey(item) ? ExpeditionMissionInfo.Missions[item].Name : item;

            completedMissionsListBox.Items.Add(realName);
        }

        missionBestTimesListBox.Items.Clear();
        foreach (var item in save?.MissionBestTimes ?? [])
        {
            var realName = ExpeditionMissionInfo.Missions.ContainsKey(item.Mission) ? ExpeditionMissionInfo.Missions[item.Mission].Name : item.Mission;

            missionBestTimesListBox.Items.Add($"{realName}: {TimeSpan.FromSeconds(item.Time):hh\\:mm\\:ss}");
        }

        challengesListBox.Items.Clear();
        foreach (var item in save?.ChallengeTypes ?? [])
        {
            challengesListBox.Items.Add($"{item.Type}: {item.Count}");
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

    private void unlocksRemoveButton_Click(object sender, EventArgs e)
    {
        var index = unlocksListBox.SelectedIndex;
        if (index != -1)
        {
            unlocksListBox.Items.RemoveAt(index);
            _save.Unlockables.RemoveAt(index);
        }
    }

    private void songsRemoveButton_Click(object sender, EventArgs e)
    {
        var index = songsListBox.SelectedIndex;
        if (index != -1)
        {
            songsListBox.Items.RemoveAt(index);
            _save.NewSongs.RemoveAt(index);
        }
    }

    private void missionBestTimesRemoveButton_Click(object sender, EventArgs e)
    {
        var index = missionBestTimesListBox.SelectedIndex;
        if (index != -1)
        {
            missionBestTimesListBox.Items.RemoveAt(index);
            _save.MissionBestTimes.RemoveAt(index);
        }
    }

    private void completedMissionsRemoveButton_Click(object sender, EventArgs e)
    {
        var index = completedMissionsListBox.SelectedIndex;
        if (index != -1)
        {
            completedMissionsListBox.Items.RemoveAt(index);
            _save.Missions.RemoveAt(index);
        }
    }

    private void completedQuestsRemoveButton_Click(object sender, EventArgs e)
    {
        var index = completedQuestsListBox.SelectedIndex;
        if (index != -1)
        {
            completedQuestsListBox.Items.RemoveAt(index);
            _save.Quests.RemoveAt(index);
        }
    }

    private void challengesRemoveButton_Click(object sender, EventArgs e)
    {
        var index = challengesListBox.SelectedIndex;
        if (index != -1)
        {
            challengesListBox.Items.RemoveAt(index);
            _save.ChallengeTypes.RemoveAt(index);
        }
    }

    private void unlocksAddButton_Click(object sender, EventArgs e)
    {
        using var dialog = new StringValueInputForm();

        dialog.AvailableOptions = ExpeditionUnlockInfo.Unlocks.Values
            .Select(x => new StringValueInputForm.Option
            {
                Value = x.Id,
                Display = $"{x.Name} ({x.Id})"
            }).ToList();

        dialog.ShowDialog();

        var item = dialog.SelectedOption;

        if (!string.IsNullOrWhiteSpace(item))
        {
            var realName = ExpeditionUnlockInfo.Unlocks.ContainsKey(item) ? ExpeditionUnlockInfo.Unlocks[item].Name : item;

            unlocksListBox.Items.Add(realName);
            _save.Unlockables.Add(item);
        }
    }

    private void songsAddButton_Click(object sender, EventArgs e)
    {
        using var dialog = new StringValueInputForm();

        dialog.AvailableOptions = ExpeditionUnlockInfo.Unlocks.Values
            .Where(x => x.Name.StartsWith("Music"))
            .Select(x => new StringValueInputForm.Option
            {
                Value = x.Id,
                Display = $"{x.Name} ({x.Id})"
            }).ToList();

        dialog.ShowDialog();

        var item = dialog.SelectedOption;

        if (!string.IsNullOrWhiteSpace(item))
        {
            var realName = ExpeditionUnlockInfo.Unlocks.ContainsKey(item) ? ExpeditionUnlockInfo.Unlocks[item].Name : item;

            songsListBox.Items.Add(realName);
            _save.NewSongs.Add(item);
        }
    }

    private void completedMissionsAddButton_Click(object sender, EventArgs e)
    {
        using var dialog = new StringValueInputForm();

        dialog.AvailableOptions = ExpeditionMissionInfo.Missions.Values
            .Select(x => new StringValueInputForm.Option
            {
                Value = x.Key,
                Display = $"{x.Name} ({x.Key})"
            }).ToList();

        dialog.ShowDialog();

        var item = dialog.SelectedOption;

        if (!string.IsNullOrWhiteSpace(item))
        {
            var realName = ExpeditionMissionInfo.Missions.ContainsKey(item) ? ExpeditionMissionInfo.Missions[item].Name : item;

            completedMissionsListBox.Items.Add(realName);
            _save.Missions.Add(item);
        }
    }

    private void completedQuestsAddButton_Click(object sender, EventArgs e)
    {
        using var dialog = new StringValueInputForm();

        dialog.AvailableOptions = ExpeditionQuestInfo.Quests.Values
            .Select(x => new StringValueInputForm.Option
            {
                Value = x.Key,
                Display = $"Quest #{x.Key[3..]} ({x.Key})"
            }).ToList();

        dialog.ShowDialog();

        var item = dialog.SelectedOption;

        if (!string.IsNullOrWhiteSpace(item))
        {
            var realName = ExpeditionQuestInfo.Quests.ContainsKey(item) ? $"Quest #{item[3..]}" : item;

            completedQuestsListBox.Items.Add(realName);
            _save.Quests.Add(item);
        }
    }

    private void missionBestTimesAddButton_Click(object sender, EventArgs e)
    {
        using var dialog = new TimeSpanValueInputForm();

        dialog.AvailableOptions = ExpeditionMissionInfo.Missions.Values
            .Select(x => new TimeSpanValueInputForm.Option
            {
                Value = x.Key,
                Display = $"{x.Name} ({x.Key})"
            }).ToList();

        dialog.ShowDialog();

        var item = dialog.SelectedOption;
        var time = dialog.SelectedTime;

        if (!string.IsNullOrWhiteSpace(item))
        {
            var realName = ExpeditionMissionInfo.Missions.ContainsKey(item) ? ExpeditionMissionInfo.Missions[item].Name : item;

            missionBestTimesListBox.Items.Add($"{realName}: {time:hh\\:mm\\:ss}");
            _save.MissionBestTimes.Add(new MissionBestTime
            {
                Mission = item,
                Time = (int)time.TotalSeconds
            });
        }
    }

    private void challengesAddButton_Click(object sender, EventArgs e)
    {
        using var dialog = new IntValueInputForm();

        // TODO: Add options for challenges you can add
        dialog.AvailableOptions = [];

        dialog.ShowDialog();

        var item = dialog.SelectedOption;
        var count = dialog.SelectedNumber;

        if (!string.IsNullOrWhiteSpace(item))
        {
            challengesListBox.Items.Add($"{item}: {count}");
            _save.ChallengeTypes.Add(new ChallengeType
            {
                Type = item,
                Count = count
            });
        }
    }

    private void slugcatPassagesNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (selectedSlugcat is null)
            return;

        int passages = (int)slugcatPassagesNumericUpDown.Value;

        int index = _save.Passages.FindIndex(x => x.Slugcat == selectedSlugcat);

        if (index == -1)
        {
            _save.Passages.Add(new PassageEntry
            {
                Slugcat = selectedSlugcat,
                Count = passages
            });
        }
        else
        {
            _save.Passages[index] = _save.Passages[index] with { Count = passages };
        }
    }

    private void slugcatWinsNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (selectedSlugcat is null)
            return;

        int wins = (int)slugcatWinsNumericUpDown.Value;

        int index = _save.WinEntries.FindIndex(x => x.Slugcat == selectedSlugcat);

        if (index == -1)
        {
            _save.WinEntries.Add(new WinEntry
            {
                Slugcat = selectedSlugcat,
                Wins = wins
            });
        }
        else
        {
            _save.WinEntries[index] = _save.WinEntries[index] with { Wins = wins };
        }
    }

    private void UpdateSlugcatMission(string mission)
    {
        if (selectedSlugcat is null)
            return;

        var matchingName = ExpeditionMissionInfo.Missions.Values.FirstOrDefault(x => $"{x.Name} ({x.Key})" == mission);

        var key = mission;

        if (matchingName is not null)
        {
            key = matchingName.Key;
        }

        int index = _save.ActiveMissionEntries.FindIndex(x => x.Slugcat == selectedSlugcat);

        if (index == -1)
        {
            _save.ActiveMissionEntries.Add(new ActiveMissionEntry
            {
                Slugcat = selectedSlugcat,
                Data = key
            });
        }
        else
        {
            _save.ActiveMissionEntries[index] = _save.ActiveMissionEntries[index] with { Data = key };
        }

        var matchingKey = ExpeditionMissionInfo.Missions.Values.FirstOrDefault(x => x.Key == key);

        if (matchingKey is not null)
        {
            if (matchingName is null)
            {
                slugcatActiveMissionMappedLabel.Text = $"{matchingKey.Name} ({matchingKey.Key})";
            }
            else
            {
                slugcatActiveMissionMappedLabel.Text = "";
            }
        }
        else
        {
            slugcatActiveMissionMappedLabel.Text = string.IsNullOrEmpty(mission) ? "[no mission]" : "[unknown mission]";
        }
    }

    private void slugcatActiveMissionComboBox_TextUpdate(object sender, EventArgs e)
    {
        UpdateSlugcatMission(slugcatActiveMissionComboBox.Text);
    }

    private void slugcatActiveMissionComboBox_DropDown(object sender, EventArgs e)
    {
        slugcatActiveMissionComboBox.Items.Clear();
        slugcatActiveMissionComboBox.Items.AddRange(ExpeditionMissionInfo.Missions.Values.Select(x => $"{x.Name} ({x.Key})").ToArray());
    }

    private void slugcatActiveMissionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
    {
        UpdateSlugcatMission((string)slugcatActiveMissionComboBox.SelectedItem!);
    }

    private void slugcatRequiredModsAddButton_Click(object sender, EventArgs e)
    {
        if (selectedSlugcat is null)
            return;

        using var dialog = new StringValueInputForm();

        var mods = new string[]
        {
            "rwremix",
            "expedition",
            "devtools",
            "moreslugcats",
            "jollycoop"
        };

        // TODO: Add a source of well known mods?
        dialog.AvailableOptions = mods.Select(x => new StringValueInputForm.Option
        {
            Value = x,
            Display = x
        }).ToList();

        dialog.ShowDialog();

        var item = dialog.SelectedOption;

        if (!string.IsNullOrWhiteSpace(item))
        {
            var list = _save.RequiredModsEntries.FirstOrDefault(x => x.Slugcat == selectedSlugcat).RequiredMods;

            if (list is null)
            {
                _save.RequiredModsEntries.Add(new RequiredModsEntry
                {
                    Slugcat = selectedSlugcat,
                    RequiredMods = list = []
                });
            }

            slugcatRequiredModsListBox.Items.Add(item);
            list.Add(item);
        }
    }

    private void slugcatRequiredModsRemoveButton_Click(object sender, EventArgs e)
    {
        if (selectedSlugcat is null)
            return;

        var item = (string?)slugcatRequiredModsListBox.SelectedItem;
        var index = slugcatRequiredModsListBox.SelectedIndex;

        if (item is not null)
        {
            var list = _save.RequiredModsEntries.FirstOrDefault(x => x.Slugcat == selectedSlugcat).RequiredMods;

            if (list is null)
            {
                _save.RequiredModsEntries.Add(new RequiredModsEntry
                {
                    Slugcat = selectedSlugcat,
                    RequiredMods = list = []
                });
            }

            list.Remove(item);
            slugcatRequiredModsListBox.Items.RemoveAt(index);
        }
    }

    private void slugcatUnlocksAddButton_Click(object sender, EventArgs e)
    {
        if (selectedSlugcat is null)
            return;

        using var dialog = new StringValueInputForm();

        // TODO: Check what the actual allowed values are supposed to be
        dialog.AvailableOptions = ExpeditionUnlockInfo.Unlocks.Values
            .Where(x => x.Name.StartsWith("Perk:") || x.Name.StartsWith("Burden:"))
            .Select(x => new StringValueInputForm.Option
            {
                Value = x.Id,
                Display = $"{x.Name} ({x.Id})"
            }).ToList();

        dialog.ShowDialog();

        var item = dialog.SelectedOption;

        if (!string.IsNullOrWhiteSpace(item))
        {
            var realName = ExpeditionUnlockInfo.Unlocks.ContainsKey(item) ? ExpeditionUnlockInfo.Unlocks[item].Name : item;

            slugcatUnlocksListBox.Items.Add(realName);
            _save.Unlocks.Add(new UnlockEntry
            {
                Slugcat = selectedSlugcat,
                Data = item
            });
        }
    }

    private void slugcatUnlocksRemoveButton_Click(object sender, EventArgs e)
    {
        if (selectedSlugcat is null)
            return;

        var item = (string?)slugcatUnlocksListBox.SelectedItem;
        var index = slugcatUnlocksListBox.SelectedIndex;
        if (item is not null)
        {
            var realKey = ExpeditionUnlockInfo.Unlocks.Values.FirstOrDefault(x => x.Name == item)?.Id;

            if (realKey is null)
            {
                Logger.Warn("TODO: Encountered a weird unlock key!");
                return;
            }

            var indexToRemove = _save.Unlocks.FindIndex(x => x.Slugcat == selectedSlugcat && x.Data == realKey);

            if (indexToRemove != -1)
            {
                _save.Unlocks.RemoveAt(indexToRemove);
                slugcatUnlocksListBox.Items.RemoveAt(index);
            }
            else
            {
                Logger.Warn("TODO: Failed to find unlock in save?!?!?!");
            }
        }
    }
}
