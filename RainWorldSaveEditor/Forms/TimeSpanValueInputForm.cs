namespace RainWorldSaveEditor.Forms;

public partial class TimeSpanValueInputForm : Form
{
    public struct Option
    {
        public string Value;
        public string Display;
    }

    public List<Option> AvailableOptions { get; set; } = [];

    public string? SelectedOption { get; set; }
    public TimeSpan SelectedTime { get; set; }

    public TimeSpanValueInputForm()
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
        if (comboBox.SelectedIndex != -1 && TimeSpan.TryParse(timeSpanTextBox.Text, out var time))
        {
            SelectedOption = AvailableOptions[comboBox.SelectedIndex].Value;
            SelectedTime = time;
            Close();
        }
    }

    private void addCustomButton_Click(object sender, EventArgs e)
    {
        if (textBox.Text != "" && TimeSpan.TryParse(timeSpanTextBox.Text, out var time))
        {
            SelectedOption = textBox.Text;
            SelectedTime = time;
            Close();
        }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
        SelectedOption = null;
        SelectedTime = TimeSpan.Zero;
        Close();
    }
}
