namespace RainWorldSaveEditor;

public class ExpeditionMissionInfo(string Key, string Name)
{
    public const string ExpeditionMissionInfoDirectoryPath = "Resources\\Expedition Missions\\Info";

    public static Dictionary<string, ExpeditionMissionInfo> Missions { get; private set; } = [];

    public string Key { get; set; } = Key;
    public string Name { get; set; } = Name;

    public static ExpeditionMissionInfo[] Read(string filepath) => System.Text.Json.JsonSerializer.Deserialize<ExpeditionMissionInfo[]>(File.ReadAllText(filepath))!;

    public static void ReadExpeditionMissionInfo()
    {
        Logger.Info("Reading Expedition Mission Info...");
        if (!Directory.Exists("Resources") || !Directory.Exists(ExpeditionMissionInfoDirectoryPath))
        {
            Logger.Warn("Expedition Mission Info did not exist, so it will be remade");
            WriteDefaultExpeditionMissionInfo();
        }

        List<ExpeditionMissionInfo> list = [];

        var files = Directory.GetFiles(ExpeditionMissionInfoDirectoryPath, "*.json", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            try
            {
                list.AddRange(Read(file));
            }
            catch (Exception ex)
            {
                Logger.DeserializationError(file, nameof(ExpeditionMissionInfo), ex);
            }
        }

        foreach (var info in list)
            Missions[info.Key] = info;

        Logger.Info("Finished Reading Expedition Mission Info");
    }

    public static void WriteDefaultExpeditionMissionInfo()
    {
        if (!Directory.Exists("Resources"))
            Directory.CreateDirectory("Resources");

        if (!Directory.Exists(ExpeditionMissionInfoDirectoryPath))
            Directory.CreateDirectory(ExpeditionMissionInfoDirectoryPath);

        Logger.Info("Writing Default Expedition Mission Information...");
        foreach (var resource in Utils.ResourceList())
        {
            if (resource.Value is null)
            {
                Logger.Error($"Unable to write \"{resource.Key}\" expedition mission information, it was null");
                continue;
            }
            if (resource.Key.ToString()!.StartsWith("ExpeditionMissionInfo_"))
            {
                if (resource.Value!.GetType() == typeof(byte[]))
                    File.WriteAllBytes($"{ExpeditionMissionInfoDirectoryPath}\\{resource.Key.ToString()!.Substring("ExpeditionMissionInfo_".Length)}.json", (byte[])resource.Value);
                else
                    Logger.Error($"Unable to write \"{resource.Key}\" expedition mission information, it was \"{resource.Value.GetType()}\"");
            }
        }

        Logger.Info("Finished Writing Default Expedition Mission Information");
    }
}
