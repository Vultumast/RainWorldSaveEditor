using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveEditor
{
    public class SlugcatInfo(string name, string saveID, bool requiresDLC, bool modded, byte pipCount, byte pipBarIndex)
    {
        public string Name { get; set; } = name;
        public string SaveID { get; set; } = saveID;
        public bool RequiresDLC { get; set; } = requiresDLC;
        public bool Modded { get; set; } = modded;
        public byte PipCount { get; set; } = pipCount;
        public byte PipBarIndex { get; set; } = pipBarIndex;
    }
}
