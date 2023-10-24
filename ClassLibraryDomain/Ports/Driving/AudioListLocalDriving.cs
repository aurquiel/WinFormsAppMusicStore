using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Ports.Driving
{
    public interface IAudioListLocalDriving
    {
        public Task CreateAudioListAsync(List<AudioFile> audioList);
        public Task DeleteAudioListAsync(int storeId);
        public Task<List<AudioFile>> GetAudioListAsync(int storeId);
    }
}
