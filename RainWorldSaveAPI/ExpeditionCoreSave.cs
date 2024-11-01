using RainWorldSaveAPI.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RainWorldSaveAPI;

public struct ChallengeType
{
    public string Type { get; set; }
    public int Count { get; set; }
}

public struct MissionBestTime
{
    public string Mission { get; set; }
    public int Time { get; set; }
}

public struct ChallengeEntry
{
    public string Slugcat { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
}

public struct UnlockEntry
{
    public string Slugcat { get; set; }
    public string Data { get; set; }
}

public struct PassageEntry
{
    public string Slugcat { get; set; }
    public int Count { get; set; }
}

public struct ActiveMissionEntry
{
    public string Slugcat { get; set; }
    public string Data { get; set; }
}

public struct RequiredModsEntry
{
    public string Slugcat { get; set; }
    public List<string> RequiredMods { get; set; }
}

public struct WinEntry
{
    public string Slugcat { get; set; }
    public int Wins { get; set; }
}

public enum RainbowAuraState
{
    SpotNotReached = 0,
    SpotReached = 1,
    /// <summary> This value only makes sense if every other slugcat's spot has been found. </summary>
    AuraActive = 2
}

public class ExpeditionCoreSave
{
    public int SaveSlot { get; set; } = 0;
    public int Level { get; set; } = 0;
    public int PerkLimit { get; set; } = 0;
    public int Points { get; set; } = 0;
    public int TotalPoints { get; set; } = 0;
    public int TotalChallenges { get; set; } = 0;
    public int TotalHiddenChallenges { get; set; } = 0;
    public int Wins { get; set; } = 0;
    public string Slugcat { get; set; } = "White";
    public bool HasViewedManual { get; set; } = false;
    public string MenuSong { get; set; } = "";
    public List<ChallengeType> ChallengeTypes { get; set; } = [];
    public List<string> Unlockables { get; set; } = [];
    public List<string> NewSongs { get; set; } = [];
    public List<string> Quests { get; set; } = [];
    public List<string> Missions { get; set; } = [];
    public List<int> Integers { get; set; } = [];
    public List<MissionBestTime> MissionBestTimes { get; set; } = [];

    public static RainbowAuraState SanitizeRainbowState(int rainbowState)
    {
        return rainbowState switch
        {
            <= 0 => RainbowAuraState.SpotNotReached,
            1 => RainbowAuraState.SpotReached,
            >= 2 => RainbowAuraState.AuraActive
        };
    }

    public static int SanitizeRainbowState(RainbowAuraState rainbowState)
    {
        return rainbowState switch
        {
            <= RainbowAuraState.SpotNotReached => 0,
            RainbowAuraState.SpotReached => 1,
            >= RainbowAuraState.AuraActive => 2
        };
    }

    public static int GetSlugcatIndex(string slugcat)
    {
        return slugcat switch
        {
            "White" => 0,
            "Yellow" => 1,
            "Red" => 2,
            "Artificer" => 3,
            "Gourmand" => 4,
            "Spear" => 5,
            "Rivulet" => 6,
            "Saint" => 7,
            _ => -1
        };
    }

    public RainbowAuraState GetRainbowAuraState(string slugcat)
    {
        int index = GetSlugcatIndex(slugcat);

        return index != -1 ? SanitizeRainbowState(Integers[index]) : RainbowAuraState.SpotNotReached;
    }

    public void SetRainbowAuraState(string slugcat, RainbowAuraState state)
    {
        int index = GetSlugcatIndex(slugcat);

        if (index != -1)
            Integers[index] = SanitizeRainbowState(state);
    }

    // Slugcat specific?
    public List<ChallengeEntry> ChallengeEntries { get; set; } = [];
    public List<UnlockEntry> Unlocks { get; set; } = [];
    public List<PassageEntry> Passages { get; set; } = [];
    public List<ActiveMissionEntry> ActiveMissionEntries { get; set; } = [];
    public List<RequiredModsEntry> RequiredModsEntries { get; set; } = [];
    public List<WinEntry> WinEntries { get; set; } = [];

    public static void AddOrUpdate<T>(List<T> list, T item, Predicate<T> predicate)
    {
        int index = list.FindIndex(predicate);

        if (index == -1)
        {
            list.Add(item);
        }
        else
        {
            list[index] = item;
        }
    }

