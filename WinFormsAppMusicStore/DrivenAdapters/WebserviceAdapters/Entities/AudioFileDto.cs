using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters.Entities
{
    public class AudioFileDto
    {
        public int id { get; set; }

        public int order { get; set; }

        public string name { get; set; }

        public int storeId { get; set; }

        public string path { get; set; }

        public double size { get; set; }

        public TimeSpan duration { get; set; }

        public bool checkForTime { get; set; }

        public TimeSpan timeToPlay { get; set; }
    }
}
