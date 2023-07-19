using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryServices
{
    public interface IAudioService
    {
        public Task<GeneralAnswer<string>> DownloadAudioList();
        public Task<GeneralAnswer<object>> SynchronizeAudioList(string plainText);
        public Task<GeneralAnswer<object>> UploadAudio(string pathFile);
        public Task<GeneralAnswer<object>> DownloadAudio(string audioName);
    }
}
