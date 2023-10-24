using AutoMapper;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driving;
using Serilog;
using System.ComponentModel;
using WinFormsAppMusicStoreAdmin.DrivingAdapters.Player;
using WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms;
using WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms.ChainOfResponsibityOperationAndWait;
using WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms.Dtos;
using static WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms.ChainOfResponsibityOperationAndWait.OperationTypes;

namespace WinFormsAppMusicStoreAdmin
{
    public partial class UserControlPlayer : UserControl
    {
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private readonly FormOperationAndWait _formOperationAndWait;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRegisterDriving _registerDriving;
        private User _activeUser;

        private BindingSource _bindingAudioListPlayer = new BindingSource();
        private BindingList<AudioFileSelect> _audioListPlayer = new BindingList<AudioFileSelect>();
        private List<AudioFileSelectPlayed> _audioListPlayerByTime = new List<AudioFileSelectPlayed>();

        private Player _player;
        private EventHandler _playNextAudio;
        private System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        private int numberOfErros = 0;
        private Registers _register;
        private int REGISTERS_TIME_INTERVAL_MINUTES;

        private readonly string PREFIX_PUBLICITY = "@PUBLICIDAD_";
        private readonly string PREFIX_TIME = "@TIEMPO_";

        //Tooltips
        ToolTip toolTipButtonPullFromPc = new ToolTip();
        ToolTip toolTipButtonPullFromServer = new ToolTip();
        ToolTip toolTipButtonPlay = new ToolTip();
        ToolTip toolTipButtonPause = new ToolTip();
        ToolTip toolTipButtonStop = new ToolTip();


        public UserControlPlayer(bool isOnlineMode, int timeIntervalRegister, FormOperationAndWait formOperationAndWait, ILogger logger, IMapper mapper, IPlayerDriving playerDriving,
            IRegisterDriving registerDriving,
             User activeUser, EventHandler<(bool, string)> raiseRichTextInsertMessage)
        {
            InitializeComponent();
            SetModeOnline(isOnlineMode);
            WireUpEvents();
            CreateToolTips();
            REGISTERS_TIME_INTERVAL_MINUTES = timeIntervalRegister;
            _formOperationAndWait = formOperationAndWait;
            _logger = logger;
            _mapper = mapper;
            _registerDriving = registerDriving;
            _activeUser = activeUser;
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
            _player = ((Player)(playerDriving));
            _player.SetRaiseRichTextInsertMessage(_raiseRichTextInsertMessage);
            _player.SetPlayNextAudio(_playNextAudio);
            trackBarVolume.Value = trackBarVolume.Maximum;
            _timer.Interval = 200;
            _timer.Tick += new EventHandler(TimerEventProcessor);
            _timer.Start();
            _register = new Registers(_registerDriving, _logger, REGISTERS_TIME_INTERVAL_MINUTES);
            _register.SetRegister(Registers.STATUS.NO_REPRODUCING, "Aplicacion abierta.");
            _register.StarRegisteringInStore(activeUser.storeId);
        }

        private void SetModeOnline(bool isOnline)
        {
            if (isOnline)
            {
                buttonPullFromServer.Enabled = true;
                labelOnline.Text = "Modo: Online";
            }
            else
            {
                buttonPullFromServer.Enabled = false;
                labelOnline.Text = "Modo: Offline";
            }
        }

        private void WireUpEvents()
        {
            this._playNextAudio += PlayNextAudioEvent;
        }

        private void PlayNextAudioEvent(object sender, object e)
        {
            PlayNextAudio();
        }

        private void CreateToolTips()
        {
            toolTipButtonPullFromPc.SetToolTip(buttonPullFromPc, "Obtener lista de reproduccion local.");
            toolTipButtonPullFromServer.SetToolTip(buttonPullFromServer, "Actualizar lista de reproduccion servidor.");
            toolTipButtonPlay.SetToolTip(buttonPlay, "Reproducir audio.");
            toolTipButtonPause.SetToolTip(buttonPause, "Pausar reproduccion.");
            toolTipButtonStop.SetToolTip(buttonStop, "Detener lista de audio.");
        }

