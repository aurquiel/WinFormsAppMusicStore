using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryServices.WebService
{
    internal class UserServiceHttp : IUserService
    {
        private WebServiceParams _webParams;

        public UserServiceHttp(WebServiceParams webParams)
        {
            _webParams = webParams;
        }

        public async Task<GeneralAnswer<UserToken>> UserAccess(UserAccess userAccess)
        {
            var result = await UserAccessHttp.UserAccessPost(userAccess, _webParams);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<UserToken>(result.Item3.status,result.Item3.statusMessage, result.Item3.data);
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<UserToken>(result.Item1, result.Item2, null);
            }
        }

        public async Task<GeneralAnswer<List<User>>> UserGetAll()
        {
            var result = await UserHttp.UserGetAll(_webParams);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<List<User>>(result.Item3.status, result.Item3.statusMessage, result.Item3.data);
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<List<User>>(result.Item1, result.Item2, null);
            }
        }

        public async Task<GeneralAnswer<object>> UserCrete(User user)
        {
            var result = await UserHttp.UserPost(user, _webParams);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<object>(result.Item3.status, result.Item3.statusMessage, result.Item3.data);
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<object>(result.Item1, result.Item2, null);
            }
        }

        public async Task<GeneralAnswer<object>> UserUpdate(User user)
        {
            var result = await UserHttp.UserPut(user, _webParams);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<object>(result.Item3.status, result.Item3.statusMessage, result.Item3.data);
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<object>(result.Item1, result.Item2, null);
            }
        }

        public async Task<GeneralAnswer<object>> UserDelete(User user)
        {
            var result = await UserHttp.UserDelete(user, _webParams);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<object> (result.Item3.status, result.Item3.statusMessage, result.Item3.data);
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<object>(result.Item1, result.Item2, null);
            }
        }
    }
}
