using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace RainWorldSaveEditor;

public static class Utils
{
    public const string RainworldSaveDirectoryPostFix = "AppData\\LocalLow\\Videocult\\Rain World";

    private static ResourceManager _resourceManager = null!;
    private static ResourceSet _resourceSet = null!;

    public static System.Text.Json.JsonSerializerOptions JSONSerializerOptions { get; private set; } = new() { WriteIndented = true };

    public static ResourceManager ResourceManager
    {
        get
        {
            if (_resourceManager is null)
                _resourceManager = new ResourceManager("RainWorldSaveEditor.Properties.Resources", Assembly.GetExecutingAssembly());

            return _resourceManager;
        }
    }

    public static ResourceSet ResourceSet
    {
        get
        {
            if (_resourceSet is null)
                _resourceSet = ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true)!;

            return _resourceSet;
        }
    }
    public static IEnumerable<DictionaryEntry> ResourceList()
    {
        foreach (var item in ResourceSet)
        {
            if (item.GetType() != typeof(DictionaryEntry))
            {
                Logger.Warn($"Resource: \"{item}\" was not expected type. It was \"{item.GetType()}\"");
                continue;
            }

            yield return (DictionaryEntry)item;
        }
    }


    public static void CreateDirectoryIfNotExist(string path)
    {
        if (!Directory.Exists(path))
        {
            Logger.Info($"Directory did not exist: \"{path}\" so it will be created.");
            Directory.CreateDirectory(path);
        }
    }
}
