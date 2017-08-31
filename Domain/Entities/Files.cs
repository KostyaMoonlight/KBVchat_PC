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
    public class Files
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("file")]
        public byte[]  File{ get; set; }


        public ICollection<MessageFiles> MessageFiles { get; set; }
    }
}
