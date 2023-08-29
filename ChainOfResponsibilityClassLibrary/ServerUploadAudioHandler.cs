using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;

namespace ChainOfResponsibilityClassLibrary
{
    public class ServerUploadAudioHandler : Handler
    {
        private IServices _services;
        private IFileManager _fileManager;
        private EventHandler<string> _updateLabelMessage;
        private EventHandler<List<AudioFileDTO>> _getAudioListFiles;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private CancellationToken _token;
        private List<AudioFileDTO> _audioFileListToUpload;

        public ServerUploadAudioHandler(
            IServices services,
            IFileManager fileManager,
            EventHandler<string> updateLabelMessage,
            EventHandler<List<AudioFileDTO>> getAudioListFiles,
            EventHandler<(bool, string)> raiseRichTextInsertMessage,
            CancellationToken token,
            List<AudioFileDTO> audioFileListToUpload)
        {
            _services = services;
            _fileManager = fileManager;
            _updateLabelMessage = updateLabelMessage;
            _getAudioListFiles = getAudioListFiles;
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
            _token = token;
            _audioFileListToUpload = audioFileListToUpload;
        }

        public override async Task HandleRequest(OperationTypes.OPERATIONS operation)
        {
            if (operation == OperationTypes.OPERATIONS.SERVER_UPLOAD_AUDIO)
            {
                _updateLabelMessage?.Invoke(this, "Subiendo audio al servidor");
                foreach (AudioFileDTO file in _audioFileListToUpload)
                {
                    _updateLabelMessage?.Invoke(this, "Subiendo audio: " + file.name);
                    var result = await _services.AudioService.UploadAudioServerAsync(file.path, _token);
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
