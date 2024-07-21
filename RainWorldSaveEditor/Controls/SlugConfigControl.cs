﻿using System;
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
    public partial class SlugConfigControl : UserControl
    {
        public SlugConfigControl()
        {
            InitializeComponent();
        }

        private void SlugConfigControl_Load(object sender, EventArgs e)
        {
            cycleNumberNumericUpDown.Minimum = uint.MinValue;
            cycleNumberNumericUpDown.Maximum = uint.MaxValue;
        }

        public uint CycleNumber
        {
            get => (uint)cycleNumberNumericUpDown.Value;
            set => cycleNumberNumericUpDown.Value = value;
        }
    }
}
