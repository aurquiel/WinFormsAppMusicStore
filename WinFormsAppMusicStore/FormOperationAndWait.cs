using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;
using Microsoft.VisualBasic.Devices;
using System.Data;
using System.IO;

namespace WinFormsAppMusicStore
{
    public partial class FormOperationAndWait : Form
    {
        private IServices _services;
        private IFileManager _fileManager;
        private List<OperationDetails> _operationDetails;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private CancellationToken _token;
        public List<OperationDetails> AudioList = new List<OperationDetails>();

        public FormOperationAndWait(IServices services, IFileManager fileManager, List<OperationDetails> operationDetails, EventHandler<(bool, string)> raiseRichTextInsertMessage)
        {
            _services = services;
            _fileManager = fileManager;
            _operationDetails = operationDetails;
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
            _token = _tokenSource.Token;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            labelTotalNumber.Text = _operationDetails.Count.ToString();
        }

        private void FormOperationAndWait_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tokenSource.Cancel(); 
            //_tokenSource.Dispose();
        }

        private async void FormWait_Shown(object sender, EventArgs e)
        {
            await Task.Delay(500);
            await DoOperations(_token);
            Close();
        }

        private async Task DoOperations(CancellationToken token)
        {
            int actualNumberOperation = 0;
            foreach (var operation in _operationDetails)
            {
                switch (operation.Operation)
                {
                    case OperationDetails.OPERATIONS.SERVER_GET_AUDIO_LIST:
                        {
                            labelOperation.Text = "Descargado Lista de Audio del servidor.";
                            SetLabelActualNumber(ref actualNumberOperation);
                            await Task.Delay(300);
                            var result = await _services.AudioService.DownloadAudioListServer(token);
                            _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                            if (result.status)
                            {
                                var audioListArray = result.data.Split(Environment.NewLine);

                                if (audioListArray.Count() == 1 && string.IsNullOrEmpty(audioListArray[0]))
                                {
                                    AudioList = new List<OperationDetails>();
                                }
                                else
                                {
                                    AudioList = audioListArray.Select(x => new OperationDetails { AudioName = Path.GetFileName(x) }).ToList();
                                }
                            }
                            else
                            {
                                AudioList = null;
                            }
                            break;
                        }
                    case OperationDetails.OPERATIONS.SERVER_UPLOAD_AUDIO:
                        {
                            labelOperation.Text = "Subiendo audio al servidor. Audio: " + operation.AudioName;
                            SetLabelActualNumber(ref actualNumberOperation);
                            await Task.Delay(300);
                            var result = await _services.AudioService.UploadAudioServer(operation.PathFileAudio, token);
                            _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                            break;
                        }
                    case OperationDetails.OPERATIONS.SERVER_DELETE_AUDIO:
                        {
                            labelOperation.Text = "Eliminando audio del servidor. Audio: " + operation.AudioName;
                            SetLabelActualNumber(ref actualNumberOperation);
                            await Task.Delay(300);
                            var result = await _services.AudioService.DeleteAudioServer(operation.AudioName, token);
                            _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                            break;
                        }
                    case OperationDetails.OPERATIONS.SERVER_SYNCHRONIZE_STORES:
                        {
                            labelOperation.Text = "Sincronizando Listas de Audios de las Tiendas";
                            SetLabelActualNumber(ref actualNumberOperation);
                            await Task.Delay(300);
                            var result = await _services.AudioService.SynchronizeAudioListAllStore(token);
                            _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                            break;
                        }
                    case OperationDetails.OPERATIONS.STORE_GET_AUDIO_LIST:
                        {
                            labelOperation.Text = $"Descargado Lista de Audio de la tienda {operation.StoreCode}.";
                            SetLabelActualNumber(ref actualNumberOperation);
                            await Task.Delay(300);
                            var result = await _services.AudioService.DownloadAudioListStore(operation.StoreCode, token);
                            _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                            if (result.status)
                            {
                                var audioListArray = result.data.Split(Environment.NewLine);

                                if (audioListArray.Count() == 1 && string.IsNullOrEmpty(audioListArray[0]))
                                {
                                    AudioList = new List<OperationDetails>();
                                }
                                else
                                {
                                    AudioList = audioListArray.Select(x => new OperationDetails { AudioName = Path.GetFileName(x) }).ToList();
                                }
                            }
                            else
                            {
                                AudioList = null;
                            }
                            break;
                        }
                    case OperationDetails.OPERATIONS.STORE_SYNCHRONIZE_LIST_AUDIO:
                        {
                            labelOperation.Text = $"Sincronizando Lista de Audio de la tienda {operation.StoreCode}.";
                            SetLabelActualNumber(ref actualNumberOperation);
                            await Task.Delay(300);
                            var result = await _services.AudioService.SynchronizeAudioListStore(operation.AudioList, operation.StoreCode, token);
                            _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                            if (result.status)
                            {
                                var audioListArray = result.data.Split(Environment.NewLine);

                                if (audioListArray.Count() == 1 && string.IsNullOrEmpty(audioListArray[0]))
                                {
                                    AudioList = new List<OperationDetails>();
                                }
                                else
                                {
                                    AudioList = audioListArray.Select(x => new OperationDetails { AudioName = Path.GetFileName(x) }).ToList();
                                }
                            }
                            else
                            {
                                AudioList = null;
                            }
                            break;
                        }
                    case OperationDetails.OPERATIONS.PLAYER_GET_AUDIO_LIST_STORE_PC:
                        {
                            labelOperation.Text = $"Obteniendo Lista de Audio del equipo, tienda {operation.StoreCode}.";
                            SetLabelActualNumber(ref actualNumberOperation);
                            await Task.Delay(300);
                            var result = _fileManager.ReadAudioListFromBinaryFile();
                            _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                            if (result.status)
                            {
                                if (result.data.Count() == 1 && string.IsNullOrEmpty(result.data[0]))
                                {
                                    AudioList = new List<OperationDetails>();
                                }
                                else
                                {
                                    AudioList = result.data.Select(x => new OperationDetails { AudioName = x, PathFileAudio = $"{_fileManager.GetAudioStorePath()}\\audio\\{x}" }).ToList();
                                }
                            }
                            else
                            {
                                AudioList = null;
                            }
                            break;
                        }
                    case OperationDetails.OPERATIONS.PLAYER_GET_AUDIO_LIST_STORE_SERVER:
                        {
                            labelOperation.Text = $"Obteniendo Lista de Audio del servidor, tienda {operation.StoreCode}.";
                            SetLabelActualNumber(ref actualNumberOperation);
                            await Task.Delay(300);
                            var result = await _services.AudioService.DownloadAudioListStore(operation.StoreCode, token);
                            if (result.status)
                            {
                                var audioListArray = result.data.Split(Environment.NewLine);

                                if (audioListArray.Count() == 1 && string.IsNullOrEmpty(audioListArray[0]))
                                {
                                    AudioList = new List<OperationDetails>();
                                }
                                else
                                {
                                    labelTotalNumber.Text = (Int32.Parse(labelTotalNumber.Text) + 1).ToString();
                                    labelOperation.Text = $"Escribiendo Lista de Audio.";
                                    SetLabelActualNumber(ref actualNumberOperation);
                                    await Task.Delay(300);
                                    _fileManager.WriteAudioListToBinaryFile(result.data);

                                    var listToDowmload = _fileManager.GetAudioListToDownload(audioListArray.ToList());
                                    if (listToDowmload.Count() > 0)
                                    {
                                        labelTotalNumber.Text = (Int32.Parse(labelTotalNumber.Text) + listToDowmload.Count()).ToString();
                                        foreach (var audio in listToDowmload)
                                        {
                                            labelOperation.Text = $"Descargando audio: {audio}.";
                                            SetLabelActualNumber(ref actualNumberOperation);
                                            await Task.Delay(300);
                                            var resultDownload = await _services.AudioService.DownloadAudioServer(operation.StoreCode, audio, token);
                                            _raiseRichTextInsertMessage?.Invoke(this, (resultDownload.status, resultDownload.statusMessage));
                                            if(token.IsCancellationRequested)
                                            {
                                                return;
                                            }
                                        }
                                    }

                                    labelTotalNumber.Text = (Int32.Parse(labelTotalNumber.Text) + 1).ToString();
                                    labelOperation.Text = $"Eliminando audios innecesarios.";
                                    SetLabelActualNumber(ref actualNumberOperation);
                                    await Task.Delay(300);
                                    _fileManager.EraseAudiosNotInAudioList(audioListArray.ToList());

                                    AudioList = audioListArray.ToList().Select(x => new OperationDetails { AudioName = x, PathFileAudio = $"{_fileManager.GetAudioStorePath()}\\audio\\{x}" }).ToList();
                                }
                            }
                            else
                            {
                                AudioList = null;
                            }
                            break;
                        }
                }
            }
        }

        private void SetLabelActualNumber(ref int previousNumber)
        {
            previousNumber++;
            labelActualNumber.Text = previousNumber.ToString();
        }

        
    }
}
