using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryServices
{
    public interface IRegisterService
    {
        Task<GeneralAnswer<List<Register>>> GetRegisters(int storeId, DateTime dateInit, DateTime dateEnd);
        Task<GeneralAnswer<List<Register>>> GetRegisterByMonth(int storeId, DateTime date);
        Task<GeneralAnswer<object>> RegisterInsert(Register register);
        Task<GeneralAnswer<object>> RegisterDelete(int storeId);
    }
}
