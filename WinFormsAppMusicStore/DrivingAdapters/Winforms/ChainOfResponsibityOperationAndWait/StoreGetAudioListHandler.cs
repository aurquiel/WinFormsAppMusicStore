using AutoMapper;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driving;
using WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms.Dtos;

namespace WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms.ChainOfResponsibityOperationAndWait
{
    public class StoreGetAudioListHandler : Handler
    {
        private readonly IMapper _mapper;
        private IAudioListDriving _audioListDriving;
        private EventHandler<string> _updateLabelMessage;
        private EventHandler<List<AudioFileSelect>> _getAudioListFiles;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private CancellationToken _token;
        private int _storeId;

        public StoreGetAudioListHandler(
            IMapper mapper,
            IAudioListDriving audioListDriving,
            EventHandler<string> updateLabelMessage,
            EventHandler<List<AudioFileSelect>> getAudioListFiles,
            EventHandler<(bool, string)> raiseRichTextInsertMessage,
            CancellationToken token,
            int storeId)
        {
            _mapper = mapper;
            _audioListDriving = audioListDriving;
            _updateLabelMessage = updateLabelMessage;
            _getAudioListFiles = getAudioListFiles;
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
            _token = token;
            _storeId = storeId;
        }

        public override async Task HandleRequest(OperationTypes.OPERATIONS operation)
        {
            if (operation == OperationTypes.OPERATIONS.STORE_GET_AUDIO_LIST)
            {
                _updateLabelMessage?.Invoke(this, "Espere obteniendo lista de audio de la tienda.");
                await Task.Delay(100);
                var result = await _audioListDriving.DownloadAudioListStoreAsync(_storeId, _token);
                _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                if (result.status)
                {
                    _getAudioListFiles?.Invoke(this, _mapper.Map<List<AudioFile>, List<AudioFileSelect>>(result.data));
                }
            }
            else if (successor != null)
            {
                await successor.HandleRequest(operation);
            }
        }
    }
}
