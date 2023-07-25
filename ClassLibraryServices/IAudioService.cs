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
        public Task<GeneralAnswer<string>> DownloadAudioListServer();
        public Task<GeneralAnswer<string>> DownloadAudioListStore(string storeCode);
        public Task<GeneralAnswer<string>> SynchronizeAudioListStore(string audioList, string storeCode);
        public Task<GeneralAnswer<object>> SynchronizeAudioListAllStore();
        public Task<GeneralAnswer<object>> UploadAudioServer(string filePath);
        public Task<GeneralAnswer<object>> DownloadAudioServer(string storeCode, string audioName);
        public Task<GeneralAnswer<object>> DeleteAudioServer(string audioName);
    }
}
