using System.Security.Cryptography;
using System.Text;

namespace RainWorldSaveEditor.Save;

public class RainWorldSave
{
    public class SaveState
    {

    }

    public static RainWorldSave Read(string saveString)
    {
        var save = new RainWorldSave();

        var hash = saveString[..32];
        var data = saveString[32..];
        var computedHash = ComputeChecksum(data);

        // Computed hash doesn't seem to match at the moment for some reason
        if (hash != computedHash)
            Console.WriteLine("Hash check failed! Save may be modified / damaged / corrupted.");
        else
            Console.WriteLine("Hash OK.");

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

    private static SaveState ReadSaveState(string data)
    {
        throw new NotImplementedException();
    }

    private static string ComputeChecksum(string data)
    {
        var utf8 = Encoding.UTF8.GetBytes(data + CheckSumSalt);
        var hash = MD5.HashData(utf8);

        var text = new StringBuilder(32);

        for (int i = 0; i < hash.Length; i++)
            text.Append(Convert.ToString(hash[i], 16).PadLeft(2, '0'));

        return text.ToString();
    }

    // Taken directly from Rain World's stuff

    private static string BaseEncryptionString { get; } = """
    IA/AF57P16dUz+wU1A/9K00Py47ND+8VBk/GRwEPxPE4D78LMM+WLCkPpQTjT5dJ
    WY+Nhg+PuYNEz6WVOo9DpOoPZ11fT3DuTU9WigSP9yeKT8U+EQ/EghqPxKqbj8AA
    IA/pihwPzuncT9L2XI/In50PzpJdj9D4nY/CVV3P8/Hdz+VOng/tp56PwAAgD90e
    Ho/ZEZ5P53TeD+57FA/wxcxPyjWFz90awE/6cbdPj6xvj7LUrY+KNaXPnRrgT4kb
    k8+BvonPgBRAj65qMc91tuRPW7PVD2aeCk/0tFEP9DhaT8AAIA/AACAPwAAgD8AA
    IA/AACAPwAAgD8AAIA/AACAPwAAgD8AAIA/AACAPwAAgD8AAIA/AACAPwAAgD8AA
    IA/AACAPxmHeD81oFA/P8swP6SJFz/vHgE/ZXrdPtG30j5HBrY+rCKYPu8egT4sB
    1A+/WAnPgjqAj6pdsY95g2TPZxOEj9YUik/Vh5FP467aT91kmw/AACAP2QCcD/5g
    HE/CbNyP+BXdD/4InY/Abx2P8cudz+NoXc/UxR4P9dgeD/f+Xg/pmx5PyEgeT9br
    Xg/d8ZQP4HxMD/mrxc/MUUBP/Ar+j7okvk+3/n4PlUE0z7DubU+MG+YPmvSgD41o
    FA+9ccmPhCDAz6YRMU9xNMnP7oGQz8zymc/AACAPwAAgD8AAIA/AACAPzsFbT9vi
    0s/f71MP5iITj+gIU8/ZpRPP+rgTz9uLVA/+xJRP/CJdT9/X1E/PTlRP/N5UD8FP
    jE/YmMXPzhmFD+sgBM/pOcSP950Ej8H0BA/W634PtpQ0z4/bbU+tLuYPueFgD49O
    VE+7S4mPhkcBD6DgxA/gq0nP/wsQz/xo2c/VC5qPwAAgD99K20/+85HPxbOLT8eZ
    y4/s+UvP3lYMD+pGEs/Q0ByP88lcz+uY3U/AACAP2w9dT+HVk0/R2QxP8wOLT9Hw
    iw/BZwsP3m2Kz/t0Co/ohEqPxYsKT9J9hA/12D4Pl6d0z67ILU+OAiZPmM5gD5F0
    lE+5ZUlPvXHJj9fFUE/wA1kPwAAgD8AAIA/AACAP/k8aD+AeUM/ToVEPx2RRT9nU
    EY/Z/JKPwEacj8AAIA/AACAPwAAgD8AAIA/AACAP9i+cz93JEw/kE1JPwwBST+It
    Eg/d4JHP+MDRj+YREU/BMZDP9QFKT8PaRE/UxT4PuLp0z421LQ+vFSZPr7Zfz5Na
    1I+OcQPP7OhJj+hO0E/fudjP699Zz8AAIA/bVdnP7cWaD+GImk/2HpqP+msaz8tw
    0Y/6z5LP7/zcT+N/3I/U3JzPwAAgD8a5XM/KnVwPyLcbz8aQ28/0INuP0s3bj/53
    mw/bflrP1zHaj9MlWk/DF9EPw6TKD8YAhI/S3v3PvIb1T4uO7Q+zYaaPq6nfj5p4
    iU/TuM/P6/bYj8AAIA/AACAPwAAgD8AAIA/AACAPwAAgD8AAIA/AACAP2Rgaz8Um
    kk/lvZuPwAAgD8AAIA/AACAPwAAgD8AAIA/AACAPwAAgD8AAIA/AACAPwAAgD8
    """.Replace("\n", "").Replace(" ", "");

    private static string EncryptionString { get; } = BaseEncryptionString.Substring(54, 1447);
    private static string CheckSumSalt { get; } = EncryptionString.Substring(64, 97);
}
