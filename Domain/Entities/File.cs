using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("files")]
    public class File
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("file")]
        public byte[]  FileName{ get; set; }


        public ICollection<MessageFile> MessageFiles { get; set; }
    }
}
