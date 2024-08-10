namespace RainWorldSaveAPI;

public enum MultiListMode
{
    /// <summary>
    /// Adds fields to the multilist if the keys are prefixed by the field name.
    /// </summary>
    Prefix,

    /// <summary>
    /// Adds fields to the multilist if the keys contain the field name as a substring.
    /// </summary>
    Substring
}

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
    /// For lists, defines the delimiter to use for elements.
    /// If set to <see cref="string.Empty"/>, the value delimiter will be used instead.
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
    /// If false, lists and raw values will be skipped from serialization if they have no elements at all. <para/>
    /// The default value is false.
    /// </summary>
    public bool SerializeIfEmpty { get; init; } = false;

    /// <summary>
    /// Controls the behaviour of multilist deserialization.
    /// </summary>
    public MultiListMode MultiListMode { get; init; } = MultiListMode.Prefix;
}

