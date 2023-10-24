using AutoMapper;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters.Entities;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters
{
    public class RegisterPersistenceAdapter : IRegisterPersistencePort
    {
        private readonly IMapper _mapper;

        public RegisterPersistenceAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }
       
        public async Task<GeneralAnswer<object>> InsertAsync(List<Register> registers)
        {
            var result = await RegisterPost(registers);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<object>(result.Item3.status, result.Item3.statusMessage, result.Item3.data);
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<object>(result.Item1, result.Item2, null);
            }
        }

        private async Task<(bool, string, GeneralAnswer<object>)> RegisterPost(List<Register> registers)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;
                var client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(WebServiceParams.TIMEOUT_WEB_SERVICE);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", WebServiceParams.TOKEN_WEB_SERVICE);
                string json = JsonSerializer.Serialize(registers);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(WebServiceParams.IP_WEB_SERVICE + "api/Register/RegisterInsert/", data);

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
                    new GeneralAnswer<object>());
            }
        }
    }
}
