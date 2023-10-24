using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters.Entities
{
    public class UserDto
    {
        public int id { get; set; }

        public string? alias { get; set; }

        public string? password { get; set; }

        public int storeId { get; set; }

        public string? rol { get; set; }

        public DateTime creationDateTime { get; set; }
    }
}
