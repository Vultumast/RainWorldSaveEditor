using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveEditor
{
    public class Settings
    {
        public const string Filepath = "settings.json";
        public Settings()
        {

        }

        /// <summary>
        /// Reset Settings to default parameters
        /// </summary>
        public void Reset()
        {
            ShowDisclaimer = false;
            RainWorldSaveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "LocalLow", "VideoCult", "Rain World");
        }

        /// <summary>
        /// Show the disclaimer when the tool begins?
        /// </summary>
        public bool ShowDisclaimer { get; set; } = true;

        /// <summary>
        /// The location of your Rain World save directory
        /// </summary>
        public string RainWorldSaveDirectory { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "LocalLow", "VideoCult", "Rain World");

        public void Save()
        {
            Logger.Info("Saving settings");
            Write(this);
        }

        public static void Write(Settings settings) => File.WriteAllText(Filepath, System.Text.Json.JsonSerializer.Serialize(settings, Utils.JSONSerializerOptions));

        public static Settings Read() => System.Text.Json.JsonSerializer.Deserialize<Settings>(File.ReadAllText(Filepath))!;
    }
}
