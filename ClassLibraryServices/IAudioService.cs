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
        public Task<GeneralAnswer<List<AudioFile>>> DownloadAudioListServerAsync(CancellationToken token);
        public Task<GeneralAnswer<List<AudioFile>>> DownloadAudioListStoreAsync(string storeCode, CancellationToken token);
        public Task<GeneralAnswer<object>> SynchronizeAudioListStore(List<AudioFile> audioList, string storeCode, CancellationToken token);
        public Task<GeneralAnswer<object>> SynchronizeAudioListAllStoreAsync(CancellationToken token);
        public Task<GeneralAnswer<object>> UploadAudioServerAsync(string filePath, CancellationToken token);
        public Task<GeneralAnswer<object>> DownloadAudioServer(string storeCode, string audioName, CancellationToken token);
        public Task<GeneralAnswer<object>> DeleteAudioServerAsync(string audioName, CancellationToken token);
    }
}
