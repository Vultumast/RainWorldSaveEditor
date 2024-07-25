using RainWorldSaveEditor.Save;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RainWorldSaveEditor.Controls;

public partial class SlugConfigControl : UserControl
{
    public SlugConfigControl()
    {
        InitializeComponent();

        cycleNumberNumericUpDown.Minimum = uint.MinValue;
        cycleNumberNumericUpDown.Maximum = uint.MaxValue;
    }

    private void SlugConfigControl_Load(object sender, EventArgs e)
    {

    }

    public void SetupFromState(SaveState state)
    {
        SetupSlugCatInfoTabPageFromState(state);
        SetupPersistentDataInfoTabPageFromState(state);
    }

    private void SetupSlugCatInfoTabPageFromState(SaveState state)
    {
        FoodPipControl.FilledPips = (byte)state.FoodCount;
        cycleNumberNumericUpDown.Value = (uint)state.CycleNumber;
        currentDenTextBox.Text = state.DenPosition;
        KarmaSelectorControl.KarmaLevel = Math.Min(state.DeathPersistentSaveData.Karma, state.DeathPersistentSaveData.KarmaCap);
        KarmaSelectorControl.KarmaMax = state.DeathPersistentSaveData.KarmaCap;
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
}
