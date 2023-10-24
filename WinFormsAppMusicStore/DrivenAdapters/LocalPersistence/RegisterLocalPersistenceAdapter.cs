using AutoMapper;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using Microsoft.EntityFrameworkCore;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence.Entities;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence
{
    public class RegisterLocalPersistenceAdapter : IRegisterLocalPersistencePort
    {
        private readonly IMapper _mapper;
        private readonly AudioStoreLocalDbContext _audioStoreLocalDbContext;

        public RegisterLocalPersistenceAdapter(IMapper mapper, AudioStoreLocalDbContext audioStoreLocalDbContext)
        {
            _mapper = mapper;
            _audioStoreLocalDbContext = audioStoreLocalDbContext;
        }

        public async Task DeletePeriodicallyRegisterLocal()
        {
            var registersToDelete = await _audioStoreLocalDbContext.RegisterEntity.Where(x => x.CreationDateTime <= DateTime.Now.AddDays(-60)).ToListAsync();
            _audioStoreLocalDbContext.RegisterEntity.RemoveRange(registersToDelete);
            await _audioStoreLocalDbContext.SaveChangesAsync();
        }

        public async Task<List<Register>> GetUncommitedRegisterLocal()
        {
            var registersUncommited = await _audioStoreLocalDbContext.RegisterEntity.Where(x => x.Commited == false).ToListAsync();
            return _mapper.Map<List<RegisterEntity>, List<Register>>(registersUncommited);
        }

        public async Task InsertRegisterLocal(Register register)
        {
            await _audioStoreLocalDbContext.RegisterEntity.AddRangeAsync(_mapper.Map<RegisterEntity>(register));
            await _audioStoreLocalDbContext.SaveChangesAsync();
        }

        public async Task MarkCommitedRegisterLocal()
        {
            var registersUncommited = await _audioStoreLocalDbContext.RegisterEntity.Where(x => x.Commited == false).ToListAsync();
            registersUncommited.ForEach(x => x.Commited = true);
            _audioStoreLocalDbContext.RegisterEntity.UpdateRange(registersUncommited);
            await _audioStoreLocalDbContext.SaveChangesAsync();
        }
    }
}