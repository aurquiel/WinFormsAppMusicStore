using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryServices
{
    public interface IServices
    {
        public IAudioService AudioService { get; set; }
        public IStoreService StoreService { get; set; }
        public IUserService UserService { get; set; }
        public IRegisterService RegisterService { get; set; }
    }
}
