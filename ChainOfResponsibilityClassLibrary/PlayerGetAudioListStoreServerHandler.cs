using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;

namespace ChainOfResponsibilityClassLibrary
{
    public class PlayerGetAudioListStoreServerHandler : Handler
    {
        private IServices _services;
        private IFileManager _fileManager;
        private EventHandler<string> _updateLabelMessage;
        private EventHandler<List<AudioFileDTO>> _getAudioListFiles;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private CancellationToken _token;
        private string _storeCode;

        public PlayerGetAudioListStoreServerHandler(
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
            if (operation == OperationTypes.OPERATIONS.PLAYER_GET_AUDIO_LIST_STORE_SERVER)
            {
                _updateLabelMessage?.Invoke(this, "Espere obteniendo lista de audio de la tienda.");
                await Task.Delay(100);
                var result = await _services.AudioService.DownloadAudioListStoreAsync(_storeCode, _token);
                _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                if (result.status)
                {
                    if(result.data.Count() == 0 )
                    {
                        _getAudioListFiles?.Invoke(this, new List<AudioFileDTO>());
                    }
                    else
                    {
                        _fileManager.WriteAudioListToBinaryFile(AudioFileDTO.TransformToDTO(result.data));

                        var listToDowmload = _fileManager.GetAudioListToDownload(result.data.Select(x => x.name).ToList());
                        if (listToDowmload.Count() > 0)
                        {
                            foreach (var audio in listToDowmload)
                            {
                                await Task.Delay(300);
                                _updateLabelMessage?.Invoke(this, $"Descargando audio: {audio}.");
                                var resultDownload = await _services.AudioService.DownloadAudioServer(_storeCode, audio, _token);
                                _raiseRichTextInsertMessage?.Invoke(this, (resultDownload.status, resultDownload.statusMessage));
                                if (_token.IsCancellationRequested)
                                {
                                    return;
                                }
                            }
                        }

                        _updateLabelMessage?.Invoke(this, "Eliminando audios innecesarios.");
                        await Task.Delay(100);
                        _fileManager.EraseAudiosNotInAudioList(result.data.Select(x => x.name).ToList());

                        _getAudioListFiles?.Invoke(this, AudioFileDTO.TransformToDTO(result.data).Select(x => new AudioFileDTO { name = x.name, duration = x.duration, size = x.size, path = $"{_fileManager.GetAudioStorePath()}\\audio\\{x.name}" }).ToList());
                    }
                }
                else if (successor != null)
                {
                    await successor.HandleRequest(operation);
                }
            }
        }
    }
}
