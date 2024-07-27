using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public Dictionary<string, SaveFileElement> SaveFileElements { get; private set; } = [ ];
    public Dictionary<string, PropertyInfo> PropertyInfos { get; private set; } = [ ];

    public Dictionary<string, string> UnrecognizedFields { get; protected set; } = [ ];


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
        var parseMethodInfo = propertyInfo.PropertyType.GetParseMethod();

        if (propertyInfo.PropertyType == typeof(bool) && elementInfo.ValueOptional)
        {
            var setMethod = propertyInfo.GetSetMethod(true);
            if (setMethod is null)
            {
                Logger.Warn($"Set Method was null for \"{propertyInfo.Name}\" with container: \"{container.GetType()}\"");
                return false;
            }
            setMethod.Invoke(container, [ true ]);
        }



        // Vultu: IDK why `method` is null for string?? It derives from `IParsable`
        if (parseMethodInfo is not null || propertyInfo.PropertyType == typeof(string))
        {
            if (!elementInfo.ValueOptional && value == string.Empty)
            {
                Logger.Error($"\"{elementInfo.Name}\" is NOT marked as ValueOptional, but no value was provided! Tell Mario or Vultu!");
                return false;
            }

            if (value != string.Empty)
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    propertyInfo.GetSetMethod()!.Invoke(container, [value]);

                    // TODO Remove this later
                    // Logger.Debug($"{propertyInfo.DeclaringType?.Name}: {propertyInfo.Name} => {value} ({value?.GetType().Name})");
                }
                else
                {
                    var propertyBinding = propertyInfo.GetValue(container);


                    /* if (propertyBinding is null)
                    {
                        Logger.Error($"Unable to get property binding for \"{propertyInfo.Name}\" with container: \"{container.GetType()}\"");
                        return false;
                    } */

                    var data = parseMethodInfo!.Invoke(propertyBinding, [value, null]);

                    var setMethod = propertyInfo.GetSetMethod(true);
                    if (setMethod is null)
                    {
                        Logger.Error($"Set Method was null for \"{propertyInfo.Name}\" with container: \"{container.GetType()}\"");
                        return false;
                    }
                    setMethod.Invoke(container, [data]);

                    // TODO Remove this later
                    // Logger.Debug($"{propertyInfo.DeclaringType?.Name}: {propertyInfo.Name} => {data} ({data?.GetType().Name})");
                }
            }
            else
            {
                Logger.Error($"UNABLE TO SET: \"{propertyInfo.Name}\"! Tell Mario or Vultu!");
                return false;
            }
        }

        return true;
    }

    private static bool SetListProperty(SaveElementContainer container, PropertyInfo propertyInfo, SaveFileElement elementInfo, string value, Type collectionInterface)
    {
        Type elementType = collectionInterface.GetGenericArguments()[0];

        var parseMethod = elementType.GetParseMethod();
        var addMethod = collectionInterface.GetMethod("Add");
        var clearMethod = collectionInterface.GetMethod("Clear");

        if (addMethod == null || clearMethod == null)
        {
            Logger.Warn("Couldn't find add / clear methods for collection type.");
            return false;
        }

        if (elementType == typeof(string))
        {
            var propertyBinding = propertyInfo.GetValue(container);

            clearMethod.Invoke(propertyBinding, null);

            foreach (var element in value.Split(elementInfo.ListDelimiter, StringSplitOptions.RemoveEmptyEntries))
            {
                addMethod.Invoke(propertyBinding, [element]);
            }

            return true;
        }
        else if (parseMethod is not null)
        {
            var propertyBinding = propertyInfo.GetValue(container);

            clearMethod.Invoke(propertyBinding, null);

            foreach (var element in value.Split(elementInfo.ListDelimiter, StringSplitOptions.RemoveEmptyEntries))
            {
                addMethod.Invoke(propertyBinding, [parseMethod.Invoke(null, [element, null])]);
            }

            return true;
        }
        else
        {
            Logger.Error($"Found Element List: \"{elementInfo.Name}\" with unparsable type: \"{propertyInfo.PropertyType}\"!");
            return false;
        }
    }

    private static void HandleUnrecognizedField(SaveElementContainer container, string key, string value)
    {
        if (!container.UnrecognizedFields.TryAdd(key, value))
            Logger.Warn($"Unable to set \"{key}\" because it was already present!");

        // TODO Remove this later
        Logger.Debug($"Unknown field: {key} => {value}");
    }

    public static void ParseField(SaveElementContainer container, string key, string value)
    {
        SaveFileElement elementInfo;
        if (container.SaveFileElements.TryGetValue(key, out elementInfo!))
        {
            var propertyInfo = container.PropertyInfos[key];

            var collectionInterface = propertyInfo.PropertyType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>)).FirstOrDefault();
            var parsableInterface = propertyInfo.PropertyType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IParsable<>)).FirstOrDefault();

            if (parsableInterface is not null)
            {
                if (SetScalarProperty(container, propertyInfo, elementInfo, value))
                    return;
            }
            else if (collectionInterface is not null)
            {
                if (elementInfo.ListDelimiter is null)
                    Logger.Debug($"{key} => {value}, \"{propertyInfo.PropertyType}\" is a collection that doesn't have a delimiter! Tell Mario or Vultu!");

                else if (SetListProperty(container, propertyInfo, elementInfo, value, collectionInterface))
                    return;
            }
            else
                Logger.Debug($"{key} => {value}, \"{propertyInfo.PropertyType}\" does not derive from IParsable! Tell Mario or Vultu!");

            HandleUnrecognizedField(container, key, value);
        }
        else
            Logger.Warn($"Unknown Key: \"{key}\" => {value}");
    }
}
