using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driving;
using Serilog;
using System.Configuration;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters;

namespace WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms
{
    public partial class FormLogin : Form
    {
        private readonly FormMain _formMain;
        private ILogger _logger;
        private readonly IFileManagerDriving _fileManagerDriving;
        private readonly IUserAccessDriving _userAccessDriving;
        private int REGISTERS_TIME_INTERVAL_MINUTES;

        public FormLogin(FormMain formMain, ILogger logger, IFileManagerDriving fileManagerDriving,
            IUserAccessDriving userAccessDriving)
        {
            InitializeComponent();
            _formMain = formMain;
            _logger = logger;
            _fileManagerDriving = fileManagerDriving;
            _fileManagerDriving.CopyLocalDbIfNotExistOrCorrupted();
            _userAccessDriving = userAccessDriving;
            GetIpWebService();
            GetTimeoutWebService();
            GetTimeoutWebServiceHeavyTask();
            GetRegistersIntervalTime();
        }

        private void GetIpWebService()
        {
            try
            {
                WebServiceParams.IP_WEB_SERVICE = ConfigurationManager.AppSettings["IP_WEB_SERVICE"].ToString();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener ip del webservice del archivo de configuracion", null);
                WebServiceParams.IP_WEB_SERVICE = "https://192.168.0.203:9097/";
            }
        }

        private void GetTimeoutWebService()
        {
            try
            {
                WebServiceParams.TIMEOUT_WEB_SERVICE = Int32.Parse(ConfigurationManager.AppSettings["TIMEOUT_WEB_SERVICE"].ToString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener timeout del webservice del archivo de configuracion", null);
                WebServiceParams.TIMEOUT_WEB_SERVICE = 25;
            }
        }

        private void GetTimeoutWebServiceHeavyTask()
        {
            try
            {
                WebServiceParams.TIMEOUT_WEB_SERVICE_HEAVY_TASK = Int32.Parse(ConfigurationManager.AppSettings["TIMEOUT_WEB_SERVICE_HEAVY_TASK"].ToString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener timeout del webservice del archivo de configuracion", null);
                WebServiceParams.TIMEOUT_WEB_SERVICE_HEAVY_TASK = 600; //10 min para descargar o subir una cancion
            }
        }

        private void GetRegistersIntervalTime()
        {
            try
            {
                REGISTERS_TIME_INTERVAL_MINUTES = Int32.Parse(ConfigurationManager.AppSettings["REGISTERS_INTERVAL_TIME_MINUTES"].ToString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener tiempo de registros", null);
                REGISTERS_TIME_INTERVAL_MINUTES = 30; //30 min
            }
        }

        private void UpdateUiFromLoadStore((bool, string) result)
        {
            if (result.Item1)
            {
                TextBoxStatusUpdate(result.Item2, Color.Green);
            }
            else
            {
                TextBoxStatusUpdate(result.Item2, Color.Red);
            }
        }

        private void TextBoxStatusUpdate(string text, Color colorBrush)
        {
            this.textBoxStatus.Text = text;
            this.textBoxStatus.ForeColor = colorBrush;
            this.textBoxStatus.BackColor = this.textBoxStatus.BackColor;
        }

        private (bool, string) ValidateInputUser()
        {
            if (string.IsNullOrWhiteSpace(textBoxUserAlias.Text))
            {
                return new(false, "Error campo de usuario vacio.");
            }
            else if (string.IsNullOrWhiteSpace(textBoxUserPassword.Text))
            {
                return new(false, "Error campo de contraseña de usuario vacio.");
            }
            else
            {
                return new(true, "Entrada de datos de usuarios validados.");
            }
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            DisableControl();

            var resultValidateUser = ValidateInputUser();

            if (resultValidateUser.Item1 == false)
            {
                UpdateUiFromLoadStore(resultValidateUser);
                goto ERROR_LOGGIN;
            }

            var resultUserAccess = await LoginUser();

            if (resultUserAccess.status == false)
            {
                UpdateUiFromLoadStore((resultUserAccess.status, resultUserAccess.statusMessage));
                _logger.Error("UserAccess: " + resultUserAccess.statusMessage);
                goto ERROR_LOGGIN;
            }

            WebServiceParams.TOKEN_WEB_SERVICE = resultUserAccess.data.token;

            CreateFolder();

            LaunchMainWindows(resultUserAccess.data.user);
            Close();

        ERROR_LOGGIN:
            EnableControl();
        }

        private async Task<GeneralAnswer<UserAccess>> LoginUser()
        {
            UpdateUiFromLoadStore((true, "Obteniendo informacion del usuario..."));
            await Task.Delay(300);

            if (radioButtonOnline.Checked)
            {
                var resultUserAccess = await _userAccessDriving.AcccesLoginTokenAsync(textBoxUserAlias.Text, textBoxUserPassword.Text);
                return resultUserAccess;
            }
            else
            {
                var resultUserAccess = await _userAccessDriving.AcccesLoginOfflineAsync(textBoxUserAlias.Text, textBoxUserPassword.Text);
                return resultUserAccess;
            }
        }

        private void CreateFolder()
        {
            _fileManagerDriving.CreateDirectoryAndFile();
        }

        private void DisableControl()
        {
            buttonLogin.Enabled = false;
            textBoxUserAlias.Enabled = false;
            textBoxUserPassword.Enabled = false;
        }

        private void EnableControl()
        {
            buttonLogin.Enabled = true;
            textBoxUserAlias.Enabled = true;
            textBoxUserPassword.Enabled = true;
        }

        private void LaunchMainWindows(User activeUser)
        {
            this.Hide();
            _formMain.SetRegistersTimeInterval(REGISTERS_TIME_INTERVAL_MINUTES);
            _formMain.SetActiveUser(activeUser);
            _formMain.InitUserControls(radioButtonOnline.Checked);
            _formMain.ShowDialog();
            _formMain.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}