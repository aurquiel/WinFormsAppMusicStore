using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;
using Serilog;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Threading;

namespace WinFormsAppMusicStore
{
    public partial class UserControlPlayer : UserControl
    {
        private InsertRegisters _insertRegisters;

        private IServices _services;
        private IFileManager _fileManager;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private User _user;
        private string _codeStore;

        private BindingSource _bindingAudioListPlayer = new BindingSource();
        private BindingList<OperationDetails> _audioListPlayer = new BindingList<OperationDetails>();

        private MediaPlayer player = new MediaPlayer();
        private bool isPaused = false;
        TimeSpan _position;
        DispatcherTimer _timer = new DispatcherTimer();


        //Tooltips
        ToolTip toolTipButtonLoadAudioListPc = new ToolTip();
        ToolTip toolTipButtonPullFromServer = new ToolTip();
        ToolTip toolTipButtonPlay = new ToolTip();
        ToolTip toolTipButtonPause = new ToolTip();
        ToolTip toolTipButtonStop = new ToolTip();


        public UserControlPlayer(IServices services, IFileManager fileManager, ILogger logger, User user, List<Store> stores, EventHandler<(bool, string)> raiseRichTextInsertMessage)
        {
            InitializeComponent();
            CreateToolTips();
            _services = services;
            _user = user;
            _codeStore = stores.Where(x => x.id == _user.storeId).Select(x => x.code).FirstOrDefault();
            _insertRegisters = new InsertRegisters(_services, user.storeId, logger);
            _insertRegisters.Message = "Aplicacion abierta, no reproduciendo";
            _insertRegisters.StarRegister();
            _fileManager = fileManager;
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
            player.MediaEnded += Player_MediaEnded;
            player.MediaFailed += Player_MediaFailed;
            player.MediaOpened += Player_MediaOpened;
            player.Volume = 1;
            trackBarVolume.Value = trackBarVolume.Maximum;
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += new EventHandler(ticktock);
            _timer.Start();
            buttonLoadAudioListPc_Click(this, EventArgs.Empty);

        }

        private void buttonLoadAudioListPc_Click(object sender, EventArgs e)
        {
            LoadAudioListFromBinaryFile();
        }

        private void LaunchOperationWaitForm(List<OperationDetails> operationDetails)
        {
            FormOperationAndWait formWait = new FormOperationAndWait(
                _services,
                _fileManager,
                operationDetails,
                _raiseRichTextInsertMessage);
            formWait.ShowDialog();

            if (formWait.AudioList != null)
            {
                BindListbox(formWait.AudioList);
                listBoxAudio.ClearSelected();
            }
            else
            {
                _audioListPlayer.Clear();
            }
        }

        private void LoadAudioListFromBinaryFile()
        {
            _insertRegisters.Message = "Aplicacion abierta reproduciendo detenida, lista de reproduccion obtenida del equipo";
            InitMediaPlayer(); 
            var operationDetailsList = new List<OperationDetails> {
                new OperationDetails {
                    Operation = OperationDetails.OPERATIONS.PLAYER_GET_AUDIO_LIST_STORE_PC,
                    StoreCode = _codeStore
                }
            };
            LaunchOperationWaitForm(operationDetailsList);
        }

        private void buttonPullFromServer_Click(object sender, EventArgs e)
        {
            _insertRegisters.Message = "Aplicacion abierta reproduciendo detenida, intentando obtener lista reprodcucion del servidor.";
            InitMediaPlayer();
            var operationDetailsList = new List<OperationDetails> {
            new OperationDetails {
                Operation = OperationDetails.OPERATIONS.PLAYER_GET_AUDIO_LIST_STORE_SERVER,
                StoreCode = _codeStore
            }};
            LaunchOperationWaitForm(operationDetailsList);  
        }

        private void Player_MediaOpened(object? sender, EventArgs e)
        {
            _insertRegisters.Message = "Aplicacion abierta, reproduciendo audio: " + Path.GetFileName(player.Source.ToString());
            _position = player.NaturalDuration.TimeSpan;
            progressBarAudio.Minimum = 0;
            progressBarAudio.Maximum = (int)_position.TotalSeconds;
        }

