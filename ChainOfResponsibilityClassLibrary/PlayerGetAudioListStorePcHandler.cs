using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;

namespace ChainOfResponsibilityClassLibrary
{
    public class PlayerGetAudioListStorePcHandler : Handler
    {
        private IServices _services;
        private IFileManager _fileManager;
        private EventHandler<string> _updateLabelMessage;
        private EventHandler<List<AudioFileDTO>> _getAudioListFiles;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private CancellationToken _token;
        private string _storeCode;

        public PlayerGetAudioListStorePcHandler(
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
            if (operation == OperationTypes.OPERATIONS.PLAYER_GET_AUDIO_LIST_STORE_PC)
            {
                _updateLabelMessage?.Invoke(this, $"Espere obteniendo lista local de audio de la tienda {_storeCode}.");
                await Task.Delay(100);
                var result = _fileManager.ReadAudioListFromBinaryFile();
                _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                if (result.status)
                {
                    _getAudioListFiles?.Invoke(this, result.data);
                }
            }
            else if (successor != null)
            {
                await successor.HandleRequest(operation);
            }
        }
    }
}
