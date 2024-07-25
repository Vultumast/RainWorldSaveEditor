namespace RainWorldSaveEditor.Controls
{
    partial class WorldPositionEditControl
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
            roomNameTextBox = new TextBox();
            roomNameLabel = new Label();
            xPositionNumericUpDown = new NumericUpDown();
            xPositionLabel = new Label();
            yPositionLabel = new Label();
            yPositionNumericUpDown = new NumericUpDown();
            abstractNodeNumericUpDown = new NumericUpDown();
            abstractNodeLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)xPositionNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)yPositionNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)abstractNodeNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // roomNameTextBox
            // 
            roomNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            roomNameTextBox.Location = new Point(51, 0);
            roomNameTextBox.Margin = new Padding(3, 3, 3, 0);
            roomNameTextBox.Name = "roomNameTextBox";
            roomNameTextBox.Size = new Size(187, 23);
            roomNameTextBox.TabIndex = 0;
            // 
            // roomNameLabel
            // 
            roomNameLabel.AutoSize = true;
            roomNameLabel.Location = new Point(3, 3);
            roomNameLabel.Name = "roomNameLabel";
            roomNameLabel.Size = new Size(42, 15);
            roomNameLabel.TabIndex = 1;
            roomNameLabel.Text = "Room:";
            // 
            // xPositionNumericUpDown
            // 
            xPositionNumericUpDown.Location = new Point(26, 23);
            xPositionNumericUpDown.Margin = new Padding(3, 0, 3, 0);
            xPositionNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            xPositionNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            xPositionNumericUpDown.Name = "xPositionNumericUpDown";
            xPositionNumericUpDown.Size = new Size(96, 23);
            xPositionNumericUpDown.TabIndex = 2;
            // 
            // xPositionLabel
            // 
            xPositionLabel.AutoSize = true;
            xPositionLabel.Location = new Point(3, 25);
            xPositionLabel.Name = "xPositionLabel";
            xPositionLabel.Size = new Size(17, 15);
            xPositionLabel.TabIndex = 3;
            xPositionLabel.Text = "X:";
            // 
            // yPositionLabel
            // 
            yPositionLabel.AutoSize = true;
            yPositionLabel.Location = new Point(119, 25);
            yPositionLabel.Name = "yPositionLabel";
            yPositionLabel.Size = new Size(17, 15);
            yPositionLabel.TabIndex = 4;
            yPositionLabel.Text = "Y:";
            // 
            // yPositionNumericUpDown
            // 
            yPositionNumericUpDown.Location = new Point(142, 23);
            yPositionNumericUpDown.Margin = new Padding(3, 0, 3, 0);
            yPositionNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            yPositionNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            yPositionNumericUpDown.Name = "yPositionNumericUpDown";
            yPositionNumericUpDown.Size = new Size(96, 23);
            yPositionNumericUpDown.TabIndex = 5;
            // 
            // abstractNodeNumericUpDown
            // 
            abstractNodeNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            abstractNodeNumericUpDown.Location = new Point(95, 46);
            abstractNodeNumericUpDown.Margin = new Padding(3, 0, 3, 0);
            abstractNodeNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            abstractNodeNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            abstractNodeNumericUpDown.Name = "abstractNodeNumericUpDown";
            abstractNodeNumericUpDown.Size = new Size(143, 23);
            abstractNodeNumericUpDown.TabIndex = 6;
            // 
            // abstractNodeLabel
            // 
            abstractNodeLabel.AutoSize = true;
            abstractNodeLabel.Location = new Point(3, 48);
            abstractNodeLabel.Name = "abstractNodeLabel";
            abstractNodeLabel.Size = new Size(86, 15);
            abstractNodeLabel.TabIndex = 7;
            abstractNodeLabel.Text = "Abstract Node:";
            // 
            // WorldPositionEditControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(abstractNodeLabel);
            Controls.Add(abstractNodeNumericUpDown);
            Controls.Add(yPositionNumericUpDown);
            Controls.Add(yPositionLabel);
            Controls.Add(xPositionLabel);
            Controls.Add(xPositionNumericUpDown);
            Controls.Add(roomNameLabel);
            Controls.Add(roomNameTextBox);
            Name = "WorldPositionEditControl";
            Size = new Size(238, 69);
            Load += WorldPositionEditControl_Load;
            Resize += WorldPositionEditControl_Resize;
            ((System.ComponentModel.ISupportInitialize)xPositionNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)yPositionNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)abstractNodeNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox roomNameTextBox;
        private Label roomNameLabel;
        private NumericUpDown xPositionNumericUpDown;
        private Label xPositionLabel;
        private Label yPositionLabel;
        private NumericUpDown yPositionNumericUpDown;
        private NumericUpDown abstractNodeNumericUpDown;
        private Label abstractNodeLabel;
    }
}
