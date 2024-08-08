namespace RainWorldSaveAPI;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SaveFieldAttribute(int order, string name) : Attribute
{
    /// <summary>
    /// Order to serialize fields in. Fields with lower values get deserialized first. <para/>
    /// This is mostly used so that generated save file ordering ois similar to the game's ordering.
    /// </summary>
    public int Order { get; } = order;

    /// <summary>
    /// The name of the property in the save file
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// For lists, defines the delimiter to use for elements. Ignored if <see cref="IsRepeatableKey"/> is set.
    /// </summary>
    public string? ListDelimiter { get; init; } = null;

    /// <summary>
    /// For lists, defines if the last element in the list should have a list delimiter after it when serializing. <para/>
    /// </summary>
    public bool TrailingListDelimiter { get; init; } = false;

    /// <summary>
    /// For fields, defines if the list of values should end with a trailing value delimiter. <para/>
    /// </summary>
    public bool TrailingValueDelimiter { get; init; } = false;

    /// <summary>
    /// If false, lists and raw values will be skipped from serialization if they have no elements at all.
    /// </summary>
    public bool SerializeIfEmpty { get; init; } = false;
}

