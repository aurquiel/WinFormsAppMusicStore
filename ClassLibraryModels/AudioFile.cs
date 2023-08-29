using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModels
{
    public class AudioFile
    {
        public string name { get; set; }

        public string path { get; set; }

        public TimeSpan duration { get; set; }

        public double size { get; set; }
    }
}
