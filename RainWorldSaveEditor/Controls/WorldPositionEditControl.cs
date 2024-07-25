using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RainWorldSaveEditor.Controls
{
    public partial class WorldPositionEditControl : UserControl
    {
        public WorldPositionEditControl()
        {
            InitializeComponent();
        }

        private void WorldPositionEditControl_Resize(object sender, EventArgs e)
        {
            int halfSize = this.Width / 2;


            xPositionLabel.Location = new(3, 25);
            yPositionLabel.Location = new(halfSize + 3, 25);

            xPositionNumericUpDown.Location = new(26, 23);
            yPositionNumericUpDown.Location = new(halfSize + 26, 23);

            xPositionNumericUpDown.Width = (halfSize - (xPositionLabel.Width + 6)) - 3;

            yPositionNumericUpDown.Width = (halfSize - (xPositionLabel.Width + 6)) - 3;
        }

        private void WorldPositionEditControl_Load(object sender, EventArgs e)
        {
            int halfSize = this.Width / 2;


            xPositionLabel.Location = new(3, 25);
            yPositionLabel.Location = new(halfSize + 3, 25);

            xPositionNumericUpDown.Location = new(26, 23);
            yPositionNumericUpDown.Location = new(halfSize + 26, 23);

            xPositionNumericUpDown.Width = (halfSize - (xPositionLabel.Width + 6)) - 3;

            yPositionNumericUpDown.Width = (halfSize - (xPositionLabel.Width + 6)) - 3;
        }


        [Category("Behavior")]
        public string RoomName
        {
            get => roomNameTextBox.Text;
            set => roomNameTextBox.Text = value;
        }

        [Category("Behavior")]
        public int X
        {
            get => (int)xPositionNumericUpDown.Value;
            set => xPositionNumericUpDown.Value = value;
        }

        [Category("Behavior")]
        public int Y
        {
            get => (int)yPositionNumericUpDown.Value;
            set => yPositionNumericUpDown.Value = value;
        }


        [Category("Behavior")]
        public int AbstractNode
        {
            get => (int)abstractNodeNumericUpDown.Value;
            set => abstractNodeNumericUpDown.Value = value;
        }
    }
}
