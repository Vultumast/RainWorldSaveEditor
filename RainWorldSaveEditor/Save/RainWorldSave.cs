namespace RainWorldSaveEditor.Save;

public class RainWorldSave
{
    public class SaveState
    {

    }

    public static RainWorldSave Read(ReadOnlySpan<char> saveString)
    {
        var save = new RainWorldSave();

        var hash = saveString[..32];
        var data = saveString[32..];

        while (data.Length > 0)
        {
            const string SaveStateStart = "SAVE STATE<progDivB>";
            const string SaveStateEnd = "<progDivA>";

            if (data.StartsWith(SaveStateStart))
            {
                var end = data.IndexOf(SaveStateEnd);

                if (end == -1)
                    throw new InvalidOperationException("Invalid SAVE STATE field.");

                var state = ReadSaveState(saveString[SaveStateStart.Length..end]);
                data = data[(end + SaveStateEnd.Length)..];
            }
            else throw new InvalidOperationException("Unknown field encountered.");
        }

        throw new NotImplementedException();
    }

    private static SaveState ReadSaveState(ReadOnlySpan<char> data)
    {
        throw new NotImplementedException();
    }
}
