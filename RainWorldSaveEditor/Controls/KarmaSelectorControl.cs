using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RainWorldSaveEditor.Controls;

public partial class KarmaSelectorControl : UserControl
{
    public KarmaSelectorControl()
    {
        InitializeComponent();
    }

    [Category("Action")]
    public event EventHandler? ReinforcedChanged;
    [Category("Action")]
    public event EventHandler? KarmaLevelChanged;
    [Category("Action")]
    public event EventHandler? KarmaMaxChanged;
    private void KarmaSelectorControl_Load(object sender, EventArgs e)
    {

    }

    private void reinforceCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        ReinforcedChanged?.Invoke(this, EventArgs.Empty);
        Invalidate();
    }


    private void karmaNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        KarmaLevelChanged?.Invoke(this, EventArgs.Empty);
        Invalidate();
    }

    private void karmaMaxNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
        karmaNumericUpDown.Maximum = karmaMaxNumericUpDown.Value;
        KarmaMaxChanged?.Invoke(this, EventArgs.Empty);
        Invalidate();
    }

    private void KarmaSelectorControl_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.Clear(BackColor);

        PointF center = new(reinforceCheckBox.Location.X / 2.0f, reinforceCheckBox.Location.X / 2.0f);
        PointF drawPos = new(center.X - 34, 0);

        if (Reinforced)
            e.Graphics.DrawImage(Properties.Resources.karma_empty_reinforced, drawPos);
        else
            e.Graphics.DrawImage(Properties.Resources.karma_empty, drawPos);

        Bitmap? img = Properties.Resources.missing;

        switch (KarmaLevel + 1)
        {
            case -1:
                img = null;
                break;
            case 0:
                img = Properties.Resources.karma00;
                break;
            case 1:
                img = Properties.Resources.karma10;
                break;
            case 2:
                img = Properties.Resources.karma20;
                break;
            case 3:
                img = Properties.Resources.karma30;
                break;
            case 4:
                img = Properties.Resources.karma40;
                break;
            case 5:
                img = Properties.Resources.karma50;
                break;
            case 6:
                img = GetKarma6Icon();
                break;
            case 7:
                img = GetKarma7Icon();
                break;
            case 8:
                img = GetKarma8Icon();
                break;
            case 9:
                img = GetKarma9Icon();
                break;
            case 10:
                img = Properties.Resources.karmaA0;
                break;
        }

        if (img is not null)
            e.Graphics.DrawImage(img, drawPos);

        img?.Dispose();

        return;

        Bitmap GetKarma6Icon()
        {
            if (KarmaMax <= 7)
                return Properties.Resources.karma61;
            else if (KarmaMax == 8)
                return Properties.Resources.karma62;
            else if (KarmaMax == 9)
                return Properties.Resources.karma63;
            else if (KarmaMax == 10)
                return Properties.Resources.karma64;
            else
                return Properties.Resources.missing;
        }

        Bitmap GetKarma7Icon()
        {
            if (KarmaMax <= 7)
                return Properties.Resources.karma71;
            else if (KarmaMax == 8)
                return Properties.Resources.karma72;
            else if (KarmaMax == 9)
                return Properties.Resources.karma73;
            else if (KarmaMax == 10)
                return Properties.Resources.karma74;
            else
                return Properties.Resources.missing;
        }

        Bitmap GetKarma8Icon()
        {
            if (KarmaMax <= 8)
                return Properties.Resources.karma82;
            else if (KarmaMax == 9)
                return Properties.Resources.karma83;
            else if (KarmaMax == 10)
                return Properties.Resources.karma84;
            else
                return Properties.Resources.missing;
        }

        Bitmap GetKarma9Icon()
        {
            if (KarmaMax <= 9)
                return Properties.Resources.karma93;
            else if (KarmaMax == 10)
                return Properties.Resources.karma94;
            else
                return Properties.Resources.missing;
        }
    }

    [Category("Appearance")]
    public bool Reinforced
    {
        get => reinforceCheckBox.Checked;
        set => reinforceCheckBox.Checked = value;
    }

    [Category("Appearance")]
    public int KarmaLevel
    {
        get => (int)(karmaNumericUpDown.Value - 1);
        set
        {

            karmaNumericUpDown.Value = (value + 1);
        }
    }

    [Category("Appearance")]
    public int KarmaMax
    {
        get => (int)(karmaMaxNumericUpDown.Value - 1);
        set
        {
            karmaNumericUpDown.Maximum = (value + 1);
            karmaMaxNumericUpDown.Value = (value + 1);
        }
    }

}
