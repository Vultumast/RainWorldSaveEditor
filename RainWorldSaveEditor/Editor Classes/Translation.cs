namespace RainWorldSaveEditor.Editor_Classes
{
    public static class Translation
    {
        public const string TranslationPath = "Resources\\Translation";
        public const string ModdedRegionNamesPath = "Resources\\Translation\\modded_region_names.json";
        public const string PearlNamesPath = "Resources\\Translation\\pearl.json";
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

        public static Dictionary<string, string> PearlNames { get; private set; } = new()
        {
            { "Misc"                 , "Generic White Pearl"                       },
            { "Misc2"                , "Generic Pink Pearl"                        },
            { "SL_moon"              , "Shoreline Looks To The Moon Pearl Icon"    },
            { "SL_bridge"            , "Shoreline Bride Pearl Icon"                },
            { "SL_chimney"           , "Shoreline Chimney Pearl Icon"              },
            { "SI_west"              , "Sky Islands West Pearl Icon"               },
            { "SI_top"               , "Sky Islands Top Pearl Icon"                },
            { "SI_chat3"             , "Sky Islands \"Chat 3\" Pearl Icon"         },
            { "SI_chat4"             , "Sky Islands \"Chat 4\" Pearl Icon"         },
            { "SI_chat5"             , "Sky Islands \"Chat 5\" Pearl Icon"         },
            { "SB_ravine"            , "Subterranean Ravine Pearl Icon"            },
            { "SU"                   , "Outskirts Pearl Icon"                      },
            { "HI"                   , "Industrial Complex Pearl Icon"             },
            { "GW"                   , "Garbage Wastes Pearl Icon"                 },
            { "MS"                   , "Submerged Superstructure Pearl Icon"       },
            { "DS"                   , "Drainage System Pearl Icon"                },
            { "SH"                   , "Shaded Citadel Pearl Icon"                 },
            { "CC"                   , "Chimney Canopy Pearl Icon"                 },
            { "VS"                   , "Pipeyard Pearl Icon"                       },
            { "UW"                   , "The Exterior Pearl Icon"                   },
            { "LF_west"              , "Farm Arrays West Pearl Icon"               },
            { "LF_bottom"            , "Farm Arrays Botton Pearl Icon"             },
            { "SB_filtration"        , "Subterranean-Filtration System Pearl Icon" },
            { "SU_filt"              , "Outskirts-Filtration System Pearl Icon"    },
            { "OE"                   , "Outer Expanse Pearl Icon"                  },
            { "LC"                   , "Metropolis Pearl Icon"                     },
            { "LC_second"            , "Metropolis Second Pearl Icon"              },
            { "RM"                   , "The Rot Pearl Icon"                        },
            { "Red_stomach"          , "Hunter's Stomach Pearl Icon"               },
            { "DM"                   , "Looks To The Moon (Region) Pearl Icon"     },
            { "Spearmasterpearl"     , "Spearmaster's Pearl Icon"                  },
            { "Rivulet_stomach"      , "Rivulet's Stomach Pearl Icon"              }
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

        private static void CreateDirectoriesIfNotPresent()
        {
            Utils.CreateDirectoryIfNotExist("Resources");
            Utils.CreateDirectoryIfNotExist(TranslationPath);
        }

        private static Dictionary<string, string> ReadDictionary(string path, string name)
        {
            Dictionary<string, string> retValue = [];

            if (!File.Exists(path))
            {
                Logger.Info($"Unable to find \"{path}\" so a new one is being made.");
                Write();
                return null!;
            }
            try
            {
                retValue = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(path))!;
                Logger.Info($"Found {retValue.Count} {name}");
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return null!;
            }

            return retValue;
        }

        private static void WriteDictionary(string path, Dictionary<string, string> dictionary)
        {
            Logger.WriteAttempt(path);
            try
            {
                File.WriteAllText(path, System.Text.Json.JsonSerializer.Serialize(dictionary, Utils.JSONSerializerOptions));
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return;
            }
        }

        public static void Read()
        {
            Logger.Info($"Reading translation information...");

            CreateDirectoriesIfNotPresent();

            ModRegionNames = ReadDictionary(ModdedRegionNamesPath, "Modded Region Names");
            PearlNames = ReadDictionary(PearlNamesPath, "Pearl Names");

            if (ModRegionNames is null)
            {
                Logger.Warn($"ModRegionNames was null, this may be from an error in deserializing \"{ModdedRegionNamesPath}\"");
                ModRegionNames = [];
            }
            if (PearlNames is null)
            {
                Logger.Warn($"PearlNames was null, this may be from an error in deserializing \"{PearlNamesPath}\"");
                PearlNames = [];
            }

            Logger.Info($"Finished reading translation information");
        }
        public static void Write()
        {
            CreateDirectoriesIfNotPresent();

            WriteDictionary(ModdedRegionNamesPath, ModRegionNames);
            WriteDictionary(PearlNamesPath, PearlNames);

            Logger.Success();
        }
    }
}
