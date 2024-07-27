namespace RainWorldSaveEditor.Controls
{
    partial class KarmaSelectorControl
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
            reinforceCheckBox = new CheckBox();
            commonToolTip = new ToolTip(components);
            karmaNumericUpDown = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            karmaMaxNumericUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)karmaNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)karmaMaxNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // reinforceCheckBox
            // 
            reinforceCheckBox.AutoSize = true;
            reinforceCheckBox.Location = new Point(76, 32);
            reinforceCheckBox.Name = "reinforceCheckBox";
            reinforceCheckBox.Size = new Size(113, 19);
            reinforceCheckBox.TabIndex = 0;
            reinforceCheckBox.Text = "Reinforce Karma";
            commonToolTip.SetToolTip(reinforceCheckBox, "Should karma be reinforced? I.E. Do you have a karma flower?");
            reinforceCheckBox.UseVisualStyleBackColor = true;
            reinforceCheckBox.CheckedChanged += reinforceCheckBox_CheckedChanged;
            // 
            // karmaNumericUpDown
            // 
            karmaNumericUpDown.Location = new Point(126, 3);
            karmaNumericUpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            karmaNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            karmaNumericUpDown.Name = "karmaNumericUpDown";
            karmaNumericUpDown.Size = new Size(64, 23);
            karmaNumericUpDown.TabIndex = 1;
            karmaNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            karmaNumericUpDown.ValueChanged += karmaNumericUpDown_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(76, 5);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 2;
            label1.Text = "Karma:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 72);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 4;
            label2.Text = "Karma Max:";
            // 
            // karmaMaxNumericUpDown
            // 
            karmaMaxNumericUpDown.Location = new Point(79, 70);
            karmaMaxNumericUpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            karmaMaxNumericUpDown.Name = "karmaMaxNumericUpDown";
            karmaMaxNumericUpDown.Size = new Size(110, 23);
            karmaMaxNumericUpDown.TabIndex = 3;
            karmaMaxNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            karmaMaxNumericUpDown.ValueChanged += karmaMaxNumericUpDown_ValueChanged;
            // 
            // KarmaSelectorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(karmaMaxNumericUpDown);
            Controls.Add(label1);
            Controls.Add(karmaNumericUpDown);
            Controls.Add(reinforceCheckBox);
            DoubleBuffered = true;
            Name = "KarmaSelectorControl";
            Size = new Size(193, 96);
            Load += KarmaSelectorControl_Load;
            Paint += KarmaSelectorControl_Paint;
            ((System.ComponentModel.ISupportInitialize)karmaNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)karmaMaxNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox reinforceCheckBox;
        private ToolTip commonToolTip;
        private NumericUpDown karmaNumericUpDown;
        private Label label1;
        private Label label2;
        private NumericUpDown karmaMaxNumericUpDown;
    }
}
