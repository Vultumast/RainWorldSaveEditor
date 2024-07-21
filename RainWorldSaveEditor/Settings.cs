using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveEditor
{
    public class Settings
    {
        public Settings()
        {
            Reset();
        }

        /// <summary>
        /// Reset Settings to default parameters
        /// </summary>
        public void Reset()
        {
            RainWorldDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "LocalLow", "VideoCult", "Rain World");
        }


        /// <summary>
        /// The location of your Rain World save directory
        /// </summary>
        public string RainWorldDirectory { get; set; } = string.Empty;
    }
}
