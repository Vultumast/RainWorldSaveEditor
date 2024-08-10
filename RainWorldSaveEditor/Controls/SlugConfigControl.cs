using RainWorldSaveEditor.Editor_Classes;
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
        InitIteratorTabPage();
    }

    #region Setup
    public void SetupFromState(SaveState state)
    {
        tabControl.Enabled = state is not null;
        tabControl.SelectedTab = slugcatInfoTabPage;

        SaveState = state!;
        SetupSlugCatInfoTabPageFromState(state!);
        SetupEchoTabPageFromState(state!);
        LoadIteratorTabPage(state!);
        SetupCommunitiesTabPage(state!);
        SetupPersistentDataInfoTabPageFromState(state!);
        SetupAdvancedInfoTabPage(state!);
    }


    #endregion

    #region Slugcat Info

    private void SetupSlugCatInfoTabPageFromState(SaveState state)
    {
        if (state is not null)
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
        else
        {
            FoodPipControl.FilledPips = 0;
            cycleNumberNumericUpDown.Value = 0;
            currentDenTextBox.Text = string.Empty;
            KarmaSelectorControl.KarmaMax = 9;
            KarmaSelectorControl.KarmaLevel = 0;
            KarmaSelectorControl.Reinforced = false;

            lastVanillaDenTextBox.Text = string.Empty;

            // Common for all Slugcats
            markOfCommunicationCheckBox.Checked = false;
            justBeatGameCheckBox.Checked = false;
            guideOverseerDeadCheckBox.Checked = false;

            // Slugcat Specific
            neuronGlowCheckBox.Checked = false;
            citizenIDDroneCheckBox.Checked = false;
            moonsCloakCheckBox.Checked = false;
        }
    }

    private void FoodPipControl_PipCountChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.FoodCount = FoodPipControl.FilledPips;
    }


    private void cycleNumberNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.CycleNumber = (int)cycleNumberNumericUpDown.Value;
    }
    private void neuronGlowCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.HasNeuronGlow = neuronGlowCheckBox.Checked;
    }
    private void justBeatGameCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.GameRecentlyBeaten = justBeatGameCheckBox.Checked;
    }
    private void markOfCommunicationCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.HasMarkOfCommunication = markOfCommunicationCheckBox.Checked;
    }


    #region Slugcat Specific
    private void citizenIDDroneCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.HasCitizenDrone = citizenIDDroneCheckBox.Checked;
    }
    private void extraHunterCyclesCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.HunterExtraCycles = extraHunterCyclesCheckBox.Checked;
    }
    private void moonsCloakCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.IsWearingCloak = moonsCloakCheckBox.Checked;
    }
    #endregion

    #region Den Info
    private void currentDenTextBox_TextChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DenPosition = currentDenTextBox.Text;
    }
    private void lastVanillaDenTextBox_TextChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.LastVanillaDen = currentDenTextBox.Text;
    }
    #endregion

    #region Karma 
    private void KarmaSelectorControl_KarmaLevelChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.Karma = KarmaSelectorControl.KarmaLevel;
    }
    private void KarmaSelectorControl_KarmaMaxChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.KarmaCap = KarmaSelectorControl.KarmaMax;
    }
    private void KarmaSelectorControl_ReinforcedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.HasReinforcedKarma = KarmaSelectorControl.Reinforced ? 1 : 0;
    }
    #endregion

    #endregion

    #region Echos tab
    private void SetupEchoTabPageFromState(SaveState state)
    {
        if (state is not null)
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
        else
            echoDataGridView.Rows.Clear();
    }

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

    #region Communities Tab

    private void LoadCommunitiesTabPage()
    {
        communityListBox.Items.Clear();
        communityListBox.Items.Add("All");

        foreach (var community in CommunityInfo.Communities)
            communityListBox.Items.Add(community);

    }
    private void SetupCommunitiesTabPage(SaveState state)
    {
        if (state is null)
            communityListBox.SelectedIndex = -1;

    }

    private void FillCommunityRegionRepListView(Community community)
    {
        communityRegionRepDataGridView.Rows.Clear();

        foreach (var pair in community.PlayerRegionalReputation)
        {
            communityRegionRepDataGridView.Rows.Add(pair.Key, Translation.GetRegionName(pair.Key), pair.Value * 100);
        }

        communityRegionRepDataGridView.Tag = community;

        communityRegionRepDataGridView.Rows[0].Cells[0].ReadOnly = true;
    }

    private void communityListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (communityListBox.SelectedIndex == -1)
        {
            communityRegionRepDataGridView.Rows.Clear();
            return;
        }

        communityRegionRepDataGridView.Rows.Clear();
        communityRegionRepDataGridView.Enabled = communityListBox.SelectedIndex != -1;

        Community communityValue = null!;

        if (communityListBox.SelectedIndex == 0)
            communityValue = SaveState.Communities.Communities["All"];
        else
            SaveState.Communities.Communities.TryGetValue(((KeyValuePair<string, CommunityInfo>)communityListBox.SelectedItem!).Value.CommunityID, out communityValue!);


        if (communityValue is not null)
            FillCommunityRegionRepListView(communityValue);
        else
            Logger.Warn($"SaveState Communities did not contain an entry for \"{communityListBox.SelectedItem!.ToString()!}\"");

    }

    string _communityCellPreValue = string.Empty;

    private void communityRegionRepDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
        var text = communityRegionRepDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

        if (text == "EVERY")
            e.Cancel = true;

        _communityCellPreValue = text!;
    }

    private void communityRegionRepDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        var userValue = communityRegionRepDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        var userValueCaps = userValue!.ToUpper();

        if (userValue == _communityCellPreValue)
            return;
        string communityID = string.Empty;
        string regionCode = string.Empty;
        float value = 0;
        switch (e.ColumnIndex)
        {
            case 0:
                if (userValue == string.Empty)
                {
                    MessageBox.Show("Region Code cannot be blank.", "Invalid Selection");
                    communityRegionRepDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _communityCellPreValue;
                }

                if (SaveState.Communities.Communities.ContainsKey(userValue) || SaveState.Communities.Communities.ContainsKey(userValueCaps))
                {
                    MessageBox.Show("Region Code has already been added.", "Invalid Selection");
                    communityRegionRepDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _communityCellPreValue;
                    return;
                }

                communityID = ((KeyValuePair<string, CommunityInfo>)communityListBox.SelectedItem!).Value.CommunityID;
                regionCode = communityRegionRepDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()!;

                value = SaveState.Communities.Communities[communityID].PlayerRegionalReputation[_communityCellPreValue];

                SaveState.Communities.Communities[communityID].PlayerRegionalReputation.Remove(_communityCellPreValue);
                SaveState.Communities.Communities[communityID].PlayerRegionalReputation.Add(regionCode, value);

                break;
            case 2:
                value = 0;
                if (float.TryParse(userValue, out value) && value >= -100 && value <= 100)
                {
                    communityID = "All";

                    if (communityListBox.SelectedIndex > 0)
                        communityID = ((KeyValuePair<string, CommunityInfo>)communityListBox.SelectedItem!).Value.CommunityID;

                    regionCode = communityRegionRepDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()!;

                    SaveState.Communities.Communities[communityID].PlayerRegionalReputation[regionCode] = (value / 100.0f);
                }
                else
                {
                    communityRegionRepDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _communityCellPreValue;
                    MessageBox.Show("Value must range from -100 to +100.", "Invalid Selection");
                }
                break;
        }
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
        SaveState.Communities.Communities[((KeyValuePair<string, CommunityInfo>)communityListBox.SelectedItem!).Value.CommunityID].PlayerRegionalReputation.Remove(e.Row.Cells[0].Value.ToString()!);
    }
    private void communityListBox_DrawItem(object sender, DrawItemEventArgs e)
    {
        // Vultu: This might cuase GC pressure later
        if (e.State.HasFlag(DrawItemState.Selected))
        {
            e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);

            var selecteditem = "All";

            if (e.Index > 0)
                selecteditem = ((KeyValuePair<string, CommunityInfo>)communityListBox.Items[e.Index]).Value.Name;

            var strHeight = e.Graphics.MeasureString(selecteditem, Font).Height;

            e.Graphics.DrawString(selecteditem, Font, Brushes.White, e.Bounds.X + communityListBox.ItemHeight, (e.Bounds.Y + e.Bounds.Height / 2.0f) - (strHeight / 2.0f));
        }
        else
        {
            using SolidBrush bgBrush = new SolidBrush(communityListBox.BackColor);

            e.Graphics.FillRectangle(bgBrush, e.Bounds);

            var selecteditem = "All";

            if (e.Index > 0)
                selecteditem = ((KeyValuePair<string, CommunityInfo>)communityListBox.Items[e.Index]).Value.Name;

            var strHeight = e.Graphics.MeasureString(selecteditem, Font).Height;

            e.Graphics.DrawString(selecteditem, Font, Brushes.Black, e.Bounds.X + communityListBox.ItemHeight, (e.Bounds.Y + e.Bounds.Height / 2.0f) - (strHeight / 2.0f));
        }

        Bitmap img = null!;
        var item = communityListBox.Items[e.Index];
        if (item.GetType() == typeof(KeyValuePair<string, CommunityInfo>))
        {
            string filepath = $"{CommunityInfo.CreatureCommunityIconsDirectoryPath}\\{((KeyValuePair<string, CommunityInfo>)item).Value.IconPath}";

            if (File.Exists(filepath))
                img = new Bitmap(filepath);
        }

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

        img?.Dispose();

    }
    #endregion

    #region Persistent Info
    private void SetupPersistentDataInfoTabPageFromState(SaveState state)
    {
        if (state is not null)
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

            totalFriendsSavedNumericUpDown.Value = state.DeathPersistentSaveData.FriendsSaved.Value;

            ascendedLooksToTheMoonCheckBox.Checked = state.DeathPersistentSaveData.IsMoonAscendedBySaint;

            ascendedCheckBox.Checked = state.DeathPersistentSaveData.HasAscended;
            hunterPermaDeathCheckBox.Checked = state.DeathPersistentSaveData.IsHunterDead;

            rngSeedNumericUpDown.Value = state.Seed;
            rngNextIssueIDNumericUpDown.Value = state.NextIssuedId;

            playTimeNumericUpDown.Value = state.TotalTimeInSeconds;
        }
        else
        {
            totalDeathsNumericUpDown.Value = 0;
            totalSurvivesNumericUpDown.Value = 0;
            totalQuitsNumericUpDown.Value = 0;
            totalFoodNumericUpDown.Value = 0;

            totalFriendsSavedNumericUpDown.Value = 0;

            ascendedLooksToTheMoonCheckBox.Checked = false;

            ascendedCheckBox.Checked = false;
            hunterPermaDeathCheckBox.Checked = false;

            rngSeedNumericUpDown.Value = 0;
            rngNextIssueIDNumericUpDown.Value = 0;
            playTimeNumericUpDown.Value = 0;
        }
    }

    private void ascendedLooksToTheMoonCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.IsMoonAscendedBySaint = ((CheckBox)sender).Checked;
    }

    private void ascendedFivePebblesCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.IsPebblesAscendedBySaint = ((CheckBox)sender).Checked;
    }

    private void hunterPermaDeathCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.IsHunterDead = ((CheckBox)sender).Checked;
    }

    private void ascendedCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.HasAscended = ((CheckBox)sender).Checked;
    }

    #region Totals
    private void totalDeathsNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.Deaths = (int)((NumericUpDown)(sender)).Value;
    }
    private void totalSurvivesNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.Survives = (int)((NumericUpDown)(sender)).Value;
    }
    private void totalQuitsNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.Quits = (int)((NumericUpDown)(sender)).Value;
    }
    private void totalFoodNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.TotalFoodEaten = (int)((NumericUpDown)(sender)).Value;
    }
    private void totalFriendsSavedNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.FriendsSaved.Value = (int)((NumericUpDown)(sender)).Value;
    }

    #endregion

    private void playTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        var t = TimeSpan.FromSeconds((double)playTimeNumericUpDown.Value);

        playTimeLabel.Text = string.Format("Playtime: {0:D5}:d{1:D2}h:{2:D2}m:{3:D2}s", t.Days,  t.Hours, t.Minutes, t.Seconds);

        if (SaveState is not null)
            SaveState.TotalTimeInSeconds = (int)playTimeNumericUpDown.Value;
    }

    #endregion

    #region Advanced
    private void SetupAdvancedInfoTabPage(SaveState state)
    {
        if (state is not null)
        {
            gameVerisonNumericUpDown.Value = state.GameVersion;
            initialGameVersionNumericUpDown.Value = state.InitialGameVersion;

            worldVersionNumericUpDown.Value = state.WorldVersion;
        }
        else
        {
            gameVerisonNumericUpDown.Value = 0;
            initialGameVersionNumericUpDown.Value = 0;

            worldVersionNumericUpDown.Value = 0;
        }

    }

    private void gameVerisonNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.GameVersion = (int)gameVerisonNumericUpDown.Value;
    }

    private void initialGameVersionNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.InitialGameVersion = (int)initialGameVersionNumericUpDown.Value;
    }

    private void worldVersionNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.WorldVersion = (int)worldVersionNumericUpDown.Value;
    }
    #endregion


    #region Iterator

    private void InitIteratorTabPage()
    {
        _pebblesCellConvoRadioButtons = [
            pebblesCellState0RadioButton,
            pebblesCellState1RadioButton,
            pebblesCellState2RadioButton,
            pebblesCellState3RadioButton,
        ];
    }
    private void LoadIteratorTabPage(SaveState state)
    {
        if (state is not null)
        {
            pebblesConversationCountNumericUpDown.Value = state.MiscWorldSaveData.TimesTalkedWithFivePebbles.Value;
            pebblesThrownOutCountNumericUpDown.Value = state.MiscWorldSaveData.TimesKickedOutByFivePebbles.Value;
            frolickedInMemoryArraysCheckBox.Checked = state.MiscWorldSaveData.HasFrolickedInMemoryArrays;

            pebblesFirstVisitCountNumericUpDown.Value = state.MiscWorldSaveData.CyclesSinceFirstFivePebblesVisit.Value;

            pebblesRivuletPostGameTalkCheckBox.Checked = state.MiscWorldSaveData.HasTalkedWithFivePebblesInRivuletPostgame;
            musicPearlStolenCheckBox.Checked = state.MiscWorldSaveData.HasStolenFivePebblesMusicPearl;
            cellRemovedCheckBox.Checked = state.MiscWorldSaveData.HasRemovedRarefactionCell;
            pebblesAscendedCheckBox.Checked = state.DeathPersistentSaveData.IsPebblesAscendedBySaint;

            FivePebblesRarefactionCellConversationState = state.MiscWorldSaveData.RarefactionCellConversationState.Value;
        }
        else
        {
            pebblesConversationCountNumericUpDown.Value = 0;
            pebblesThrownOutCountNumericUpDown.Value = 0;
            frolickedInMemoryArraysCheckBox.Checked = false;

            pebblesFirstVisitCountNumericUpDown.Value = 0;

            pebblesRivuletPostGameTalkCheckBox.Checked = false;
            musicPearlStolenCheckBox.Checked = false;
            cellRemovedCheckBox.Checked = false;
            pebblesAscendedCheckBox.Checked = false;

            FivePebblesRarefactionCellConversationState = 0;
        }

    }
    private void fivePebblesConversationCountNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.MiscWorldSaveData.TimesTalkedWithFivePebbles.Value = (int)pebblesConversationCountNumericUpDown.Value;
    }
    private void pebblesThrownOutCountNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.MiscWorldSaveData.TimesKickedOutByFivePebbles.Value = (int)pebblesConversationCountNumericUpDown.Value;
    }

    private void frolickedInMemoryArraysCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.MiscWorldSaveData.HasFrolickedInMemoryArrays = frolickedInMemoryArraysCheckBox.Checked;
    }

    private void pebblesFirstVisitCountNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.MiscWorldSaveData.CyclesSinceFirstFivePebblesVisit.Value = (int)pebblesFirstVisitCountNumericUpDown.Value;
    }

    private RadioButton[] _pebblesCellConvoRadioButtons = Array.Empty<RadioButton>();
    public int FivePebblesRarefactionCellConversationState
    {
        get => (int)pebblesCellConvoStateNumericUpDown.Value;
        set
        {
            if (value == pebblesCellConvoStateNumericUpDown.Value)
                return;

            pebblesCellConvoStateNumericUpDown.Value = value;

            var disable = value >= 0 && value < _pebblesCellConvoRadioButtons.Length;

            for (var i = 0; i < _pebblesCellConvoRadioButtons.Length; i++)
            {
                if (disable)
                    _pebblesCellConvoRadioButtons[i].Checked = false;
                else
                    _pebblesCellConvoRadioButtons[i].Checked = i == value;
            }
        }
    }

    private void pebblesCellStateRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (((RadioButton)sender).Checked)
            pebblesCellConvoStateNumericUpDown.Value = int.Parse((string)((Control)sender).Tag!);
    }


    private void pebblesCellConvoStateNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        var value = pebblesCellConvoStateNumericUpDown.Value;
        var disable = !(value >= 0 && value < _pebblesCellConvoRadioButtons.Length);

        for (var i = 0; i < _pebblesCellConvoRadioButtons.Length; i++)
        {
            if (disable)
                _pebblesCellConvoRadioButtons[i].Checked = false;
            else
                _pebblesCellConvoRadioButtons[i].Checked = i == value;
        }

        if (SaveState is not null)
            SaveState.MiscWorldSaveData.RarefactionCellConversationState.Value = (int)pebblesCellConvoStateNumericUpDown.Value;
    }


    private void pebblesRivuletPostGameTalkCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.MiscWorldSaveData.HasTalkedWithFivePebblesInRivuletPostgame = pebblesRivuletPostGameTalkCheckBox.Checked;
    }

    private void pebblesMusicPearlStolenCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (musicPearlStolenCheckBox.Checked)
            commonToolTip.SetToolTip((Control)sender, "Did you steal Five Pebbles' music pearl?\nYou monster.");
        else
            commonToolTip.SetToolTip((Control)sender, "Did you steal Five Pebbles' music pearl?");

        if (SaveState is not null)
            SaveState.MiscWorldSaveData.HasStolenFivePebblesMusicPearl = musicPearlStolenCheckBox.Checked;

    }
    private void cellRemovedCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.MiscWorldSaveData.HasRemovedRarefactionCell = cellRemovedCheckBox.Checked;
    }

    private void pebblesAscendedCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SaveState is not null)
            SaveState.DeathPersistentSaveData.IsPebblesAscendedBySaint = pebblesRivuletPostGameTalkCheckBox.Checked;
    }
    #endregion




    private void rngSeedNumericUpDown_ValueChanged(object sender, EventArgs e)
    {

    }

    private void rngNextIssueIDNumericUpDown_ValueChanged(object sender, EventArgs e)
    {

    }

}
