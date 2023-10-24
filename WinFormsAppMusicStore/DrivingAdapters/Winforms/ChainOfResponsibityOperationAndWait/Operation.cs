using ClassLibraryDomain.Models;
using WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms.Dtos;
using static WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms.ChainOfResponsibityOperationAndWait.OperationTypes;

namespace WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms.ChainOfResponsibityOperationAndWait
{
    public class Operation
    {
        public Operation(OPERATIONS typeOfOperation, int storeId, List<AudioFileSelect> audioFileToOperate)
        {
            TypeOfOperation = typeOfOperation;
            StoreId= storeId;
            AudioFileToOperate = audioFileToOperate;
        }

        public OPERATIONS TypeOfOperation { get; set; }
        public int StoreId { get; set; }
        public List<AudioFileSelect> AudioFileToOperate { get; set; }
    }
}
