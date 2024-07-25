﻿namespace RainWorldSaveEditor.Controls
{
    partial class SlugConfigControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            FoodPipControl = new FoodPipControl();
            cycleNumberNumericUpDown = new NumericUpDown();
            label1 = new Label();
            tabControl1 = new TabControl();
            slugcatInfoTabPage = new TabPage();
            guideOverseerDeadCheckBox = new CheckBox();
            extraHunterCyclesCheckBox = new CheckBox();
            moonsCloakCheckBox = new CheckBox();
            justBeatGameCheckBox = new CheckBox();
            citizenIDDroneCheckBox = new CheckBox();
            pictureBox1 = new PictureBox();
            neuronGlowCheckBox = new CheckBox();
            denInfoGroupBox = new GroupBox();
            lastVanillaDenTextBox = new TextBox();
            lastVanillaDenLabel = new Label();
            currentDenTextBox = new TextBox();
            currentDenLabel = new Label();
            slugcatInfoKarmaGroupBox = new GroupBox();
            KarmaSelectorControl = new KarmaSelectorControl();
            worldInfoTabPage = new TabPage();
            worldInfoTabControl = new TabControl();
            commonWorldInfoTabPage = new TabPage();
            worldInfoOutskirtstabPage = new TabPage();
            tabPage1 = new TabPage();
            persistentInfoTabPage = new TabPage();
            markOfCommunicationCheckBox = new CheckBox();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            hunterPermaDeathCheckBox = new CheckBox();
            ascendedCheckBox = new CheckBox();
            label2 = new Label();
            ascendedFivePebblesCheckBox = new CheckBox();
            ascendedLooksToTheMoonCheckBox = new CheckBox();
            totalsGroupBox = new GroupBox();
            friendsSavedLabel = new Label();
            totalFriendsSavedNumericUpDown = new NumericUpDown();
            totalQuitsLabel = new Label();
            totalQuitsNumericUpDown = new NumericUpDown();
            totalSurvivesLabel = new Label();
            totalSurvivesNumericUpDown = new NumericUpDown();
            totalFoodNumericUpDown = new NumericUpDown();
            totalFoodLabel = new Label();
            totalDeathsNumericUpDown = new NumericUpDown();
            totalDeathsLabel = new Label();
            karmaFlowerGroupBox = new GroupBox();
            karmaFlowerWorldPositionEditControl = new WorldPositionEditControl();
            commonToolTip = new ToolTip(components);
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)cycleNumberNumericUpDown).BeginInit();
            tabControl1.SuspendLayout();
            slugcatInfoTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            denInfoGroupBox.SuspendLayout();
            slugcatInfoKarmaGroupBox.SuspendLayout();
            worldInfoTabPage.SuspendLayout();
            worldInfoTabControl.SuspendLayout();
            persistentInfoTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            totalsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)totalFriendsSavedNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)totalQuitsNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)totalSurvivesNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)totalFoodNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)totalDeathsNumericUpDown).BeginInit();
            karmaFlowerGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            SuspendLayout();
            // 
            // FoodPipControl
            // 
            FoodPipControl.Dock = DockStyle.Top;
            FoodPipControl.FilledPips = 0;
            FoodPipControl.Location = new Point(3, 3);
            FoodPipControl.Name = "FoodPipControl";
            FoodPipControl.PipBarIndex = 4;
            FoodPipControl.PipCount = 7;
            FoodPipControl.Size = new Size(535, 42);
            FoodPipControl.TabIndex = 0;
            // 
            // cycleNumberNumericUpDown
            // 
            cycleNumberNumericUpDown.Location = new Point(98, 51);
            cycleNumberNumericUpDown.Name = "cycleNumberNumericUpDown";
            cycleNumberNumericUpDown.Size = new Size(120, 23);
            cycleNumberNumericUpDown.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 53);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 2;
            label1.Text = "Cycle Number:";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(slugcatInfoTabPage);
            tabControl1.Controls.Add(worldInfoTabPage);
            tabControl1.Controls.Add(persistentInfoTabPage);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(549, 440);
            tabControl1.TabIndex = 3;
            // 
            // slugcatInfoTabPage
            // 
            slugcatInfoTabPage.BackColor = SystemColors.Control;
            slugcatInfoTabPage.Controls.Add(label3);
            slugcatInfoTabPage.Controls.Add(pictureBox6);
            slugcatInfoTabPage.Controls.Add(pictureBox5);
            slugcatInfoTabPage.Controls.Add(guideOverseerDeadCheckBox);
            slugcatInfoTabPage.Controls.Add(extraHunterCyclesCheckBox);
            slugcatInfoTabPage.Controls.Add(moonsCloakCheckBox);
            slugcatInfoTabPage.Controls.Add(justBeatGameCheckBox);
            slugcatInfoTabPage.Controls.Add(citizenIDDroneCheckBox);
            slugcatInfoTabPage.Controls.Add(pictureBox1);
            slugcatInfoTabPage.Controls.Add(neuronGlowCheckBox);
            slugcatInfoTabPage.Controls.Add(denInfoGroupBox);
            slugcatInfoTabPage.Controls.Add(slugcatInfoKarmaGroupBox);
            slugcatInfoTabPage.Controls.Add(FoodPipControl);
            slugcatInfoTabPage.Controls.Add(label1);
            slugcatInfoTabPage.Controls.Add(cycleNumberNumericUpDown);
            slugcatInfoTabPage.Location = new Point(4, 24);
            slugcatInfoTabPage.Name = "slugcatInfoTabPage";
            slugcatInfoTabPage.Padding = new Padding(3);
            slugcatInfoTabPage.Size = new Size(541, 412);
            slugcatInfoTabPage.TabIndex = 0;
            slugcatInfoTabPage.Text = "Slugcat Info";
            // 
            // guideOverseerDeadCheckBox
            // 
            guideOverseerDeadCheckBox.AutoSize = true;
            guideOverseerDeadCheckBox.Location = new Point(253, 102);
            guideOverseerDeadCheckBox.Name = "guideOverseerDeadCheckBox";
            guideOverseerDeadCheckBox.Size = new Size(136, 19);
            guideOverseerDeadCheckBox.TabIndex = 15;
            guideOverseerDeadCheckBox.Text = "Guide Overseer Dead";
            commonToolTip.SetToolTip(guideOverseerDeadCheckBox, "Did you kill the Guide Overseer?");
            guideOverseerDeadCheckBox.UseVisualStyleBackColor = true;
            // 
            // extraHunterCyclesCheckBox
            // 
            extraHunterCyclesCheckBox.AutoSize = true;
            extraHunterCyclesCheckBox.Location = new Point(253, 247);
            extraHunterCyclesCheckBox.Name = "extraHunterCyclesCheckBox";
            extraHunterCyclesCheckBox.Size = new Size(162, 19);
            extraHunterCyclesCheckBox.TabIndex = 14;
            extraHunterCyclesCheckBox.Text = "Extra Hunter Cycles Given";
            commonToolTip.SetToolTip(extraHunterCyclesCheckBox, "Did Five Pebbles give Hunter Extra Cycles?");
            extraHunterCyclesCheckBox.UseVisualStyleBackColor = true;
            // 
            // moonsCloakCheckBox
            // 
            moonsCloakCheckBox.AutoSize = true;
            moonsCloakCheckBox.Location = new Point(253, 274);
            moonsCloakCheckBox.Name = "moonsCloakCheckBox";
            moonsCloakCheckBox.Size = new Size(99, 19);
            moonsCloakCheckBox.TabIndex = 13;
            moonsCloakCheckBox.Text = "Moon's Cloak";
            commonToolTip.SetToolTip(moonsCloakCheckBox, "Are you wearing Moon's Cloak?");
            moonsCloakCheckBox.UseVisualStyleBackColor = true;
            // 
            // justBeatGameCheckBox
            // 
            justBeatGameCheckBox.AutoSize = true;
            justBeatGameCheckBox.Location = new Point(253, 77);
            justBeatGameCheckBox.Name = "justBeatGameCheckBox";
            justBeatGameCheckBox.Size = new Size(106, 19);
            justBeatGameCheckBox.TabIndex = 12;
            justBeatGameCheckBox.Text = "Just Beat Game";
            commonToolTip.SetToolTip(justBeatGameCheckBox, "Was the last exit the result of you beating the game?");
            justBeatGameCheckBox.UseVisualStyleBackColor = true;
            // 
            // citizenIDDroneCheckBox
            // 
            citizenIDDroneCheckBox.AutoSize = true;
            citizenIDDroneCheckBox.Location = new Point(253, 222);
            citizenIDDroneCheckBox.Name = "citizenIDDroneCheckBox";
            citizenIDDroneCheckBox.Size = new Size(111, 19);
            citizenIDDroneCheckBox.TabIndex = 11;
            citizenIDDroneCheckBox.Text = "Citizen ID Drone";
            commonToolTip.SetToolTip(citizenIDDroneCheckBox, "Do you have Artificer's citizen ID drone?");
            citizenIDDroneCheckBox.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Neuron_Fly;
            pictureBox1.Location = new Point(224, 51);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(11, 26);
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // neuronGlowCheckBox
            // 
            neuronGlowCheckBox.AutoSize = true;
            neuronGlowCheckBox.Location = new Point(253, 53);
            neuronGlowCheckBox.Name = "neuronGlowCheckBox";
            neuronGlowCheckBox.Size = new Size(96, 19);
            neuronGlowCheckBox.TabIndex = 9;
            neuronGlowCheckBox.Text = "Neuron Glow";
            commonToolTip.SetToolTip(neuronGlowCheckBox, "Have you eaten a Neuron Fly?");
            neuronGlowCheckBox.UseVisualStyleBackColor = true;
            // 
            // denInfoGroupBox
            // 
            denInfoGroupBox.Controls.Add(lastVanillaDenTextBox);
            denInfoGroupBox.Controls.Add(lastVanillaDenLabel);
            denInfoGroupBox.Controls.Add(currentDenTextBox);
            denInfoGroupBox.Controls.Add(currentDenLabel);
            denInfoGroupBox.Location = new Point(13, 227);
            denInfoGroupBox.Name = "denInfoGroupBox";
            denInfoGroupBox.Size = new Size(205, 139);
            denInfoGroupBox.TabIndex = 7;
            denInfoGroupBox.TabStop = false;
            denInfoGroupBox.Text = "Den Info";
            // 
            // lastVanillaDenTextBox
            // 
            lastVanillaDenTextBox.Location = new Point(104, 45);
            lastVanillaDenTextBox.Name = "lastVanillaDenTextBox";
            lastVanillaDenTextBox.Size = new Size(95, 23);
            lastVanillaDenTextBox.TabIndex = 8;
            // 
            // lastVanillaDenLabel
            // 
            lastVanillaDenLabel.AutoSize = true;
            lastVanillaDenLabel.Location = new Point(6, 48);
            lastVanillaDenLabel.Name = "lastVanillaDenLabel";
            lastVanillaDenLabel.Size = new Size(92, 15);
            lastVanillaDenLabel.TabIndex = 7;
            lastVanillaDenLabel.Text = "Last Vanilla Den:";
            // 
            // currentDenTextBox
            // 
            currentDenTextBox.Location = new Point(86, 16);
            currentDenTextBox.Name = "currentDenTextBox";
            currentDenTextBox.Size = new Size(113, 23);
            currentDenTextBox.TabIndex = 5;
            // 
            // currentDenLabel
            // 
            currentDenLabel.AutoSize = true;
            currentDenLabel.Location = new Point(6, 19);
            currentDenLabel.Name = "currentDenLabel";
            currentDenLabel.Size = new Size(74, 15);
            currentDenLabel.TabIndex = 6;
            currentDenLabel.Text = "Current Den:";
            // 
            // slugcatInfoKarmaGroupBox
            // 
            slugcatInfoKarmaGroupBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            slugcatInfoKarmaGroupBox.Controls.Add(KarmaSelectorControl);
            slugcatInfoKarmaGroupBox.Location = new Point(13, 80);
            slugcatInfoKarmaGroupBox.Name = "slugcatInfoKarmaGroupBox";
            slugcatInfoKarmaGroupBox.Size = new Size(205, 140);
            slugcatInfoKarmaGroupBox.TabIndex = 4;
            slugcatInfoKarmaGroupBox.TabStop = false;
            slugcatInfoKarmaGroupBox.Text = "Karma";
            // 
            // KarmaSelectorControl
            // 
            KarmaSelectorControl.KarmaLevel = 0;
            KarmaSelectorControl.KarmaMax = 9;
            KarmaSelectorControl.Location = new Point(6, 22);
            KarmaSelectorControl.Name = "KarmaSelectorControl";
            KarmaSelectorControl.Reinforced = false;
            KarmaSelectorControl.Size = new Size(193, 96);
            KarmaSelectorControl.TabIndex = 3;
            // 
            // worldInfoTabPage
            // 
            worldInfoTabPage.Controls.Add(worldInfoTabControl);
            worldInfoTabPage.Location = new Point(4, 24);
            worldInfoTabPage.Name = "worldInfoTabPage";
            worldInfoTabPage.Padding = new Padding(3);
            worldInfoTabPage.Size = new Size(541, 412);
            worldInfoTabPage.TabIndex = 1;
            worldInfoTabPage.Text = "World Info";
            worldInfoTabPage.UseVisualStyleBackColor = true;
            // 
            // worldInfoTabControl
            // 
            worldInfoTabControl.Controls.Add(commonWorldInfoTabPage);
            worldInfoTabControl.Controls.Add(worldInfoOutskirtstabPage);
            worldInfoTabControl.Controls.Add(tabPage1);
            worldInfoTabControl.Location = new Point(41, 51);
            worldInfoTabControl.Name = "worldInfoTabControl";
            worldInfoTabControl.SelectedIndex = 0;
            worldInfoTabControl.Size = new Size(382, 282);
            worldInfoTabControl.TabIndex = 0;
            // 
            // commonWorldInfoTabPage
            // 
            commonWorldInfoTabPage.Location = new Point(4, 24);
            commonWorldInfoTabPage.Name = "commonWorldInfoTabPage";
            commonWorldInfoTabPage.Padding = new Padding(3);
            commonWorldInfoTabPage.Size = new Size(374, 254);
            commonWorldInfoTabPage.TabIndex = 0;
            commonWorldInfoTabPage.Text = "Common";
            commonWorldInfoTabPage.ToolTipText = "Info for the world that doesn't fit any specific region";
            commonWorldInfoTabPage.UseVisualStyleBackColor = true;
            // 
            // worldInfoOutskirtstabPage
            // 
            worldInfoOutskirtstabPage.Location = new Point(4, 24);
            worldInfoOutskirtstabPage.Name = "worldInfoOutskirtstabPage";
            worldInfoOutskirtstabPage.Padding = new Padding(3);
            worldInfoOutskirtstabPage.Size = new Size(374, 254);
            worldInfoOutskirtstabPage.TabIndex = 1;
            worldInfoOutskirtstabPage.Text = "Outskirts";
            worldInfoOutskirtstabPage.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(374, 254);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // persistentInfoTabPage
            // 
            persistentInfoTabPage.BackColor = SystemColors.Control;
            persistentInfoTabPage.Controls.Add(markOfCommunicationCheckBox);
            persistentInfoTabPage.Controls.Add(pictureBox4);
            persistentInfoTabPage.Controls.Add(pictureBox3);
            persistentInfoTabPage.Controls.Add(pictureBox2);
            persistentInfoTabPage.Controls.Add(hunterPermaDeathCheckBox);
            persistentInfoTabPage.Controls.Add(ascendedCheckBox);
            persistentInfoTabPage.Controls.Add(label2);
            persistentInfoTabPage.Controls.Add(ascendedFivePebblesCheckBox);
            persistentInfoTabPage.Controls.Add(ascendedLooksToTheMoonCheckBox);
            persistentInfoTabPage.Controls.Add(totalsGroupBox);
            persistentInfoTabPage.Controls.Add(karmaFlowerGroupBox);
            persistentInfoTabPage.Location = new Point(4, 24);
            persistentInfoTabPage.Name = "persistentInfoTabPage";
            persistentInfoTabPage.Padding = new Padding(3);
            persistentInfoTabPage.Size = new Size(541, 412);
            persistentInfoTabPage.TabIndex = 2;
            persistentInfoTabPage.Text = "Persistent Info";
            persistentInfoTabPage.ToolTipText = "Data that persists across death";
            // 
            // markOfCommunicationCheckBox
            // 
            markOfCommunicationCheckBox.AutoSize = true;
            markOfCommunicationCheckBox.Location = new Point(349, 125);
            markOfCommunicationCheckBox.Name = "markOfCommunicationCheckBox";
            markOfCommunicationCheckBox.Size = new Size(180, 19);
            markOfCommunicationCheckBox.TabIndex = 10;
            markOfCommunicationCheckBox.Text = "Has Mark of Communication";
            markOfCommunicationCheckBox.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.slugcatdead;
            pictureBox4.Location = new Point(246, 76);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(23, 24);
            pictureBox4.TabIndex = 9;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.lookstothemoondeadicon;
            pictureBox3.Location = new Point(246, 16);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(23, 24);
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.fivepebblesdeadicon;
            pictureBox2.Location = new Point(246, 46);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(23, 24);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // hunterPermaDeathCheckBox
            // 
            hunterPermaDeathCheckBox.AutoSize = true;
            hunterPermaDeathCheckBox.Location = new Point(275, 76);
            hunterPermaDeathCheckBox.Name = "hunterPermaDeathCheckBox";
            hunterPermaDeathCheckBox.Size = new Size(105, 19);
            hunterPermaDeathCheckBox.TabIndex = 6;
            hunterPermaDeathCheckBox.Text = "Currently Dead";
            commonToolTip.SetToolTip(hunterPermaDeathCheckBox, "Did you quit on the death screen? (Controls Hunter Permadeath)");
            hunterPermaDeathCheckBox.UseVisualStyleBackColor = true;
            // 
            // ascendedCheckBox
            // 
            ascendedCheckBox.AutoSize = true;
            ascendedCheckBox.Location = new Point(275, 101);
            ascendedCheckBox.Name = "ascendedCheckBox";
            ascendedCheckBox.Size = new Size(78, 19);
            ascendedCheckBox.TabIndex = 5;
            ascendedCheckBox.Text = "Ascended";
            commonToolTip.SetToolTip(ascendedCheckBox, "Has this slugcat ascended?");
            ascendedCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(348, 178);
            label2.Name = "label2";
            label2.Size = new Size(181, 15);
            label2.TabIndex = 4;
            label2.Text = "idk how to sort these yet sorry lol";
            // 
            // ascendedFivePebblesCheckBox
            // 
            ascendedFivePebblesCheckBox.AutoSize = true;
            ascendedFivePebblesCheckBox.Location = new Point(275, 46);
            ascendedFivePebblesCheckBox.Name = "ascendedFivePebblesCheckBox";
            ascendedFivePebblesCheckBox.Size = new Size(146, 19);
            ascendedFivePebblesCheckBox.TabIndex = 3;
            ascendedFivePebblesCheckBox.Text = "Ascended Five Pebbles";
            commonToolTip.SetToolTip(ascendedFivePebblesCheckBox, "Was Five Pebbles ascended by Saint?");
            ascendedFivePebblesCheckBox.UseVisualStyleBackColor = true;
            // 
            // ascendedLooksToTheMoonCheckBox
            // 
            ascendedLooksToTheMoonCheckBox.AutoSize = true;
            ascendedLooksToTheMoonCheckBox.Location = new Point(275, 16);
            ascendedLooksToTheMoonCheckBox.Name = "ascendedLooksToTheMoonCheckBox";
            ascendedLooksToTheMoonCheckBox.Size = new Size(184, 19);
            ascendedLooksToTheMoonCheckBox.TabIndex = 2;
            ascendedLooksToTheMoonCheckBox.Text = "Ascended Looks To The Moon";
            commonToolTip.SetToolTip(ascendedLooksToTheMoonCheckBox, "Was Looks To The Moon ascended by Saint?");
            ascendedLooksToTheMoonCheckBox.UseVisualStyleBackColor = true;
            // 
            // totalsGroupBox
            // 
            totalsGroupBox.Controls.Add(friendsSavedLabel);
            totalsGroupBox.Controls.Add(totalFriendsSavedNumericUpDown);
            totalsGroupBox.Controls.Add(totalQuitsLabel);
            totalsGroupBox.Controls.Add(totalQuitsNumericUpDown);
            totalsGroupBox.Controls.Add(totalSurvivesLabel);
            totalsGroupBox.Controls.Add(totalSurvivesNumericUpDown);
            totalsGroupBox.Controls.Add(totalFoodNumericUpDown);
            totalsGroupBox.Controls.Add(totalFoodLabel);
            totalsGroupBox.Controls.Add(totalDeathsNumericUpDown);
            totalsGroupBox.Controls.Add(totalDeathsLabel);
            totalsGroupBox.Location = new Point(12, 125);
            totalsGroupBox.Name = "totalsGroupBox";
            totalsGroupBox.Size = new Size(330, 219);
            totalsGroupBox.TabIndex = 1;
            totalsGroupBox.TabStop = false;
            totalsGroupBox.Text = "Totals";
            // 
            // friendsSavedLabel
            // 
            friendsSavedLabel.AutoSize = true;
            friendsSavedLabel.Location = new Point(8, 140);
            friendsSavedLabel.Name = "friendsSavedLabel";
            friendsSavedLabel.Size = new Size(82, 15);
            friendsSavedLabel.TabIndex = 3;
            friendsSavedLabel.Text = "Friends Saved:";
            // 
            // totalFriendsSavedNumericUpDown
            // 
            totalFriendsSavedNumericUpDown.Location = new Point(96, 138);
            totalFriendsSavedNumericUpDown.Maximum = new decimal(new int[] { 7, 5, 0, 0 });
            totalFriendsSavedNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            totalFriendsSavedNumericUpDown.Name = "totalFriendsSavedNumericUpDown";
            totalFriendsSavedNumericUpDown.Size = new Size(228, 23);
            totalFriendsSavedNumericUpDown.TabIndex = 3;
            // 
            // totalQuitsLabel
            // 
            totalQuitsLabel.AutoSize = true;
            totalQuitsLabel.Location = new Point(7, 82);
            totalQuitsLabel.Name = "totalQuitsLabel";
            totalQuitsLabel.Size = new Size(38, 15);
            totalQuitsLabel.TabIndex = 3;
            totalQuitsLabel.Text = "Quits:";
            // 
            // totalQuitsNumericUpDown
            // 
            totalQuitsNumericUpDown.Location = new Point(96, 80);
            totalQuitsNumericUpDown.Maximum = new decimal(new int[] { 7, 5, 0, 0 });
            totalQuitsNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            totalQuitsNumericUpDown.Name = "totalQuitsNumericUpDown";
            totalQuitsNumericUpDown.Size = new Size(228, 23);
            totalQuitsNumericUpDown.TabIndex = 4;
            // 
            // totalSurvivesLabel
            // 
            totalSurvivesLabel.AutoSize = true;
            totalSurvivesLabel.Location = new Point(6, 53);
            totalSurvivesLabel.Name = "totalSurvivesLabel";
            totalSurvivesLabel.Size = new Size(53, 15);
            totalSurvivesLabel.TabIndex = 2;
            totalSurvivesLabel.Text = "Survives:";
            // 
            // totalSurvivesNumericUpDown
            // 
            totalSurvivesNumericUpDown.Location = new Point(96, 51);
            totalSurvivesNumericUpDown.Maximum = new decimal(new int[] { 7, 5, 0, 0 });
            totalSurvivesNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            totalSurvivesNumericUpDown.Name = "totalSurvivesNumericUpDown";
            totalSurvivesNumericUpDown.Size = new Size(228, 23);
            totalSurvivesNumericUpDown.TabIndex = 3;
            // 
            // totalFoodNumericUpDown
            // 
            totalFoodNumericUpDown.Location = new Point(96, 109);
            totalFoodNumericUpDown.Maximum = new decimal(new int[] { 7, 5, 0, 0 });
            totalFoodNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            totalFoodNumericUpDown.Name = "totalFoodNumericUpDown";
            totalFoodNumericUpDown.Size = new Size(228, 23);
            totalFoodNumericUpDown.TabIndex = 2;
            // 
            // totalFoodLabel
            // 
            totalFoodLabel.AutoSize = true;
            totalFoodLabel.Location = new Point(7, 111);
            totalFoodLabel.Name = "totalFoodLabel";
            totalFoodLabel.Size = new Size(37, 15);
            totalFoodLabel.TabIndex = 2;
            totalFoodLabel.Text = "Food:";
            // 
            // totalDeathsNumericUpDown
            // 
            totalDeathsNumericUpDown.Location = new Point(96, 22);
            totalDeathsNumericUpDown.Maximum = new decimal(new int[] { 7, 5, 0, 0 });
            totalDeathsNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            totalDeathsNumericUpDown.Name = "totalDeathsNumericUpDown";
            totalDeathsNumericUpDown.Size = new Size(228, 23);
            totalDeathsNumericUpDown.TabIndex = 1;
            // 
            // totalDeathsLabel
            // 
            totalDeathsLabel.AutoSize = true;
            totalDeathsLabel.Location = new Point(6, 24);
            totalDeathsLabel.Name = "totalDeathsLabel";
            totalDeathsLabel.Size = new Size(46, 15);
            totalDeathsLabel.TabIndex = 0;
            totalDeathsLabel.Text = "Deaths:";
            // 
            // karmaFlowerGroupBox
            // 
            karmaFlowerGroupBox.Controls.Add(karmaFlowerWorldPositionEditControl);
            karmaFlowerGroupBox.Location = new Point(6, 6);
            karmaFlowerGroupBox.Name = "karmaFlowerGroupBox";
            karmaFlowerGroupBox.Size = new Size(234, 113);
            karmaFlowerGroupBox.TabIndex = 0;
            karmaFlowerGroupBox.TabStop = false;
            karmaFlowerGroupBox.Text = "Karma Flower";
            // 
            // karmaFlowerWorldPositionEditControl
            // 
            karmaFlowerWorldPositionEditControl.AbstractNode = 0;
            karmaFlowerWorldPositionEditControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            karmaFlowerWorldPositionEditControl.Location = new Point(6, 22);
            karmaFlowerWorldPositionEditControl.Name = "karmaFlowerWorldPositionEditControl";
            karmaFlowerWorldPositionEditControl.RoomName = "";
            karmaFlowerWorldPositionEditControl.Size = new Size(222, 69);
            karmaFlowerWorldPositionEditControl.TabIndex = 1;
            karmaFlowerWorldPositionEditControl.X = 0;
            karmaFlowerWorldPositionEditControl.Y = 0;
            // 
            // pictureBox5
            // 
            pictureBox5.BackgroundImage = Properties.Resources.hunterhappy;
            pictureBox5.BackgroundImageLayout = ImageLayout.Center;
            pictureBox5.Location = new Point(224, 245);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(23, 23);
            pictureBox5.TabIndex = 16;
            pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.BackgroundImageLayout = ImageLayout.Center;
            pictureBox6.Image = Properties.Resources.Yellow_Overseer_icon;
            pictureBox6.Location = new Point(224, 102);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(23, 23);
            pictureBox6.TabIndex = 17;
            pictureBox6.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(234, 204);
            label3.Name = "label3";
            label3.Size = new Size(234, 15);
            label3.TabIndex = 18;
            label3.Text = "slug cat specific (make this groupbox alter)";
            // 
            // SlugConfigControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Name = "SlugConfigControl";
            Size = new Size(549, 440);
            Load += SlugConfigControl_Load;
            ((System.ComponentModel.ISupportInitialize)cycleNumberNumericUpDown).EndInit();
            tabControl1.ResumeLayout(false);
            slugcatInfoTabPage.ResumeLayout(false);
            slugcatInfoTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            denInfoGroupBox.ResumeLayout(false);
            denInfoGroupBox.PerformLayout();
            slugcatInfoKarmaGroupBox.ResumeLayout(false);
            worldInfoTabPage.ResumeLayout(false);
            worldInfoTabControl.ResumeLayout(false);
            persistentInfoTabPage.ResumeLayout(false);
            persistentInfoTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            totalsGroupBox.ResumeLayout(false);
            totalsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)totalFriendsSavedNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)totalQuitsNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)totalSurvivesNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)totalFoodNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)totalDeathsNumericUpDown).EndInit();
            karmaFlowerGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private NumericUpDown cycleNumberNumericUpDown;
        private Label label1;
        public FoodPipControl FoodPipControl;
        private TabControl tabControl1;
        private TabPage slugcatInfoTabPage;
        private TabPage worldInfoTabPage;
        private TabPage persistentInfoTabPage;
        private TabControl worldInfoTabControl;
        private TabPage commonWorldInfoTabPage;
        private TabPage worldInfoOutskirtstabPage;
        private GroupBox slugcatInfoKarmaGroupBox;
        private TabPage tabPage1;
        private GroupBox denInfoGroupBox;
        private TextBox currentDenTextBox;
        private Label currentDenLabel;
        public KarmaSelectorControl KarmaSelectorControl;
        private WorldPositionEditControl karmaFlowerWorldPositionEditControl;
        private GroupBox karmaFlowerGroupBox;
        private TextBox lastVanillaDenTextBox;
        private Label lastVanillaDenLabel;
        private GroupBox totalsGroupBox;
        private NumericUpDown totalDeathsNumericUpDown;
        private Label totalDeathsLabel;
        private Label totalSurvivesLabel;
        private NumericUpDown totalSurvivesNumericUpDown;
        private NumericUpDown totalFoodNumericUpDown;
        private Label totalFoodLabel;
        private Label totalQuitsLabel;
        private NumericUpDown totalQuitsNumericUpDown;
        private Label friendsSavedLabel;
        private NumericUpDown totalFriendsSavedNumericUpDown;
        private CheckBox ascendedLooksToTheMoonCheckBox;
        private CheckBox hunterPermaDeathCheckBox;
        private CheckBox ascendedCheckBox;
        private Label label2;
        private CheckBox ascendedFivePebblesCheckBox;
        private CheckBox neuronGlowCheckBox;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private CheckBox markOfCommunicationCheckBox;
        private ToolTip commonToolTip;
        private CheckBox moonsCloakCheckBox;
        private CheckBox justBeatGameCheckBox;
        private CheckBox citizenIDDroneCheckBox;
        private CheckBox extraHunterCyclesCheckBox;
        private CheckBox guideOverseerDeadCheckBox;
        private PictureBox pictureBox5;
        private Label label3;
        private PictureBox pictureBox6;
    }
}
