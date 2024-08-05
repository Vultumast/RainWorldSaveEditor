using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveAPI.Base;
public class RawValues : IRWSerializable<RawValues>
{
    public string[] Values { get; set; } = [];

    public static RawValues Deserialize(string key, string[] values, SerializationContext? context)
    {
        var data = new RawValues
        {
            Values = [.. values]
        };

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [..Values];
        return true;
    }
}
