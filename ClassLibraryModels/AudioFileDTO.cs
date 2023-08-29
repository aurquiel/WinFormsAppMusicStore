using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModels
{
    public class AudioFileDTO
    {
        public bool select { get; set; }

        public string name { get; set; }

        public string path { get; set; }

        public TimeSpan duration { get; set; }

        public string size { get; set; }
   

        public AudioFileDTO()
        {

        }

        public AudioFileDTO(AudioFileDTO audioFileDTO)
        {
            this.select = audioFileDTO.select;
            this.name = audioFileDTO.name;
            this.path = audioFileDTO.path;
            this.duration = audioFileDTO.duration;
            this.size = audioFileDTO.size;
        }

        public static AudioFileDTO TransformToDTO(AudioFile audioFile)
        {
            return new AudioFileDTO
            {
                select = false,
                name = audioFile.name,
                path = audioFile.path,
                duration = audioFile.duration,
                size = audioFile.size.ToString("0.##"),
            };
        }

        public static List<AudioFileDTO> TransformToDTO(List<AudioFile> audioFileList)
        {
            List<AudioFileDTO> result = new();

            foreach (var item in audioFileList)
            {
                result.Add(TransformToDTO(item));
            }

            return result;
        }

        public static AudioFile TransformFromDTO(AudioFileDTO audioFileDTO)
        {
            return new AudioFile
            {
                name = audioFileDTO.name,
                path = audioFileDTO.path,
                duration = audioFileDTO.duration,
                size = Double.Parse(audioFileDTO.size)
            };
        }

        public static List<AudioFile> TransformFromDTO(List<AudioFileDTO> audioFileDTOlist)
        {
            List<AudioFile> result = new();

            foreach (var item in audioFileDTOlist)
            {
                result.Add(TransformFromDTO(item));
            }

            return result;
        }
    }
}
