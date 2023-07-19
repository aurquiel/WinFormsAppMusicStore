using ClassLibraryFiles;
using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassLibraryServices.WebService
{
    internal class AudioHttp
    {
        internal static async Task<(bool, string, GeneralAnswer<string>)> AduioGetAudioList(WebServiceParams _params)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;

                var client = new HttpClient(handler);

                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);

                var response = await client.GetAsync(_params._IP_WEB_SERVICE + $"api/Audio/DownloadAudioList");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                    true,
                    "Archivo lista de audio obtenido del servidor.",
                    JsonSerializer.Deserialize<GeneralAnswer<string>>(result));
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
        
        internal static async Task<(bool, string, GeneralAnswer<object>)> SynchronizeAudioList(WebServiceParams _params, string textFile)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;

                var client = new HttpClient(handler);

                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);

                string json = JsonSerializer.Serialize(textFile);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_params._IP_WEB_SERVICE + $"api/Audio/SynchronizeAudioList", data);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return (
                    true,
                    "Archivo lista de audio sincronizado en el servidor.",
                    null);
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

        internal static async Task<(bool, string, GeneralAnswer<object>)> UploadAudio(WebServiceParams _params, string pathFile)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;

                var client = new HttpClient(handler);

                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);

                var form = new MultipartFormDataContent();

                byte[] fileData = File.ReadAllBytes(pathFile);

                ByteArrayContent byteContent = new ByteArrayContent(fileData);

                byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                form.Add(byteContent, "file", Path.GetFileName(pathFile));

                var response = await client.PostAsync(_params._IP_WEB_SERVICE + $"api/Audio/UploadAudio",form);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return (
                    true,
                    "Audio subido con exito al servidor servidor.",
                    null);
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

        internal static async Task<(bool, string, object)> DownloadAudio(WebServiceParams _params, string audioName, IFileManager fileManager)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;

                var client = new HttpClient(handler);

                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);

                var response = await client.GetAsync(_params._IP_WEB_SERVICE + $"api/Audio/DownloadAudio/" + audioName);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Stream streamToReadFrom = await response.Content.ReadAsStreamAsync();
                    using (var fs = new FileStream(fileManager.GetAudioPath() + audioName, FileMode.Create))
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
