namespace RainWorldSaveEditor.Forms
{
    partial class AddEchoForm
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
            echoRegionCodeTextBox = new TextBox();
            valueComboBox = new ComboBox();
            echoRegionCodeLabel = new Label();
            echoValueLabel = new Label();
            panel1 = new Panel();
            okButton = new Button();
            cancelButton = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // echoRegionCodeTextBox
            // 
            echoRegionCodeTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            echoRegionCodeTextBox.Location = new Point(96, 12);
            echoRegionCodeTextBox.Name = "echoRegionCodeTextBox";
            echoRegionCodeTextBox.Size = new Size(190, 23);
            echoRegionCodeTextBox.TabIndex = 0;
            // 
            // valueComboBox
            // 
            valueComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            valueComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            valueComboBox.FormattingEnabled = true;
            valueComboBox.Items.AddRange(new object[] { "Not Met", "Active", "Visited" });
            valueComboBox.Location = new Point(96, 41);
            valueComboBox.Name = "valueComboBox";
            valueComboBox.Size = new Size(190, 23);
            valueComboBox.TabIndex = 1;
            // 
            // echoRegionCodeLabel
            // 
            echoRegionCodeLabel.AutoSize = true;
            echoRegionCodeLabel.Location = new Point(12, 15);
            echoRegionCodeLabel.Name = "echoRegionCodeLabel";
            echoRegionCodeLabel.Size = new Size(78, 15);
            echoRegionCodeLabel.TabIndex = 2;
            echoRegionCodeLabel.Text = "Region Code:";
            // 
            // echoValueLabel
            // 
            echoValueLabel.AutoSize = true;
            echoValueLabel.Location = new Point(12, 44);
            echoValueLabel.Name = "echoValueLabel";
            echoValueLabel.Size = new Size(38, 15);
            echoValueLabel.TabIndex = 3;
            echoValueLabel.Text = "Value:";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(okButton);
            panel1.Controls.Add(cancelButton);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 95);
            panel1.Name = "panel1";
            panel1.Size = new Size(298, 29);
            panel1.TabIndex = 4;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(130, 3);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 5;
            okButton.Text = "Ok";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(211, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 6;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // AddEchoForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            CancelButton = cancelButton;
            ClientSize = new Size(298, 124);
            Controls.Add(panel1);
            Controls.Add(echoValueLabel);
            Controls.Add(echoRegionCodeLabel);
            Controls.Add(valueComboBox);
            Controls.Add(echoRegionCodeTextBox);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddEchoForm";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Echo";
            Load += AddEchoForm_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox echoRegionCodeTextBox;
        private ComboBox valueComboBox;
        private Label echoRegionCodeLabel;
        private Label echoValueLabel;
        private Panel panel1;
        private Button okButton;
        private Button cancelButton;
    }
}