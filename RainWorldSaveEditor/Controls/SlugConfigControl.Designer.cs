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
            ((System.ComponentModel.ISupportInitialize)cycleNumberNumericUpDown).BeginInit();
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
            // SlugConfigControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(cycleNumberNumericUpDown);
            Controls.Add(foodPipControl);
            Name = "SlugConfigControl";
            Size = new Size(415, 336);
            Load += SlugConfigControl_Load;
            ((System.ComponentModel.ISupportInitialize)cycleNumberNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private NumericUpDown cycleNumberNumericUpDown;
        private Label label1;
        public FoodPipControl foodPipControl;
    }
}
