using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveAPI;

public static class RWFileSerializer
{
    public static RainWorldSave? ReadSavExpFile(string filePath)
    {
        using var fs = File.OpenRead(filePath);
        var table = HashtableSerializer.Read(fs);
        fs.Close();

        if (table["save"] is string saveData)
        {
            var save = new RainWorldSave();
            save.Read(saveData);
            return save;
        }
        else
        {
            return null;
        }
    }

    public static void WriteSavExpFile(string filePath, RainWorldSave save)
    {
        var data = save.Write();
        using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        HashtableSerializer.Write(fs, new System.Collections.Hashtable
        {
            ["save"] = data,
            ["save__Backup"] = data
        });
        fs.Close();
    }

    public static ExpeditionCoreSave? ReadExpcoreFile(string filePath)
    {
        using var fs = File.OpenRead(filePath);
        var table = HashtableSerializer.Read(fs);
        fs.Close();

        if (table["core"] is string saveData)
        {
            var save = new ExpeditionCoreSave();
            save.Read(saveData);
            return save;
        }
        else
        {
            return null;
        }
    }

    public static void WriteExpcoreFile(string filePath, ExpeditionCoreSave save)
    {
        var data = save.Write();
        using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        HashtableSerializer.Write(fs, new System.Collections.Hashtable
        {
            ["core"] = data
        });
        fs.Close();
    }
}
