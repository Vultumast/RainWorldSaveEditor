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
        menuStrip1.SuspendLayout();
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
        openToolStripMenuItem.Size = new Size(180, 22);
        openToolStripMenuItem.Text = "Open";
        // 
        // userProfileToolStripMenuItem
        // 
        userProfileToolStripMenuItem.Name = "userProfileToolStripMenuItem";
        userProfileToolStripMenuItem.Size = new Size(180, 22);
        userProfileToolStripMenuItem.Text = "User Profile";
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(177, 6);
        // 
        // closeToolStripMenuItem
        // 
        closeToolStripMenuItem.Name = "closeToolStripMenuItem";
        closeToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
        closeToolStripMenuItem.Size = new Size(180, 22);
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
        openToolStripMenuItem1.Size = new Size(180, 22);
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
        openFileToolStripMenuItem.Size = new Size(180, 22);
        openFileToolStripMenuItem.Text = "Open File...";
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
        // aboutToolStripMenuItem
        // 
        aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        aboutToolStripMenuItem.Size = new Size(52, 20);
        aboutToolStripMenuItem.Text = "About";
        aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
        // 
        // ExpeditionCoreSaveForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Name = "ExpeditionCoreSaveForm";
        Text = "ExpeditionCoreSaveForm";
        Load += ExpeditionCoreSaveForm_Load;
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
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
}