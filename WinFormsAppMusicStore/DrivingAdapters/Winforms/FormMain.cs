using AutoMapper;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driving;
using Serilog;
using System.Data;

namespace WinFormsAppMusicStoreAdmin
{
    public partial class FormMain : Form
    {
        private ILogger _logger;
        private readonly IMapper _mapper;
        private IAudioDriving _audioDriving;
        private IAudioListDriving _audioListDriving;
        private readonly IFileManagerDriving _fileManagerDriving;
        private readonly IRegisterDriving _registerDriving;
        private readonly IPlayerDriving _playerDriving;
        private readonly IAudioListLocalDriving _audioListLocalDriving;
        private FormOperationAndWait _formOperationAndWait;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;

        private User _user;
        private UserControlPlayer _userControlPlayer;
        private int REGISTERS_TIME_INTERVAL_MINUTES;

        public FormMain(ILogger logger, IMapper mapper, IAudioDriving audioDriving, IAudioListDriving audioListDriving,
            IFileManagerDriving fileManagerDriving, IRegisterDriving registerDriving,
            IPlayerDriving playerDriving, IAudioListLocalDriving audioListLocalDriving, FormOperationAndWait formOperationAndWait)
        {
            InitializeComponent();
            WireUpEvents();

            _audioDriving = audioDriving;
            _audioListDriving = audioListDriving;
            _fileManagerDriving = fileManagerDriving;
            _registerDriving = registerDriving;
            _playerDriving = playerDriving;
            _audioListLocalDriving = audioListLocalDriving;
            _formOperationAndWait = formOperationAndWait;
            _formOperationAndWait.SuscribeRichTextEvent(_raiseRichTextInsertMessage);
            _logger = logger;
            _mapper = mapper;
        }

        internal void SetRegistersTimeInterval(int time)
        {
            REGISTERS_TIME_INTERVAL_MINUTES = time;
        }

        internal void SetActiveUser(User user)
        {
            _user = user;
        }

        internal void InitUserControls(bool isOnlineMode)
        {
            _userControlPlayer = new UserControlPlayer(isOnlineMode, REGISTERS_TIME_INTERVAL_MINUTES, _formOperationAndWait, _logger, _mapper, _playerDriving, _registerDriving, _user, _raiseRichTextInsertMessage);
            OpenChildForm(_userControlPlayer);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void WireUpEvents()
        {
            this._raiseRichTextInsertMessage += UpdateOnRichTextInsertNewMessage;
        }

        private void UpdateOnRichTextInsertNewMessage(object sender, (bool, string) e)
        {
            if (e.Item1)
            {
                _logger.Information(e.Item2);
                AppendText(true, e.Item2, Color.Green, true);
            }
            else
            {
                _logger.Error(e.Item2);
                AppendText(false, e.Item2, Color.Red, true);
            }
        }

        private string GetTime()
        {
            return "Tiempo: " + DateTime.Now.ToString(@"hh\:mm\:ss\:fff") + ". ";
        }

        private void AppendText(bool status, string text, Color color, bool addNewLine = false)
        {
            if (status)
            {
                text = text.Insert(0, "EXITOSO: " + GetTime());
            }
            else
            {
                text = text.Insert(0, "ERROR: " + GetTime());
            }

            richTextBoxStatusMessages.SuspendLayout();
            richTextBoxStatusMessages.SelectionColor = color;
            richTextBoxStatusMessages.AppendText(addNewLine ? $"{text}{Environment.NewLine}" : text);
            richTextBoxStatusMessages.ScrollToCaret();
            richTextBoxStatusMessages.ResumeLayout();
        }

        private void OpenChildForm(UserControl childForm)
        {
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Clear();
            panelChildForm.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }

        
    }
}
