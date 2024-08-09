using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RainWorldSaveEditor
{
    public class SlugcatInfo(string name, string saveID, bool requiresDLC, bool modded, byte pipCount, byte pipBarIndex)
    {
        public const string SlugcatInfoDirectoryPath = "Resources\\Slugcat\\Info";
        public const string SlugcatIconsDirectoryPath = "Resources\\Slugcat\\Icons";

        public string Name { get; set; } = name;
        public string SaveID { get; set; } = saveID;
        public bool RequiresDLC { get; set; } = requiresDLC;
        public bool Modded { get; set; } = modded;
        public byte PipCount { get; set; } = pipCount;
        public byte PipBarIndex { get; set; } = pipBarIndex;



        public static SlugcatInfo[] SlugcatInfos { get; private set; } = Array.Empty<SlugcatInfo>();

        public static void ReadSlugcatInfo()
        {
            Logger.Info("Reading Slugcat Info...");

            if (!Directory.Exists("Resources"))
                Directory.CreateDirectory("Resources");

            if (!Directory.Exists("Resources\\Slugcat\\Info"))
            {
                Logger.Info("Unable to find Slugcat info, so new info will be created.");
                Directory.CreateDirectory("Resources\\Slugcat_Info");
                WriteDefaultSlugcatInfo();
            }

            List<SlugcatInfo> list = [];

            var slugcatFiles = Directory.GetFiles("Resources\\Slugcat\\Info", "*.json", SearchOption.AllDirectories);
            foreach (var slugcatFile in slugcatFiles)
            {
                var data = JsonSerializer.Deserialize<SlugcatInfo>(File.ReadAllText(slugcatFile));

                if (data == null)
                {
                    Logger.Warn($"Deserialization of {slugcatFile} returned null, skipping...");
                    continue;
                }

                list.Add(data);
            }

            SlugcatInfos = list.ToArray();

            Logger.Info("Finished Reading Slugcat Info");
        }

        public static void WriteDefaultSlugcatInfo()
        {
            if (!Directory.Exists("Resources"))
                Directory.CreateDirectory("Resources");

            if (!Directory.Exists(SlugcatInfoDirectoryPath))
                Directory.CreateDirectory(SlugcatInfoDirectoryPath);
            if (!Directory.Exists(SlugcatIconsDirectoryPath))
                Directory.CreateDirectory(SlugcatIconsDirectoryPath);

            Logger.Info("Writing Default Slugcat Information...");
            foreach (var resource in Utils.ResourceList())
            {
                if (resource.Value is null)
                {
                    Logger.Error($"Unable to write \"{resource.Key}\" community information, it was null");
                    continue;
                }
                if (resource.Key.ToString()!.EndsWith("_slugcat_icon"))
                {
                    if (resource.Value!.GetType() == typeof(Bitmap))
                        ((Bitmap)resource.Value).Save($"{SlugcatIconsDirectoryPath}\\{resource.Key.ToString()}.png", System.Drawing.Imaging.ImageFormat.Png);
                    else
                        Logger.Error($"Unable to write \"{resource.Key}\" slugcat information, it was \"{resource.Value.GetType()}\"");
                }
                else if (resource.Key.ToString()!.StartsWith("SlugcatInfo_"))
                {
                    if (resource.Value!.GetType() == typeof(byte[]))
                        File.WriteAllBytes($"{SlugcatInfoDirectoryPath}\\{resource.Key.ToString()!.Substring("SlugcatInfo_".Length)}.json", (byte[])resource.Value);
                    else
                        Logger.Error($"Unable to write \"{resource.Key}\" slugcat information, it was \"{resource.Value.GetType()}\"");
                }

            }
            Logger.Info("Finished Writing Default Slugcat Information");
        }
    }
}
