using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveAPI;

public enum FieldTags
{
    /// <summary>
    /// Field is used only if "More Slugcats Expansion" mod is enabled.
    /// </summary>
    MSC,

    /// <summary>
    /// Field is used only if "Rain World Remix" (MMF) mod is enabled.
    /// </summary>
    MMF,
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class FieldTagAttribute(params FieldTags[] tags) : Attribute
{
    public FieldTags[] Tags { get; } = tags;
}