        private void LaunchOperationWaitForm(List<Operation> operations)
        {
            _formOperationAndWait.AudioFileListDownloaded = new List<AudioFileSelect>();
            _formOperationAndWait.Operations = operations;
            _formOperationAndWait.ShowDialog();

            if (_formOperationAndWait.AudioFileListDownloaded != null)
            {
                BindListbox(_formOperationAndWait.AudioFileListDownloaded);
                GetAudioListByTime(_formOperationAndWait.AudioFileListDownloaded);
                listBoxAudio.ClearSelected();
            }
            else
            {
                _audioListPlayer.Clear();
            }
        }

        private void buttonPullFromPc_Click(object sender, EventArgs e)
        {
            _register.SetRegister(Registers.STATUS.NO_REPRODUCING, $"Obteniendo lista de audio local");
            _player.Stop();
            progressBarAudio.Value = 0;
            labelTotalTime.Text = "00:00";
            labelCurrentTime.Text = "00:00";
            var op = new List<Operation> {
                new Operation (OPERATIONS.PLAYER_GET_AUDIO_LIST_STORE_PC, _activeUser.storeId, new List<AudioFileSelect>())
            };
            LaunchOperationWaitForm(op);
        }

        private void buttonPullFromServer_Click(object sender, EventArgs e)
        {
            _register.SetRegister(Registers.STATUS.NO_REPRODUCING, $"Obteniendo lista de audio del servidor");
            _player.Stop();
            progressBarAudio.Value = 0;
            labelTotalTime.Text = "00:00";
            labelCurrentTime.Text = "00:00";
            var op = new List<Operation> {
                new Operation(OPERATIONS.PLAYER_GET_AUDIO_LIST_STORE_SERVER, _activeUser.storeId, new List<AudioFileSelect>())
            };
            LaunchOperationWaitForm(op);
        }

        void TimerEventProcessor(object sender, EventArgs e)
        {
            if (_player.IsPlaying())
            {
                progressBarAudio.Maximum = (int)_player.GetLength();
                labelTotalTime.Text = _player.TotalTime().ToString("mm\\:ss");
                int pos = (int)_player.GetPosition();
                progressBarAudio.Value = pos > progressBarAudio.Maximum ? progressBarAudio.Maximum : pos;
                labelCurrentTime.Text = _player.CurrentTime().ToString("mm\\:ss");
            }
        }

        private void BindListbox(List<AudioFileSelect> audioOperationList)
        {
            _audioListPlayer = new BindingList<AudioFileSelect>(audioOperationList);
            _bindingAudioListPlayer.DataSource = _audioListPlayer;
            listBoxAudio.DataSource = _bindingAudioListPlayer;
            listBoxAudio.DisplayMember = "Name";
        }

        private void GetAudioListByTime(List<AudioFileSelect> audioOperationList)
        {
            _audioListPlayerByTime = _mapper.Map<List<AudioFileSelect>, List<AudioFileSelectPlayed>>(audioOperationList.Where(x => x.CheckForTime).ToList());
            _audioListPlayerByTime.ForEach(x => x.Played = false);
        }

        private void PlayNextAudio()
        {
            if (numberOfErros >= listBoxAudio.Items.Count)
            {
                numberOfErros = 0;
                return;
            }

            bool flag = false;
            if ((int)listBoxAudio.Invoke(new Func<int>(() => listBoxAudio.SelectedIndex)) < listBoxAudio.Items.Count - 1)
            {
                flag = true;
                listBoxAudio.Invoke(new Action(() => listBoxAudio.SelectedIndex = listBoxAudio.SelectedIndex + 1));

            }
            else if (listBoxAudio.Items.Count > 0)
            {
                flag = true;
                listBoxAudio.Invoke(new Action(() => listBoxAudio.SelectedIndex = 0));
            }

            if (flag)
            {
                buttonPlay_Click(null, null);
            }
        }

        private async void buttonPlay_Click(object sender, EventArgs e)
        {
            if (listBoxAudio.Items.Count > 0 &&
                (int)listBoxAudio.Invoke(new Func<int>(() => listBoxAudio.SelectedItems.Count)) == 0)
            {
                listBoxAudio.Invoke(new Action(() => listBoxAudio.SelectedIndex = 0));
            }

            var selectedItem = (AudioFileSelect)listBoxAudio.Invoke(new Func<object>(() => listBoxAudio.SelectedItem));
            if (selectedItem != null)
            {
                await CheckTimeToPlayAudio(selectedItem);
            }
        }

