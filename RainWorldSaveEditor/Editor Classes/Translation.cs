using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveEditor.Editor_Classes
{
    public static class Translation
    {
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


        public static string GetRegionName(string internalname)
        {
            if (RegionNames.ContainsKey(internalname))
                return RegionNames[internalname];

            return internalname;
        }
    }
}
