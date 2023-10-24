using AutoMapper;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using Microsoft.EntityFrameworkCore;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence.Entities;
using WinFormsAppMusicStoreAdmin.Utils;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence
{
    public class UserAccessLocalPersistenceAdapter : IUserAccessLocalPersistencePort
    {
        private readonly IMapper _mapper;
        private readonly AudioStoreLocalDbContext _audioStoreLocalDbContext;

        public UserAccessLocalPersistenceAdapter(IMapper mapper,AudioStoreLocalDbContext audioStoreLocalDbContext)
        {
            _mapper = mapper;
            _audioStoreLocalDbContext = audioStoreLocalDbContext;
        }

        public async Task CreateAudioListAsync(List<AudioFile> audioList)
        {
            audioList.ForEach(x => x.Id = 0);
            await _audioStoreLocalDbContext.AudioListEntity.AddRangeAsync(_mapper.Map<List<AudioFile>, List<AudioListEntity>>(audioList));
            await _audioStoreLocalDbContext.SaveChangesAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _audioStoreLocalDbContext.UsersEntity.AddAsync(_mapper.Map<UserEntity>(user));
            await _audioStoreLocalDbContext.SaveChangesAsync();
        }

        public async Task DeleteUsersAsync()
        {
            _audioStoreLocalDbContext.UsersEntity.RemoveRange(_audioStoreLocalDbContext.UsersEntity);
            await _audioStoreLocalDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(string alias, string password)
        {
            var user = await _audioStoreLocalDbContext.UsersEntity.Where(x => x.Alias == alias && x.Password == Hash256.HashOfUserPassword(password)).FirstOrDefaultAsync();
            return _mapper.Map<User>(user);
        }
    }
}
