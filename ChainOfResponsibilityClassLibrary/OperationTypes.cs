using ClassLibraryModels;
using static ChainOfResponsibilityClassLibrary.OperationTypes;

namespace ChainOfResponsibilityClassLibrary
{
    public class Operation
    {
        public Operation(OPERATIONS typeOfOperation, string storeCode, List<AudioFileDTO> audioFileToOperate)
        {
            TypeOfOperation = typeOfOperation;
            StoreCode = storeCode;
            AudioFileToOperate = audioFileToOperate;
        }

        public OPERATIONS TypeOfOperation { get; set; }
        public string StoreCode { get; set; }
        public List<AudioFileDTO> AudioFileToOperate { get; set; }
    }


    public class OperationTypes
    {
        public enum OPERATIONS
        {
            NONE = 0, SERVER_GET_AUDIO_LIST = 1, SERVER_UPLOAD_AUDIO = 2, SERVER_DELETE_AUDIO = 3, SERVER_SYNCHRONIZE_STORES = 4,
            STORE_GET_AUDIO_LIST = 5, STORE_SYNCHRONIZE_LIST_AUDIO = 6, PLAYER_GET_AUDIO_LIST_STORE_PC = 7, PLAYER_GET_AUDIO_LIST_STORE_SERVER = 9,
            PLAYER_DOWMLOAD_AUDIO = 10
        }
    }
}
