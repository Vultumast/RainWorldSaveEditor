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
        saveToolStripMenuItem = new ToolStripMenuItem();
        saveAsToolStripMenuItem = new ToolStripMenuItem();
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
        saveToolStripMenuItem1 = new ToolStripMenuItem();
        saveAsToolStripMenuItem1 = new ToolStripMenuItem();
        aboutToolStripMenuItem = new ToolStripMenuItem();
        toggleConsoleToolStripMenuItem = new ToolStripMenuItem();
        mainTabControl = new TabControl();
        slugcatIconImageList = new ImageList(components);
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, saveSlotToolStripMenuItem, aboutToolStripMenuItem, toggleConsoleToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(800, 24);
        menuStrip1.TabIndex = 0;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator1, closeAndLaunchGameToolStripMenuItem, closeToolStripMenuItem });
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
        openRainWorldSaveDirectoryToolStripMenuItem.Size = new Size(238, 22);
        openRainWorldSaveDirectoryToolStripMenuItem.Text = "Rain World Save Directory";
        openRainWorldSaveDirectoryToolStripMenuItem.Click += openRainWorldSaveDirectoryToolStripMenuItem_Click;
        // 
        // rainworldExecutableDirectoryToolStripMenuItem
        // 
        rainworldExecutableDirectoryToolStripMenuItem.Name = "rainworldExecutableDirectoryToolStripMenuItem";
        rainworldExecutableDirectoryToolStripMenuItem.Size = new Size(238, 22);
        rainworldExecutableDirectoryToolStripMenuItem.Text = "Rainworld Executable Directory";
        rainworldExecutableDirectoryToolStripMenuItem.Click += rainworldExecutableDirectoryToolStripMenuItem_Click;
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
        saveToolStripMenuItem.Size = new Size(202, 22);
        saveToolStripMenuItem.Text = "Save";
        // 
        // saveAsToolStripMenuItem
        // 
        saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
        saveAsToolStripMenuItem.Size = new Size(202, 22);
        saveAsToolStripMenuItem.Text = "Save As...";
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
        saveSlotToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem1, toolStripSeparator2, saveToolStripMenuItem1, saveAsToolStripMenuItem1 });
        saveSlotToolStripMenuItem.Name = "saveSlotToolStripMenuItem";
        saveSlotToolStripMenuItem.Size = new Size(66, 20);
        saveSlotToolStripMenuItem.Text = "Save Slot";
        // 
        // openToolStripMenuItem1
        // 
        openToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { openFile1ToolStripMenuItem, openFile2ToolStripMenuItem, openFile3ToolStripMenuItem, toolStripSeparator3, openFileToolStripMenuItem });
        openToolStripMenuItem1.Name = "openToolStripMenuItem1";
        openToolStripMenuItem1.Size = new Size(123, 22);
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
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new Size(120, 6);
        // 
        // saveToolStripMenuItem1
        // 
        saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
        saveToolStripMenuItem1.Size = new Size(123, 22);
        saveToolStripMenuItem1.Text = "Save";
        // 
        // saveAsToolStripMenuItem1
        // 
        saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
        saveAsToolStripMenuItem1.Size = new Size(123, 22);
        saveAsToolStripMenuItem1.Text = "Save As...";
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
        // mainTabControl
        // 
        mainTabControl.Dock = DockStyle.Fill;
        mainTabControl.Location = new Point(0, 24);
        mainTabControl.Name = "mainTabControl";
        mainTabControl.SelectedIndex = 0;
        mainTabControl.Size = new Size(800, 426);
        mainTabControl.TabIndex = 1;
        // 
        // slugcatIconImageList
        // 
        slugcatIconImageList.ColorDepth = ColorDepth.Depth32Bit;
        slugcatIconImageList.ImageSize = new Size(16, 16);
        slugcatIconImageList.TransparentColor = Color.Transparent;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(mainTabControl);
        Controls.Add(menuStrip1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MainMenuStrip = menuStrip1;
        Name = "MainForm";
        Text = "Rain World Save Editor";
        Load += MainForm_Load;
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem closeToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ToolStripMenuItem saveAsToolStripMenuItem;
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
    private ToolStripMenuItem saveToolStripMenuItem1;
    private ToolStripMenuItem saveAsToolStripMenuItem1;
    private TabControl mainTabControl;
    private ImageList slugcatIconImageList;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStripMenuItem toggleConsoleToolStripMenuItem;
    private ToolStripMenuItem rainworldExecutableDirectoryToolStripMenuItem;
}
