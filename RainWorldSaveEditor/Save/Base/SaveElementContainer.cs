using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RainWorldSaveEditor.Save.Base
{
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



        public static void ParseField(SaveElementContainer container, string key, string value)
        {
            if (container.SaveFileElements.ContainsKey(key))
            {
                var elementInfo = container.SaveFileElements[key];
                var propertyInfo = container.PropertyInfos[key];

                var listInterface = propertyInfo.PropertyType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IList<>)).FirstOrDefault();
                var parsableInterface = propertyInfo.PropertyType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IParsable<>)).FirstOrDefault();

                if (listInterface is not null)
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
                else if (parsableInterface is not null)
                {
                    MethodInfo parseMethodInfo = null!;
                    // Vultu: Get method ``Parse(string s, IFormatProvider? provider)``
                    foreach (var method in propertyInfo.PropertyType.GetMethods())
                    {
                        var parameters = method.GetParameters();
                        if (method.Name == "Parse" && parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(IFormatProvider))
                        {
                            parseMethodInfo = method;
                            break;
                        }
                    }


                    // Vultu: IDK why `method` is null for string?? It derives from `IParsable`
                    if (parseMethodInfo is not null || propertyInfo.PropertyType == typeof(string))
                    {
                        if (!elementInfo.ValueOptional && value == string.Empty)
                        {
                            Console.WriteLine($"ERROR: \"{elementInfo.Name}\" is NOT marked as ValueOptional, but no value was provided! Tell Mario or Vultu!");
                            goto AddToUnrecognizedFields;
                        }

                        if (value != string.Empty)
                        {
                            if (propertyInfo.PropertyType == typeof(string))
                                propertyInfo.GetSetMethod()!.Invoke(container, [value]);
                            else
                            {
                                var propertyBinding = propertyInfo.GetValue(container);
                                var data = parseMethodInfo!.Invoke(propertyBinding, [value, null]);

                                propertyInfo.GetSetMethod()!.Invoke(container, [ data ]);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"DEBUG: UNABLE TO SET: \"{propertyInfo.Name}\"! Tell Mario or Vultu!");
                            goto AddToUnrecognizedFields;
                        }
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
}
