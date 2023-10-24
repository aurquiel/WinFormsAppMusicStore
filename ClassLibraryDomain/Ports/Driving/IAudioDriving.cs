using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Ports.Driving
{
    public interface IAudioDriving
    {
        Task<GeneralAnswer<object>> DownloadAudioServerAsync(string audioName, CancellationToken token);
    }
}
