using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveAPI;

public enum RepeatMode
{
    None,
    Exact,
    Prefix
}

// TODO: Implement a way to read fields of type "integersArray", "miscBools", etc.
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
    /// For lists, defines that this key can appear multiple times and the fields should be added into the list.
    /// </summary>
    public RepeatMode IsRepeatableKey { get; init; } = RepeatMode.None;

}

