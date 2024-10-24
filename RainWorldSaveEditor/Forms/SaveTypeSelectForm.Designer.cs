namespace RainWorldSaveEditor.Forms;

partial class SaveTypeSelectForm
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
        tableLayoutPanel1 = new TableLayoutPanel();
        button2 = new Button();
        pictureBox1 = new PictureBox();
        label2 = new Label();
        label1 = new Label();
        button1 = new Button();
        tableLayoutPanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Controls.Add(button2, 1, 3);
        tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
        tableLayoutPanel1.Controls.Add(label2, 0, 2);
        tableLayoutPanel1.Controls.Add(label1, 0, 1);
        tableLayoutPanel1.Controls.Add(button1, 0, 3);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 4;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        tableLayoutPanel1.Size = new Size(472, 433);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // button2
        // 
        button2.Anchor = AnchorStyles.None;
        button2.AutoSize = true;
        button2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        button2.Enabled = false;
        button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
        button2.Location = new Point(253, 392);
        button2.Name = "button2";
        button2.Size = new Size(202, 31);
        button2.TabIndex = 8;
        button2.Text = "Edit Expedition Unlocks";
        button2.UseVisualStyleBackColor = true;
        // 
        // pictureBox1
        // 
        pictureBox1.BackgroundImage = Properties.Resources.editoriconimg;
        pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
        tableLayoutPanel1.SetColumnSpan(pictureBox1, 2);
        pictureBox1.Dock = DockStyle.Fill;
        pictureBox1.Location = new Point(3, 3);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(466, 297);
        pictureBox1.TabIndex = 4;
        pictureBox1.TabStop = false;
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        label2.AutoSize = true;
        tableLayoutPanel1.SetColumnSpan(label2, 2);
        label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label2.Location = new Point(3, 352);
        label2.Name = "label2";
        label2.Size = new Size(466, 21);
        label2.TabIndex = 6;
        label2.Text = "Created by Marioalexsan and Vultumast";
        label2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        label1.AutoSize = true;
        tableLayoutPanel1.SetColumnSpan(label1, 2);
        label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
        label1.Location = new Point(3, 307);
        label1.Name = "label1";
        label1.Size = new Size(466, 32);
        label1.TabIndex = 5;
        label1.Text = "Rain World Save Editor";
        label1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // button1
        // 
        button1.Anchor = AnchorStyles.None;
        button1.AutoSize = true;
        button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
        button1.Location = new Point(31, 392);
        button1.Name = "button1";
        button1.Size = new Size(173, 31);
        button1.TabIndex = 7;
        button1.Text = "Edit Main Save Data";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // SaveTypeSelectForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(472, 433);
        Controls.Add(tableLayoutPanel1);
        Name = "SaveTypeSelectForm";
        Text = "Rain World Save Editor";
        Load += SaveTypeSelectForm_Load;
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private PictureBox pictureBox1;
    private Label label2;
    private Label label1;
    private Button button2;
    private Button button1;
}