using ClassLibraryModels;

namespace ClassLibraryFiles
{
    public interface IFileManager
    {
        string GetAudioStorePath();
        void CreateDictoryAndFile();
        void DeleteDictory();

        GeneralAnswer<object> WriteAudioListToBinaryFile(List<AudioFileDTO> audioList);
        GeneralAnswer<List<AudioFileDTO>> ReadAudioListFromBinaryFile();
        void EraseAudiosNotInAudioList(List<string> audioList);
        List<string> GetAudioListToDownload(List<string> listAudioFormServer);
    }
}