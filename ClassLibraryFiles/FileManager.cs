using ClassLibraryModels;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace ClassLibraryFiles
{
    public class FileManager : IFileManager
    {
        private readonly string AUDIO_STORE_PATH = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AudioStore\\data";

        public string GetAudioStorePath()
        { 
            return AUDIO_STORE_PATH; 
        }

        public void CreateDictoryAndFile()
        {
            if (!Directory.Exists(AUDIO_STORE_PATH))
            {
                Directory.CreateDirectory(AUDIO_STORE_PATH);
            }

            if (!Directory.Exists(AUDIO_STORE_PATH + "\\audio"))
            {
                Directory.CreateDirectory(AUDIO_STORE_PATH + "\\audio");
            }

            if (!File.Exists(AUDIO_STORE_PATH + $"\\audioList.bin"))
            {
                var s = File.Create(AUDIO_STORE_PATH + "\\audioList.bin");
                s.Close();
            }
        }

        public void DeleteDictory()
        {
            if (Directory.Exists(AUDIO_STORE_PATH))
            {
                Directory.Delete(AUDIO_STORE_PATH, true);
            }
        }

        public GeneralAnswer<object> WriteAudioListToBinaryFile(List<AudioFileDTO> audioList)
        {
            try
            {
                using (BinaryWriter binWriter = new BinaryWriter(new FileStream(AUDIO_STORE_PATH + $"\\audioList.bin", FileMode.Create), Encoding.UTF8))
                {
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(String.Join(Environment.NewLine, audioList.Select(x => x.name).ToArray()));
                    binWriter.Write(System.Convert.ToBase64String(plainTextBytes));
                }
                return new GeneralAnswer<object>(true, "Lista de Audio escrita en archivo binario.", null);
            }
            catch(Exception ex)
            {
                return new GeneralAnswer<object> (false, "Error escribiendo Lista de Audio a archivo binario. Excepcion: " + ex.Message, null);
            }   
        }

        public GeneralAnswer<List<AudioFileDTO>> ReadAudioListFromBinaryFile()
        {
            try
            {
                List<string> audioList = new List<string>();
                using (BinaryReader binReader = new BinaryReader(File.OpenRead(AUDIO_STORE_PATH + $"\\audioList.bin"), Encoding.UTF8))
                {
                    var base64EncodedBytes = System.Convert.FromBase64String(binReader.ReadString());
                    audioList = new List<string>(System.Text.Encoding.UTF8.GetString(base64EncodedBytes).Split(Environment.NewLine));
                }
                var audioFiles = new List<AudioFileDTO>();
                foreach (var audio in audioList)
                {
                    audioFiles.Add(new AudioFileDTO { name = audio, path = Path.Combine(AUDIO_STORE_PATH + "\\audio", audio) });
                }

                return new GeneralAnswer<List<AudioFileDTO>>(true, "Lista de Audio obtenida de archivo binario.", audioFiles);
            }
            catch (Exception ex)
            {
                return new GeneralAnswer<List<AudioFileDTO>>(false, "Error al  obtener Lista de Audio de archivo binario. Excepcion: " + ex.Message, null);
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
                    if(audio == Path.GetFileName(item))
                    {
                        File.Delete(item);
                    }
                }      
            }
        }

        public List<string> GetAudioListToDownload(List<string> listAudioFormServer)
        {
            List<string> listAudioPc = new List<string>();
            foreach(var item in Directory.GetFiles(AUDIO_STORE_PATH + $"\\audio"))
            {
                listAudioPc.Add(Path.GetFileName(item));
            }
            
            return listAudioFormServer.Except(listAudioPc).ToList();

        }

    }
}