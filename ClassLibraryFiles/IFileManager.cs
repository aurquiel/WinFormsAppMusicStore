using ClassLibraryModels;

namespace ClassLibraryFiles
{
    public interface IFileManager
    {
        void CreateDictories();
        string GetAudioListPath();
        string GetAudioPath();

        Task<GeneralAnswer<List<AudioOperation>>> GetAudioList();
        GeneralAnswer<object> WriteAudioListToBinaryFile(string audioList);
        GeneralAnswer<List<string>> ReadAudioListFromBinaryFile();
        void EraseAudiosNotInAudioList(List<string> audioList);
        List<string> GetAudioListToDownload(List<string> listAudioFormServer);
    }
}