        public static bool IsWithin15Minutes(TimeSpan timeSpan)
        {
            TimeSpan startTime = timeSpan;
            TimeSpan endTime = timeSpan + TimeSpan.FromMinutes(15);

            return DateTime.Now.TimeOfDay >= startTime && DateTime.Now.TimeOfDay <= endTime;
        }

        private async Task CheckTimeToPlayAudio(AudioFileSelect audioFileSelect)
        {
            var audioToPlayByTime = _audioListPlayerByTime.Where(
                x => x.Played == false &&
                IsWithin15Minutes(x.TimeToPlay)
                ).FirstOrDefault();

            if (audioToPlayByTime is not null)
            {
                for (int i = 0; i < _audioListPlayerByTime.Count; i++)
                {
                    if (_audioListPlayerByTime[i].Id == audioToPlayByTime.Id)
                    {
                        audioToPlayByTime.Played = true;
                    }
                }

                if ((int)listBoxAudio.Invoke(new Func<int>(() => listBoxAudio.SelectedIndex)) < listBoxAudio.Items.Count - 1)
                {
                    listBoxAudio.Invoke(new Action(() => listBoxAudio.SelectedIndex = listBoxAudio.SelectedIndex - 1));

                }
                else if (listBoxAudio.Items.Count > 0)
                {
                    listBoxAudio.Invoke(new Action(() => listBoxAudio.SelectedIndex = 0));
                }

                _register.SetRegister(Registers.STATUS.REPRODUCING, $"Reproduciendo audio: {audioToPlayByTime.Name}, volumen aplicacion: {(_player.GetVolume() * 100).ToString("F0")}%, volumen windows: {(_player.GetWindowsVolume() * 100).ToString("F0")}%");
                MaxNumberOfErrors(await _player.Play(_mapper.Map<AudioFileSelect>(audioToPlayByTime).Path));

            }
            else
            {
                if (audioFileSelect.Name.Contains(PREFIX_TIME))
                {
                    numberOfErros++;
                    PlayNextAudio();
                    return;
                }
                _register.SetRegister(Registers.STATUS.REPRODUCING, $"Reproduciendo audio: {((AudioFileSelect)audioFileSelect).Name}, volumen aplicacion: {(_player.GetVolume() * 100).ToString("F0")}%, volumen windows: {(_player.GetWindowsVolume() * 100).ToString("F0")}%");
                MaxNumberOfErrors(await _player.Play(((AudioFileSelect)audioFileSelect).Path));
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            _register.SetRegister(0, $"Reproduccion pausada, volumen aplicacion: {(_player.GetVolume() * 100).ToString("F0")}%, volumen windows: {(_player.GetWindowsVolume() * 100).ToString("F0")}%");
            _player.Pause();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (listBoxAudio.Items.Count > 0)
            {
                listBoxAudio.Invoke(new Action(() => listBoxAudio.SelectedIndex = 0));
            }
            _register.SetRegister(0, $"Reproduccion detenida, volumen aplicacion: {(_player.GetVolume() * 100).ToString("F0")}%, volumen windows: {(_player.GetWindowsVolume() * 100).ToString("F0")}%");
            _player.Stop();
            progressBarAudio.Value = 0;
            labelCurrentTime.Text = "00:00";
            labelTotalTime.Text = "00:00";
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            _player.SetVolume(trackBarVolume.Value / (double)100);
        }

        private async void listBoxAudio_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBoxAudio.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                _player.Stop();
                var selectedItem = (AudioFileSelect)listBoxAudio.SelectedItem;
                if (selectedItem != null)
                {
                    if (selectedItem.Name.Contains(PREFIX_TIME))
                    {
                        numberOfErros++;
                        PlayNextAudio();
                        return;
                    }
                    MaxNumberOfErrors(await _player.Play((selectedItem).Path));
                }
            }
        }

        private void progressBarAudio_MouseDown(object sender, MouseEventArgs e)
        {
            if (_player.IsPlaying())
            {
                _player.Seek(_player.GetLength() * e.X / progressBarAudio.Width);
            }
        }

        private void MaxNumberOfErrors(bool playStatus)
        {
            if (playStatus)
            {
                numberOfErros = 0;
            }
            else
            {
                numberOfErros++;
            }
        }
    }
}
