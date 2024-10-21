namespace RainWorldSaveEditor;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        openToolStripMenuItem = new ToolStripMenuItem();
        openRainWorldSaveDirectoryToolStripMenuItem = new ToolStripMenuItem();
        rainworldExecutableDirectoryToolStripMenuItem = new ToolStripMenuItem();
        userProfileToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        closeAndLaunchGameToolStripMenuItem = new ToolStripMenuItem();
        closeToolStripMenuItem = new ToolStripMenuItem();
        saveSlotToolStripMenuItem = new ToolStripMenuItem();
        openToolStripMenuItem1 = new ToolStripMenuItem();
        openFile1ToolStripMenuItem = new ToolStripMenuItem();
        openFile2ToolStripMenuItem = new ToolStripMenuItem();
        openFile3ToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator3 = new ToolStripSeparator();
        openFileToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator2 = new ToolStripSeparator();
        saveToolStripMenuItem = new ToolStripMenuItem();
        saveAsToolStripMenuItem = new ToolStripMenuItem();
        slugcatsToolStripMenuItem = new ToolStripMenuItem();
        vanillaSlugcatsToolStripMenuItem = new ToolStripMenuItem();
        dlcSlugcatsToolStripMenuItem = new ToolStripMenuItem();
        moddedSlugcatsToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator4 = new ToolStripSeparator();
        exportToolStripMenuItem = new ToolStripMenuItem();
        importToolStripMenuItem = new ToolStripMenuItem();
        aboutToolStripMenuItem = new ToolStripMenuItem();
        toggleConsoleToolStripMenuItem = new ToolStripMenuItem();
        explorerToolStripMenuItem = new ToolStripMenuItem();
        slugcatIconImageList = new ImageList(components);
        slugConfigControl = new Controls.SlugConfigControl();
        tabControl1 = new TabControl();
        tabPage1 = new TabPage();
        tabPage2 = new TabPage();
        tabPage3 = new TabPage();
        tabPage4 = new TabPage();
        tabPage5 = new TabPage();
        tabPage6 = new TabPage();
        menuStrip1.SuspendLayout();
        tabControl1.SuspendLayout();
        tabPage1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, saveSlotToolStripMenuItem, slugcatsToolStripMenuItem, aboutToolStripMenuItem, toggleConsoleToolStripMenuItem, explorerToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(800, 24);
        menuStrip1.TabIndex = 0;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, userProfileToolStripMenuItem, toolStripSeparator1, closeAndLaunchGameToolStripMenuItem, closeToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "File";
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openRainWorldSaveDirectoryToolStripMenuItem, rainworldExecutableDirectoryToolStripMenuItem });
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        openToolStripMenuItem.Size = new Size(202, 22);
        openToolStripMenuItem.Text = "Open";
        // 
        // openRainWorldSaveDirectoryToolStripMenuItem
        // 
        openRainWorldSaveDirectoryToolStripMenuItem.Name = "openRainWorldSaveDirectoryToolStripMenuItem";
        openRainWorldSaveDirectoryToolStripMenuItem.Size = new Size(260, 22);
        openRainWorldSaveDirectoryToolStripMenuItem.Text = "Save Directory in file explorer";
        openRainWorldSaveDirectoryToolStripMenuItem.ToolTipText = "Open the Rain World Save directory in file explorer";
        openRainWorldSaveDirectoryToolStripMenuItem.Click += openRainWorldSaveDirectoryToolStripMenuItem_Click;
        // 
        // rainworldExecutableDirectoryToolStripMenuItem
        // 
        rainworldExecutableDirectoryToolStripMenuItem.Name = "rainworldExecutableDirectoryToolStripMenuItem";
        rainworldExecutableDirectoryToolStripMenuItem.Size = new Size(260, 22);
        rainworldExecutableDirectoryToolStripMenuItem.Text = "Executable Directory in file explorer";
        rainworldExecutableDirectoryToolStripMenuItem.ToolTipText = "Open the Rain World Executable directory";
        rainworldExecutableDirectoryToolStripMenuItem.Click += rainworldExecutableDirectoryToolStripMenuItem_Click;
        // 
        // userProfileToolStripMenuItem
        // 
        userProfileToolStripMenuItem.Name = "userProfileToolStripMenuItem";
        userProfileToolStripMenuItem.Size = new Size(202, 22);
        userProfileToolStripMenuItem.Text = "User Profile";
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(199, 6);
        // 
        // closeAndLaunchGameToolStripMenuItem
        // 
        closeAndLaunchGameToolStripMenuItem.Name = "closeAndLaunchGameToolStripMenuItem";
        closeAndLaunchGameToolStripMenuItem.Size = new Size(202, 22);
        closeAndLaunchGameToolStripMenuItem.Text = "Close and Launch Game";
        closeAndLaunchGameToolStripMenuItem.ToolTipText = "Close the editor and launch Rain World";
        // 
        // closeToolStripMenuItem
        // 
        closeToolStripMenuItem.Name = "closeToolStripMenuItem";
        closeToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
        closeToolStripMenuItem.Size = new Size(202, 22);
        closeToolStripMenuItem.Text = "Close";
        closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
        // 
        // saveSlotToolStripMenuItem
        // 
        saveSlotToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem1, toolStripSeparator2, saveToolStripMenuItem, saveAsToolStripMenuItem });
        saveSlotToolStripMenuItem.Name = "saveSlotToolStripMenuItem";
        saveSlotToolStripMenuItem.Size = new Size(66, 20);
        saveSlotToolStripMenuItem.Text = "Save Slot";
        // 
        // openToolStripMenuItem1
        // 
        openToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { openFile1ToolStripMenuItem, openFile2ToolStripMenuItem, openFile3ToolStripMenuItem, toolStripSeparator3, openFileToolStripMenuItem });
        openToolStripMenuItem1.Name = "openToolStripMenuItem1";
        openToolStripMenuItem1.Size = new Size(180, 22);
        openToolStripMenuItem1.Text = "Open";
        // 
        // openFile1ToolStripMenuItem
        // 
        openFile1ToolStripMenuItem.Name = "openFile1ToolStripMenuItem";
        openFile1ToolStripMenuItem.Size = new Size(133, 22);
        openFile1ToolStripMenuItem.Text = "File 1";
        openFile1ToolStripMenuItem.ToolTipText = "Switch to Save File 1";
        openFile1ToolStripMenuItem.Click += openFile1ToolStripMenuItem_Click;
        // 
        // openFile2ToolStripMenuItem
        // 
        openFile2ToolStripMenuItem.Name = "openFile2ToolStripMenuItem";
        openFile2ToolStripMenuItem.Size = new Size(133, 22);
        openFile2ToolStripMenuItem.Text = "File 2";
        openFile2ToolStripMenuItem.ToolTipText = "Switch to Save File 2";
        openFile2ToolStripMenuItem.Click += openFile2ToolStripMenuItem_Click;
        // 
        // openFile3ToolStripMenuItem
        // 
        openFile3ToolStripMenuItem.Name = "openFile3ToolStripMenuItem";
        openFile3ToolStripMenuItem.Size = new Size(133, 22);
        openFile3ToolStripMenuItem.Text = "File 3";
        openFile3ToolStripMenuItem.ToolTipText = "Switch to Save File 3";
        openFile3ToolStripMenuItem.Click += openFile3ToolStripMenuItem_Click;
        // 
        // toolStripSeparator3
        // 
        toolStripSeparator3.Name = "toolStripSeparator3";
        toolStripSeparator3.Size = new Size(130, 6);
        // 
        // openFileToolStripMenuItem
        // 
        openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
        openFileToolStripMenuItem.Size = new Size(133, 22);
        openFileToolStripMenuItem.Text = "Open File...";
        openFileToolStripMenuItem.ToolTipText = "Open an external Save File";
        openFileToolStripMenuItem.Click += openFileToolStripMenuItem_Click;
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new Size(177, 6);
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Enabled = false;
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.Size = new Size(180, 22);
        saveToolStripMenuItem.Text = "Save";
        saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
        // 
        // saveAsToolStripMenuItem
        // 
        saveAsToolStripMenuItem.Enabled = false;
        saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        saveAsToolStripMenuItem.Size = new Size(180, 22);
        saveAsToolStripMenuItem.Text = "Save As...";
        saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
        // 
        // slugcatsToolStripMenuItem
        // 
        slugcatsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { vanillaSlugcatsToolStripMenuItem, dlcSlugcatsToolStripMenuItem, moddedSlugcatsToolStripMenuItem, toolStripSeparator4, exportToolStripMenuItem, importToolStripMenuItem });
        slugcatsToolStripMenuItem.Enabled = false;
        slugcatsToolStripMenuItem.Name = "slugcatsToolStripMenuItem";
        slugcatsToolStripMenuItem.Size = new Size(63, 20);
        slugcatsToolStripMenuItem.Text = "Slugcats";
        // 
        // vanillaSlugcatsToolStripMenuItem
        // 
        vanillaSlugcatsToolStripMenuItem.Name = "vanillaSlugcatsToolStripMenuItem";
        vanillaSlugcatsToolStripMenuItem.Size = new Size(180, 22);
        vanillaSlugcatsToolStripMenuItem.Text = "Vanilla";
        // 
        // dlcSlugcatsToolStripMenuItem
        // 
        dlcSlugcatsToolStripMenuItem.Name = "dlcSlugcatsToolStripMenuItem";
        dlcSlugcatsToolStripMenuItem.Size = new Size(180, 22);
        dlcSlugcatsToolStripMenuItem.Text = "DLC";
        // 
        // moddedSlugcatsToolStripMenuItem
        // 
        moddedSlugcatsToolStripMenuItem.Name = "moddedSlugcatsToolStripMenuItem";
        moddedSlugcatsToolStripMenuItem.Size = new Size(180, 22);
        moddedSlugcatsToolStripMenuItem.Text = "Modded";
        // 
        // toolStripSeparator4
        // 
        toolStripSeparator4.Name = "toolStripSeparator4";
        toolStripSeparator4.Size = new Size(177, 6);
        // 
        // exportToolStripMenuItem
        // 
        exportToolStripMenuItem.Enabled = false;
        exportToolStripMenuItem.Name = "exportToolStripMenuItem";
        exportToolStripMenuItem.Size = new Size(180, 22);
        exportToolStripMenuItem.Text = "Export...";
        exportToolStripMenuItem.ToolTipText = "Export current slugcat slot";
        exportToolStripMenuItem.Click += exportToolStripMenuItem_Click;
        // 
        // importToolStripMenuItem
        // 
        importToolStripMenuItem.Enabled = false;
        importToolStripMenuItem.Name = "importToolStripMenuItem";
        importToolStripMenuItem.Size = new Size(180, 22);
        importToolStripMenuItem.Text = "Import...";
        importToolStripMenuItem.ToolTipText = "Import a file to overwrite a slugcat slot";
        importToolStripMenuItem.Click += importToolStripMenuItem_Click;
        // 
        // aboutToolStripMenuItem
        // 
        aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        aboutToolStripMenuItem.Size = new Size(52, 20);
        aboutToolStripMenuItem.Text = "About";
        aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
        // 
        // toggleConsoleToolStripMenuItem
        // 
        toggleConsoleToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
        toggleConsoleToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toggleConsoleToolStripMenuItem.Image = Properties.Resources.console;
        toggleConsoleToolStripMenuItem.Name = "toggleConsoleToolStripMenuItem";
        toggleConsoleToolStripMenuItem.Size = new Size(28, 20);
        toggleConsoleToolStripMenuItem.Text = "Toggle Console";
        toggleConsoleToolStripMenuItem.Click += toggleConsoleToolStripMenuItem_Click;
        // 
        // explorerToolStripMenuItem
        // 
        explorerToolStripMenuItem.Name = "explorerToolStripMenuItem";
        explorerToolStripMenuItem.Size = new Size(62, 20);
        explorerToolStripMenuItem.Text = "Explorer";
        explorerToolStripMenuItem.Click += explorerToolStripMenuItem_Click;
        // 
        // slugcatIconImageList
        // 
        slugcatIconImageList.ColorDepth = ColorDepth.Depth32Bit;
        slugcatIconImageList.ImageSize = new Size(16, 16);
        slugcatIconImageList.TransparentColor = Color.Transparent;
        // 
        // slugConfigControl
        // 
        slugConfigControl.Dock = DockStyle.Fill;
        slugConfigControl.FivePebblesRarefactionCellConversationState = 0;
        slugConfigControl.Location = new Point(3, 3);
        slugConfigControl.Name = "slugConfigControl";
        slugConfigControl.Size = new Size(786, 392);
        slugConfigControl.TabIndex = 3;
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabPage1);
        tabControl1.Controls.Add(tabPage2);
        tabControl1.Controls.Add(tabPage3);
        tabControl1.Controls.Add(tabPage4);
        tabControl1.Controls.Add(tabPage5);
        tabControl1.Controls.Add(tabPage6);
        tabControl1.Dock = DockStyle.Fill;
        tabControl1.Location = new Point(0, 24);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(800, 426);
        tabControl1.TabIndex = 4;
        // 
        // tabPage1
        // 
        tabPage1.Controls.Add(slugConfigControl);
        tabPage1.Location = new Point(4, 24);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new Padding(3);
        tabPage1.Size = new Size(792, 398);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "Save States";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // tabPage2
        // 
        tabPage2.Location = new Point(4, 24);
        tabPage2.Name = "tabPage2";
        tabPage2.Padding = new Padding(3);
        tabPage2.Size = new Size(792, 398);
        tabPage2.TabIndex = 1;
        tabPage2.Text = "VanillaMaps";
        tabPage2.UseVisualStyleBackColor = true;
        // 
        // tabPage3
        // 
        tabPage3.Location = new Point(4, 24);
        tabPage3.Name = "tabPage3";
        tabPage3.Padding = new Padding(3);
        tabPage3.Size = new Size(484, 312);
        tabPage3.TabIndex = 2;
        tabPage3.Text = "VanillaMapUpdates";
        tabPage3.UseVisualStyleBackColor = true;
        // 
        // tabPage4
        // 
        tabPage4.Location = new Point(4, 24);
        tabPage4.Name = "tabPage4";
        tabPage4.Padding = new Padding(3);
        tabPage4.Size = new Size(484, 312);
        tabPage4.TabIndex = 3;
        tabPage4.Text = "Modded Maps";
        tabPage4.UseVisualStyleBackColor = true;
        // 
        // tabPage5
        // 
        tabPage5.Location = new Point(4, 24);
        tabPage5.Name = "tabPage5";
        tabPage5.Padding = new Padding(3);
        tabPage5.Size = new Size(792, 398);
        tabPage5.TabIndex = 4;
        tabPage5.Text = "Modded Map Updates";
        tabPage5.UseVisualStyleBackColor = true;
        // 
        // tabPage6
        // 
        tabPage6.Location = new Point(4, 24);
        tabPage6.Name = "tabPage6";
        tabPage6.Padding = new Padding(3);
        tabPage6.Size = new Size(792, 398);
        tabPage6.TabIndex = 5;
        tabPage6.Text = "Misc Progression Data";
        tabPage6.UseVisualStyleBackColor = true;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(tabControl1);
        Controls.Add(menuStrip1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MainMenuStrip = menuStrip1;
        Name = "MainForm";
        Text = "Rain World Save Editor";
        Load += MainForm_Load;
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        tabControl1.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem closeToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem openRainWorldSaveDirectoryToolStripMenuItem;
    private ToolStripMenuItem closeAndLaunchGameToolStripMenuItem;
    private ToolStripMenuItem saveSlotToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem1;
    private ToolStripMenuItem openFile1ToolStripMenuItem;
    private ToolStripMenuItem openFile2ToolStripMenuItem;
    private ToolStripMenuItem openFile3ToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem openFileToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ToolStripMenuItem saveAsToolStripMenuItem;
    private ImageList slugcatIconImageList;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStripMenuItem toggleConsoleToolStripMenuItem;
    private ToolStripMenuItem rainworldExecutableDirectoryToolStripMenuItem;
    private ToolStripMenuItem slugcatsToolStripMenuItem;
    private ToolStripMenuItem vanillaSlugcatsToolStripMenuItem;
    private ToolStripMenuItem dlcSlugcatsToolStripMenuItem;
    private ToolStripMenuItem moddedSlugcatsToolStripMenuItem;
    private Controls.SlugConfigControl slugConfigControl;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem exportToolStripMenuItem;
    private ToolStripMenuItem importToolStripMenuItem;
    private ToolStripMenuItem userProfileToolStripMenuItem;
    private ToolStripMenuItem explorerToolStripMenuItem;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private TabPage tabPage3;
    private TabPage tabPage4;
    private TabPage tabPage5;
    private TabPage tabPage6;
}
