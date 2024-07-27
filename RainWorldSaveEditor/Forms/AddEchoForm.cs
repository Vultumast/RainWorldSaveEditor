using RainWorldSaveAPI;
using RainWorldSaveAPI.SaveElements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RainWorldSaveEditor.Forms
{
    public partial class AddEchoForm : Form
    {
        public SaveState SaveState { get; private set; } = null!;
        public AddEchoForm(SaveState state)
        {
            SaveState = state;
            InitializeComponent();
        }

        private void AddEchoForm_Load(object sender, EventArgs e)
        {
            valueComboBox.SelectedIndex = 0;
        }

        public string RegionCode 
        {
            get => echoRegionCodeTextBox.Text;
            set => echoRegionCodeTextBox.Text = value;
        } 

        public EchoState EchoState
        {
            get => (EchoState)valueComboBox.SelectedIndex;
            set => valueComboBox.SelectedIndex = (int)value;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(echoRegionCodeTextBox.Text))
            {
                MessageBox.Show("Region code cannot be null, empty or consist only of whitespace characters.", "Cannot add Echo");
                return;
            }

            if (SaveState.DeathPersistentSaveData.Echos.EchoStates.ContainsKey(echoRegionCodeTextBox.Text.ToUpper()))
            {
                MessageBox.Show($"This save already defines an entry for \"{echoRegionCodeTextBox.Text}\"", "Cannot add Echo");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
