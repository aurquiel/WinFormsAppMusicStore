using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters.Entities
{
    public class UserAccessDto
    {
        public UserDto user { get; set; }
        public string? token { get; set; }
    }
}
