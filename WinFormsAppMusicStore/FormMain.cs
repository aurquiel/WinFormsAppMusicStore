using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;
using Serilog;
using System.Data;

namespace WinFormsAppMusicStore
{
    public partial class FormMain : Form
    {
        private IServices _services;
        private ILogger _logger;
        private IFileManager _fileManager;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;

        private User _user;
        private List<Store> _stores;

        public FormMain(IServices services, ILogger logger, User user, List<Store> stores)
        {
            InitializeComponent();
            WireUpEvents();
            _services = services;
            _logger = logger;
            _user = user;
            _stores = stores;
            _fileManager = new FileManager();
            OpenChildForm(new UserControlPlayer(_services, _fileManager, _logger, _user, stores, _raiseRichTextInsertMessage));
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
