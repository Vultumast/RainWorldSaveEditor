namespace RainWorldSaveEditor.Forms
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            iconPictureBox = new PictureBox();
            editorNameLabel = new Label();
            disclaimerLabel = new Label();
            leadDevsLabel = new Label();
            githubLinkLabel = new LinkLabel();
            label1 = new Label();
            licenseTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox).BeginInit();
            SuspendLayout();
            // 
            // iconPictureBox
            // 
            iconPictureBox.BackgroundImage = Properties.Resources.editoriconimg;
            iconPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            iconPictureBox.Location = new Point(12, 12);
            iconPictureBox.Name = "iconPictureBox";
            iconPictureBox.Size = new Size(128, 128);
            iconPictureBox.TabIndex = 0;
            iconPictureBox.TabStop = false;
            // 
            // editorNameLabel
            // 
            editorNameLabel.AutoSize = true;
            editorNameLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            editorNameLabel.Location = new Point(146, 12);
            editorNameLabel.Name = "editorNameLabel";
            editorNameLabel.Size = new Size(169, 20);
            editorNameLabel.TabIndex = 1;
            editorNameLabel.Text = "Rain World Save Editor";
            // 
            // disclaimerLabel
            // 
            disclaimerLabel.AutoSize = true;
            disclaimerLabel.Location = new Point(12, 143);
            disclaimerLabel.Name = "disclaimerLabel";
            disclaimerLabel.Size = new Size(384, 30);
            disclaimerLabel.TabIndex = 2;
            disclaimerLabel.Text = "No contributors of \"Rain World Save Editor\" are affliated with Videocult.\r\nPlease consult the included LICENSE.txt for licensing information.";
            // 
            // leadDevsLabel
            // 
            leadDevsLabel.AutoSize = true;
            leadDevsLabel.Location = new Point(146, 47);
            leadDevsLabel.Name = "leadDevsLabel";
            leadDevsLabel.Size = new Size(217, 15);
            leadDevsLabel.TabIndex = 3;
            leadDevsLabel.Text = "Created by Marioalexsan and Vultumast";
            // 
            // githubLinkLabel
            // 
            githubLinkLabel.AutoSize = true;
            githubLinkLabel.Location = new Point(146, 32);
            githubLinkLabel.Name = "githubLinkLabel";
            githubLinkLabel.Size = new Size(68, 15);
            githubLinkLabel.TabIndex = 4;
            githubLinkLabel.TabStop = true;
            githubLinkLabel.Text = "Github Link";
            githubLinkLabel.LinkClicked += githubLinkLabel_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(146, 125);
            label1.Name = "label1";
            label1.Size = new Size(129, 15);
            label1.TabIndex = 5;
            label1.Text = "Icon created by Saneko";
            // 
            // licenseTextBox
            // 
            licenseTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            licenseTextBox.BorderStyle = BorderStyle.FixedSingle;
            licenseTextBox.Location = new Point(12, 176);
            licenseTextBox.Multiline = true;
            licenseTextBox.Name = "licenseTextBox";
            licenseTextBox.ReadOnly = true;
            licenseTextBox.ScrollBars = ScrollBars.Vertical;
            licenseTextBox.Size = new Size(380, 197);
            licenseTextBox.TabIndex = 6;
            licenseTextBox.Text = resources.GetString("licenseTextBox.Text");
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(404, 385);
            Controls.Add(licenseTextBox);
            Controls.Add(label1);
            Controls.Add(githubLinkLabel);
            Controls.Add(leadDevsLabel);
            Controls.Add(disclaimerLabel);
            Controls.Add(editorNameLabel);
            Controls.Add(iconPictureBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AboutForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            TopMost = true;
            Load += AboutForm_Load;
            ((System.ComponentModel.ISupportInitialize)iconPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox iconPictureBox;
        private Label editorNameLabel;
        private Label disclaimerLabel;
        private Label leadDevsLabel;
        private LinkLabel githubLinkLabel;
        private Label label1;
        private TextBox licenseTextBox;
    }
}