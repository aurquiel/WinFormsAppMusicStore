using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using ClassLibraryDomain.Ports.Driving;

namespace ClassLibraryDomain.UsesCases
{
    public class FileManagerUseCase : IFileManagerDriving
    {
        private readonly IFileManagerPersistencePort _fileManagerPersistencePort;

        public FileManagerUseCase(IFileManagerPersistencePort fileManagerPersistencePort)
        {
            _fileManagerPersistencePort = fileManagerPersistencePort;
        }

        public void CopyLocalDbIfNotExistOrCorrupted()
        {
            _fileManagerPersistencePort.CopyLocalDbIfNotExistOrCorrupted();
        }

        public string GetLocalDbPath()
        {
            return _fileManagerPersistencePort.GetLocalDbPath();
        }

        public void CreateDirectoryAndFile()
        {
            _fileManagerPersistencePort.CreateDictoryOfStore();
        }

        public void EraseAudiosNotInAudioList(List<string> audioList)
        {
            _fileManagerPersistencePort.EraseAudiosNotInAudioList(audioList);
        }

        public List<string> GetAudioListToDownload(List<string> audioList)
        {
            return _fileManagerPersistencePort.GetAudioListToDownload(audioList);
        }

        public string GetAudioStoreAdminPath()
        {
            return _fileManagerPersistencePort.GetAudioStoreAdminPath();
        }
    }
}