    public void Read(string saveString)
    {
        string[] parts = saveString.Split("<expC>");

        string? section = null;

        foreach (var part in parts)
        {
            if (section == null && part.StartsWith('[') && part.EndsWith(']'))
            {
                section = part[1..^1];

                section = section switch
                {
                    "PASSAGES" => section,
                    "UNLOCKS" => section,
                    "CHALLENGES" => section,
                    "MISSION" => section,
                    "TIMES" => section,
                    "CONTENT" => section,
                    _ => null,
                };

                continue;
            }

            if (section != null && part.StartsWith("[END ") && part.EndsWith(']'))
            {
                section = part[5..^1];

                section = section switch
                {
                    "PASSAGES" => null,
                    "UNLOCKS" => null,
                    "CHALLENGES" => null,
                    "MISSION" => null,
                    "TIMES" => null,
                    "CONTENT" => null,
                    _ => section,
                };

                continue;
            }

            if (section != null)
            {
                if (section == "PASSAGES")
                {
                    string[] args = part.Split('#');
                    string slugcatName = args[0];
                    int value = int.Parse(args[1]);

                    var entry = new PassageEntry
                    {
                        Slugcat = slugcatName,
                        Count = value
                    };
                    AddOrUpdate(Passages, entry, x => x.Slugcat == slugcatName);
                }
                else if (section == "UNLOCKS")
                {
                    string[] args = part.Split('#');
                    string slugcatName = args[0];
                    string[] args2 = args[1].Split("><");

                    foreach (var arg in args2)
                    {
                        var entry = new UnlockEntry
                        {
                            Slugcat = slugcatName,
                            Data = arg
                        };
                        Unlocks.Add(entry);
                    }
                }
                else if (section == "CHALLENGES")
                {
                    string[] args = part.Split('#');
                    string slugcatName = args[0];
                    string[] args2 = args[1].Split('~');
                    string type = args2[0];
                    string data = args2[1];

                    var entry = new ChallengeEntry
                    {
                        Slugcat = slugcatName,
                        Type = type,
                        Data = data
                    };
                    ChallengeEntries.Add(entry);
                }
                else if (section == "MISSION")
                {
                    string[] args = part.Split('#');
                    string slugcatName = args[0];
                    string activeMissions = args[1];

                    if (!ActiveMissionEntries.Any(x => x.Slugcat == slugcatName))
                    {
                        var entry = new ActiveMissionEntry
                        {
                            Slugcat = slugcatName,
                            Data = activeMissions
                        };
                        ActiveMissionEntries.Add(entry);
                    }
                }
                else if (section == "TIMES")
                {
                    string[] args = part.Split('#');
                    string mission = args[0];
                    int time = int.Parse(args[1]);
                    var entry = new MissionBestTime()
                    {
                        Mission = mission,
                        Time = time
                    };
                    AddOrUpdate(MissionBestTimes, entry, x => x.Mission == mission);
                }
                else if (section == "CONTENT")
                {
                    string[] args = part.Split('#');
                    string slugcatName = args[0];
                    string[] mods = args[1].Split("<mod>");

                    var entry = new RequiredModsEntry
                    {
                        Slugcat = slugcatName,
                        RequiredMods = [.. mods]
                    };
                    AddOrUpdate(RequiredModsEntries, entry, x => x.Slugcat == slugcatName);
                }

                continue;
            }

            if (part.StartsWith("SLOT:"))
            {
                SaveSlot = int.Parse(part["SLOT:".Length..]);
            }
            else if (part.StartsWith("LEVEL:"))
            {
                Level = int.Parse(part["LEVEL:".Length..]);
            }
            else if (part.StartsWith("PERKLIMIT:"))
            {
                PerkLimit = int.Parse(part["PERKLIMIT:".Length..]);
            }
            else if (part.StartsWith("POINTS:"))
            {
                Points = int.Parse(part["POINTS:".Length..]);
            }
            else if (part.StartsWith("TOTALPOINTS:"))
            {
                TotalPoints = int.Parse(part["TOTALPOINTS:".Length..]);
            }
            else if (part.StartsWith("TOTALCHALLENGES:"))
            {
                TotalChallenges = int.Parse(part["TOTALCHALLENGES:".Length..]);
            }
            else if (part.StartsWith("TOTALHIDDENCHALLENGES:"))
            {
                TotalHiddenChallenges = int.Parse(part["TOTALHIDDENCHALLENGES:".Length..]);
            }
            else if (part.StartsWith("WINS:"))
            {
                Wins = int.Parse(part["WINS:".Length..]);
            }
            else if (part.StartsWith("SLUG:"))
            {
                Slugcat = part["SLUG:".Length..];
            }
            else if (part.StartsWith("MANUAL:"))
            {
                HasViewedManual = part["MANUAL:".Length..] == "1";
            }
            else if (part.StartsWith("MENUSONG:"))
            {
                MenuSong = part["MENUSONG:".Length..];
            }
            else if (part.StartsWith("CHALLENGETYPES:"))
            {
                string[] challenges = part["CHALLENGETYPES:".Length..].Split("<>", StringSplitOptions.RemoveEmptyEntries);

                foreach (var challenge in challenges)
                {
                    string[] args = challenge.Split('#');
                    ChallengeTypes.Add(new()
                    {
                        Type = args[0],
                        Count = int.Parse(args[1])
                    });
                }
            }
            else if (part.StartsWith("SLUGWINS:"))
            {
                string[] slugwins = part["SLUGWINS:".Length..].Split("<>", StringSplitOptions.RemoveEmptyEntries);

                foreach (var slugwin in slugwins)
                {
                    string[] args = slugwin.Split('#');
                    var slugcatName = args[0];
                    var wins = int.Parse(args[1]);

                    var entry = new WinEntry
                    {
                        Slugcat = slugcatName,
                        Wins = wins
                    };
                    AddOrUpdate(WinEntries, entry, x => x.Slugcat == slugcatName);
                }
            }
            else if (part.StartsWith("UNLOCKS:"))
            {
                string[] unlocks = part["UNLOCKS:".Length..].Split("<>", StringSplitOptions.RemoveEmptyEntries);

                foreach (var unlock in unlocks)
                {
                    Unlockables.Add(unlock);
                }
            }
            else if (part.StartsWith("NEWSONGS:"))
            {
                string[] newsongs = part["NEWSONGS:".Length..].Split("<>", StringSplitOptions.RemoveEmptyEntries);

                foreach (var newsong in newsongs)
                {
                    NewSongs.Add(newsong);
                }
            }
            else if (part.StartsWith("QUESTS:"))
            {
                string[] quests = part["QUESTS:".Length..].Split("<>", StringSplitOptions.RemoveEmptyEntries);

                foreach (var quest in quests)
                {
                    Quests.Add(quest);
                }
            }
            else if (part.StartsWith("MISSIONS:"))
            {
                string[] missions = part["MISSIONS:".Length..].Split("<>", StringSplitOptions.RemoveEmptyEntries);

                foreach (var mission in missions)
                {
                    Missions.Add(mission);
                }
            }
            else if (part.StartsWith("INTS:"))
            {
                Integers.AddRange(part["INTS:".Length..].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            }
            else
            {
                Logger.Warn($"Couldn\'t recognize field {part}");
            }
        }
    }

    public string Write()
    {
        var list = new List<string>
        {
            $"SLOT:{SaveSlot}",
            $"LEVEL:{Level}",
            $"POINTS:{Points}",
            $"SLUG:{Slugcat}",
            $"PERKLIMIT:{PerkLimit}",
            $"TOTALPOINTS:{TotalPoints}",
            $"TOTALCHALLENGES:{TotalChallenges}",
            $"TOTALHIDDENCHALLENGES:{TotalHiddenChallenges}",
            $"WINS:{Wins}",
            $"INTS:{string.Join(",", Integers)}",
            $"MANUAL:{(HasViewedManual ? 1 : 0)}",
            $"SLUGWINS:{string.Join("<>", WinEntries.Where(x => x.Wins > 0).Select(x => $"{x.Slugcat}#{x.Wins}"))}"
        };

        if (ChallengeTypes.Count > 0)
        {
            list.Add($"CHALLENGETYPES:{string.Join("<>", ChallengeTypes.Select(x => $"{x.Type}#{x.Count}"))}");
        }

        if (Unlockables.Count > 0)
        {
            list.Add($"UNLOCKS:{string.Join("<>", Unlockables)}");
        }

        list.Add($"NEWSONGS:{string.Join("<>", NewSongs)}");
        list.Add($"QUESTS:{string.Join("<>", Quests)}");
        list.Add($"MISSIONS:{string.Join("<>", Missions)}");
        list.Add($"MENUSONG:{MenuSong}");

        // Challenges

        list.Add($"[CHALLENGES]");

        foreach (var data in ChallengeEntries)
        {
            list.Add($"{data.Slugcat}#{data.Type}~{data.Data}");
        }

        list.Add($"[END CHALLENGES]");

        // Unlocks

        list.Add($"[UNLOCKS]");

        foreach (var data in Unlocks)
        {
            list.Add($"{data.Slugcat}#{string.Join("><", data.Data)}");
        }

        list.Add($"[END UNLOCKS]");

        // Passages

        list.Add($"[PASSAGES]");

        foreach (var data in Passages)
        {
            list.Add($"{data.Slugcat}#{data.Count}");
        }

        list.Add($"[END PASSAGES]");

        // Missions

        if (ActiveMissionEntries.Select(x => x.Data).Where(x => x != "").Any())
        {
            list.Add($"[MISSION]");

            foreach (var data in ActiveMissionEntries.Where(x => x.Data != ""))
            {
                list.Add($"{data.Slugcat}#{data.Data}");
            }

            list.Add($"[END MISSION]");
        }

        // Times

        if (MissionBestTimes.Count > 0)
        {
            list.Add($"[TIMES]");

            foreach (var time in MissionBestTimes)
            {
                list.Add($"{time.Mission}#{time.Time}");
            }

            list.Add($"[END TIMES]");
        }

        // Content

        if (RequiredModsEntries.Select(x => x.RequiredMods.Count).Where(x => x > 0).Any())
        {
            list.Add($"[CONTENT]");

            foreach (var data in RequiredModsEntries.Where(x => x.RequiredMods.Count > 0))
            {
                list.Add($"{data.Slugcat}#{string.Join("<mod>", data.RequiredMods)}");
            }

            list.Add($"[END CONTENT]");
        }

        return string.Join("<expC>", list);
    }
}
