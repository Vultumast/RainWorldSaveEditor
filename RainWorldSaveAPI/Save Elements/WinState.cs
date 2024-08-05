using RainWorldSaveAPI.Base;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveAPI.SaveElements;

public class EndgameTracker
{
    public string ID { get; set; } = "";

    public bool Consumed { get; set; } = false;

    public virtual void FromString(string[] split)
    {
        ID = split[0];
        Consumed = split[1] == "1";
    }

    public virtual string[] ToSplit()
    {
        return [
            ID,
            Consumed ? "1" : "0"
        ];
    }
}

[DebuggerDisplay("Integer | ID = {ID} | Consumed = {Consumed} | Progress = {Progress}")]
public class IntegerTracker : EndgameTracker
{
    public int Progress { get; set; } = 0;

    public override void FromString(string[] split)
    {
        base.FromString(split);
        Progress = int.Parse(split[2], NumberStyles.Any, CultureInfo.InvariantCulture);
    }

    public override string[] ToSplit()
    {
        return [
            ID,
            Consumed ? "1" : "0",
            Progress.ToString()
        ];
    }
}

[DebuggerDisplay("Float | ID = {ID} | Consumed = {Consumed} | Progress = {Progress}")]
public class FloatTracker : EndgameTracker
{
    public float Progress { get; set; } = 0;

    public override void FromString(string[] split)
    {
        base.FromString(split);
        Progress = float.Parse(split[2], NumberStyles.Any, CultureInfo.InvariantCulture);
    }

    public override string[] ToSplit()
    {
        return [
            ID,
            Consumed ? "1" : "0",
            Progress.ToString()
        ];
    }
}

[DebuggerDisplay("BoolArray | ID = {ID} | Consumed = {Consumed} | Progress = {string.Join(\", \", Progress)}")]
public class BoolArrayTracker : EndgameTracker
{
    public List<bool> Progress { get; } = [];

    public override void FromString(string[] split)
    {
        base.FromString(split);
        if (split.Length > 2)
        {
            string[] flags = split[2].Split(".", StringSplitOptions.RemoveEmptyEntries);

            Progress.Clear();
            foreach (var flag in flags)
            {
                Progress.Add(flag == "1");
            }
        }
    }

    public override string[] ToSplit()
    {
        return [
            ID,
            Consumed ? "1" : "0",
            string.Join(".", Progress.Select(x => x ? "1" : "0"))
        ];
    }
}

[DebuggerDisplay("List | ID = {ID} | Consumed = {Consumed} | Progress = {string.Join(\", \", Progress)}")]
public class ListTracker : EndgameTracker
{
    public List<int> Progress { get; } = [];

    public override void FromString(string[] split)
    {
        base.FromString(split);
        if (split.Length > 2)
        {
            string[] flags = split[2].Split(".", StringSplitOptions.RemoveEmptyEntries);

            Progress.Clear();
            foreach (var flag in flags)
            {
                Progress.Add(int.Parse(flag, NumberStyles.Any, CultureInfo.InvariantCulture));
            }
        }
    }

    public override string[] ToSplit()
    {
        return [
            ID,
            Consumed ? "1" : "0",
            string.Join(".", Progress)
        ];
    }
}

[DebuggerDisplay("FoodQuest | ID = {ID} | Consumed = {Consumed} | Progress = {string.Join(\", \", Progress)}")]
public class GourmandFoodQuestTracker : EndgameTracker
{
    public List<int> Progress { get; } = [];

    public override void FromString(string[] split)
    {
        base.FromString(split);
        if (split.Length > 2)
        {
            string[] flags = split[2].Split(".", StringSplitOptions.RemoveEmptyEntries);

            Progress.Clear();
            foreach (var flag in flags)
            {
                Progress.Add(int.Parse(flag, NumberStyles.Any, CultureInfo.InvariantCulture));
            }
        }
    }

    public override string[] ToSplit()
    {
        return [
            ID,
            Consumed ? "1" : "0",
            string.Join(".", Progress)
        ];
    }
}

public class GenericTracker : EndgameTracker
{
    public List<string> Fields { get; } = [];

    public override void FromString(string[] split)
    {
        base.FromString(split);
        if (split.Length > 2)
        {
            Fields.Clear();
            Fields.AddRange(split[2..]);
        }
    }

    public override string[] ToSplit()
    {
        return [
            ID,
            Consumed ? "1" : "0",
            .. Fields
        ];
    }
}

public class WinState : IRWSerializable<WinState>
{
    public List<EndgameTracker> Trackers { get; } = [];

    public static WinState Deserialize(string key, string[] values, SerializationContext? context)
    {
        var winState = new WinState();

        string[] trackersData = values[0].Split("<wsA>", StringSplitOptions.RemoveEmptyEntries);

        foreach (var trackerData in trackersData)
        {
            string[] parts = trackerData.Split("<egA>", StringSplitOptions.RemoveEmptyEntries);

            var tracker = GetTrackerFromId(parts[0]);
            tracker.FromString(parts);

            winState.Trackers.Add(tracker);
        }

        return winState;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            string.Concat(Trackers.Select(x => $"{x}<wsA>"))
        ];

        return true;
    }

    private static EndgameTracker GetTrackerFromId(string id) => id switch
    {
        "Survivor" => new IntegerTracker(),
        "Hunter" => new IntegerTracker(),
        "Saint" => new IntegerTracker(),
        "Traveller" => new BoolArrayTracker(),
        "Chieftain" => new FloatTracker(),
        "Monk" => new IntegerTracker(),
        "Outlaw" => new IntegerTracker(),
        "DragonSlayer" => new ListTracker(), // TODO: This uses bool array tracker in vanilla and list in MSC?!
        "Scholar" => new ListTracker(),
        "Friend" => new FloatTracker(),
        "Gourmand" => new GourmandFoodQuestTracker(),
        "Nomad" => new ListTracker(),
        "Martyr" => new FloatTracker(),
        "Pilgrim" => new BoolArrayTracker(),
        "Mother" => new FloatTracker(),
        _ => new GenericTracker()
    };
}
