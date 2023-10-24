using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Ports.Driven
{
    public interface IRegisterPersistencePort
    {
        Task<GeneralAnswer<object>> InsertAsync(List<Register> registers);
    }
}
