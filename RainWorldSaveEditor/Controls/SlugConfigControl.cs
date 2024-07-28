using RainWorldSaveEditor.Editor_Classes;
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
using RainWorldSaveAPI;
using RainWorldSaveAPI.SaveElements;
using RainWorldSaveEditor.Forms;

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

    #region Setup
    public void SetupFromState(SaveState state)
    {
        SaveState = state;
        SetupSlugCatInfoTabPageFromState(state);
        SetupEchoTabPageFromState(state);
        SetupPersistentDataInfoTabPageFromState(state);
        SetupAdvancedInfoTabPage(state);
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

    private void SetupEchoTabPageFromState(SaveState state)
    {
        echoDataGridView.Rows.Clear();


        foreach (var echo in state.DeathPersistentSaveData.Echos.EchoStates)
        {
            var stateStr = echo.Value switch
            {
                EchoState.NotMet => "Not Met",
                EchoState.Visited => "Active",
                EchoState.Met => "Met",
                _ => "Not Met",
            };
            echoDataGridView.Rows.Add([echo.Key, Translation.GetRegionName(echo.Key), stateStr]);
        }

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

        rngSeedNumericUpDown.Value = state.Seed;
        rngNextIssueIDNumericUpDown.Value = state.NextIssuedId;

    }

    private void SetupAdvancedInfoTabPage(SaveState state)
    {
        gameVerisonNumericUpDown.Value = state.GameVersion;
        initialGameVersionNumericUpDown.Value = state.InitialGameVersion;

        worldVersionNumericUpDown.Value = state.WorldVersion;

    }

    #endregion

    #region Slugcat Info
    private void FoodPipControl_PipCountChanged(object sender, EventArgs e) => SaveState.FoodCount = FoodPipControl.FilledPips;


    private void cycleNumberNumericUpDown_ValueChanged(object sender, EventArgs e) => SaveState.CycleNumber = (int)cycleNumberNumericUpDown.Value;
    private void neuronGlowCheckBox_CheckedChanged(object sender, EventArgs e) => SaveState.HasNeuronGlow = neuronGlowCheckBox.Checked;
    private void justBeatGameCheckBox_CheckedChanged(object sender, EventArgs e) => SaveState.GameRecentlyBeaten = justBeatGameCheckBox.Checked;
    private void markOfCommunicationCheckBox_CheckedChanged(object sender, EventArgs e) => SaveState.DeathPersistentSaveData.HasMarkOfCommunication = markOfCommunicationCheckBox.Checked;


    #region Slugcat Specific
    private void citizenIDDroneCheckBox_CheckedChanged(object sender, EventArgs e) => SaveState.HasCitizenDrone = citizenIDDroneCheckBox.Checked;
    private void extraHunterCyclesCheckBox_CheckedChanged(object sender, EventArgs e) => SaveState.HunterExtraCycles = extraHunterCyclesCheckBox.Checked;
    private void moonsCloakCheckBox_CheckedChanged(object sender, EventArgs e) => SaveState.IsWearingCloak = moonsCloakCheckBox.Checked;
    #endregion

    #region Den Info
    private void currentDenTextBox_TextChanged(object sender, EventArgs e) => SaveState.DenPosition = currentDenTextBox.Text;
    private void lastVanillaDenTextBox_TextChanged(object sender, EventArgs e) => SaveState.LastVanillaDen = currentDenTextBox.Text;
    #endregion

    #region Karma 
    private void KarmaSelectorControl_KarmaLevelChanged(object sender, EventArgs e) => SaveState.DeathPersistentSaveData.Karma = KarmaSelectorControl.KarmaLevel;
    private void KarmaSelectorControl_KarmaMaxChanged(object sender, EventArgs e) => SaveState.DeathPersistentSaveData.KarmaCap = KarmaSelectorControl.KarmaMax;
    private void KarmaSelectorControl_ReinforcedChanged(object sender, EventArgs e) => SaveState.DeathPersistentSaveData.HasReinforcedKarma = KarmaSelectorControl.Reinforced ? 1 : 0;
    #endregion

    #endregion

    #region Echos tab

    #endregion

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

        if (userValue == _communityCellPreValue)
            return;

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
    private void communityListBox_DrawItem(object sender, DrawItemEventArgs e)
    {
        // Vultu: This might cuase GC pressure later
        if (e.State.HasFlag(DrawItemState.Selected))
        {
            e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);

            var item = communityListBox.Items[e.Index].ToString();
            var strHeight = e.Graphics.MeasureString(item, Font).Height;

            e.Graphics.DrawString(item, Font, Brushes.White, e.Bounds.X + communityListBox.ItemHeight, (e.Bounds.Y + e.Bounds.Height / 2.0f) - (strHeight / 2.0f));
        }
        else
        {
            using SolidBrush bgBrush = new SolidBrush(communityListBox.BackColor);

            e.Graphics.FillRectangle(bgBrush, e.Bounds);

            var item = communityListBox.Items[e.Index].ToString();
            var strHeight = e.Graphics.MeasureString(item, Font).Height;

            e.Graphics.DrawString(item, Font, Brushes.Black, e.Bounds.X + communityListBox.ItemHeight, (e.Bounds.Y + e.Bounds.Height / 2.0f) - (strHeight / 2.0f));
        }


        // TEMP
        Bitmap img = e.Index switch
        {
            1 => Properties.Resources.scavenger_community_icon,
            2 => Properties.Resources.lizard_community_icon,
            3 => Properties.Resources.squidcada_community_icon,
            4 => Properties.Resources.garbageworm_community_icon,
            5 => Properties.Resources.raindeer_community_icon,
            6 => Properties.Resources.jetfish_community_icon,
            _ => null!
        };
        // END OF TEMP

        if (img is not null)
        {
            float imgHeight = communityListBox.ItemHeight;
            float imgWidth = communityListBox.ItemHeight;

            var aspectRatio = (float)img.Width / (float)img.Height;

            // Logger.Trace($"Aspect Ratio[{e.Index}] {aspectRatio}");

            if (img.Width > img.Height)
                imgHeight = communityListBox.ItemHeight * aspectRatio;
            else if (img.Height < img.Width)
                imgWidth = communityListBox.ItemHeight * aspectRatio;

            var imagePosCenter = new PointF(e.Bounds.X + (communityListBox.ItemHeight / 2.0f), e.Bounds.Y + (communityListBox.ItemHeight / 2.0f));
            var imageSizeCenter = new PointF(imgHeight / 2.0f, imgWidth / 2.0f);

            e.Graphics.DrawImage(img, imageSizeCenter.X - imageSizeCenter.X, imagePosCenter.Y - imageSizeCenter.Y, imgHeight, imgWidth);
        }

    }
    #endregion

    #region Persistent Info

    private void ascendedLooksToTheMoonCheckBox_CheckedChanged(object sender, EventArgs e) => SaveState.DeathPersistentSaveData.IsMoonAscendedBySaint = ((CheckBox)sender).Checked;

    private void ascendedFivePebblesCheckBox_CheckedChanged(object sender, EventArgs e) => SaveState.DeathPersistentSaveData.IsPebblesAscendedBySaint = ((CheckBox)sender).Checked;

    private void hunterPermaDeathCheckBox_CheckedChanged(object sender, EventArgs e) => SaveState.DeathPersistentSaveData.IsHunterDead = ((CheckBox)sender).Checked;

    private void ascendedCheckBox_CheckedChanged(object sender, EventArgs e) => SaveState.DeathPersistentSaveData.HasAscended = ((CheckBox)sender).Checked;

    #region Totals
    private void totalDeathsNumericUpDown_ValueChanged(object sender, EventArgs e) => SaveState.DeathPersistentSaveData.Deaths = (int)((NumericUpDown)(sender)).Value;
    private void totalSurvivesNumericUpDown_ValueChanged(object sender, EventArgs e) => SaveState.DeathPersistentSaveData.Survives = (int)((NumericUpDown)(sender)).Value;
    private void totalQuitsNumericUpDown_ValueChanged(object sender, EventArgs e) => SaveState.DeathPersistentSaveData.Quits = (int)((NumericUpDown)(sender)).Value;
    private void totalFoodNumericUpDown_ValueChanged(object sender, EventArgs e) => SaveState.TotalFoodEaten = (int)((NumericUpDown)(sender)).Value;
    private void totalFriendsSavedNumericUpDown_ValueChanged(object sender, EventArgs e) => SaveState.DeathPersistentSaveData.FriendsSaved = (int)((NumericUpDown)(sender)).Value;

    #endregion
    #endregion

    #region Advanced
    private void gameVerisonNumericUpDown_ValueChanged(object sender, EventArgs e)
    {

    }

    private void initialGameVersionNumericUpDown_ValueChanged(object sender, EventArgs e)
    {

    }

    private void worldVersionNumericUpDown_ValueChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region Echo Tabpage
    private void echoDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (e.Row is null)
            return;

        string regionCode = e.Row.Cells[0].Value!.ToString()!;
        string regionName = "";
        if (Translation.RegionNames.TryGetValue(regionCode, out regionName!))
        {
            if (MessageBox.Show($"Deleting a vanilla game's echo information might cause instability!\nAre you sure you want to delete \"{regionCode}\" - \"{regionName}\"?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }

    }

    private void echoDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        SaveState.DeathPersistentSaveData.Echos.EchoStates.Remove(e.Row.Cells[0].Value.ToString()!);
    }

    private void echoDataGridView_SelectionChanged(object sender, EventArgs e)
    {
        removeEchoToolStripMenuItem.Enabled = echoDataGridView.SelectedRows.Count > 0;
    }

    #region Context Menu
    private void addEchoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using AddEchoForm form = new AddEchoForm(SaveState);

        if (form.ShowDialog() == DialogResult.Cancel)
            return;

        var upperRegionCode = form.RegionCode.ToUpper();

        var stateStr = form.EchoState switch
        {
            EchoState.NotMet => "Not Met",
            EchoState.Visited => "Active",
            EchoState.Met => "Met",
            _ => "Not Met",
        };

        SaveState.DeathPersistentSaveData.Echos.EchoStates.Add(upperRegionCode, form.EchoState);
        echoDataGridView.Rows.Add([upperRegionCode, Translation.GetRegionName(upperRegionCode), stateStr]);
    }

    private void removeEchoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        string[] selectedRows = new string[echoDataGridView.SelectedRows.Count];

        for (var i = 0; i < echoDataGridView.SelectedRows.Count; i++)
            selectedRows[i] = echoDataGridView.SelectedRows[i].Cells[0].Value.ToString()!;


        for (var i = 0; i < selectedRows.Length; i++)
        {
            int id = -1;
            for (var r = 0; r < echoDataGridView.Rows.Count; r++)
            {
                if (echoDataGridView.Rows[r].Cells[0].Value.ToString() == selectedRows[i])
                {
                    id = i;
                    break;
                }
            }

            if (id == -1)
            {
                Logger.Error($"Unable to find id {id} for echo: \"{selectedRows[i]}\"");
                continue;
            }

            string regionCode = echoDataGridView.Rows[id].Cells[0].Value!.ToString()!;
            string regionName = "";
            if (Translation.RegionNames.TryGetValue(regionCode, out regionName!))
            {
                if (MessageBox.Show($"Deleting a vanilla game's echo information might cause instability!\nAre you sure you want to delete \"{regionCode}\" - \"{regionName}\"?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    continue;
                }
            }

            SaveState.DeathPersistentSaveData.Echos.EchoStates.Remove(regionCode);
            echoDataGridView.Rows.RemoveAt(id);
        }
    }

    private void duplicateEchoToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }
    #endregion
    #endregion

}
