using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RainWorldSaveEditor.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            // Makes it somewhat more readable without having to edit the resource text
            licenseTextBox.Text = LineEndingsRegex().Replace(licenseTextBox.Text, " ");
            licenseTextBox.Text = TripleLineRegex().Replace(licenseTextBox.Text, Environment.NewLine + Environment.NewLine);
        }

        private void githubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo() { UseShellExecute = true, FileName = "https://github.com/Vultumast/RainWorldSaveEditor" });
            githubLinkLabel.LinkVisited = true;
        }

        [GeneratedRegex("(?<=\\S)(\\r\\n|\\n)(?=\\S)")]
        private static partial Regex LineEndingsRegex();

        [GeneratedRegex("(\\r\\n|\\n)(\\r\\n|\\n)(\\r\\n|\\n)")]
        private static partial Regex TripleLineRegex();
    }
}
