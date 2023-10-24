using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using ClassLibraryDomain.Ports.Driving;

namespace ClassLibraryDomain.UsesCases
{
    public class AudioListLocalUseCase : IAudioListLocalDriving
    {
        private readonly IAudioListLocalPersistencePort _audioListLocalPersistencePort;

        public AudioListLocalUseCase(IAudioListLocalPersistencePort audioListLocalPersistencePort)
        {
            _audioListLocalPersistencePort = audioListLocalPersistencePort;
        }

        public async Task CreateAudioListAsync(List<AudioFile> audioList)
        {
            await _audioListLocalPersistencePort.CreateAudioListAsync(audioList);
        }

        public async Task DeleteAudioListAsync(int storeId)
        {
            await _audioListLocalPersistencePort.DeleteAudioListAsync(storeId);
        }

        public async Task<List<AudioFile>> GetAudioListAsync(int storeId)
        {
            return await _audioListLocalPersistencePort.GetAudioListAsync(storeId);
        }
    }
}
