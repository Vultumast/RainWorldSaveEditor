using RainWorldSaveAPI.Base;
using RainWorldSaveAPI.SaveElements;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace RainWorldSaveAPI;

public class AbstractObject
{
    public string EntityID { get; set; } = "";
    public string ObjectType { get; set; } = "";
    public WorldCoordinate Position { get; set; } = new();

    public List<string> State { get; set; } = [];
}

public class AbstractCreature
{
    public string EntityID { get; set; } = "";
    public string EntityType { get; set; } = "";
    public string Room { get; set; } = "";
    public int AbstractNode { get; set; } = 0;

    public List<string> State { get; set; } = [];
}

public class AbstractObjectOrCreature : IRWSerializable<AbstractObjectOrCreature>
{
    public AbstractObject? Object
    {
        get => _object;
        set
        {
            _object = value;
            _creature = null;
        }
    }
    private AbstractObject? _object;

    public AbstractCreature? Creature
    {
        get => _creature;
        set
        {
            _object = null;
            _creature = value;
        }
    }
    private AbstractCreature? _creature;

    public static AbstractObjectOrCreature Deserialize(string key, string[] values, SerializationContext? context)
    {
        var data = new AbstractObjectOrCreature();

        var value = values[0];

        if (value.Contains("<oA>"))
        {
            string[] parts = values[0].Split("<oA>");

            data.Object = new()
            {
                EntityID = parts[0],
                ObjectType = parts[1],
                Position = WorldCoordinate.Deserialize("", [parts[2]], null),
                State = new(parts.Length >= 3 ? parts[3..] : [])
            };
        }
        else if (value.Contains("<cA>"))
        {
            string[] parts = values[0].Split("<cA>");
            string[] args = parts[2].Split('.');

            data.Creature = new()
            {
                EntityType = parts[0],
                EntityID = parts[1],
                Room = args[0],
                AbstractNode = int.Parse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture),
                State = new(parts[3].Split("<cB>"))
            };
        }
        else throw new InvalidOperationException("Couldn't determine the type of AbstractObjectOrCreature");

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        if (Object != null)
        {
            Object.Position.Serialize(out _, out var posValues, null);

            key = null;
            values = [
                $"{Object.EntityID}<oA>" +
                $"{Object.ObjectType}<oA>" +
                $"{posValues[0]}" +
                $"{string.Concat(Object.State.Select(x => $"<oA>{x}"))}"
            ];
        }
        else if (Creature != null)
        {
            key = null;
            values = [
                $"{Creature.EntityType}<cA>" +
                $"{Creature.EntityID}<cA>" +
                $"{Creature.Room}.{Creature.AbstractNode}<cA>" +
                $"{string.Join("<cB>", Creature.State)}"
            ];
        }
        else throw new InvalidOperationException("Couldn't determine the type of AbstractObjectOrCreature");

        return true;
    }
}
