using AutoMapper;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters.Entities;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters
{
    public class AudioListPersistenceAdapter : IAudioListPersistencePort
    {
        private readonly IMapper _mapper;

        public AudioListPersistenceAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<GeneralAnswer<List<AudioFile>>> DownloadAudioListStoreAsync(int storeId, CancellationToken token)
        {
            var result = await GetAudioListStore(storeId, token);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<List<AudioFile>>(result.Item3.status, result.Item3.statusMessage, _mapper.Map<List<AudioFileDto>, List<AudioFile>>(result.Item3.data));
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<List<AudioFile>>(result.Item1, result.Item2, null);
            }
        }

        private async Task<(bool, string, GeneralAnswer<List<AudioFileDto>>)> GetAudioListStore(int storeId, CancellationToken token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;
                var httpClient = new HttpClient(handler);
                httpClient.Timeout = TimeSpan.FromSeconds(WebServiceParams.TIMEOUT_WEB_SERVICE);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", WebServiceParams.TOKEN_WEB_SERVICE);

                var response = await httpClient.GetAsync(WebServiceParams.IP_WEB_SERVICE + $"api/AudioList/DownloadAudioListStore/{storeId}", token);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                        true,
                        "Archivo lista de audio obtenido del servidor.",
                        JsonSerializer.Deserialize<GeneralAnswer<List<AudioFileDto>>>(result));
                }
                else
                {
                    return (
                        false,
                        "Error al obtener archivo lista de audio. Estatus: " + response.StatusCode,
                        null);
                }
            }
            catch (Exception ex)
            {
                return (
                    false,
                    "Error al obtener archivo lista de audio, Excepcion: " + ex.Message,
                    null);
            }
        }
    }
}
