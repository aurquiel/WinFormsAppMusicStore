using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModels
{
    public class AudioOperation
    {
        public enum OPERATIONS { NONE = 0, UPLOAD = 1}

        public string? Name { get; set; }
        public OPERATIONS Operation { get; set; } = OPERATIONS.NONE;
        public string? PathFileAudio { get; set; } = string.Empty;
    }
}
