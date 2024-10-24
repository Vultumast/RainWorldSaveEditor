using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace RainWorldSaveEditor.Forms;
public partial class SaveTypeSelectForm : Form
{
    public SaveTypeSelectForm()
    {
        InitializeComponent();
    }

    void UpdateTitle()
    {
        var targetName = $"Rain World Save Editor (Beta) - {Assembly.GetExecutingAssembly().GetName().Version!.ToString()}"; ;

        if (Text != targetName)
            Text = targetName;
    }

    private void SaveTypeSelectForm_Load(object sender, EventArgs e)
    {
        UpdateTitle();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        var frm = new MainForm
        {
            Location = Location,
            StartPosition = FormStartPosition.Manual
        };
        frm.FormClosing += OnChildFormClose;
        frm.Show();
        Hide();
    }

    private void OnChildFormClose(object? sender, FormClosingEventArgs e)
    {
        Location = (sender as Form)!.Location;
        Show();
    }
}