        private void CreateToolTips()
        {
            toolTipButtonLoadAudioListPc.SetToolTip(buttonLoadAudioListPc, "Obtener lista de reproduccion del equipo.");
            toolTipButtonPullFromServer.SetToolTip(buttonPullFromServer, "Actualizar lista de reproduccion.");
            toolTipButtonPlay.SetToolTip(buttonPlay, "Reproducir audio.");
            toolTipButtonPause.SetToolTip(buttonPause, "Pausar reproduccion.");
            toolTipButtonStop.SetToolTip(buttonStop, "Detener lista de audio.");
        }

        void ticktock(object sender, EventArgs e)
        {
            progressBarAudio.Value = (int)player.Position.TotalSeconds;
        }

        private void InitMediaPlayer()
        {
            player.Stop();
            player = new MediaPlayer();
            player.MediaEnded += Player_MediaEnded;
            player.MediaFailed += Player_MediaFailed;
            player.MediaOpened += Player_MediaOpened;
            player.Volume = 1;
            trackBarVolume.Value = trackBarVolume.Maximum;
        }

        private void Player_MediaFailed(object? sender, ExceptionEventArgs e)
        {
            InitMediaPlayer();
            PlayNextAudio();
            _raiseRichTextInsertMessage?.Invoke(this, (false, "Error al reproducir archvio de audio. Excepcion: " + e.ErrorException.Message));
        }

        private void BindListbox(List<OperationDetails> audioOperationList)
        {
            _audioListPlayer = new BindingList<OperationDetails>(audioOperationList);
            _bindingAudioListPlayer.DataSource = _audioListPlayer;
            listBoxAudio.DataSource = _bindingAudioListPlayer;
            listBoxAudio.DisplayMember = "AudioName";
        }
        private void PlayNextAudio()
        {
            bool flag = false;
            if (listBoxAudio.SelectedIndex < listBoxAudio.Items.Count - 1)
            {
                flag = true;
                listBoxAudio.SelectedIndex = listBoxAudio.SelectedIndex + 1;
            }
            else if (listBoxAudio.Items.Count > 0)
            {
                flag = true;
                listBoxAudio.SelectedIndex = 0;
            }

            if (flag)
            {
                buttonPlay_Click(null, null);
            }
        }

        private void Player_MediaEnded(object? sender, EventArgs e)
        {
            PlayNextAudio();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (listBoxAudio.Items.Count > 0 && listBoxAudio.SelectedItems.Count == 0)
            {
                listBoxAudio.SelectedIndex = 0;
            }

            if (isPaused == false)
            {
                var selectedItem = listBoxAudio.SelectedItem;
                if (selectedItem != null)
                {
                    player.Open(new Uri(((OperationDetails)selectedItem).PathFileAudio));
                }
            }
            isPaused = false;
            player.Play();
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            _insertRegisters.Message = "Aplicacion abierta, reproduciendo pausada: " + Path.GetFileName(player.Source.ToString());
            isPaused = true;
            player.Pause();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (listBoxAudio.Items.Count > 0)
            {
                listBoxAudio.SelectedIndex = 0;
            }
            _insertRegisters.Message = "Aplicacion abierta, reproduciendo detenida";
            player.Stop();
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            player.Volume = (trackBarVolume.Value / (double)100);
        }

        private void listBoxAudio_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBoxAudio.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                player.Stop();
                var selectedItem = listBoxAudio.SelectedItem;
                if (selectedItem != null)
                {
                    player.Open(new Uri(((OperationDetails)selectedItem).PathFileAudio));
                }
                buttonPlay_Click(null, null);
            }
        }

        private void progressBarAudio_MouseDown(object sender, MouseEventArgs e)
        {
            if (player.NaturalDuration.HasTimeSpan)
            {
                player.Position = player.NaturalDuration.TimeSpan * e.X / progressBarAudio.Width;
            }
        }
    }
}
