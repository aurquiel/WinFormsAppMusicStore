using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;

namespace ChainOfResponsibilityClassLibrary
{
    public class StoreGetAudioListHandler : Handler
    {
        private IServices _services;
        private IFileManager _fileManager;
        private EventHandler<string> _updateLabelMessage;
        private EventHandler<List<AudioFileDTO>> _getAudioListFiles;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private CancellationToken _token;
        private string _storeCode;

        public StoreGetAudioListHandler(
            IServices services, 
            IFileManager fileManager,
            EventHandler<string> updateLabelMessage,
            EventHandler<List<AudioFileDTO>> getAudioListFiles,
            EventHandler<(bool, string)> raiseRichTextInsertMessage, 
            CancellationToken token,
            string storeCode)
        {
            _services = services;
            _fileManager = fileManager;
            _updateLabelMessage = updateLabelMessage;
            _getAudioListFiles = getAudioListFiles;
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
            _token = token;
            _storeCode = storeCode;
        }

        public override async Task HandleRequest(OperationTypes.OPERATIONS operation)
        {
            if (operation == OperationTypes.OPERATIONS.STORE_GET_AUDIO_LIST)
            {
                _updateLabelMessage?.Invoke(this, "Espere obteniendo lista de audio de la tienda.");
                await Task.Delay(100);
                var result = await _services.AudioService.DownloadAudioListStoreAsync(_storeCode,_token);
                _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                if (result.status)
                {
                    _getAudioListFiles?.Invoke(this, AudioFileDTO.TransformToDTO(result.data));
                }
            }
            else if (successor != null)
            {
                await successor.HandleRequest(operation);
            }
        }
    }
}
