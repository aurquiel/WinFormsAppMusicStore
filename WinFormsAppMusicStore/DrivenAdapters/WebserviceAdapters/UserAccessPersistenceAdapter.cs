using AutoMapper;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using System.Text.Json;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters.Entities;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters
{
    public class UserAccessPersistenceAdapter : IUserAccessPersistencePort
    {
        private readonly IMapper _mapper;

        public UserAccessPersistenceAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<GeneralAnswer<UserAccess>> AcccesLoginTokenAsync(string alias, string password)
        {
            var result = await AcccesLoginTokenHttpAsync(alias, password);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<UserAccess>(result.Item3.status, result.Item3.statusMessage, _mapper.Map<UserAccess>(result.Item3.data));
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<UserAccess>(result.Item1, result.Item2, null);
            }
        }

        private async Task<(bool, string, GeneralAnswer<UserAccessDto>)> AcccesLoginTokenHttpAsync(string alias, string password)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = Certificate.ValidateServerCertificate;
                var client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(WebServiceParams.TIMEOUT_WEB_SERVICE);
                var response = await client.GetAsync(WebServiceParams.IP_WEB_SERVICE + $"api/Access/{alias}/{password}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (
                        true,
                        "Respuesta del servidor obtenida con exito.",
                        JsonSerializer.Deserialize<GeneralAnswer<UserAccessDto>>(result));
                }
                else
                {
                    return (
                        false,
                        "Error al obtener credenciales. Estatus: " + response.StatusCode,
                        null);
                }

            }
            catch (Exception ex)
            {
                return (
                    false,
                    "Error AcccesLoginTokenHttpAsync, Excepcion: " + ex.Message,
                    new GeneralAnswer<UserAccessDto>());
            }
        }
    }
}
