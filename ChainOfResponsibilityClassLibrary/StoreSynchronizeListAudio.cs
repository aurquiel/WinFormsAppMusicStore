using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;

namespace ChainOfResponsibilityClassLibrary
{
    public class StoreSynchronizeListAudio : Handler
    {
        private IServices _services;
        private IFileManager _fileManager;
        private EventHandler<string> _updateLabelMessage;
        private EventHandler<List<AudioFileDTO>> _getAudioListFiles;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private CancellationToken _token;
        private List<AudioFileDTO> _audioFileListToSynchronize;
        private string _storeCode;

        public StoreSynchronizeListAudio(
            IServices services,
            IFileManager fileManager,
            EventHandler<string> updateLabelMessage,
            EventHandler<List<AudioFileDTO>> getAudioListFiles,
            EventHandler<(bool, string)> raiseRichTextInsertMessage,
            CancellationToken token,
            List<AudioFileDTO> audioFileListToSynchronize,
            string storeCode)
        {
            _services = services;
            _fileManager = fileManager;
            _updateLabelMessage = updateLabelMessage;
            _getAudioListFiles = getAudioListFiles;
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
            _token = token;
            _audioFileListToSynchronize = audioFileListToSynchronize;
            _storeCode = storeCode;
        }

        public override async Task HandleRequest(OperationTypes.OPERATIONS operation)
        {
            if (operation == OperationTypes.OPERATIONS.STORE_SYNCHRONIZE_LIST_AUDIO)
            {
                _updateLabelMessage?.Invoke(this, $"Subiendo y sincronizando lista de audio de tienda: {_storeCode}");
                var result = await _services.AudioService.SynchronizeAudioListStore(AudioFileDTO.TransformFromDTO(_audioFileListToSynchronize), _storeCode, _token);
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
