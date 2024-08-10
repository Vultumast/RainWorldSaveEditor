namespace RainWorldSaveAPI;

public enum Tag
{
    /// <summary>
    /// Field is used only if "More Slugcats Expansion" mod is enabled.
    /// </summary>
    MSC,

    /// <summary>
    /// Field is used only if "Rain World Remix" (MMF) mod is enabled.
    /// </summary>
    MMF,

    /// <summary>
    /// Modded field that is used by SlugBase mod.
    /// </summary>
    SlugBase
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class FieldTagsAttribute(params Tag[] tags) : Attribute
{
    public Tag[] Tags { get; } = tags;
}
