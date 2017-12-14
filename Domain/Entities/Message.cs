using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("messages")]
    public class Message
    {
        [Key, Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("id_sender")]
        public int IdSender { get; set; }

        [Column("is_read")]
        public bool IsRead { get; set; }

        [Column("is_delivered")]
        public bool IsDelivered { get; set; }

        [Column("text")]
        public string Text { get; set; }

        [Column("id_files")]
        public int? IdFiles { get; set; }

        [Column("time")]
        public DateTime Time { get; set; }

        [Column("id_group")]
        public int IdGroup { get; set; }


        public virtual ICollection<MessageFile> Files { get; set; }

        public Group Group { get; set; }

        public User User { get; set; }
    }
}
