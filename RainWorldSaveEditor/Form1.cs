using RainWorldSaveEditor.Save;

namespace RainWorldSaveEditor;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent(); 

        var table = HashtableSerializer.Read(File.OpenRead("TestFiles/sav.xml"));
        HashtableSerializer.Write(File.OpenWrite("TestFiles/savsaved.xml"), table);

        if (table["save"] is string saveData)
        {
            RainWorldSave save = RainWorldSave.Read(saveData);
        }
        else
        {
            Console.WriteLine("Save data not found.");
        }

        if (table["save__Backup"] is string saveBackupData)
        {
            RainWorldSave save = RainWorldSave.Read(saveBackupData);
        }
        else
        {
            Console.WriteLine("Save data not found.");
        }
    }
}
