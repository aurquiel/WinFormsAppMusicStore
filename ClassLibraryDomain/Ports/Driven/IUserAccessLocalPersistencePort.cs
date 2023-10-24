using ClassLibraryDomain.Models;

namespace ClassLibraryDomain.Ports.Driven
{
    public interface IUserAccessLocalPersistencePort
    {
        public Task CreateUserAsync(User user);
        public Task<User> GetUserAsync(string alias, string password);
        public Task DeleteUsersAsync();
    }
}
