using ClassLibraryModels;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ClassLibraryServices.WebService
{
    internal class RegisterHttp
    {
        internal static async Task<(bool, string, GeneralAnswer<List<Register>>)> RegisterGetByDate(WebServiceParams _params, DateTime date)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;

                var client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);
                var response = await client.GetAsync(_params._IP_WEB_SERVICE + $"api/Register/GetRegistersByDate/{date.ToString("dd-MM-yyyy")}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                    true,
                    "Respuesta del servidor obtenida con exito.",
                    JsonSerializer.Deserialize<GeneralAnswer<List<Register>>>(result));
                }
                else
                {
                    return (
                        false,
                        "Error al obtener Registros del servidor. Estatus: " + response.StatusCode,
                        null);
                }
            }
            catch (Exception ex)
            {
                return (
                    false,
                    "Error al obtener Registros del servidor, Excepcion: " + ex.Message,
                    new GeneralAnswer<List<Register>>());
            }
        }

        internal static async Task<(bool, string, GeneralAnswer<List<Register>>)> RegisterGet(WebServiceParams _params, int storeId, DateTime dateInit, DateTime dateEnd)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;

                var client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);
                var response = await client.GetAsync(_params._IP_WEB_SERVICE + $"api/Register/GetRegisters/{storeId}/{dateInit.ToString("dd-MM-yyyy")}/{dateEnd.ToString("dd-MM-yyyy")}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                    true,
                    "Respuesta del servidor obtenida con exito.",
                    JsonSerializer.Deserialize<GeneralAnswer<List<Register>>>(result));
                }
                else
                {
                    return (
                        false,
                        "Error al obtener Registros del servidor. Estatus: " + response.StatusCode,
                        null);
                }
            }
            catch (Exception ex)
            {
                return (
                    false,
                    "Error al obtener Registros del servidor, Excepcion: " + ex.Message,
                    new GeneralAnswer<List<Register>>());
            }
        }

        internal static async Task<(bool, string, GeneralAnswer<object>)> RegisterDelete(WebServiceParams _params, int storeId)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(_params._IP_WEB_SERVICE + $"api/Register/RegisterDelete/{storeId}"),
                };

                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;
                var client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);
                var response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                        true,
                        "Respuesta del servidor obtenida con exito.",
                        JsonSerializer.Deserialize<GeneralAnswer<object>>(result));
                }
                else
                {
                    return (
                        false,
                        "Error al eliminar Registros en el servidor. Estatus: " + response.StatusCode,
                        null);
                }
            }
            catch (Exception ex)
            {
                return (
                    false,
                    "Error al eliminar Registros en el servidor, Excepcion: " + ex.Message,
                    new GeneralAnswer<object>());
            }
        }

        internal static async Task<(bool, string, GeneralAnswer<object>)> RegisterPost(WebServiceParams _params, Register register)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;
                var client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(_params._TIMEOUT_WEB_SERVICE);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _params._TOKEN_WEB_SERVICE);
                string json = JsonSerializer.Serialize(register);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_params._IP_WEB_SERVICE + "api/Register/RegisterInsert/", data);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                        true,
                        "Respuesta del servidor obtenida con exito.",
                        JsonSerializer.Deserialize<GeneralAnswer<object>>(result));
                }
                else
                {
                    return (
                        false,
                        "Error al crear registro en el servidor. Estatus: " + response.StatusCode,
                        null);
                }

            }
            catch (Exception ex)
            {
                return (
                false,
                    "Error al crear registro en el servidor., Excepcion: " + ex.Message,
                    null);
            }
        }
    }
}
