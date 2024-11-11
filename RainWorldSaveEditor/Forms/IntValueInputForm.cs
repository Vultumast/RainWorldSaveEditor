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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RainWorldSaveEditor.Forms;
public partial class IntValueInputForm : Form
{
    public struct Option
    {
        public string Value;
        public string Display;
    }

    public List<Option> AvailableOptions { get; set; } = [];

    public string? SelectedOption { get; set; }
    public int SelectedNumber { get; set; }

    public IntValueInputForm()
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
        if (comboBox.SelectedIndex != -1 && int.TryParse(numberTextBox.Text, out var number))
        {
            SelectedOption = AvailableOptions[comboBox.SelectedIndex].Value;
            SelectedNumber = number;
            Close();
        }
    }

    private void addCustomButton_Click(object sender, EventArgs e)
    {
        if (textBox.Text != "" && int.TryParse(numberTextBox.Text, out var number))
        {
            SelectedOption = textBox.Text;
            SelectedNumber = number;
            Close();
        }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
        SelectedOption = null;
        SelectedNumber = 0;
        Close();
    }
}
