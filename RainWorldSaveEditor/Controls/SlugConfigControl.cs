using RainWorldSaveEditor.Editor_Classes;
using RainWorldSaveEditor.Save;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RainWorldSaveEditor.Controls;

public partial class SlugConfigControl : UserControl
{
    public SlugConfigControl()
    {
        InitializeComponent();

        cycleNumberNumericUpDown.Minimum = uint.MinValue;
        cycleNumberNumericUpDown.Maximum = uint.MaxValue;
    }

    public SaveState SaveState { get; private set; } = null!;
    private void SlugConfigControl_Load(object sender, EventArgs e)
    {
        LoadCommunitiesTabPage();
    }

    public void SetupFromState(SaveState state)
    {
        SaveState = state;
        SetupSlugCatInfoTabPageFromState(state);
        SetupPersistentDataInfoTabPageFromState(state);
    }

    private void SetupSlugCatInfoTabPageFromState(SaveState state)
    {
        FoodPipControl.FilledPips = (byte)state.FoodCount;
        cycleNumberNumericUpDown.Value = (uint)state.CycleNumber;
        currentDenTextBox.Text = state.DenPosition;
        KarmaSelectorControl.KarmaMax = state.DeathPersistentSaveData.KarmaCap;
        KarmaSelectorControl.KarmaLevel = Math.Min(state.DeathPersistentSaveData.Karma, state.DeathPersistentSaveData.KarmaCap);
        KarmaSelectorControl.Reinforced = state.DeathPersistentSaveData.HasReinforcedKarma == 1;

        lastVanillaDenTextBox.Text = state.LastVanillaDen;

        // Common for all Slugcats
        markOfCommunicationCheckBox.Checked = state.DeathPersistentSaveData.HasMarkOfCommunication;
        justBeatGameCheckBox.Checked = state.GameRecentlyBeaten;
        guideOverseerDeadCheckBox.Checked = state.IsGuideOverseerDead;

        // Slugcat Specific
        neuronGlowCheckBox.Checked = state.HasNeuronGlow;
        citizenIDDroneCheckBox.Checked = state.HasCitizenDrone;
        moonsCloakCheckBox.Checked = state.IsWearingCloak;

    }

    private void SetupPersistentDataInfoTabPageFromState(SaveState state)
    {
        if (state.DeathPersistentSaveData.KarmaFlowerPosition is not null)
        {
            karmaFlowerGroupBox.Enabled = true;
            karmaFlowerWorldPositionEditControl.RoomName = state.DeathPersistentSaveData.KarmaFlowerPosition!.RoomName;
            karmaFlowerWorldPositionEditControl.X = state.DeathPersistentSaveData.KarmaFlowerPosition!.X;
            karmaFlowerWorldPositionEditControl.Y = state.DeathPersistentSaveData.KarmaFlowerPosition!.Y;
            karmaFlowerWorldPositionEditControl.AbstractNode = state.DeathPersistentSaveData.KarmaFlowerPosition!.AbstractNode;
        }
        else
        {
            karmaFlowerGroupBox.Enabled = false;
            karmaFlowerWorldPositionEditControl.RoomName = string.Empty;
            karmaFlowerWorldPositionEditControl.X = 0;
            karmaFlowerWorldPositionEditControl.Y = 0;
            karmaFlowerWorldPositionEditControl.AbstractNode = 0;
        }

        totalDeathsNumericUpDown.Value = state.DeathPersistentSaveData.Deaths;
        totalSurvivesNumericUpDown.Value = state.DeathPersistentSaveData.Survives;
        totalQuitsNumericUpDown.Value = state.DeathPersistentSaveData.Quits;
        totalFoodNumericUpDown.Value = state.TotalFoodEaten;

        totalFriendsSavedNumericUpDown.Value = state.DeathPersistentSaveData.FriendsSaved;

        ascendedLooksToTheMoonCheckBox.Checked = state.DeathPersistentSaveData.IsMoonAscendedBySaint;
        ascendedFivePebblesCheckBox.Checked = state.DeathPersistentSaveData.IsPebblesAscendedBySaint;

        ascendedCheckBox.Checked = state.DeathPersistentSaveData.HasAscended;
        hunterPermaDeathCheckBox.Checked = state.DeathPersistentSaveData.IsHunterDead;


    }

