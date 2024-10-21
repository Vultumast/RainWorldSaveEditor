using RainWorldSaveAPI.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RainWorldSaveAPI;

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

public class ExpeditionCoreSave
{
    public string Data { get; set; } = "";

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
                        AddOrUpdate(Unlocks, entry, x => x.Slugcat == slugcatName);
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
                    AddOrUpdate(ChallengeEntries, entry, x => x.Slugcat == slugcatName);
                }
                else if (section == "MISSION")
                {
                    string[] args = part.Split('#');
                    string slugcatName = args[0];
                    string activeMissions = args[1];

                    var entry = new ActiveMissionEntry
                    {
                        Slugcat = slugcatName,
                        Data = activeMissions
                    };
                    AddOrUpdate(ActiveMissionEntries, entry, x => x.Slugcat == slugcatName);
                }
                else if (section == "TIMES")
                {
                    string[] args = part.Split('#');
                    string mission = args[0];
                    int time = int.Parse(args[1]);
                    MissionBestTimes.Add(new()
                    {
                        Mission = mission,
                        Time = time
                    });
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
                string[] challenges = part["CHALLENGETYPES:".Length..].Split("<>");

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
                string[] slugwins = part["SLUGWINS:".Length..].Split("<>");

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
                string[] unlocks = part["UNLOCKS:".Length..].Split("<>");

                foreach (var unlock in unlocks)
                {
                    Unlockables.Add(unlock);
                }
            }
            else if (part.StartsWith("NEWSONGS:"))
            {
                string[] newsongs = part["NEWSONGS:".Length..].Split("<>");

                foreach (var newsong in newsongs)
                {
                    NewSongs.Add(newsong);
                }
            }
            else if (part.StartsWith("QUESTS:"))
            {
                string[] quests = part["QUESTS:".Length..].Split("<>");

                foreach (var quest in quests)
                {
                    Quests.Add(quest);
                }
            }
            else if (part.StartsWith("MISSIONS:"))
            {
                string[] missions = part["MISSIONS:".Length..].Split("<>");

                foreach (var mission in missions)
                {
                    Missions.Add(mission);
                }
            }
            else if (part.StartsWith("INTS:"))
            {
                Integers.AddRange(part["INTS:".Length..].Split(",").Select(int.Parse));
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
