using ClassLibraryModels;
using System.Text;

namespace ClassLibraryFiles
{
    public class FileManager : IFileManager
    {
        private readonly string AUIDO_LIST_PATH = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MusicStore\\data\\audioList.bin";
        private readonly string AUIDO_PATH = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MusicStore\\data\\audio\\";
        private readonly string MUSIC_STORE_PATH = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MusicStore";
        private readonly string MUSIC_STORE_DATA_PATH = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MusicStore\\data";

        public void CreateDictories()
        {
            if(!Directory.Exists(MUSIC_STORE_PATH))
            {
                Directory.CreateDirectory(MUSIC_STORE_PATH);
            }

            if (!Directory.Exists(MUSIC_STORE_DATA_PATH))
            {
                Directory.CreateDirectory(MUSIC_STORE_DATA_PATH);
            }

            if (!Directory.Exists(AUIDO_PATH))
            {
                Directory.CreateDirectory(AUIDO_PATH);
            }
        }

        public string GetAudioListPath()
        {
            return AUIDO_LIST_PATH;
        }

        public string GetAudioPath()
        {
            return AUIDO_PATH;
        }

        public async Task<GeneralAnswer<List<AudioOperation>>> GetAudioList()
        {
            List<AudioOperation> list = new();
            try
            {
                if (File.Exists(AUIDO_LIST_PATH))
                {
                    new List<string>(await File.ReadAllLinesAsync(AUIDO_LIST_PATH)).ForEach(x => list.Add(new AudioOperation { Name = x }));
                    return new GeneralAnswer<List<AudioOperation>> { status = true, statusMessage = "Audios obtenidos de archivo del equipo.", data = list };
                }

                return new GeneralAnswer<List<AudioOperation>> { status = false, statusMessage = "Archivo de audio no existe en el equipo.", data = list };
            }
            catch (Exception ex)
            {
                return new GeneralAnswer<List<AudioOperation>> { status = false, statusMessage = "Error al obtener archivo de audio del equipo, Excepcion: " + ex.Message, data = list };
            }
        }

        public GeneralAnswer<object> WriteAudioListToBinaryFile(string audioList)
        {
            try
            {
                using (BinaryWriter binWriter = new BinaryWriter(new FileStream(AUIDO_LIST_PATH, FileMode.Create), Encoding.UTF8))
                {
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(audioList);
                    binWriter.Write(System.Convert.ToBase64String(plainTextBytes));
                }
                return new GeneralAnswer<object>(true, "Lista de Audio escritas en archivo binario.", null);
            }
            catch(Exception ex)
            {
                return new GeneralAnswer<object> (false, "Error escribiendo Lista de Audio a archivo binario.", null);
            }   
        }

        public GeneralAnswer<List<string>> ReadAudioListFromBinaryFile()
        {
            try
            {
                List<string> audioList = new List<string>();
                using (BinaryReader binReader = new BinaryReader(File.OpenRead(AUIDO_LIST_PATH), Encoding.UTF8))
                {
                    var base64EncodedBytes = System.Convert.FromBase64String(binReader.ReadString());
                    audioList = new List<string>(System.Text.Encoding.UTF8.GetString(base64EncodedBytes).Split(Environment.NewLine));
                }
                return new GeneralAnswer<List<string>>(true, "Lista de Audio obtenida de archivo binario.", audioList);
            }
            catch (Exception ex)
            {
                return new GeneralAnswer<List<string>>(false, "Error al  obtener Lista de Audio de archivo binario. Excepcion: " + ex.Message, null);
            }
        }

        public void EraseAudiosNotInAudioList(List<string> audioList)
        {
            List<string> listAudioPc = new List<string>();
            foreach (var item in Directory.GetFiles(AUIDO_PATH))
            {
                listAudioPc.Add(Path.GetFileName(item));
            }

            List<string> notInAudioList = listAudioPc.Except(audioList).ToList();

            foreach (var audio in notInAudioList)
            {
                foreach (var item in Directory.GetFiles(AUIDO_PATH))
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
            foreach(var item in Directory.GetFiles(AUIDO_PATH))
            {
                listAudioPc.Add(Path.GetFileName(item));
            }
            
            return listAudioFormServer.Except(listAudioPc).ToList();

        }
    }
}