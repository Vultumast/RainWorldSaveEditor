using System.Collections;
using System.Xml;
using System.Runtime.Serialization;

namespace RainWorldSaveEditor.Save;

public static class HashtableSerializer
{
    public static Hashtable Read(Stream input)
    {
        var reader = new XmlTextReader(input)
        {
            Namespaces = false
        };

        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(reader);

        var data = new Hashtable();

        try
        {
            var keys = xmlDocument.SelectSingleNode("//Keys");
            var values = xmlDocument.SelectSingleNode("//Values");

            if (keys != null && values != null)
            {
                XmlNodeList keyChildNodes = keys.ChildNodes;
                XmlNodeList valueChildNodes = values.ChildNodes;
                for (int i = 0; i < keyChildNodes.Count && i < valueChildNodes.Count; i++)
                {
                    var keyNode = keyChildNodes[i];
                    var valueNode = valueChildNodes[i];

                    if (keyNode != null && valueNode != null)
                    {
                        data[keyNode.InnerText] = valueNode.InnerText;
                    }

                }

                return data;
            }

            var keyValues = xmlDocument.SelectNodes("//KeyValueOfanyTypeanyType");

            if (keyValues != null && keyValues.Count > 0)
            {
                for (int j = 0; j < keyValues.Count; j++)
                {
                    var keyValue = keyValues[j];

                    if (keyValue != null)
                    {
                        var key = keyValue.SelectSingleNode("Key");
                        var value = keyValue.SelectSingleNode("Value");

                        if (key != null && value != null)
                        {
                            data[key.InnerText] = value.InnerText;
                        }
                    }
                }

                return data;
            }
            else
            {
                throw new InvalidOperationException("");
            }
        }
        catch
        {
            throw new InvalidOperationException("");
        }
    }

    public static void Write(Stream output, Hashtable data)
    {
        var serializer = new DataContractSerializer(typeof(Hashtable));

        serializer.WriteObject(output, data);
    }
}
