using ClassLibraryModels;

namespace ClassLibraryFiles
{
    public interface IFileManager
    {
        string GetAudioStorePath();
        void CreateDictoryAndFile();

        GeneralAnswer<object> WriteAudioListToBinaryFile(string audioList);
        GeneralAnswer<List<string>> ReadAudioListFromBinaryFile();
        void EraseAudiosNotInAudioList(List<string> audioList);
        List<string> GetAudioListToDownload(List<string> listAudioFormServer);
    }
}