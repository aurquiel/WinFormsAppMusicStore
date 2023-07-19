using ClassLibraryFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryServices.WebService
{
    public class WebService : IServices
    {
        public IUserService UserService { get; set; }
        public IAudioService AudioService { get; set; }
        public IStoreService StoreService { get; set; }
        public IRegisterService RegisterService { get; set; }

        private WebServiceParams _webParams;
        private IFileManager _fileManager;

        public WebService(string IP_WEB_SERVICE, int TIMEOUT_WEB_SERVICE, IFileManager fileManager)
        {
            _webParams = new WebServiceParams(IP_WEB_SERVICE, TIMEOUT_WEB_SERVICE);
            UserService = new UserServiceHttp(_webParams);
            _fileManager = fileManager;
        }

        public void SetToken(string token)
        {
            _webParams.SetToken(token);
            UserService = new UserServiceHttp(_webParams);
            StoreService = new StoreServiceHttp(_webParams);    
            AudioService = new AudioServiceHttp(_webParams, _fileManager);
            RegisterService = new RegisterServiceHttp(_webParams);
        }
    }
}
