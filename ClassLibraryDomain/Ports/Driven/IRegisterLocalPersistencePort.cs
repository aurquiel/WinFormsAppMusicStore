using ClassLibraryDomain.Models;

namespace ClassLibraryDomain.Ports.Driven
{
    public interface IRegisterLocalPersistencePort
    {
        public Task InsertRegisterLocal(Register register);
        public Task<List<Register>> GetUncommitedRegisterLocal();
        public Task MarkCommitedRegisterLocal();
        public Task DeletePeriodicallyRegisterLocal();
    }
}
