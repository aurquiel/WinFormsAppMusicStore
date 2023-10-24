using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Ports.Driven
{
    public interface IFileManagerPersistencePort
    {
        string GetAudioStoreAdminPath();
        string GetLocalDbPath();
        void CopyLocalDbIfNotExist();
        void CreateDictoryOfStore();

        void EraseAudiosNotInAudioList(List<string> audioList);
        List<string> GetAudioListToDownload(List<string> audioList);
    }
}
