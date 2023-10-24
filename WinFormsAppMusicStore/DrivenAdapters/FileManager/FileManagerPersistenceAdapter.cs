using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.FileManager
{
    public class FileManagerPersistenceAdapter : IFileManagerPersistencePort
    {
        private readonly string AUDIO_STORE_PATH = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AudioStore";
        private readonly string AUDIO_STORE_LOCAL_DB_PATH = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AudioStore";

        public string GetAudioStoreAdminPath()
        {
            return AUDIO_STORE_PATH;
        }

        public string GetLocalDbPath()
        {
            return AUDIO_STORE_LOCAL_DB_PATH;
        }

        public void CreateDictoryOfStore()
        {
            if (!Directory.Exists(AUDIO_STORE_PATH))
            {
                Directory.CreateDirectory(AUDIO_STORE_PATH);
            }

            if (!Directory.Exists(AUDIO_STORE_PATH + "\\audio"))
            {
                Directory.CreateDirectory(AUDIO_STORE_PATH + "\\audio");
            }
        }

        public void CopyLocalDbIfNotExist()
        {
            if(!File.Exists(AUDIO_STORE_LOCAL_DB_PATH + "\\AudioStoreLocal.db"))
            {
                File.WriteAllBytes(AUDIO_STORE_LOCAL_DB_PATH + "\\AudioStoreLocal.db", Properties.Resources.AudioStoreLocal);
            }
        }

        public void EraseAudiosNotInAudioList(List<string> audioList)
        {
            List<string> listAudioPc = new List<string>();
            foreach (var item in Directory.GetFiles(AUDIO_STORE_PATH + "\\audio"))
            {
                listAudioPc.Add(Path.GetFileName(item));
            }

            List<string> notInAudioList = listAudioPc.Except(audioList).ToList();

            foreach (var audio in notInAudioList)
            {
                foreach (var item in Directory.GetFiles(AUDIO_STORE_PATH + $"\\audio"))
                {
                    if (audio == Path.GetFileName(item))
                    {
                        File.Delete(item);
                    }
                }
            }
        }

        public List<string> GetAudioListToDownload(List<string> audioListFromServer)
        {
            List<string> listAudioPc = new List<string>();
            foreach (var item in Directory.GetFiles(AUDIO_STORE_PATH + $"\\audio"))
            {
                listAudioPc.Add(Path.GetFileName(item));
            }

            return audioListFromServer.Except(listAudioPc).ToList();
        }

        
    }
}
