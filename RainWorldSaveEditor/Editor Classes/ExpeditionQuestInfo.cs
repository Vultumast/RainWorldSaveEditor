namespace RainWorldSaveEditor;

public class ExpeditionQuestInfo(string Key)
{
    public const string ExpeditionQuestInfoDirectoryPath = "Resources\\Expedition Quests\\Info";

    public static Dictionary<string, ExpeditionQuestInfo> Quests { get; private set; } = [];

    public string Key { get; set; } = Key;

    public static ExpeditionQuestInfo[] Read(string filepath) => System.Text.Json.JsonSerializer.Deserialize<ExpeditionQuestInfo[]>(File.ReadAllText(filepath))!;

    public static void ReadExpeditionQuestInfo()
    {
        Logger.Info("Reading Expedition Quest Info...");
        if (!Directory.Exists("Resources") || !Directory.Exists(ExpeditionQuestInfoDirectoryPath))
        {
            Logger.Warn("Expedition Quest Info did not exist, so it will be remade");
            WriteDefaultExpeditionQuestInfo();
        }

        List<ExpeditionQuestInfo> list = [];

        var files = Directory.GetFiles(ExpeditionQuestInfoDirectoryPath, "*.json", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            try
            {
                list.AddRange(Read(file));
            }
            catch (Exception ex)
            {
                Logger.DeserializationError(file, nameof(ExpeditionQuestInfo), ex);
            }
        }

        foreach (var info in list)
            Quests[info.Key] = info;

        Logger.Info("Finished Reading Expedition Quest Info");
    }

    public static void WriteDefaultExpeditionQuestInfo()
    {
        if (!Directory.Exists("Resources"))
            Directory.CreateDirectory("Resources");

        if (!Directory.Exists(ExpeditionQuestInfoDirectoryPath))
            Directory.CreateDirectory(ExpeditionQuestInfoDirectoryPath);

        Logger.Info("Writing Default Expedition Quest Information...");
        foreach (var resource in Utils.ResourceList())
        {
            if (resource.Value is null)
            {
                Logger.Error($"Unable to write \"{resource.Key}\" expedition quest information, it was null");
                continue;
            }
            if (resource.Key.ToString()!.StartsWith("ExpeditionQuestInfo_"))
            {
                if (resource.Value!.GetType() == typeof(byte[]))
                    File.WriteAllBytes($"{ExpeditionQuestInfoDirectoryPath}\\{resource.Key.ToString()!.Substring("ExpeditionQuestInfo_".Length)}.json", (byte[])resource.Value);
                else
                    Logger.Error($"Unable to write \"{resource.Key}\" expedition quest information, it was \"{resource.Value.GetType()}\"");
            }
        }

        Logger.Info("Finished Writing Default Expedition Quest Information");
    }
}
