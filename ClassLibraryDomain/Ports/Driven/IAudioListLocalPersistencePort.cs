using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Ports.Driven
{
    public interface IAudioListLocalPersistencePort
    {
        public Task CreateAudioListAsync(List<AudioFile> audioList);
        public Task DeleteAudioListAsync(int storeId);
        public Task<List<AudioFile>> GetAudioListAsync(int storeId);
    }
}
