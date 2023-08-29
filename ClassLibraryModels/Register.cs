using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModels
{
    public class Register
    {
        public int id { get; set; }
        public int storeId { get; set; }
        public int activity { get; set; }
        public string message { get; set; }
        public DateTime creationDateTime { get; set; }
    }
}
