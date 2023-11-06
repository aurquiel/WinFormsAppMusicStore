using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Ports.Driving
{
    public interface IFileManagerDriving
    {
        string GetAudioStoreAdminPath();
        string GetLocalDbPath();
        void CopyLocalDbIfNotExistOrCorrupted();
        void CreateDirectoryAndFile();

        void EraseAudiosNotInAudioList(List<string> audioList);
        List<string> GetAudioListToDownload(List<string> audioList);
    }
}
