using ChainOfResponsibilityClassLibrary;
using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;

namespace WinFormsAppMusicStoreAdmin
{
    public partial class FormOperationAndWait : Form
    {
        private IServices _services;
        private IFileManager _fileManager;
        private List<Operation> _operations;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private CancellationToken _token;

        public List<AudioFileDTO> AudioFileListDownloaded { get; private set; } = new List<AudioFileDTO>();

        private Handler h1;
        private Handler h2;
        private Handler h3;
        private Handler h4;
        private Handler h5;
        private Handler h6;
        private Handler h7;
        private Handler h8;

        private EventHandler<string> _updateLabelMessage;
        private EventHandler<List<AudioFileDTO>> _getAudioListFiles;

        public FormOperationAndWait(IServices services, IFileManager fileManager, List<Operation> operations, EventHandler<(bool, string)> raiseRichTextInsertMessage)
        {
            _services = services;
            _fileManager = fileManager;
            _operations = operations;
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
            _token = _tokenSource.Token;
            WireUpEvents();
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        //Patron de diseño de responsabilidad
        private void InitChainOfResponsibility(Operation operation)
        {
            h1 = new ServerGetAudioListHandler(
                _services,
                _fileManager,
                _updateLabelMessage,
                _getAudioListFiles,
                _raiseRichTextInsertMessage,
                _token);

            h2 = new ServerUploadAudioHandler(
                _services,
                _fileManager,
                _updateLabelMessage,
                _getAudioListFiles,
                _raiseRichTextInsertMessage,
                _token,
                operation.AudioFileToOperate
                );
            h3 = new ServerDeleteAudioHandler(
                _services,
                _fileManager,
                _updateLabelMessage,
                _getAudioListFiles,
                _raiseRichTextInsertMessage,
                _token,
                operation.AudioFileToOperate
                );
            h4 = new ServerSynchronizeStoresHandler(
                _services,
                _fileManager,
                _updateLabelMessage,
                _getAudioListFiles,
                _raiseRichTextInsertMessage,
                _token
                );
            h5 = new StoreGetAudioListHandler(
                _services,
                _fileManager,
                _updateLabelMessage,
                _getAudioListFiles,
                _raiseRichTextInsertMessage,
                _token,
                operation.StoreCode
                );
            h6 = new StoreSynchronizeListAudio(
                _services,
                _fileManager,
                _updateLabelMessage,
                _getAudioListFiles,
                _raiseRichTextInsertMessage,
                _token,
                operation.AudioFileToOperate,
                operation.StoreCode
                );
            h7 = new PlayerGetAudioListStorePcHandler(
                _services,
                _fileManager,
                _updateLabelMessage,
                _getAudioListFiles,
                _raiseRichTextInsertMessage,
                _token,
                operation.StoreCode
                );
            h8 = new PlayerGetAudioListStoreServerHandler(
                _services,
                _fileManager,
                _updateLabelMessage,
                _getAudioListFiles,
                _raiseRichTextInsertMessage,
                _token,
                operation.StoreCode
                );

            h1.SetSuccessor(h2);
            h2.SetSuccessor(h3);
            h3.SetSuccessor(h4);
            h4.SetSuccessor(h5);
            h5.SetSuccessor(h6);
            h6.SetSuccessor(h7);
            h7.SetSuccessor(h8);
        }

        private void WireUpEvents()
        {
            _updateLabelMessage += UpdateLabelMessage;
            _getAudioListFiles += GetAudioListFiles;
        }

        private void UpdateLabelMessage(object sender, string e)
        {
            labelMessage.Text = e;
        }

        private void GetAudioListFiles(object sender, List<AudioFileDTO> e)
        {
            AudioFileListDownloaded = e;
        }

        private void FormOperationAndWait_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tokenSource.Cancel();
        }

        private async void FormWait_Shown(object sender, EventArgs e)
        {
            await Task.Delay(500);
            await DoOperations(_token);
            Close();
        }

        private async Task DoOperations(CancellationToken token)
        {
            foreach (var operation in _operations)
            {
                InitChainOfResponsibility(operation);
                if (token.IsCancellationRequested)
                {
                    return;
                }
                await h1.HandleRequest(operation.TypeOfOperation);
            }
        }
    }
}
