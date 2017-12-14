using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("message_files")]
    public class MessageFile
    {
        [Key, Column("id")]
        public int Id { get; set; } 

        [Column("id_message")]
        public int IdMessage { get; set; }

        [Column("id_file")]
        public int IdFile { get; set; }


        public File File { get; set; }

        public Message Message { get; set; }

    }
}
