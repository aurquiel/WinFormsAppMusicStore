using ClassLibraryFiles;
using ClassLibraryModels;
using ClassLibraryServices;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppMusicStore
{
    public partial class FormWait : Form
    {
        private IServices _services;
        private IFileManager _fileManager;
        private List<AudioOperation> _audioOperationList;
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        public enum TypeOperation { PullFromServer = 0, PushToServer = 1 };
        private TypeOperation _typeOperation;
        public FormWait(IServices services, IFileManager fileManager, List<AudioOperation> audioOperationList, EventHandler<(bool, string)> raiseRichTextInsertMessage, TypeOperation typeOperation)
        {
            _typeOperation = typeOperation;
            _services = services;
            _fileManager = fileManager;
            _audioOperationList = audioOperationList;
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private async void FormWait_Shown(object sender, EventArgs e)
        {
            await Task.Delay(500);
            if (_typeOperation == TypeOperation.PushToServer)
            {
                await MusicPush();
                Close();
            }
            else
            {
                await SynchronizeAudioListPc();
                Close();
            }
        }

        private async Task MusicPush()
        {
            await TaskPush();
            labelMessage.Text = string.Empty;
        }

        private async Task TaskPush()
        {
            var listToUpload = _audioOperationList.Where(x => x.Operation == AudioOperation.OPERATIONS.UPLOAD).ToList();
            labelTotalNumber.Text = (listToUpload.Count() + 1).ToString();
            if (listToUpload.Count() > 0)
            {
                await UploadFiles(listToUpload);
            }
            await SynchronizeAudioListServer(listToUpload.Count());
        }

        private async Task UploadFiles(List<AudioOperation> listToUpload)
        {
            int i = 1;
            foreach (var file in listToUpload)
            {
                labelOperation.Text = "Subiendo archivo: " + file.Name;
                labelActualNumber.Text = i.ToString();
                var result = await _services.AudioService.UploadAudio(file.PathFileAudio);
                _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
                i++;
                await Task.Delay(300);
            }
        }

        private async Task SynchronizeAudioListServer(int uploadCount)
        {
            labelOperation.Text = "Sincronizando archivos";
            if (uploadCount > 0)
            {
                labelActualNumber.Text = (Int32.Parse(labelActualNumber.Text) + 1).ToString();
            }
            var result = await _services.AudioService.SynchronizeAudioList(TransformAudioOperationListToTextFile());
            _raiseRichTextInsertMessage?.Invoke(this, (result.status, result.statusMessage));
            await Task.Delay(300);
        }

        private string TransformAudioOperationListToTextFile()
        {
            string result = string.Empty;
            foreach (var audioOperation in _audioOperationList)
            {
                result += audioOperation.Name + Environment.NewLine;
            }

            return result.TrimEnd('\r', '\n');
        }

        private async Task SynchronizeAudioListPc()
        {
            labelTotalNumber.Text = (1 + 1 + 1).ToString();
            labelActualNumber.Text = "1";
            labelOperation.Text = "Descargando Lista de Audio.";
            var resultGetAudioList = await _services.AudioService.DownloadAudioList();
            _raiseRichTextInsertMessage?.Invoke(this, (resultGetAudioList.status, resultGetAudioList.statusMessage));

            if (resultGetAudioList.status == false)
            {
                return;
            }

            await Task.Delay(300);

            labelActualNumber.Text = "2";
            labelOperation.Text = "Escribiendo Lista de Audio al disco.";
            var resultWriteBinaryFile =  _fileManager.WriteAudioListToBinaryFile(resultGetAudioList.data);
            _raiseRichTextInsertMessage?.Invoke(this, (resultWriteBinaryFile.status, resultWriteBinaryFile.statusMessage));

            if (resultWriteBinaryFile.status == false)
            {
                return;
            }

            await Task.Delay(300);

            List<string> audios;
            if (resultGetAudioList.data != string.Empty)
            {
                audios = new List<string>(resultGetAudioList.data.Split(Environment.NewLine));
            }
            else
            {
                audios = new List<string>();
            }

            labelActualNumber.Text = "3";
            labelOperation.Text = "Eliminando audios huerfanos.";
            _fileManager.EraseAudiosNotInAudioList(audios);

            await Task.Delay(300);

            var listAudioToDownload = _fileManager.GetAudioListToDownload(audios);

            labelTotalNumber.Text = (3 + listAudioToDownload.Count()).ToString();
            foreach (var audio in listAudioToDownload)
            {
                labelOperation.Text = "Descargando archivo: " + audio;
                labelActualNumber.Text = (Int32.Parse(labelActualNumber.Text) + 1).ToString();
                var resultUploadAudio = await _services.AudioService.DownloadAudio(audio);
                _raiseRichTextInsertMessage?.Invoke(this, (resultUploadAudio.status, resultUploadAudio.statusMessage));
                await Task.Delay(300);
            }
        }
    }
}
