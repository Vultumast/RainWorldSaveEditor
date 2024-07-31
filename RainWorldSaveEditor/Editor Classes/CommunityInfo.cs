using RainWorldSaveAPI.SaveElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveEditor;

public class CommunityInfo(string communityID, string name, string iconPath)
{
    public const string CreatureCommunityInfoDirectoryPath = "Resources\\Creature Community\\Info";
    public const string CreatureCommunityIconsDirectoryPath = "Resources\\Creature Community\\Icons";
    public string CommunityID { get; private set; } = communityID;
    public string Name { get; private set; } = name;
    public string IconPath { get; private set; } = iconPath;



    public static void Write(string name, CommunityInfo info) => File.WriteAllText($"Resources\\Creature Community\\{name}.json", System.Text.Json.JsonSerializer.Serialize(info, Utils.JSONSerializerOptions));

    public static CommunityInfo Read(string filepath) => System.Text.Json.JsonSerializer.Deserialize<CommunityInfo>(File.ReadAllText(filepath))!;

    public static Dictionary<string, CommunityInfo> Communities { get; private set; } = [];

    public static void ReadCommunities()
    {
        Logger.Info("Reading Creature Community Info...");
        if (!Directory.Exists("Resources") || !Directory.Exists(CreatureCommunityInfoDirectoryPath))
        {
            Logger.Warn("Community Info did not exist, so it will be remade");
            WriteDefaultCommunities();
        }

        List<CommunityInfo> list = [];

        var files = Directory.GetFiles(CreatureCommunityInfoDirectoryPath, "*.json", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            try
            {
                list.Add(Read(file));
            }
            catch (Exception ex)
            { 
                Logger.DeserializationError(file, nameof(CommunityInfo), ex);
            }
        }

        foreach (var info in list)
            Communities.Add(info.Name, info);

        Logger.Info("Finished Reading Creature Community Info");
    }

    public static void WriteDefaultCommunities()
    {
        if (!Directory.Exists("Resources"))
            Directory.CreateDirectory("Resources");

        if (!Directory.Exists(CreatureCommunityInfoDirectoryPath))
            Directory.CreateDirectory(CreatureCommunityInfoDirectoryPath);
        if (!Directory.Exists(CreatureCommunityIconsDirectoryPath))
            Directory.CreateDirectory(CreatureCommunityIconsDirectoryPath);

        Logger.Info("Writing Default Creature Community Information...");
        foreach (var resource in Utils.ResourceList())
        {
            if (resource.Value is null)
            {
                Logger.Error($"Unable to write \"{resource.Key}\" community information, it was null");
                continue;
            }
            if (resource.Key.ToString()!.EndsWith("_community_icon"))
            {
                if (resource.Value!.GetType() == typeof(Bitmap))
                    ((Bitmap)resource.Value).Save($"{CreatureCommunityIconsDirectoryPath}\\{resource.Key.ToString()}.png", System.Drawing.Imaging.ImageFormat.Png);
                else
                    Logger.Error($"Unable to write \"{resource.Key}\" community information, it was \"{resource.Value.GetType()}\"");
            }
            else if (resource.Key.ToString()!.StartsWith("CommunityInfo_"))
            {
                if (resource.Value!.GetType() == typeof(byte[]))
                    File.WriteAllBytes($"{CreatureCommunityInfoDirectoryPath}\\{resource.Key.ToString()!.Substring("CommunityInfo_".Length)}.json", (byte[])resource.Value);
                else
                    Logger.Error($"Unable to write \"{resource.Key}\" community information, it was \"{resource.Value.GetType()}\"");
            }
            
        }
        Logger.Info("Finished Writing Default Creature Community Information");
    }

}
