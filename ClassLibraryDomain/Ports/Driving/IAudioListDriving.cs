using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Ports.Driving
{
    public interface IAudioListDriving
    {
        Task<GeneralAnswer<List<AudioFile>>> DownloadAudioListStoreAsync(int storeId, CancellationToken token);
    }
}
