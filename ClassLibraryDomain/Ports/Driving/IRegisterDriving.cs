using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Ports.Driving
{
    public interface IRegisterDriving
    {
        Task<GeneralAnswer<object>> InsertAsync(Register register);
        Task<GeneralAnswer<object>> DeleteOldLocalRegistersAsync();
    }
}
