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
            foodPipControl = new FoodPipControl();
            cycleNumberNumericUpDown = new NumericUpDown();
            label1 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            ((System.ComponentModel.ISupportInitialize)cycleNumberNumericUpDown).BeginInit();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // foodPipControl
            // 
            foodPipControl.FilledPips = 0;
            foodPipControl.Location = new Point(3, 3);
            foodPipControl.Name = "foodPipControl";
            foodPipControl.PipBarIndex = 4;
            foodPipControl.PipCount = 7;
            foodPipControl.Size = new Size(242, 42);
            foodPipControl.TabIndex = 0;
            // 
            // cycleNumberNumericUpDown
            // 
            cycleNumberNumericUpDown.Location = new Point(95, 51);
            cycleNumberNumericUpDown.Name = "cycleNumberNumericUpDown";
            cycleNumberNumericUpDown.Size = new Size(120, 23);
            cycleNumberNumericUpDown.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 53);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 2;
            label1.Text = "Cycle Number:";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(69, 127);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(371, 222);
            tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(363, 194);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Slugcat Info";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(192, 72);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "World Info";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(363, 194);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Persistent Info";
            tabPage3.ToolTipText = "Data that persists across death";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // SlugConfigControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Controls.Add(label1);
            Controls.Add(cycleNumberNumericUpDown);
            Controls.Add(foodPipControl);
            Name = "SlugConfigControl";
            Size = new Size(549, 440);
            Load += SlugConfigControl_Load;
            ((System.ComponentModel.ISupportInitialize)cycleNumberNumericUpDown).EndInit();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private NumericUpDown cycleNumberNumericUpDown;
        private Label label1;
        public FoodPipControl foodPipControl;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
    }
}
