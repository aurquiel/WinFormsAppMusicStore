using ClassLibraryFiles;
using ClassLibraryModels;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ClassLibraryServices.WebService
{
    internal class AudioHttp
    {
        internal static async Task<(bool, string, GeneralAnswer<List<AudioFile>>)> GetAudioList(WebServiceParams _params, CancellationToken token)
        {
            try
            {

                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;
                var httpClient = new HttpClient(handler);
                httpClient.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);

                var response = await httpClient.GetAsync(_params._IP_WEB_SERVICE + $"api/Audio/DownloadAudioListServer", token);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                        true,
                        "Archivo lista de audio obtenido del servidor.",
                        JsonSerializer.Deserialize<GeneralAnswer<List<AudioFile>>>(result));
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

        internal static async Task<(bool, string, GeneralAnswer<List<AudioFile>>)> GetAudioListStore(WebServiceParams _params, string storeCode, CancellationToken token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;
                var httpClient = new HttpClient(handler);
                httpClient.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);

                var response = await httpClient.GetAsync(_params._IP_WEB_SERVICE + $"api/Audio/DownloadAudioListStore/{storeCode}", token);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                        true,
                        "Archivo lista de audio obtenido del servidor.",
                        JsonSerializer.Deserialize<GeneralAnswer<List<AudioFile>>>(result));
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

        internal static async Task<(bool, string, GeneralAnswer<object>)> SynchronizeAudioListStore(WebServiceParams _params, List<AudioFile> audioList, string storeCode, CancellationToken token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;
                var client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);

                string json = JsonSerializer.Serialize(new SynchronizeAudioListStoreInfo { audioList = audioList, storeCode = storeCode });
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_params._IP_WEB_SERVICE + $"api/Audio/SynchronizeAudioListStore", data, token);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                        true,
                        "Archivo lista de audio sincronizado en el servidor.",
                        JsonSerializer.Deserialize<GeneralAnswer<object>>(result));
                }
                else
                {
                    return (
                        false,
                        "Error al sincronizar lista de audio. Estatus: " + response.StatusCode,
                        null);
                }
            }
            catch (Exception ex)
            {
                return (
                    false,
                    "Error al sincronizar lista de audio, Excepcion: " + ex.Message,
                    null);
            }
        }

        internal static async Task<(bool, string, GeneralAnswer<string>)> SynchronizeAudioListAllStore(WebServiceParams _params, CancellationToken token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;
                var client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);

                var response = await client.PostAsync(_params._IP_WEB_SERVICE + $"api/Audio/SynchronizeAudioListAllStore", null, token);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                        true,
                        "Listas de audio sincronizadas en el servidor.",
                        JsonSerializer.Deserialize<GeneralAnswer<string>>(result));
                }
                else
                {
                    return (
                        false,
                        "Error al sincronizar listas de audio en el servidor. Estatus: " + response.StatusCode,
                        null);
                }
            }
            catch (Exception ex)
            {
                return (
                    false,
                    "Error al sincronizar listas de audio en el servidor, Excepcion: " + ex.Message,
                    null);
            }
        }

        internal static async Task<(bool, string, GeneralAnswer<object>)> UploadAudio(WebServiceParams _params, string filePath, CancellationToken token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;
                var client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE_HEAVY_TASK);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);

                var form = new MultipartFormDataContent();

                byte[] fileData = File.ReadAllBytes(filePath);

                ByteArrayContent byteContent = new ByteArrayContent(fileData);

                byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                form.Add(byteContent, "file", Path.GetFileName(filePath));

                var response = await client.PostAsync(_params._IP_WEB_SERVICE + $"api/Audio/UploadAudio", form, token);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                        true,
                        "Audio subido con exito al servidor servidor.",
                        JsonSerializer.Deserialize<GeneralAnswer<object>>(result));
                }
                else
                {
                    return (
                        false,
                        "Error al subir audio al servidor. Estatus: " + response.StatusCode,
                        null);
                }
            }
            catch (Exception ex)
            {
                return (
                    false,
                    "Error al subir audio al servidor, Excepcion: " + ex.Message,
                    null);
            }
        }

        internal static async Task<(bool, string, object)> DownloadAudio(WebServiceParams _params, string storeCode, string audioName, IFileManager fileManager, CancellationToken token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;

                var client = new HttpClient(handler);

                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE_HEAVY_TASK);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);

                var response = await client.GetAsync(_params._IP_WEB_SERVICE + $"api/Audio/DownloadAudio/" + audioName, token);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Stream streamToReadFrom = await response.Content.ReadAsStreamAsync();
                    var path = fileManager.GetAudioStorePath() + $"\\audio\\{audioName}";
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

        internal static async Task<(bool, string, GeneralAnswer<string>)> DeleteAudio(WebServiceParams _params, string audioName, CancellationToken token)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(_params._IP_WEB_SERVICE + $"api/Audio/DeleteAudio"),
                    Content = new StringContent(JsonSerializer.Serialize(audioName), Encoding.UTF8, "application/json")
                };

                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;
                var client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);
                var response = await client.SendAsync(request, token);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                        true,
                        "Archivo de audio eliminado del servisor.",
                        JsonSerializer.Deserialize<GeneralAnswer<string>>(result));
                }
                else
                {
                    return (
                        false,
                        "Error al eliminar archivo de audio del servidor. Estatus: " + response.StatusCode,
                        null);
                }
            }
            catch (Exception ex)
            {
                return (
                    false,
                    "Error al eliminar archivo de audio del servidor, Excepcion: " + ex.Message,
                    null);
            }
        }
    }
}
