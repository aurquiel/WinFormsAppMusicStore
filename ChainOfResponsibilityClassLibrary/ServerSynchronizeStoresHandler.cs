using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;

namespace ChainOfResponsibilityClassLibrary
{
    public class ServerSynchronizeStoresHandler : Handler
    {
        private IServices _services;
        private IFileManager _fileManager;
        private EventHandler<string> _updateLabelMessage;
        private EventHandler<List<AudioFileDTO>> _getAudioListFiles;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private CancellationToken _token;

        public ServerSynchronizeStoresHandler(
            IServices services, 
            IFileManager fileManager,
            EventHandler<string> updateLabelMessage,
            EventHandler<List<AudioFileDTO>> getAudioListFiles,
            EventHandler<(bool, string)> raiseRichTextInsertMessage, 
            CancellationToken token)
        {
            _services = services;
            _fileManager = fileManager;
            _updateLabelMessage = updateLabelMessage;
            _getAudioListFiles = getAudioListFiles;
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
            _token = token;
        }

        public override async Task HandleRequest(OperationTypes.OPERATIONS operation)
        {
            if (operation == OperationTypes.OPERATIONS.SERVER_SYNCHRONIZE_STORES)
            {
                _updateLabelMessage?.Invoke(this, "Sincronizando tiendas");
                await Task.Delay(100);
                var result = await _services.AudioService.SynchronizeAudioListAllStoreAsync(_token);
                _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                if (_token.IsCancellationRequested)
                {
                    _raiseRichTextInsertMessage?.Invoke(this, (false, "Operacion Cancelada."));
                    return;
                }
            }
            else if (successor != null)
            {
                await successor.HandleRequest(operation);
            }
        }
    }
}
