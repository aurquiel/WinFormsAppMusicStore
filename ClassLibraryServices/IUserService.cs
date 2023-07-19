using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryServices
{
    public interface IUserService
    {
        public Task<GeneralAnswer<UserToken>> UserAccess(UserAccess userAccess);
        public Task<GeneralAnswer<List<User>>> UserGetAll();
        public Task<GeneralAnswer<object>> UserCrete(User user);
        public Task<GeneralAnswer<object>> UserUpdate(User user);
        public Task<GeneralAnswer<object>> UserDelete(User user);
    }
}
