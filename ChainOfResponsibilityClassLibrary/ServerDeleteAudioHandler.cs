using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;

namespace ChainOfResponsibilityClassLibrary
{
    public class ServerDeleteAudioHandler : Handler
    {
        private IServices _services;
        private IFileManager _fileManager;
        private EventHandler<string> _updateLabelMessage;
        private EventHandler<List<AudioFileDTO>> _getAudioListFiles;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private CancellationToken _token;
        private List<AudioFileDTO> _audioFileListToDelete;

        public ServerDeleteAudioHandler(
            IServices services, 
            IFileManager fileManager,
            EventHandler<string> updateLabelMessage,
            EventHandler<List<AudioFileDTO>> getAudioListFiles,
            EventHandler<(bool, string)> raiseRichTextInsertMessage, 
            CancellationToken token,
            List<AudioFileDTO> audioFileListToDelete)
        {
            _services = services;
            _fileManager = fileManager;
            _updateLabelMessage = updateLabelMessage;
            _getAudioListFiles = getAudioListFiles;
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
            _token = token;
            _audioFileListToDelete = audioFileListToDelete;
        }

        public override async Task HandleRequest(OperationTypes.OPERATIONS operation)
        {
            if (operation == OperationTypes.OPERATIONS.SERVER_DELETE_AUDIO)
            {
                _updateLabelMessage?.Invoke(this, "Eliminando audios del servidor");
                await Task.Delay(100);
                foreach (AudioFileDTO file in _audioFileListToDelete)
                {
                    _updateLabelMessage?.Invoke(this, "Eliminando audio: " + file.name);
                    var result = await _services.AudioService.DeleteAudioServerAsync(file.name, _token);
                    _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                    if (_token.IsCancellationRequested)
                    {
                        _raiseRichTextInsertMessage?.Invoke(this, (false, "Operacion Cancelada."));
                        return;
                    }
                }
            }
            else if (successor != null)
            {
                await successor.HandleRequest(operation);
            }
        }
    }
}
