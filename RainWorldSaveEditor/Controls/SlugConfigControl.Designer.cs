namespace RainWorldSaveEditor.Controls
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
            FoodPipControl = new FoodPipControl();
            cycleNumberNumericUpDown = new NumericUpDown();
            label1 = new Label();
            tabControl1 = new TabControl();
            slugcatInfoTabPage = new TabPage();
            KarmaSelectorControl = new KarmaSelectorControl();
            worldInfoTabPage = new TabPage();
            persistentInfoTabPage = new TabPage();
            worldInfoTabControl = new TabControl();
            commonWorldInfoTabPage = new TabPage();
            worldInfoOutskirtstabPage = new TabPage();
            tabPage1 = new TabPage();
            slugcatInfoKarmaGroupBox = new GroupBox();
            currentDenTextBox = new TextBox();
            currentDenLabel = new Label();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)cycleNumberNumericUpDown).BeginInit();
            tabControl1.SuspendLayout();
            slugcatInfoTabPage.SuspendLayout();
            worldInfoTabPage.SuspendLayout();
            worldInfoTabControl.SuspendLayout();
            slugcatInfoKarmaGroupBox.SuspendLayout();
            groupBox1.SuspendLayout();
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
            slugcatInfoTabPage.Controls.Add(groupBox1);
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
            // KarmaSelectorControl
            // 
            KarmaSelectorControl.KarmaLevel = 1;
            KarmaSelectorControl.KarmaMax = 10;
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
            // persistentInfoTabPage
            // 
            persistentInfoTabPage.Location = new Point(4, 24);
            persistentInfoTabPage.Name = "persistentInfoTabPage";
            persistentInfoTabPage.Padding = new Padding(3);
            persistentInfoTabPage.Size = new Size(541, 412);
            persistentInfoTabPage.TabIndex = 2;
            persistentInfoTabPage.Text = "Persistent Info";
            persistentInfoTabPage.ToolTipText = "Data that persists across death";
            persistentInfoTabPage.UseVisualStyleBackColor = true;
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
            commonWorldInfoTabPage.Size = new Size(236, 156);
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
            // currentDenTextBox
            // 
            currentDenTextBox.Location = new Point(86, 16);
            currentDenTextBox.Name = "currentDenTextBox";
            currentDenTextBox.Size = new Size(136, 23);
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
            // groupBox1
            // 
            groupBox1.Controls.Add(currentDenTextBox);
            groupBox1.Controls.Add(currentDenLabel);
            groupBox1.Location = new Point(224, 81);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(228, 139);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
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
            worldInfoTabPage.ResumeLayout(false);
            worldInfoTabControl.ResumeLayout(false);
            slugcatInfoKarmaGroupBox.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
        private GroupBox groupBox1;
        private TextBox currentDenTextBox;
        private Label currentDenLabel;
        public KarmaSelectorControl KarmaSelectorControl;
    }
}
