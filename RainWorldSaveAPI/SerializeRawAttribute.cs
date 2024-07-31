using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveAPI;

/// <summary>
/// If present, specifies that the given class should receive the entire field (key + delimiter + value) as part of serialization.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SerializeRawAttribute(string keyValueDelimiter) : Attribute
{
    /// <summary>
    /// String to use to delimit key and value in combined field.
    /// </summary>
    public string KeyValueDelimiter { get; } = keyValueDelimiter;
}
