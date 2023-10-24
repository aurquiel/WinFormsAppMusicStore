using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence.Entities
{
    [Table("User")]
    public class UserEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Alias { get; set; }

        [Required]
        [StringLength(300)]
        public string? Password { get; set; }

        [Required]
        public int StoreId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Rol { get; set; }

        [Required]
        public DateTime CreationDateTime { get; set; }
    }
}
