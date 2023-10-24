using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence.Entities
{
    [Table("AudioList")]
    public class AudioListEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int StoreId { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public double Size { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public bool CheckForTime { get; set; }

        [Required]
        public TimeSpan TimeToPlay { get; set; }
    }
}
