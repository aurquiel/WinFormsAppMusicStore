using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryServices.WebService
{
    public class WebServiceParams
    {
        public readonly string _IP_WEB_SERVICE;
        public readonly int _TIMEOUT_WEB_SERVICE;
        public readonly int _TIMEOUT_WEB_SERVICE_HEAVY_TASK;
        public string _TOKEN_WEB_SERVICE = string.Empty;

        public WebServiceParams(string IP_WEB_SERVICE, int TIMEOUT_WEB_SERVICE, int TIMEOUT_WEB_SERVICE_HEAVY_TASK)
        {
            _IP_WEB_SERVICE = IP_WEB_SERVICE;
            _TIMEOUT_WEB_SERVICE = TIMEOUT_WEB_SERVICE;
            _TIMEOUT_WEB_SERVICE_HEAVY_TASK = TIMEOUT_WEB_SERVICE_HEAVY_TASK;
        }

        public void SetToken(string token)
        {
            _TOKEN_WEB_SERVICE = token;
        }
    }
}
