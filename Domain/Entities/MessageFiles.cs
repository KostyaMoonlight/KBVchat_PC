using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("messagefiles")]
    public class MessageFiles
    {
        [Key, Column("id")]
        public int Id { get; set; } 

        [Column("idmessage")]
        public int IdMessage { get; set; }

        [Column("idfile")]
        public int IdFile { get; set; }
    }
}
