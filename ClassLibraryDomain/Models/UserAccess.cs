using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Models
{
    public class UserAccess
    {
        public User user { get; set; }
        public string? token { get; set; }
    }
}
