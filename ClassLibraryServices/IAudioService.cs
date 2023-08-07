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
        public Task<GeneralAnswer<string>> DownloadAudioListServer(CancellationToken token);
        public Task<GeneralAnswer<string>> DownloadAudioListStore(string storeCode, CancellationToken token);
        public Task<GeneralAnswer<string>> SynchronizeAudioListStore(string audioList, string storeCode, CancellationToken token);
        public Task<GeneralAnswer<object>> SynchronizeAudioListAllStore(CancellationToken token);
        public Task<GeneralAnswer<object>> UploadAudioServer(string filePath, CancellationToken token);
        public Task<GeneralAnswer<object>> DownloadAudioServer(string storeCode, string audioName, CancellationToken token);
        public Task<GeneralAnswer<object>> DeleteAudioServer(string audioName, CancellationToken token);
    }
}
