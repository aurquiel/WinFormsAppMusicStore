using ClassLibraryDomain.Models;

namespace ClassLibraryDomain.Ports.Driven
{
    public interface IAudioPersistencePort
    {
        Task<GeneralAnswer<object>> DownloadAudioServerAsync(string audioName, CancellationToken token);
    }
}
