using ClassLibraryFiles;
using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryServices.WebService
{
    internal class AudioServiceHttp : IAudioService
    {
        private WebServiceParams _webParams;
        private IFileManager _fileManager;

        public AudioServiceHttp(WebServiceParams webParams, IFileManager fileManager)
        {
            _webParams = webParams;
            _fileManager = fileManager;
        }

        public async Task<GeneralAnswer<string>> DownloadAudioList()
        {
            var result = await AudioHttp.AduioGetAudioList(_webParams);

            if (result.Item1) //Obtenido del servidor
            {
                return new GeneralAnswer<string>(result.Item3.status, result.Item3.statusMessage, result.Item3.data);
            }
            else // No Obtenido del servidor
            {
                return new GeneralAnswer<string>(result.Item1, result.Item2, null);
            }
        }

        public async Task<GeneralAnswer<object>> SynchronizeAudioList(string plainText)
        {
            var result = await AudioHttp.SynchronizeAudioList(_webParams, plainText);

            return new GeneralAnswer<object>(result.Item1, result.Item2, null);
        }

        public async Task<GeneralAnswer<object>> UploadAudio(string pathFile)
        {
            var result = await AudioHttp.UploadAudio(_webParams, pathFile);

            return new GeneralAnswer<object>(result.Item1, result.Item2, null);
        }

        public async Task<GeneralAnswer<object>> DownloadAudio(string audioName)
        {
            var result = await AudioHttp.DownloadAudio(_webParams, audioName, _fileManager);

            return new GeneralAnswer<object>(result.Item1, result.Item2, null);
        }
    }
}
