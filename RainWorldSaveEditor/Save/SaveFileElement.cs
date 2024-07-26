using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveEditor.Save
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SaveFileElement(string name, bool valueOptional = false) : Attribute
    {
        /// <summary>
        /// The name of the property in the save file
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// Can the property be valueless?
        /// </summary>
        public bool ValueOptional { get; } = valueOptional;

        /// <summary>
        /// For lists, defines the delimiter to use for elements.
        /// </summary>
        public string? ListDelimiter { get; init; } = null;

    }
}
