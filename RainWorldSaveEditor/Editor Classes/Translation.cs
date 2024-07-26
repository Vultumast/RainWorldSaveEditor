using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RainWorldSaveEditor.Editor_Classes
{
    public static class Translation
    {
        public const string SavePath = "Resources\\modded_region_names.json";

        public static Dictionary<string, string> RegionNames = new()
        {
            // Base game
            { "SU", "Outskirts" },
            { "HI", "Industrial Complex" },
            { "SH", "Shaded Citadel" },
            { "DS", "Drainage System" },
            { "CC", "Chimney Canopy" },
            { "GW", "Garbage Wastes" },
            { "LF", "Farm Arrays" },
            { "SB", "Subterranean" },
            { "SL", "Shoreline" },
            { "SS", "Five Pebbles" },
            { "UW", "The Exterior" },
            { "SI", "Sky Islands" },

            // Downpour (shared)
            { "MS", "Submerged Superstructure" },
            { "OE", "Outer Expanse" },
            { "VS", "Pipeyard" },

            // Downpour (Artificer/Spearmaster)
            { "LC", "Metropolis" },
            { "LM", "Waterfront Facility" },
            { "DM", "Looks To The Moon" },

            // Downpour (Rivulet)
            { "RM", "The Rot" },

            // Downpour (Saint)
            { "UG", "Undergrowth" },
            { "CL", "Silent Construct" },
            { "HR", "Rubicon" }

            // Vultu: This is where I would put The Watcher region names.... if i had any... /ref
        };

        public static Dictionary<string, string> ModRegionNames = new()
        {
            // Hunter Expansion
            { "NSH", "No Significant Harassment" }
        };

        public static string GetRegionName(string internalname)
        {
            if (internalname == "EVERY" || internalname == "ALL")
                return internalname;

            if (RegionNames.ContainsKey(internalname))
                return RegionNames[internalname];

            if (ModRegionNames.ContainsKey(internalname))
                return ModRegionNames[internalname];

            return $"Unknown Region: \"{internalname}\"";
        }

        public static void Read()
        {
            Logger.ReadAttempt(SavePath);
            if (!File.Exists(SavePath))
            {
                Logger.Info($"Unable to find \"{SavePath}\" so a new one is being made.");
                Write();
                return;
            }
            try
            {
                ModRegionNames = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(SavePath))!;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return;
            }


            if (ModRegionNames is null)
            {
                Logger.Warn($"ModRegionNames was null, this may be from an error in deserializing \"{SavePath}\"");
                ModRegionNames = new();
            }
            Logger.Success();
        }
        public static void Write()
        {
            Logger.WriteAttempt(SavePath);
            try
            {
                File.WriteAllText(SavePath, System.Text.Json.JsonSerializer.Serialize(ModRegionNames, new System.Text.Json.JsonSerializerOptions() { WriteIndented = true }));
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return;
            }
            Logger.Success();
        }
    }
}
