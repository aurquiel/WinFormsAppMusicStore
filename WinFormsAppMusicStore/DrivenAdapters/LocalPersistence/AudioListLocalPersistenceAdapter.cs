using AutoMapper;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence.Entities;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence
{
    public class AudioListLocalPersistenceAdapter : IAudioListLocalPersistencePort
    {
        private readonly IMapper _mapper;
        private readonly AudioStoreLocalDbContext _audioStoreLocalDbContext;

        public AudioListLocalPersistenceAdapter(IMapper mapper,AudioStoreLocalDbContext audioStoreLocalDbContext)
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

        public async Task DeleteAudioListAsync(int storeId)
        {
            List<AudioListEntity> enitites = await _audioStoreLocalDbContext.AudioListEntity.Where(x => x.StoreId == storeId).ToListAsync();
            _audioStoreLocalDbContext.RemoveRange(enitites);
            await _audioStoreLocalDbContext.SaveChangesAsync();
        }

        public async Task<List<AudioFile>> GetAudioListAsync(int storeId)
        {
            return _mapper.Map<List<AudioListEntity>, List<AudioFile>>(await _audioStoreLocalDbContext.AudioListEntity.Where(x => x.StoreId == storeId).ToListAsync());
        }
    }
}
