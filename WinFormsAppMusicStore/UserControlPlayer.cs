using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;

namespace WinFormsAppMusicStore
{
    public partial class UserControlPlayer : UserControl
    {
        private IServices _services;
        private IFileManager _fileManager;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private BindingSource _bindingAudioListOperationList = new BindingSource();
        private BindingList<AudioOperation> _audioOperationList;
        private MediaPlayer player = new MediaPlayer();
        private bool isPaused = false;
        TimeSpan _position;
        DispatcherTimer _timer = new DispatcherTimer();

        //Tooltips
        ToolTip toolTipButtonPullFromServer = new ToolTip();
        ToolTip toolTipButtonPlay = new ToolTip();
        ToolTip toolTipButtonPause = new ToolTip();
        ToolTip toolTipButtonStop = new ToolTip();

        public UserControlPlayer(IServices services, IFileManager fileManager, EventHandler<(bool, string)> raiseRichTextInsertMessage)
        {
            InitializeComponent();
            CreateToolTips();
            _services = services;
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
            LoadAuidoListFRomBinaryFile();
        }

        private void Player_MediaOpened(object? sender, EventArgs e)
        {
            _position = player.NaturalDuration.TimeSpan;
            progressBarAudio.Minimum = 0;
            progressBarAudio.Maximum = (int)_position.TotalSeconds;
        }

        private void CreateToolTips()
        {
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

        private void LoadAuidoListFRomBinaryFile()
        {
            InitMediaPlayer();
            var result = _fileManager.ReadAudioListFromBinaryFile();
            if (result.status)
            {
                if (result.data.Count() == 1 && result.data[0] == string.Empty)
                {
                    result.data.RemoveAt(0);
                }

                var audioList = result.data.Select(x => new AudioOperation { Name = x, Operation = AudioOperation.OPERATIONS.NONE, PathFileAudio = _fileManager.GetAudioPath() + x }).ToList();
                _audioOperationList = new BindingList<AudioOperation>(audioList);
                BindListbox(_audioOperationList);
            }

            _raiseRichTextInsertMessage?.Invoke(this, new(result.status, result.statusMessage));
        }

        private void BindListbox(BindingList<AudioOperation> audioOperationList)
        {
            _bindingAudioListOperationList.DataSource = audioOperationList;
            listBoxAudio.DataSource = _bindingAudioListOperationList;
            listBoxAudio.DisplayMember = "Name";
        }

        private void buttonPullFromServer_Click(object sender, EventArgs e)
        {
            FormWait formWait = new FormWait(_services, _fileManager, null, _raiseRichTextInsertMessage, FormWait.TypeOperation.PullFromServer);
            formWait.ShowDialog();
            LoadAuidoListFRomBinaryFile();
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
                    player.Open(new Uri(((AudioOperation)selectedItem).PathFileAudio));
                }
            }
            isPaused = false;
            player.Play();
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            isPaused = true;
            player.Pause();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (listBoxAudio.Items.Count > 0)
            {
                listBoxAudio.SelectedIndex = 0;
            }
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
                    player.Open(new Uri(((AudioOperation)selectedItem).PathFileAudio));
                }
                buttonPlay_Click(null, null);
            }
        }

        private void progressBarAudio_MouseDown(object sender, MouseEventArgs e)
        {
            player.Position = player.NaturalDuration.TimeSpan * e.X / progressBarAudio.Width;
        }
    }
}
