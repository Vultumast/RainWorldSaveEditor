namespace RainWorldSaveAPI;

public enum RepeatMode
{
    None,
    Exact,
    Prefix
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SaveFileElement(string name, bool valueOptional = false) : Attribute
{
    /// <summary>
    /// The name of the property in the save file
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Can the property be valueless?
    /// </summary>
    public bool ValueOptional { get; } = valueOptional;

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
    /// Order to serialize fields in. Fields with lower values get deserialized first.
    /// </summary>
    public int Order { get; set; } = -9999;

}

