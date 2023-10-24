using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using ClassLibraryDomain.Ports.Driving;

namespace ClassLibraryDomain.UsesCases
{
    public class UserAccessUseCase : IUserAccessDriving
    {
        private readonly IUserAccessPersistencePort _userAccessPersstencePort;
        private readonly IUserAccessLocalPersistencePort _userAccessLocalPersistencePort;

        public UserAccessUseCase(IUserAccessPersistencePort userAccessPeristencePort, IUserAccessLocalPersistencePort userAccessLocalPersistencePort)
        {
            _userAccessPersstencePort = userAccessPeristencePort;
            _userAccessLocalPersistencePort = userAccessLocalPersistencePort;
        }

        public async Task<GeneralAnswer<UserAccess>> AcccesLoginOfflineAsync(string alias, string password)
        {
            User user = await _userAccessLocalPersistencePort.GetUserAsync(alias, password);

            if (user is not null)
            {
                return new GeneralAnswer<UserAccess>(true, $"Login offline exitoso. usuario {alias}", new UserAccess { user = user, token = string.Empty });
            }
            else
            {
                return new GeneralAnswer<UserAccess>(false, $"Login offline fallido. usuario: {alias}.", null);
            }
        }

        public async Task<GeneralAnswer<UserAccess>> AcccesLoginTokenAsync(string alias, string password)
        {
            try
            {
                GeneralAnswer<UserAccess> result = await _userAccessPersstencePort.AcccesLoginTokenAsync(alias, password);

                if (result.status == true)
                {
                    await _userAccessLocalPersistencePort.DeleteUsersAsync();
                    await _userAccessLocalPersistencePort.CreateUserAsync(result.data.user); 
                }

                return result;
            }
            catch (Exception ex)
            {
                return new GeneralAnswer<UserAccess>(false, $"Login fallido. usuario: {alias}. Excepcion: " + ex.Message, null);
            }
           
        }

    }
}
