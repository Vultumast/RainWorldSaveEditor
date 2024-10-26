namespace RainWorldSaveEditor.Forms;

partial class ExpeditionCoreSaveForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        openToolStripMenuItem = new ToolStripMenuItem();
        userProfileToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        closeToolStripMenuItem = new ToolStripMenuItem();
        saveSlotToolStripMenuItem = new ToolStripMenuItem();
        openToolStripMenuItem1 = new ToolStripMenuItem();
        file1ToolStripMenuItem = new ToolStripMenuItem();
        file2ToolStripMenuItem = new ToolStripMenuItem();
        file3ToolStripMenuItem = new ToolStripMenuItem();
        openFileToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator2 = new ToolStripSeparator();
        saveToolStripMenuItem = new ToolStripMenuItem();
        saveAsToolStripMenuItem = new ToolStripMenuItem();
        aboutToolStripMenuItem = new ToolStripMenuItem();
        expeditionSaveTabControl = new TabControl();
        tabPage1 = new TabPage();
        viewedManualCheckBox = new CheckBox();
        viewedManualLabel = new Label();
        selectedMenuSongTextBox = new TextBox();
        selectedMenuSongLabel = new Label();
        selectedSlugcatTextBox = new TextBox();
        selectedSlugcatLabel = new Label();
        winsLabel = new Label();
        winsNumericUpDown = new NumericUpDown();
        totalHiddenChallengesLabel = new Label();
        totalHiddenChallengesNumericUpDown = new NumericUpDown();
        totalChallengesLabel = new Label();
        totalChallengesNumericUpDown = new NumericUpDown();
        totalPointsLabel = new Label();
        totalPointsNumericUpDown = new NumericUpDown();
        pointsNumericUpDown = new NumericUpDown();
        pointsLabel = new Label();
        perkLimitNumericUpDown = new NumericUpDown();
        levelNumericUpDown = new NumericUpDown();
        saveSlotNumericUpDown = new NumericUpDown();
        perkLimitLabel = new Label();
        levelLabel = new Label();
        saveSlotLabel = new Label();
        menuStrip1.SuspendLayout();
        expeditionSaveTabControl.SuspendLayout();
        tabPage1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)winsNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)totalHiddenChallengesNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)totalChallengesNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)totalPointsNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pointsNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)perkLimitNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)levelNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)saveSlotNumericUpDown).BeginInit();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, saveSlotToolStripMenuItem, aboutToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(800, 24);
        menuStrip1.TabIndex = 0;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, userProfileToolStripMenuItem, toolStripSeparator1, closeToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "File";
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.Enabled = false;
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        openToolStripMenuItem.Size = new Size(145, 22);
        openToolStripMenuItem.Text = "Open";
        // 
        // userProfileToolStripMenuItem
        // 
        userProfileToolStripMenuItem.Name = "userProfileToolStripMenuItem";
        userProfileToolStripMenuItem.Size = new Size(145, 22);
        userProfileToolStripMenuItem.Text = "User Profile";
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(142, 6);
        // 
        // closeToolStripMenuItem
        // 
        closeToolStripMenuItem.Name = "closeToolStripMenuItem";
        closeToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
        closeToolStripMenuItem.Size = new Size(145, 22);
        closeToolStripMenuItem.Text = "Close";
        closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
        // 
        // saveSlotToolStripMenuItem
        // 
        saveSlotToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem1, openFileToolStripMenuItem, toolStripSeparator2, saveToolStripMenuItem, saveAsToolStripMenuItem });
        saveSlotToolStripMenuItem.Name = "saveSlotToolStripMenuItem";
        saveSlotToolStripMenuItem.Size = new Size(66, 20);
        saveSlotToolStripMenuItem.Text = "Save Slot";
        // 
        // openToolStripMenuItem1
        // 
        openToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { file1ToolStripMenuItem, file2ToolStripMenuItem, file3ToolStripMenuItem });
        openToolStripMenuItem1.Name = "openToolStripMenuItem1";
        openToolStripMenuItem1.Size = new Size(133, 22);
        openToolStripMenuItem1.Text = "Open";
        // 
        // file1ToolStripMenuItem
        // 
        file1ToolStripMenuItem.Name = "file1ToolStripMenuItem";
        file1ToolStripMenuItem.Size = new Size(101, 22);
        file1ToolStripMenuItem.Text = "File 1";
        file1ToolStripMenuItem.Click += file1ToolStripMenuItem_Click;
        // 
        // file2ToolStripMenuItem
        // 
        file2ToolStripMenuItem.Name = "file2ToolStripMenuItem";
        file2ToolStripMenuItem.Size = new Size(101, 22);
        file2ToolStripMenuItem.Text = "File 2";
        file2ToolStripMenuItem.Click += file2ToolStripMenuItem_Click;
        // 
        // file3ToolStripMenuItem
        // 
        file3ToolStripMenuItem.Name = "file3ToolStripMenuItem";
        file3ToolStripMenuItem.Size = new Size(101, 22);
        file3ToolStripMenuItem.Text = "File 3";
        file3ToolStripMenuItem.Click += file3ToolStripMenuItem_Click;
        // 
        // openFileToolStripMenuItem
        // 
        openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
        openFileToolStripMenuItem.Size = new Size(133, 22);
        openFileToolStripMenuItem.Text = "Open File...";
        openFileToolStripMenuItem.Click += openFileToolStripMenuItem_Click;
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new Size(130, 6);
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Enabled = false;
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.Size = new Size(133, 22);
        saveToolStripMenuItem.Text = "Save";
        saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
        // 
        // saveAsToolStripMenuItem
        // 
        saveAsToolStripMenuItem.Enabled = false;
        saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        saveAsToolStripMenuItem.Size = new Size(133, 22);
        saveAsToolStripMenuItem.Text = "Save As...";
        saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
        // 
        // aboutToolStripMenuItem
        // 
        aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        aboutToolStripMenuItem.Size = new Size(52, 20);
        aboutToolStripMenuItem.Text = "About";
        aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
        // 
        // expeditionSaveTabControl
        // 
        expeditionSaveTabControl.Controls.Add(tabPage1);
        expeditionSaveTabControl.Dock = DockStyle.Fill;
        expeditionSaveTabControl.Location = new Point(0, 24);
        expeditionSaveTabControl.Name = "expeditionSaveTabControl";
        expeditionSaveTabControl.SelectedIndex = 0;
        expeditionSaveTabControl.Size = new Size(800, 426);
        expeditionSaveTabControl.TabIndex = 1;
        // 
        // tabPage1
        // 
        tabPage1.Controls.Add(viewedManualCheckBox);
        tabPage1.Controls.Add(viewedManualLabel);
        tabPage1.Controls.Add(selectedMenuSongTextBox);
        tabPage1.Controls.Add(selectedMenuSongLabel);
        tabPage1.Controls.Add(selectedSlugcatTextBox);
        tabPage1.Controls.Add(selectedSlugcatLabel);
        tabPage1.Controls.Add(winsLabel);
        tabPage1.Controls.Add(winsNumericUpDown);
        tabPage1.Controls.Add(totalHiddenChallengesLabel);
        tabPage1.Controls.Add(totalHiddenChallengesNumericUpDown);
        tabPage1.Controls.Add(totalChallengesLabel);
        tabPage1.Controls.Add(totalChallengesNumericUpDown);
        tabPage1.Controls.Add(totalPointsLabel);
        tabPage1.Controls.Add(totalPointsNumericUpDown);
        tabPage1.Controls.Add(pointsNumericUpDown);
        tabPage1.Controls.Add(pointsLabel);
        tabPage1.Controls.Add(perkLimitNumericUpDown);
        tabPage1.Controls.Add(levelNumericUpDown);
        tabPage1.Controls.Add(saveSlotNumericUpDown);
        tabPage1.Controls.Add(perkLimitLabel);
        tabPage1.Controls.Add(levelLabel);
        tabPage1.Controls.Add(saveSlotLabel);
        tabPage1.Location = new Point(4, 24);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new Padding(3);
        tabPage1.Size = new Size(792, 398);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "General Fields";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // viewedManualCheckBox
        // 
        viewedManualCheckBox.AutoSize = true;
        viewedManualCheckBox.Location = new Point(151, 342);
        viewedManualCheckBox.Name = "viewedManualCheckBox";
        viewedManualCheckBox.Size = new Size(15, 14);
        viewedManualCheckBox.TabIndex = 24;
        viewedManualCheckBox.UseVisualStyleBackColor = true;
        viewedManualCheckBox.CheckedChanged += viewedManualCheckBox_CheckedChanged;
        // 
        // viewedManualLabel
        // 
        viewedManualLabel.AutoSize = true;
        viewedManualLabel.Location = new Point(16, 341);
        viewedManualLabel.Name = "viewedManualLabel";
        viewedManualLabel.Size = new Size(93, 15);
        viewedManualLabel.TabIndex = 23;
        viewedManualLabel.Text = "Viewed Manual?";
        // 
        // selectedMenuSongTextBox
        // 
        selectedMenuSongTextBox.Location = new Point(151, 294);
        selectedMenuSongTextBox.Name = "selectedMenuSongTextBox";
        selectedMenuSongTextBox.Size = new Size(100, 23);
        selectedMenuSongTextBox.TabIndex = 22;
        selectedMenuSongTextBox.TextChanged += selectedMenuSongTextBox_TextChanged;
        // 
        // selectedMenuSongLabel
        // 
        selectedMenuSongLabel.AutoSize = true;
        selectedMenuSongLabel.Location = new Point(16, 297);
        selectedMenuSongLabel.Name = "selectedMenuSongLabel";
        selectedMenuSongLabel.Size = new Size(115, 15);
        selectedMenuSongLabel.TabIndex = 21;
        selectedMenuSongLabel.Text = "Selected Menu Song";
        // 
        // selectedSlugcatTextBox
        // 
        selectedSlugcatTextBox.Location = new Point(151, 265);
        selectedSlugcatTextBox.Name = "selectedSlugcatTextBox";
        selectedSlugcatTextBox.Size = new Size(100, 23);
        selectedSlugcatTextBox.TabIndex = 20;
        selectedSlugcatTextBox.TextChanged += selectedSlugcatTextBox_TextChanged;
        // 
        // selectedSlugcatLabel
        // 
        selectedSlugcatLabel.AutoSize = true;
        selectedSlugcatLabel.Location = new Point(16, 268);
        selectedSlugcatLabel.Name = "selectedSlugcatLabel";
        selectedSlugcatLabel.Size = new Size(93, 15);
        selectedSlugcatLabel.TabIndex = 19;
        selectedSlugcatLabel.Text = "Selected Slugcat";
        // 
        // winsLabel
        // 
        winsLabel.AutoSize = true;
        winsLabel.Location = new Point(15, 217);
        winsLabel.Name = "winsLabel";
        winsLabel.Size = new Size(33, 15);
        winsLabel.TabIndex = 18;
        winsLabel.Text = "Wins";
        // 
        // winsNumericUpDown
        // 
        winsNumericUpDown.Location = new Point(174, 215);
        winsNumericUpDown.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
        winsNumericUpDown.Name = "winsNumericUpDown";
        winsNumericUpDown.Size = new Size(77, 23);
        winsNumericUpDown.TabIndex = 17;
        winsNumericUpDown.ValueChanged += winsNumericUpDown_ValueChanged;
        // 
        // totalHiddenChallengesLabel
        // 
        totalHiddenChallengesLabel.AutoSize = true;
        totalHiddenChallengesLabel.Location = new Point(15, 188);
        totalHiddenChallengesLabel.Name = "totalHiddenChallengesLabel";
        totalHiddenChallengesLabel.Size = new Size(135, 15);
        totalHiddenChallengesLabel.TabIndex = 16;
        totalHiddenChallengesLabel.Text = "Total Hidden Challenges";
        // 
        // totalHiddenChallengesNumericUpDown
        // 
        totalHiddenChallengesNumericUpDown.Location = new Point(174, 186);
        totalHiddenChallengesNumericUpDown.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
        totalHiddenChallengesNumericUpDown.Name = "totalHiddenChallengesNumericUpDown";
        totalHiddenChallengesNumericUpDown.Size = new Size(77, 23);
        totalHiddenChallengesNumericUpDown.TabIndex = 15;
        totalHiddenChallengesNumericUpDown.ValueChanged += totalHiddenChallengesNumericUpDown_ValueChanged;
        // 
        // totalChallengesLabel
        // 
        totalChallengesLabel.AutoSize = true;
        totalChallengesLabel.Location = new Point(15, 159);
        totalChallengesLabel.Name = "totalChallengesLabel";
        totalChallengesLabel.Size = new Size(93, 15);
        totalChallengesLabel.TabIndex = 14;
        totalChallengesLabel.Text = "Total Challenges";
        // 
        // totalChallengesNumericUpDown
        // 
        totalChallengesNumericUpDown.Location = new Point(174, 157);
        totalChallengesNumericUpDown.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
        totalChallengesNumericUpDown.Name = "totalChallengesNumericUpDown";
        totalChallengesNumericUpDown.Size = new Size(77, 23);
        totalChallengesNumericUpDown.TabIndex = 13;
        totalChallengesNumericUpDown.ValueChanged += totalChallengesNumericUpDown_ValueChanged;
        // 
        // totalPointsLabel
        // 
        totalPointsLabel.AutoSize = true;
        totalPointsLabel.Location = new Point(15, 130);
        totalPointsLabel.Name = "totalPointsLabel";
        totalPointsLabel.Size = new Size(68, 15);
        totalPointsLabel.TabIndex = 12;
        totalPointsLabel.Text = "Total Points";
        // 
        // totalPointsNumericUpDown
        // 
        totalPointsNumericUpDown.Location = new Point(174, 128);
        totalPointsNumericUpDown.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
        totalPointsNumericUpDown.Name = "totalPointsNumericUpDown";
        totalPointsNumericUpDown.Size = new Size(77, 23);
        totalPointsNumericUpDown.TabIndex = 11;
        totalPointsNumericUpDown.ValueChanged += totalPointsNumericUpDown_ValueChanged;
        // 
        // pointsNumericUpDown
        // 
        pointsNumericUpDown.Location = new Point(174, 99);
        pointsNumericUpDown.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
        pointsNumericUpDown.Name = "pointsNumericUpDown";
        pointsNumericUpDown.Size = new Size(77, 23);
        pointsNumericUpDown.TabIndex = 10;
        pointsNumericUpDown.ValueChanged += pointsNumericUpDown_ValueChanged;
        // 
        // pointsLabel
        // 
        pointsLabel.AutoSize = true;
        pointsLabel.Location = new Point(15, 101);
        pointsLabel.Name = "pointsLabel";
        pointsLabel.Size = new Size(40, 15);
        pointsLabel.TabIndex = 9;
        pointsLabel.Text = "Points";
        // 
        // perkLimitNumericUpDown
        // 
        perkLimitNumericUpDown.Location = new Point(174, 70);
        perkLimitNumericUpDown.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
        perkLimitNumericUpDown.Name = "perkLimitNumericUpDown";
        perkLimitNumericUpDown.Size = new Size(77, 23);
        perkLimitNumericUpDown.TabIndex = 8;
        perkLimitNumericUpDown.ValueChanged += perkLimitNumericUpDown_ValueChanged;
        // 
        // levelNumericUpDown
        // 
        levelNumericUpDown.Location = new Point(174, 41);
        levelNumericUpDown.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
        levelNumericUpDown.Name = "levelNumericUpDown";
        levelNumericUpDown.Size = new Size(77, 23);
        levelNumericUpDown.TabIndex = 7;
        levelNumericUpDown.ValueChanged += levelNumericUpDown_ValueChanged;
        // 
        // saveSlotNumericUpDown
        // 
        saveSlotNumericUpDown.Enabled = false;
        saveSlotNumericUpDown.Location = new Point(174, 12);
        saveSlotNumericUpDown.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
        saveSlotNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
        saveSlotNumericUpDown.Name = "saveSlotNumericUpDown";
        saveSlotNumericUpDown.Size = new Size(77, 23);
        saveSlotNumericUpDown.TabIndex = 6;
        saveSlotNumericUpDown.ValueChanged += saveSlotNumericUpDown_ValueChanged;
        // 
        // perkLimitLabel
        // 
        perkLimitLabel.AutoSize = true;
        perkLimitLabel.Location = new Point(15, 72);
        perkLimitLabel.Name = "perkLimitLabel";
        perkLimitLabel.Size = new Size(60, 15);
        perkLimitLabel.TabIndex = 5;
        perkLimitLabel.Text = "Perk Limit";
        // 
        // levelLabel
        // 
        levelLabel.AutoSize = true;
        levelLabel.Location = new Point(15, 43);
        levelLabel.Name = "levelLabel";
        levelLabel.Size = new Size(34, 15);
        levelLabel.TabIndex = 2;
        levelLabel.Text = "Level";
        // 
        // saveSlotLabel
        // 
        saveSlotLabel.AutoSize = true;
        saveSlotLabel.Location = new Point(15, 14);
        saveSlotLabel.Name = "saveSlotLabel";
        saveSlotLabel.Size = new Size(54, 15);
        saveSlotLabel.TabIndex = 0;
        saveSlotLabel.Text = "Save Slot";
        // 
        // ExpeditionCoreSaveForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(expeditionSaveTabControl);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Name = "ExpeditionCoreSaveForm";
        Text = "ExpeditionCoreSaveForm";
        Load += ExpeditionCoreSaveForm_Load;
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        expeditionSaveTabControl.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        tabPage1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)winsNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)totalHiddenChallengesNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)totalChallengesNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)totalPointsNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)pointsNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)perkLimitNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)levelNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)saveSlotNumericUpDown).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem saveSlotToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripMenuItem userProfileToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem closeToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem1;
    private ToolStripMenuItem file1ToolStripMenuItem;
    private ToolStripMenuItem file2ToolStripMenuItem;
    private ToolStripMenuItem file3ToolStripMenuItem;
    private ToolStripMenuItem openFileToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ToolStripMenuItem saveAsToolStripMenuItem;
    private TabControl expeditionSaveTabControl;
    private TabPage tabPage1;
    private Label saveSlotLabel;
    private Label levelLabel;
    private Label perkLimitLabel;
    private TextBox textBox3;
    private TextBox textBox2;
    private NumericUpDown saveSlotNumericUpDown;
    private NumericUpDown pointsNumericUpDown;
    private Label pointsLabel;
    private NumericUpDown perkLimitNumericUpDown;
    private NumericUpDown levelNumericUpDown;
    private Label totalPointsLabel;
    private NumericUpDown totalPointsNumericUpDown;
    private Label winsLabel;
    private NumericUpDown winsNumericUpDown;
    private Label totalHiddenChallengesLabel;
    private NumericUpDown totalHiddenChallengesNumericUpDown;
    private Label totalChallengesLabel;
    private NumericUpDown totalChallengesNumericUpDown;
    private TextBox selectedSlugcatTextBox;
    private Label selectedSlugcatLabel;
    private TextBox selectedMenuSongTextBox;
    private Label selectedMenuSongLabel;
    private CheckBox viewedManualCheckBox;
    private Label viewedManualLabel;
}