using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("friends")]
    public class Friend
    {
        [Key, Column("id_first", Order = 1)]
        public int IdFirst { get; set; }

        [Key, Column("id_second", Order = 2)]
        public int IdSecond { get; set; }

        [Column("is_confirmed")]
        public bool IsConfirmed { get; set; }

        public User FirstUser { get; set; }

        public User SecondUser { get; set; }
    }
}
