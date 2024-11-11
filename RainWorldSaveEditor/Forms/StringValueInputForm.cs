using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RainWorldSaveEditor.Forms;
public partial class StringValueInputForm : Form
{
    public struct Option
    {
        public string Value;
        public string Display;
    }

    public List<Option> AvailableOptions { get; set; } = [];

    public string? SelectedOption { get; set; }

    public StringValueInputForm()
    {
        InitializeComponent();
    }

    private void StringValueInputForm_Load(object sender, EventArgs e)
    {
        comboBox.Items.Clear();
        foreach (var item in AvailableOptions)
        {
            comboBox.Items.Add(item.Display);
        }
    }

    private void addSelectionButton_Click(object sender, EventArgs e)
    {
        if (comboBox.SelectedIndex != -1)
        {
            SelectedOption = AvailableOptions[comboBox.SelectedIndex].Value;
            Close();
        }
    }

    private void addCustomButton_Click(object sender, EventArgs e)
    {
        if (textBox.Text != "")
        {
            SelectedOption = textBox.Text;
            Close();
        }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
        SelectedOption = null;
        Close();
    }
}
