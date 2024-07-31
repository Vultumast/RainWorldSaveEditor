using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RainWorldSaveEditor
{
    public static class EditorCommon
    {

        public static SlugcatInfo[] SlugcatInfo { get; private set; } = Array.Empty<SlugcatInfo>();

        public static void ReadSlugcatInfo()
        {
            if (!Directory.Exists("Resources"))
                Directory.CreateDirectory("Resources");

            if (!Directory.Exists("Resources\\Slugcat\\Info"))
            {
                Logger.Info("Unable to find Slugcat info, so new info will be created.");
                Directory.CreateDirectory("Resources\\Slugcat_Info");
                RewriteSlugcatInfo();
            }

            List<SlugcatInfo> list = [];

            var slugcatFiles = Directory.GetFiles("Resources\\Slugcat\\Info", "*.json", SearchOption.AllDirectories);
            foreach (var slugcatFile in slugcatFiles)
                list.Add(JsonSerializer.Deserialize<SlugcatInfo>(File.ReadAllText(slugcatFile)));

            SlugcatInfo = list.ToArray();
        }

        public static void RewriteSlugcatInfo()
        {

        }
    }
}
