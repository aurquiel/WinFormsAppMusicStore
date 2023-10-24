using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using ClassLibraryDomain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.UsesCases
{
    public class AudioUseCase : IAudioDriving
    {
        private readonly IAudioPersistencePort _audioPersistencePort;

        public AudioUseCase(IAudioPersistencePort audioPersistencePort)
        {
            _audioPersistencePort = audioPersistencePort;
        }

        public async Task<GeneralAnswer<object>> DownloadAudioServerAsync(string audioName, CancellationToken token)
        {
            return await _audioPersistencePort.DownloadAudioServerAsync(audioName, token);
        }
    }
}
