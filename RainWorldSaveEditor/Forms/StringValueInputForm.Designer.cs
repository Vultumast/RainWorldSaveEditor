namespace RainWorldSaveEditor.Forms;

partial class StringValueInputForm
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
        selectFromListLabel = new Label();
        selectCustomLabel = new Label();
        comboBox = new ComboBox();
        textBox = new TextBox();
        addSelectionButton = new Button();
        addCustomButton = new Button();
        cancelButton = new Button();
        SuspendLayout();
        // 
        // selectFromListLabel
        // 
        selectFromListLabel.AutoSize = true;
        selectFromListLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        selectFromListLabel.Location = new Point(12, 20);
        selectFromListLabel.Name = "selectFromListLabel";
        selectFromListLabel.Size = new Size(164, 17);
        selectFromListLabel.TabIndex = 0;
        selectFromListLabel.Text = "Select a value from the list:";
        // 
        // selectCustomLabel
        // 
        selectCustomLabel.AutoSize = true;
        selectCustomLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        selectCustomLabel.Location = new Point(12, 51);
        selectCustomLabel.Name = "selectCustomLabel";
        selectCustomLabel.Size = new Size(150, 17);
        selectCustomLabel.TabIndex = 1;
        selectCustomLabel.Text = "Or input a custom value:";
        // 
        // comboBox
        // 
        comboBox.FormattingEnabled = true;
        comboBox.Location = new Point(182, 19);
        comboBox.Name = "comboBox";
        comboBox.Size = new Size(218, 23);
        comboBox.TabIndex = 2;
        // 
        // textBox
        // 
        textBox.Location = new Point(182, 50);
        textBox.Name = "textBox";
        textBox.Size = new Size(218, 23);
        textBox.TabIndex = 3;
        // 
        // addSelectionButton
        // 
        addSelectionButton.Location = new Point(406, 22);
        addSelectionButton.Name = "addSelectionButton";
        addSelectionButton.Size = new Size(120, 23);
        addSelectionButton.TabIndex = 4;
        addSelectionButton.Text = "Add Selected";
        addSelectionButton.UseVisualStyleBackColor = true;
        addSelectionButton.Click += addSelectionButton_Click;
        // 
        // addCustomButton
        // 
        addCustomButton.Location = new Point(406, 51);
        addCustomButton.Name = "addCustomButton";
        addCustomButton.Size = new Size(120, 23);
        addCustomButton.TabIndex = 5;
        addCustomButton.Text = "Add Custom";
        addCustomButton.UseVisualStyleBackColor = true;
        addCustomButton.Click += addCustomButton_Click;
        // 
        // cancelButton
        // 
        cancelButton.Location = new Point(406, 80);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(120, 23);
        cancelButton.TabIndex = 6;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += cancelButton_Click;
        // 
        // StringValueInputForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(538, 115);
        Controls.Add(cancelButton);
        Controls.Add(addCustomButton);
        Controls.Add(addSelectionButton);
        Controls.Add(textBox);
        Controls.Add(comboBox);
        Controls.Add(selectCustomLabel);
        Controls.Add(selectFromListLabel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Name = "StringValueInputForm";
        Text = "StringValueInputForm";
        Load += StringValueInputForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label selectFromListLabel;
    private Label selectCustomLabel;
    private ComboBox comboBox;
    private TextBox textBox;
    private Button addSelectionButton;
    private Button addCustomButton;
    private Button cancelButton;
}