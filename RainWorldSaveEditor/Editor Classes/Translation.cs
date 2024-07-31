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

        public static Dictionary<string, string> RegionNames { get; private set; } = new()
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

        // List taken from https://rainworldmods.miraheze.org/wiki/Regions
        public static Dictionary<string, string> ModRegionNames { get; private set; } = new()
        {
            { "ZZ", "Aerial Arrays" },
            { "KF", "Archaic Facility" },
            { "AB", "Arid Barrens" },
            { "OA", "Aqueducts" },
            { "BV", "Background Valley" },
            { "BL", "Badlands" },
            { "BI", "Bioengineering Center" },
            { "UU", "Citadel" },
            { "HF", "Community Gallery Region" },
            { "RF", "Coral Caves" },
            { "PQ", "Corroded Passage" },
            { "AK", "Curious Ascent" },
            { "DKM", "Dark Meadows" },
            { "KR", "Deep Pipeline" },
            { "DW", "Desert Wastelands" },
            { "NS", "Forbidden Tropics" },
            { "GS", "Gilded Sanctuary/Hanging Gardens" },
            { "WY", "Grand Reservoir" },
            { "MT", "The Grinder" },
            { "HH", "Hallowed Grotto" },
            { "HC", "Howling Rift" },
            { "XD", "Lost Cranny" },
            { "LW", "Lush Mire" },
            { "ML", "Marshland Wastes" },
            { "TM", "The Mast" },
            { "NM", "Midnight Meadows" },
            { "HW", "Moonlit Acres" },
            { "NF", "Neuron Forest" },
            { "VQ", "Outer Outskirts" },
            { "TO", "The Outline" },
            { "W2", "Overgrown Facility" },
            { "BY", "Purification Conduits" },
            { "RA", "The Radiosphere" },
            { "YL", "Rainforest" },
            { "SC", "Sacred Garden" },
            { "QQ", "Scraggy Town" },
            { "RW", "Side House" },
            { "UF", "Sizzling Sewers" },
            { "KY", "Sky Tower" },
            { "SP", "Slag Pits/Sunlit Power Plant" },
            { "SK", "Stormy Coast/Slug King" },
            { "TZ", "Testing Simulation" },
            { "QW", "Timeless Conservatory" },
            { "TT", "The Tower" },
            { "UC", "Underground City" },
            { "US", "Undersea" },
            { "RV", "Urban Reservoir" },
            { "CR", "Ventiliation Ducts" },
            { "WM", "Washroom" },

            // Old New Horizons Regions
            { "FN", "Farlands" },
            { "CA", "Railway" },
            { "CF", "Contral Factory" },
            { "VI", "Suburbs" },
            { "MA", "Swamplands" },
            { "OS", "Oil Station" },
            { "ME", "Subway Lines" },
            { "AY", "Abyss" },
            { "SW", "Swamplands (Purple)" },
            { "DI", "Industrial District" },


            // Sunlit Trail Regions
            { "SD", "Scorched District" },
            { "PA", "Pilgrims' Ascent" },
            { "FR", "Far Shore" },
            { "MF", "Moss Fields" },
            { "CW", "Chasing Wind" },
            { "TK", "Smokestack Treetop" },
            { "AQ", "Aquifer Tunnels" },
            { "WD", "Nascent Woods" },

            // Reclaming Entropy Regions
            { "SO", "Sunlit Alleyways" },
            { "WT", "Withstanding Factory" },
            { "PY", "Pipegrave" },

            // Hunter Expansion Regions
            { "NSH", "No Significant Harassment" },

            // Wirecat Regions
            { "WTA", "The Ascent" },
            { "WBRB", "Boundless Resonant Basis" }

        };

        public static string GetRegionName(string internalname)
        {
            if (internalname == "EVERY" || internalname == "ALL")
                return internalname;

            string value;

            if (RegionNames.TryGetValue(internalname, out value!))
                return value;

            if (ModRegionNames.TryGetValue(internalname, out value!))
                return value;

            return $"Unknown Region: \"{internalname}\"";
        }

        public static void Read()
        {
            Logger.Info($"Reading translation information...");
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
                Logger.Info($"Found {ModRegionNames.Count} Modded Region Names");
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return;
            }


            if (ModRegionNames is null)
            {
                Logger.Warn($"ModRegionNames was null, this may be from an error in deserializing \"{SavePath}\"");
                ModRegionNames = [];
            }
            Logger.Info($"Finished reading translation information");
        }
        public static void Write()
        {
            Logger.WriteAttempt(SavePath);
            try
            {
                File.WriteAllText(SavePath, System.Text.Json.JsonSerializer.Serialize(ModRegionNames, Utils.JSONSerializerOptions));
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
