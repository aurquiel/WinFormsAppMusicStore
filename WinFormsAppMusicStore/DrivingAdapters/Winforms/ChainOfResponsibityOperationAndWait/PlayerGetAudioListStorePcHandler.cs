using AutoMapper;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driving;
using WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms.Dtos;

namespace WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms.ChainOfResponsibityOperationAndWait
{
    public class PlayerGetAudioListStorePcHandler : Handler
    {
        private readonly IMapper _mapper;
        private IAudioListLocalDriving _audioListLocalDriving;
        private EventHandler<string> _updateLabelMessage;
        private EventHandler<List<AudioFileSelect>> _getAudioListFiles;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private CancellationToken _token;
        private int _storeId;

        public PlayerGetAudioListStorePcHandler(
            IMapper mapper,
            IAudioListLocalDriving audioListLocalDriving,
            EventHandler<string> updateLabelMessage,
            EventHandler<List<AudioFileSelect>> getAudioListFiles,
            EventHandler<(bool, string)> raiseRichTextInsertMessage,
            CancellationToken token,
            int storeId)
        {
            _mapper = mapper;
            _audioListLocalDriving = audioListLocalDriving;
            _updateLabelMessage = updateLabelMessage;
            _getAudioListFiles = getAudioListFiles;
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
            _token = token;
            _storeId = storeId;
        }

        public override async Task HandleRequest(OperationTypes.OPERATIONS operation)
        {
            if (operation == OperationTypes.OPERATIONS.PLAYER_GET_AUDIO_LIST_STORE_PC)
            {
                _updateLabelMessage?.Invoke(this, $"Espere obteniendo lista local de audio de la tienda");
                await Task.Delay(100);
                try
                {
                    var listAudio = await _audioListLocalDriving.GetAudioListAsync(_storeId);
                    _raiseRichTextInsertMessage?.Invoke(this, (true, $"Lista de reproduccion obtenida localmente tienda."));
                    _getAudioListFiles?.Invoke(this, _mapper.Map<List<AudioFile>, List<AudioFileSelect>>(listAudio));
                }
                catch(Exception ex) 
                {
                    _raiseRichTextInsertMessage?.Invoke(this, (false, $"Error al obtener lista local de reproduccion tienda, excepcion: " + ex.Message));
                    _getAudioListFiles?.Invoke(this, new List<AudioFileSelect>());
                }
            }
            else if (successor != null)
            {
                await successor.HandleRequest(operation);
            }
        }
    }
}
