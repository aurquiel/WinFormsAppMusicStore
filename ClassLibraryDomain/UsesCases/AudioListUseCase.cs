using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using ClassLibraryDomain.Ports.Driving;

namespace ClassLibraryDomain.UsesCases
{
    public class AudioListUseCase : IAudioListDriving
    {
        private readonly IAudioListPersistencePort _audioListPersistencePort;

        public AudioListUseCase(IAudioListPersistencePort audioListPersistencePort)
        {
            _audioListPersistencePort = audioListPersistencePort;
        }

        public async Task<GeneralAnswer<List<AudioFile>>> DownloadAudioListStoreAsync(int storeId, CancellationToken token)
        {
            return await _audioListPersistencePort.DownloadAudioListStoreAsync(storeId, token);
        }
    }
}
