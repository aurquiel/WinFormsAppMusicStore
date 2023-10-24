using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters.Entities
{
    public class RegisterDto
    {
        public int iId { get; set; }

        public int storeId { get; set; }

        public int activity { get; set; }

        public string? message { get; set; }

        public DateTime creationDateTime { get; set; }
    }
}
