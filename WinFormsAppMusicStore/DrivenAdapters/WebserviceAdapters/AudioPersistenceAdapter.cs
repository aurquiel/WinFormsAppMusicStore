using AutoMapper;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters.Entities;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters
{
    public class AudioPersistenceAdapter : IAudioPersistencePort
    {
        private readonly IMapper _mapper;
        private readonly IFileManagerPersistencePort _fileManagerPersistencePort;

        public AudioPersistenceAdapter(IMapper mapper, IFileManagerPersistencePort fileManagerPersistencePort)
        {
            _mapper = mapper;
            _fileManagerPersistencePort = fileManagerPersistencePort;
        }

        public async Task<GeneralAnswer<object>> DownloadAudioServerAsync(string audioName, CancellationToken token)
        {
            var result = await DownloadAudio(audioName, token);

            return new GeneralAnswer<object>(result.Item1, result.Item2, null);
        }

        private async Task<(bool, string, object)> DownloadAudio(string audioName, CancellationToken token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;

                var client = new HttpClient(handler);

                client.Timeout = TimeSpan.FromSeconds(WebServiceParams.TIMEOUT_WEB_SERVICE_HEAVY_TASK);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", WebServiceParams.TOKEN_WEB_SERVICE);

                var response = await client.GetAsync(WebServiceParams.IP_WEB_SERVICE + $"api/Audio/DownloadAudio/" + audioName, token);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Stream streamToReadFrom = await response.Content.ReadAsStreamAsync();
                    var path = _fileManagerPersistencePort.GetAudioStoreAdminPath() + $"\\audio\\{audioName}";
                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        await response.Content.CopyToAsync(fs);
                    }

                    return (
                        true,
                        "Archivo de audio obtenido del servidor.",
                        null);
                }
                else
                {
                    return (
                        false,
                        "Error al obtener archivo de audio del servidor. Estatus: " + response.StatusCode,
                        null);
                }
            }
            catch (Exception ex)
            {
                return (
                    false,
                    "Error al obtener archivo de audio del servidor. Excepcion: " + ex.Message,
                    null);
            }
        }
    }
}
