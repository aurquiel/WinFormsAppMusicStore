using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms.Dtos
{
    internal class AudioFileSelectPlayed
    {
        public bool Played { get; set; }

        public bool Select { get; set; }

        public int Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public int StoreId { get; set; }

        public string Path { get; set; }

        public double Size { get; set; }

        public TimeSpan Duration { get; set; }

        public bool CheckForTime { get; set; }

        public TimeSpan TimeToPlay { get; set; }
    }
}
