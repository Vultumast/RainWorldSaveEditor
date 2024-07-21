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
            RainWorldSave save = new();
            save.Read(saveData);
        }
        else
        {
            Logger.Log("Save data not found.");
        }

        if (table["save__Backup"] is string saveBackupData)
        {
            RainWorldSave saveBackup = new();
            saveBackup.Read(saveBackupData);
        }
        else
        {
            Logger.Log("Save data not found.");
        }
    }
}
