using RainWorldSaveAPI.Base;
using RainWorldSaveAPI.SaveElements;
using System.Globalization;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RainWorldSaveAPI;

public class ObjectData
{
    public string EntityID { get; set; } = "";
    public string ObjectType { get; set; } = "";
    public WorldCoordinate Position { get; set; } = new();

    public List<string> State { get; set; } = [];
}

public class CreatureData
{
    public string EntityID { get; set; } = "";
    public string EntityType { get; set; } = "";
    public string Room { get; set; } = "";
    public int AbstractNode { get; set; } = 0;

    public List<string> State { get; set; } = [];
}

public class AbstractObject : IRWSerializable<AbstractObject>
{
    public ObjectData Object { get; set; } = new();

    public static AbstractObject Deserialize(string key, string[] values, SerializationContext? context)
    {
        var data = new AbstractObject();

        string[] parts = values[0].Split("<oA>");

        data.Object = new()
        {
            EntityID = parts[0],
            ObjectType = parts[1],
            Position = WorldCoordinate.Parse(parts[2], null),
            State = new(parts.Length >= 3 ? parts[3..] : [])
        };

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            $"{Object.EntityID}<oA>" +
            $"{Object.ObjectType}<oA>" +
            $"{Object.Position}" +
            $"{string.Concat(Object.State.Select(x => $"<oA>{x}"))}"
        ];

        return true;
    }
}

public class AbstractCreature : IRWSerializable<AbstractCreature>
{
    public CreatureData Creature { get; set; } = new();

    public static AbstractCreature Deserialize(string key, string[] values, SerializationContext? context)
    {
        var data = new AbstractCreature();

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

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            $"{Creature.EntityType}<cA>" +
            $"{Creature.EntityID}<cA>" +
            $"{Creature.Room}.{Creature.AbstractNode}<cA>" +
            $"{string.Join("<cB>", Creature.State)}"
        ];

        return true;
    }
}

public class AbstractObjectOrCreature : IRWSerializable<AbstractObjectOrCreature>
{
    public ObjectData? Object
    {
        get => _object;
        set
        {
            _object = value;
            _creature = null;
        }
    }
    private ObjectData? _object = new();

    public CreatureData? Creature
    {
        get => _creature;
        set
        {
            _object = null;
            _creature = value;
        }
    }
    private CreatureData? _creature;

    public static AbstractObjectOrCreature Deserialize(string key, string[] values, SerializationContext? context)
    {
        var data = new AbstractObjectOrCreature();

        var value = values[0];

        if (value.Contains("<oA>"))
        {
            var obj = AbstractObject.Deserialize(key, values, context);
            data.Object = obj.Object;
        }
        else if (value.Contains("<cA>"))
        {
            var obj = AbstractCreature.Deserialize(key, values, context);
            data.Creature = obj.Creature;
        }
        else if (value == "0")
        {
            // TODO This is not a clean way to handle this

            if (context?.Metadata == null || !(context.Metadata.Name == "SWALLOWEDITEMS" || context.Metadata.Name == "SAINTSTOMACH"))
                Logger.Warn("Encountered an abstract object or creature that was set to \"0\" that was not a SWALLOWEDITEMS or SAINTSTOMACH item!");

            data.Creature = null;
            data.Object = null;
        }
        else throw new InvalidOperationException("Couldn't determine the type of AbstractObjectOrCreature");

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        if (Object == null && Creature == null)
        {
            // TODO This is not a clean way to handle this

            if (context?.Metadata == null || !(context.Metadata.Name == "SWALLOWEDITEMS" || context.Metadata.Name == "SAINTSTOMACH"))
                Logger.Warn("Encountered an abstract object or creature that was set to \"0\" that was not a SWALLOWEDITEMS or SAINTSTOMACH item!");

            key = null;
            values = ["0"];
            return true;
        }

        if (Object != null)
        {
            var obj = new AbstractObject() { Object = Object };
            return obj.Serialize(out key, out values, context);
        }
        else if (Creature != null)
        {
            var obj = new AbstractCreature() { Creature = Creature };
            return obj.Serialize(out key, out values, context);
        }
        else throw new InvalidOperationException("Couldn't determine the type of AbstractObjectOrCreature");
    }
}