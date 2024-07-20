using RainWorldSaveEditor.Save;

namespace RainWorldSaveEditor;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent(); 

        var table = HashtableSerializer.Read(File.OpenRead("TestFiles/sav.xml"));

        HashtableSerializer.Write(File.OpenWrite("TestFiles/savsaved.xml"), table);
    }
}