    #region Communities Tab
    DataTable _communitiesDataTable = new DataTable();
    private void LoadCommunitiesTabPage()
    {
        _communitiesDataTable.Columns.Add("Region Code");
        _communitiesDataTable.Columns.Add("Region Name");
        _communitiesDataTable.Columns.Add("Value");
        communityRegionRepDataGridView.DataSource = _communitiesDataTable;
        communityRegionRepDataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        communityRegionRepDataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

        communityRegionRepDataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        communityRegionRepDataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
        communityRegionRepDataGridView.Columns[1].ReadOnly = true;

        communityRegionRepDataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        communityRegionRepDataGridView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
    }

    private void FillCommunityRegionRepListView(Community community)
    {
        _communitiesDataTable.Rows.Clear();

        foreach (var pair in community.PlayerRegionalReputation)
        {
            _communitiesDataTable.Rows.Add(pair.Key, Translation.GetRegionName(pair.Key), pair.Value * 100);
        }

        communityRegionRepDataGridView.Tag = community;
    }

    private void communityListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        communityRegionRepDataGridView.Enabled = communityListBox.SelectedIndex != -1;

        switch (communityListBox.SelectedItem)
        {
            case "All":
                FillCommunityRegionRepListView(SaveState.Communities.All);
                break;
            case "Scavengers":
                FillCommunityRegionRepListView(SaveState.Communities.Scavengers);
                break;
            case "Lizards":
                FillCommunityRegionRepListView(SaveState.Communities.Lizards);
                break;
            case "Cicadas":
                FillCommunityRegionRepListView(SaveState.Communities.Cicadas);
                break;
            case "Garbage Worms":
                FillCommunityRegionRepListView(SaveState.Communities.GarbageWorms);
                break;
            case "Deer":
                FillCommunityRegionRepListView(SaveState.Communities.Deer);
                break;
            case "Jet Fish":
                FillCommunityRegionRepListView(SaveState.Communities.JetFish);
                break;
        }
    }

    #endregion

    string _communityCellPreValue = string.Empty;

    private void communityRegionRepDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
        var text = _communitiesDataTable.Rows[e.RowIndex][e.ColumnIndex].ToString();

        if (text == "EVERY")
            e.Cancel = true;

        _communityCellPreValue = text!;
    }

    private void communityRegionRepDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        var userValue = _communitiesDataTable.Rows[e.RowIndex][e.ColumnIndex].ToString();
        var userValueCaps = userValue!.ToUpper();

        switch (e.ColumnIndex)
        {
            case 0:
                if (userValue == string.Empty)
                {
                    MessageBox.Show("Region Code cannot be blank.", "Invalid Selection");
                    _communitiesDataTable.Rows[e.RowIndex][e.ColumnIndex] = _communityCellPreValue;
                }

                foreach (DataRow item in _communitiesDataTable.Rows)
                {
                    if (item.ItemArray[0]!.ToString() == userValueCaps)
                    {
                        MessageBox.Show("Region Code has already been added.", "Invalid Selection");
                        _communitiesDataTable.Rows[e.RowIndex][e.ColumnIndex] = _communityCellPreValue;
                        return;
                    }
                }

                // Adjust value here?
                break;
            case 2:
                float value = 0;
                if (float.TryParse(userValue, out value) && value >= -100 && value <= 100)
                {
                    // Set the value here
                }
                else
                {
                    _communitiesDataTable.Rows[e.RowIndex][e.ColumnIndex] = _communityCellPreValue;
                    MessageBox.Show("Value must range from -100 to +100.", "Invalid Selection");
                }
                break;
        }
    }

    private void communityRegionRepDataGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
    {
        _communitiesDataTable.Rows.Add();


    }

    private void communityRegionRepDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        var value = e.Row!.Cells[0].Value.ToString()!;

        if (value == "EVERY")
        {
            MessageBox.Show("You cannot delete the \"EVERY\" entry from this list.");
            e.Cancel = true;
            return;
        }

        if (Translation.RegionNames.ContainsKey(value))
        {
            if (MessageBox.Show($"Deleting a vanilla game's region information might cause instability!\nAre you sure you want to delete \"{value}\" - \"{Translation.GetRegionName(value)}\"?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }
    }

    private void communityRegionRepDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {

    }
}
