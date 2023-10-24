using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence.Entities
{
    public class RegisterEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public bool Commited { get; set; }

        [Required]
        public int StoreId { get; set; }

        [Required]
        public int Activity { get; set; }

        [Required]
        [StringLength(1000)]
        public string? Message { get; set; }

        [Required]
        public DateTime CreationDateTime { get; set; }
    }
}
