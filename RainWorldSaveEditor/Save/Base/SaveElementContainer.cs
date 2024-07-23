using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RainWorldSaveEditor.Save;

public abstract class SaveElementContainer
{
    public SaveElementContainer()
    {
        var properties = this.GetType().GetProperties().Where(property => Attribute.IsDefined(property, typeof(SaveFileElement)));
        foreach (var prop in properties)
        {
            SaveFileElement data = (SaveFileElement)prop.GetCustomAttribute(typeof(SaveFileElement))!;
        
            PropertyInfos.Add(data.Name, prop);
            SaveFileElements.Add(data.Name, data);
        }
    }

    public Dictionary<string, SaveFileElement> SaveFileElements { get; private set; } = new();
    public Dictionary<string, PropertyInfo> PropertyInfos { get; private set; } = new();

    public Dictionary<string, string> UnrecognizedFields { get; protected set; } = [];


    /// <summary>
    /// Sets the value for a scalar property. I.E. A property that is not a collection
    /// </summary>
    /// <param name="container">The SaveElementContainer who is calling</param>
    /// <param name="propertyInfo">The information of the property</param>
    /// <param name="elementInfo">The SaveFileElement information for the property</param>
    /// <param name="value">The value to set the property too</param>
    /// <returns>True on success, false otherwise</returns>
    private static bool SetScalarProperty(SaveElementContainer container, PropertyInfo propertyInfo, SaveFileElement elementInfo, string value)
    {
        var parseMethodInfo = propertyInfo.GetParseMethod();

        // Vultu: IDK why `method` is null for string?? It derives from `IParsable`
        if (parseMethodInfo is not null || propertyInfo.PropertyType == typeof(string))
        {
            if (!elementInfo.ValueOptional && value == string.Empty)
            {
                Console.WriteLine($"ERROR: \"{elementInfo.Name}\" is NOT marked as ValueOptional, but no value was provided! Tell Mario or Vultu!");
                return false;
            }

            if (value != string.Empty)
            {
                if (propertyInfo.PropertyType == typeof(string))
                    propertyInfo.GetSetMethod()!.Invoke(container, [value]);
                else
                {
                    var propertyBinding = propertyInfo.GetValue(container);


                    if (propertyBinding is null)
                    {
                        Console.WriteLine($"Unable to get property binding for \"{propertyInfo.Name}\" with container: \"{container.GetType()}\"");
                        return false;
                    }

                    var data = parseMethodInfo!.Invoke(propertyBinding, [value, null]);

                    var setMethod = propertyInfo.GetSetMethod(true);
                    if (setMethod is null)
                    {
                        Console.WriteLine($"Set Method was null for \"{propertyInfo.Name}\" with container: \"{container.GetType()}\"");
                        return false;
                    }
                    setMethod.Invoke(container, [data]);
                }
            }
            else
            {
                Console.WriteLine($"DEBUG: UNABLE TO SET: \"{propertyInfo.Name}\"! Tell Mario or Vultu!");
                return false;
            }
        }
        return true;
    }

    public static void ParseField(SaveElementContainer container, string key, string value)
    {
        if (container.SaveFileElements.ContainsKey(key))
        {
            var elementInfo = container.SaveFileElements[key];
            var propertyInfo = container.PropertyInfos[key];

            var listInterface = propertyInfo.PropertyType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IList<>)).FirstOrDefault();
            var parsableInterface = propertyInfo.PropertyType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IParsable<>)).FirstOrDefault();

            if (parsableInterface is not null)
            {
                if (!SetScalarProperty(container, propertyInfo, elementInfo, value))
                    goto AddToUnrecognizedFields;
            }
            else if (listInterface is not null)
            {
                Type var = listInterface.GetGenericArguments()[0];
                var method = var.GetMethods().Where(x => x.Name == "Parse" || x.Name == "TryParse").FirstOrDefault();

                if (method is not null)
                {

                }
                else
                {
                    Console.WriteLine("Found List with unparsable type!");
                    goto AddToUnrecognizedFields;
                }
            }
            else
            {
                Console.WriteLine($"DEBUG: \"{propertyInfo.PropertyType}\" does not derive from IParsable! Tell Mario or Vultu!");
                goto AddToUnrecognizedFields;
            }

            return;
        }

    AddToUnrecognizedFields:
        if (!container.UnrecognizedFields.ContainsKey(key))
            container.UnrecognizedFields.Add(key, value);
        else
            Console.WriteLine($"Unable to set \"{key}\" because it was already present!");
    }
}
