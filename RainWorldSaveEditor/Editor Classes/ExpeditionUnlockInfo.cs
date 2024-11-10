namespace RainWorldSaveEditor;

public class ExpeditionUnlockInfo(string Id, string Name, string Description)
{
    public const string ExpeditionUnlockInfoDirectoryPath = "Resources\\Expedition Unlocks\\Info";
    public const string ExpeditionUnlockIconDirectoryPath = "Resources\\Expedition Unlocks\\Icons";

    public static Dictionary<string, ExpeditionUnlockInfo> Unlocks { get; private set; } = [];

    public string Id { get; set; } = Id;
    public string Name { get; set; } = Name;
    public string Description { get; set; } = Description;

    public static ExpeditionUnlockInfo[] Read(string filepath) => System.Text.Json.JsonSerializer.Deserialize<ExpeditionUnlockInfo[]>(File.ReadAllText(filepath))!;

    public static void ReadExpeditionUnlockInfo()
    {
        Logger.Info("Reading Expedition Unlock Info...");
        if (!Directory.Exists("Resources") || !Directory.Exists(ExpeditionUnlockIconDirectoryPath))
        {
            Logger.Warn("Expedition Unlock Info did not exist, so it will be remade");
            WriteDefaultExpeditionUnlockInfo();
        }

        List<ExpeditionUnlockInfo> list = [];

        var files = Directory.GetFiles(ExpeditionUnlockInfoDirectoryPath, "*.json", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            try
            {
                list.AddRange(Read(file));
            }
            catch (Exception ex)
            {
                Logger.DeserializationError(file, nameof(CommunityInfo), ex);
            }
        }

        foreach (var info in list)
            Unlocks[info.Id] = info;

        Logger.Info("Finished Reading Expedition Unlock Info");
    }

    public static void WriteDefaultExpeditionUnlockInfo()
    {
        if (!Directory.Exists("Resources"))
            Directory.CreateDirectory("Resources");

        if (!Directory.Exists(ExpeditionUnlockInfoDirectoryPath))
            Directory.CreateDirectory(ExpeditionUnlockInfoDirectoryPath);
        if (!Directory.Exists(ExpeditionUnlockIconDirectoryPath))
            Directory.CreateDirectory(ExpeditionUnlockIconDirectoryPath);

        Logger.Info("Writing Default Expedition Unlock Information...");
        foreach (var resource in Utils.ResourceList())
        {
            if (resource.Value is null)
            {
                Logger.Error($"Unable to write \"{resource.Key}\" expedition unlock information, it was null");
                continue;
            }
            if (resource.Key.ToString()!.EndsWith("_expedition_unlock_icon"))
            {
                if (resource.Value!.GetType() == typeof(Bitmap))
                    ((Bitmap)resource.Value).Save($"{ExpeditionUnlockIconDirectoryPath}\\{resource.Key.ToString()}.png", System.Drawing.Imaging.ImageFormat.Png);
                else
                    Logger.Error($"Unable to write \"{resource.Key}\" expedition unlock information, it was \"{resource.Value.GetType()}\"");
            }
            else if (resource.Key.ToString()!.StartsWith("ExpeditionUnlockInfo_"))
            {
                if (resource.Value!.GetType() == typeof(byte[]))
                    File.WriteAllBytes($"{ExpeditionUnlockInfoDirectoryPath}\\{resource.Key.ToString()!.Substring("ExpeditionUnlockInfo_".Length)}.json", (byte[])resource.Value);
                else
                    Logger.Error($"Unable to write \"{resource.Key}\" expedition unlock information, it was \"{resource.Value.GetType()}\"");
            }
        }

        Logger.Info("Finished Writing Default Expedition Unlock Information");
    }
}
