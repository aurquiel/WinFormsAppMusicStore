using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibraryServices.WebService
{
    internal class RegisterServiceHttp : IRegisterService
    {
        private WebServiceParams _webParams;

        public RegisterServiceHttp(WebServiceParams webParams)
        {
            _webParams = webParams;
        }

        public async Task<GeneralAnswer<List<Register>>> GetRegisterByMonth(int storeId, DateTime date)
        {
            var result = await RegisterHttp.RegisterGetByMonth(_webParams, storeId, date);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<List<Register>>(result.Item3.status, result.Item3.statusMessage, result.Item3.data);
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<List<Register>>(result.Item1, result.Item2, null);
            }
        }

        public async Task<GeneralAnswer<List<Register>>> GetRegisters(int storeId, DateTime dateInit, DateTime dateEnd)
        {
            var result = await RegisterHttp.RegisterGet(_webParams, storeId, dateInit, dateEnd);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<List<Register>>(result.Item3.status, result.Item3.statusMessage, result.Item3.data);
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<List<Register>>(result.Item1, result.Item2, null);
            }
        }

        public async Task<GeneralAnswer<object>> RegisterDelete(int storeId)
        {
            var result = await RegisterHttp.RegisterDelete(_webParams, storeId);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<object>(result.Item3.status, result.Item3.statusMessage, result.Item3.data);
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<object>(result.Item1, result.Item2, null);
            }
        }

        public async Task<GeneralAnswer<object>> RegisterInsert(Register register)
        {
            var result = await RegisterHttp.RegisterPost(_webParams, register);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<object>(result.Item3.status, result.Item3.statusMessage, result.Item3.data);
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<object>(result.Item1, result.Item2, null);
            }
        }
    }
}
